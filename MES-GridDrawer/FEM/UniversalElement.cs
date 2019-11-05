using System;

namespace MES_GridDrawer.FEM {
    public class UniversalElement {
        public double[] Weights;
        public double[] IntegrationPoints;


        public static UniversalElement CreateDefault2Point() {
            var element = new UniversalElement {
                Weights = new[] {-1d, 1d}, 
                IntegrationPoints = new[] {-1d / Math.Sqrt(3), 1d / Math.Sqrt(3)}
            };
            return element;
        }
    }
}