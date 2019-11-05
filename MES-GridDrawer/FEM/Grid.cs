namespace MES_GridDrawer.FEM {
    public class Grid {
        public Element[] Elements;
        public Node[] Nodes;

        public double[,] NValues = new double[4, 4];
        public double[,] KsiValues = new double[4, 4];
        public double[,] EtaValues = new double[4, 4];

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

        public void CalculateMatrixes(UniversalElement universalElement) {
            var Weights = universalElement.Weights;
            var points = universalElement.IntegrationPoints;

            double eta = 0;
            double ksi = 0;
            for (int i = 0; i < 4; i++) {
                if (i == 0) {
                    ksi = points[0];
                    eta = points[0];
                } else if (i == 1) {
                    ksi = points[1];
                    eta = points[0];
                } else if (i == 2) {
                    ksi = points[0];
                    eta = points[1];
                } else if (i == 3) {
                    ksi = points[1];
                    eta = points[1];
                }

                NValues[i, 0] = (0.25 * (1 - ksi) * (1 - eta));
                NValues[i, 1] = (0.25 * (1 + ksi) * (1 - eta));
                NValues[i, 2] = (0.25 * (1 + ksi) * (1 + eta));
                NValues[i, 3] = (0.25 * (1 - ksi) * (1 + eta));

                KsiValues[i, 0] = (-0.25 * (1 - eta));
                KsiValues[i, 1] = (0.25 * (1 - eta));
                KsiValues[i, 2] = (0.25 * (1 + eta));
                KsiValues[i, 3] = (-0.25 * (1 + eta));

                EtaValues[i, 0] = (-0.25 * (1 - ksi));
                EtaValues[i, 1] = (-0.25 * (1 + ksi));
                EtaValues[i, 2] = (0.25 * (1 + ksi));
                EtaValues[i, 3] = (0.25 * (1 - ksi));
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