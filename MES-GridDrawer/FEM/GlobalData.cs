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

        public double AmbientTemperature;
        public double Alpha;
        public double SpecificHeat;
        public double Conductivity;
        public double Density;
        public double InitialTemperature;

    }
}