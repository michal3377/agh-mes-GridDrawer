namespace MES_GridDrawer.FEM {
    public class GlobalData {
        public double RealHeight; //H
        public double RealLength; //L

        public int NodesHeight; //nH
        public int NodesLength; //nL

        public int NodesCount => NodesHeight * NodesLength;
        public int ElementsCount => (NodesHeight - 1) * (NodesLength - 1);
    }
}