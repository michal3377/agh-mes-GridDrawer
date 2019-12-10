namespace MES_GridDrawer.FEM {
    public class IntegrationPoint {
        public double X, Y;
        public double WeightX, WeightY;

        public IntegrationPoint(double x, double y, double weightX, double weightY) {
            X = x;
            Y = y;
            WeightX = weightX;
            WeightY = weightY;
        }
    }
}