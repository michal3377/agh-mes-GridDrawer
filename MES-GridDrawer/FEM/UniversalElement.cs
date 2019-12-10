using System;

namespace MES_GridDrawer.FEM {
    
    
    public class UniversalElement {

        public const int POINTS_COUNT = 4;
        
        public double[] Weights;
        public double[] IntegrationPoints;
        public Point[] Points;
        
        public double[,] NValues = new double[4, 4];
        public double[,] KsiValues = new double[4, 4];
        public double[,] EtaValues = new double[4, 4];
        
        public void CalculateMatrixes() {
            double eta = 0;
            double ksi = 0;
            for (int i = 0; i < 4; i++) {
                if (i == 0) {
                    ksi = IntegrationPoints[0];
                    eta = IntegrationPoints[0];
                } else if (i == 1) {
                    ksi = IntegrationPoints[1];
                    eta = IntegrationPoints[0];
                } else if (i == 2) {
                    ksi = IntegrationPoints[0];
                    eta = IntegrationPoints[1];
                } else if (i == 3) {
                    ksi = IntegrationPoints[1];
                    eta = IntegrationPoints[1];
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


        public static UniversalElement CreateDefault2Point() {
            double x1 = -1d / Math.Sqrt(3);
            double x2 = 1d / Math.Sqrt(3);
            var element = new UniversalElement {
                Weights = new[] {1d, 1d}, 
                IntegrationPoints = new[] {x1, x2},
                Points = new [] {
                    new Point(x1, x1),
                    new Point(x2, x1),
                    new Point(x2, x2), 
                    new Point(x1, x2) 
                }
            };
            return element;
        }
    }
}