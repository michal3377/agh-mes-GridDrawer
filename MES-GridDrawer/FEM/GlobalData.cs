namespace MES_GridDrawer.FEM {
    public class GlobalData {
        public double RealLength; //L
        public double RealHeight; //H

        public int NodesHeight; //nH
        public int NodesLength; //nL

        public int NodesCount => NodesHeight * NodesLength;
        public int ElementsCount => (NodesHeight - 1) * (NodesLength - 1);
    }
}