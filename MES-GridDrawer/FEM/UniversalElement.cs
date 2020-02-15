using System;
using System.Collections.Generic;
using MES_GridDrawer.Utils;

namespace MES_GridDrawer.FEM {

    /// <summary>
    /// Klasa ta grupuje wspolne dla kazdego elementu (w ukladzie lokalnym) dane.
    /// W tym przypadku ograniczamy sie do elementu 2D 4-wezlowego, ktory dla podanych punktow calkowania
    /// wyznacza macierz wartosci funkcji ksztaltu w tych punktach oraz ich pochodnych czastkowych
    /// po ksi i eta. Wartosci te sa uniwersalne bo nie zaleza od danych globalnych. 
    /// </summary>
    public class UniversalElement {
        
        public IntegrationPoint[] Points;
        public int PointsCount;
        
        // [ indeks punktu ] [ funkcja ksztaltu ]
        public double[][] NValues, dNdKsiValues, dNdEtaValues;

        public UniversalElement(IntegrationPoint[] points) {
            Points = points;
            PointsCount = points.Length;

            NValues = new double[PointsCount][];
            dNdKsiValues = new double[PointsCount][];
            dNdEtaValues = new double[PointsCount][];
            for (int i = 0; i < PointsCount; i++) {
                NValues[i] = new double[4];
                dNdKsiValues[i] = new double[4];
                dNdEtaValues[i] = new double[4];
            }
            CalculateMatrices();
        }

        private void CalculateMatrices() {
            for (int i = 0; i < PointsCount; i++) {
                var ksi = Points[i].Ksi;
                var eta = Points[i].Eta;

                NValues[i] = CalculateNValues(Points[i]);

                dNdKsiValues[i][0] = -0.25 * (1 - eta);
                dNdKsiValues[i][1] = 0.25 * (1 - eta);
                dNdKsiValues[i][2] = 0.25 * (1 + eta);
                dNdKsiValues[i][3] = -0.25 * (1 + eta);

                dNdEtaValues[i][0] = -0.25 * (1 - ksi);
                dNdEtaValues[i][1] = -0.25 * (1 + ksi);
                dNdEtaValues[i][2] = 0.25 * (1 + ksi);
                dNdEtaValues[i][3] = 0.25 * (1 - ksi);
            }
            NValues.Round();
            dNdKsiValues.Round();
            dNdEtaValues.Round();
        }

        public double[] CalculateNValues(IntegrationPoint point) {
            return new[] {
                CalculateNValue(point, 0),
                CalculateNValue(point, 1),
                CalculateNValue(point, 2),
                CalculateNValue(point, 3)
            };
        }
        
        private double CalculateNValue(IntegrationPoint point, int NIndex) {
            if(NIndex == 0) return 0.25 * (1 - point.Ksi) * (1 - point.Eta);
            if(NIndex == 1) return 0.25 * (1 + point.Ksi) * (1 - point.Eta);
            if(NIndex == 2) return 0.25 * (1 + point.Ksi) * (1 + point.Eta);
            return 0.25 * (1 - point.Ksi) * (1 + point.Eta);
        }

        public IntegrationPoint[] GetIntegrationPointsProjectedOntoEdge(int edgeIndex) {
            int pointsCountForEdge = Math.Round(Math.Sqrt(PointsCount)).ToInt();
            var points = new List<IntegrationPoint>();
            for (int i = 0; i < pointsCountForEdge; i++) {

                if (edgeIndex == 0 || edgeIndex == 2) {
                    //horizontal
                    var point = Points[i];
                    var eta = edgeIndex == 0 ? -1 : 1;
                    var projected = new IntegrationPoint(point.Ksi, eta, point.WeightKsi, 1);
                    points.Add(projected);
                } else {
                    //vertical
                    var point = Points[i * pointsCountForEdge];
                    var ksi = edgeIndex == 3 ? -1 : 1;
                    var projected = new IntegrationPoint(ksi, point.Eta, 1, point.WeightEta);
                    points.Add(projected);
                }
            }

            return points.ToArray();
        }
        
        /// <summary>
        /// Pomocnicza metoda tworzaca element uniwersalny dla dwupunktowego schematu calkowania
        /// -1/√3; 1/√3; waga 1; 1
        /// </summary>
        /// <returns></returns>
        public static UniversalElement CreateDefault2Point() {
            double p1 = -1d / Math.Sqrt(3);
            double p2 = 1d / Math.Sqrt(3);
            var element = new UniversalElement(
                new[] {
                    new IntegrationPoint(p1, p1, 1, 1), //lewy dolny
                    new IntegrationPoint(p2, p1, 1, 1), //prawy dolny
                    new IntegrationPoint(p1, p2, 1, 1),  //lewy gorny
                    new IntegrationPoint(p2, p2, 1, 1), //prawy gorny
                }
            );
            return element;
        }     
        

        public static UniversalElement CreateDefault3Point() {
            double p1 = -Math.Sqrt(3 / 5d);
            double p2 = 0;
            double p3 = Math.Sqrt(3 / 5d);
            double w1 = 5 / 9d;
            double w2 = 8 / 9d;
            double w3 = 5 / 9d;
            var element = new UniversalElement(
                new[] {
                    new IntegrationPoint(p1, p1, w1, w1),
                    new IntegrationPoint(p2, p1, w2, w1),
                    new IntegrationPoint(p3, p1, w3, w1),
                    
                    new IntegrationPoint(p1, p2, w1, w2),
                    new IntegrationPoint(p2, p2, w2, w2),
                    new IntegrationPoint(p3, p2, w3, w2),
                    
                    new IntegrationPoint(p1, p3, w1, w3),
                    new IntegrationPoint(p2, p3, w2, w3),
                    new IntegrationPoint(p3, p3, w3, w3),
                }
            );
            return element;
        }
    }
}