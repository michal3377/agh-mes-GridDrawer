namespace MES_GridDrawer.FEM {
    public class GlobalData {
        public double RealLength; //L
        public double RealHeight; //H

        public int NodesHeight; //nH
        public int NodesLength; //nL

        public int ElementsHeight => NodesHeight - 1;
        public int ElementsLength => NodesLength - 1;

        public int NodesCount => NodesHeight * NodesLength;
        public int ElementsCount => ElementsHeight * ElementsLength;
    }
}