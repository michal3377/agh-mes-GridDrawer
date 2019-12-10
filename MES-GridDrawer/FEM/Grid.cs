namespace MES_GridDrawer.FEM {
    public class Grid {
        public Element[] Elements;
        public Node[] Nodes;

       

        private GlobalData _globalData;

        public Grid(GlobalData globalData) {
            _globalData = globalData;
            Elements = new Element[globalData.ElementsCount];
            Nodes = new Node[globalData.NodesCount];
        }

        public void ConstructGrid() {
            CreateNodesArray();
            CreateElementsArray();
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
                    var element = new Element {
                        Id = index,
                        X = x,
                        Y = y,
                        Nodes = FindElementNodes(x, y)
                    };
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
            return new[] {bottomLeft, topLeft, topRight, bottomRight};
        }
    }
}