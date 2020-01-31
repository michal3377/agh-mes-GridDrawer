using System;

namespace MES_GridDrawer.FEM {
    public class Grid {
        public Element[] Elements;
        public Node[] Nodes;

        public UniversalElement UniversalElement;
        
        private GlobalData _globalData;

        public double[,] HGlobalMatrix; 
        public double[,] CGlobalMatrix; 
        public double[,] PGlobalVector; 

        public Grid(GlobalData globalData) {
            _globalData = globalData;
            Elements = new Element[globalData.ElementsCount];
            Nodes = new Node[globalData.NodesCount];
            
            UniversalElement = UniversalElement.CreateDefault2Point();

            int dof = Nodes.Length;
            HGlobalMatrix = new double[dof, dof];
            CGlobalMatrix = new double[dof, dof];
            PGlobalVector = new double[dof, 1];
        }

        public void ConstructGrid() {
            CreateNodesArray();
            CreateElementsArray();
        }

        public void CalculateMatrices() {
            foreach (var element in Elements) {
                element.CalculateMatrices();
            }
            AggregateLocalValues();
        }

        public Node GetNodeAt(int x, int y) {
            //verify indexes
            int id = x * _globalData.NodesHeight + y;
            return Nodes[id];
        }

        public Element GetElementAt(int x, int y) {
            //verify indexes
            int id = x * (_globalData.NodesHeight - 1) + y;
            return Elements[id];
        }

        private void CreateNodesArray() {
            int index = 0;
            for (int x = 0; x < _globalData.NodesLength; x++) {
                for (int y = 0; y < _globalData.NodesHeight; y++) {
                    var node = new Node {
                        Id = index,
                        X = x,
                        Y = y,
                        RealX = CalculateRealX(x),
                        RealY = CalculateRealY(y),
                        BoundaryCondition = DetermineBoundaryCondition(x, y)
                    };
                    Nodes[index] = node;
                    index++;
                }
            }
        }

        


        private void CreateElementsArray() {
            int index = 0;
            for (int x = 0; x < _globalData.NodesLength - 1; x++) {
                for (int y = 0; y < _globalData.NodesHeight - 1; y++) {
                    var element = new Element(index, x, y, FindElementNodes(x, y), UniversalElement, _globalData);
                    Elements[index] = element;
                    index++;
                }
            }
        }

        private int DetermineBoundaryCondition(int x, int y) {
            if (x == 0 || y == 0
                       || x == _globalData.NodesLength - 1
                       || y == _globalData.NodesHeight - 1)
                return 1;
            return 0;
        }


        private Node[] FindElementNodes(int elementX, int elementY) {
            var bottomLeft = GetNodeAt(elementX, elementY);
            var topLeft = GetNodeAt(elementX, elementY + 1);
            var topRight = GetNodeAt(elementX + 1, elementY + 1);
            var bottomRight = GetNodeAt(elementX + 1, elementY);
            return new[] {bottomLeft, bottomRight, topRight, topLeft};
        }

        private double CalculateRealX(int xIndex) {
            return (_globalData.RealLength / _globalData.ElementsLength) * xIndex;
        }

        private double CalculateRealY(int yIndex) {
            return (_globalData.RealHeight / _globalData.ElementsHeight) * yIndex;
        }

        private void AggregateLocalValues() {
            foreach (var element in Elements) {
                var nodeIndices = new int[element.Nodes.Length];
                for (var i = 0; i < element.Nodes.Length; i++) nodeIndices[i] = element.Nodes[i].Id;

                for (int i = 0; i < Element.ELEMENT_NODES_COUNT; i++) {
                    for (int j = 0; j < Element.ELEMENT_NODES_COUNT; j++) {
                        HGlobalMatrix[nodeIndices[i], nodeIndices[j]] += element.HLocalMatrix[i, j]
                                                                         + element.HBcLocalMatrix[i, j];
                        CGlobalMatrix[nodeIndices[i], nodeIndices[j]] += element.CLocalMatrix[i, j];
                    }

                    PGlobalVector[nodeIndices[i], 0] += element.PVector[i, 0];
                }
            }
        }
    }
}