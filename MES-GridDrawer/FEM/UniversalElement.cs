using System;
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

                NValues[i][0] = 0.25 * (1 - ksi) * (1 - eta);
                NValues[i][1] = 0.25 * (1 + ksi) * (1 - eta);
                NValues[i][2] = 0.25 * (1 + ksi) * (1 + eta);
                NValues[i][3] = 0.25 * (1 - ksi) * (1 + eta);

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
                    new IntegrationPoint(p2, p2, 1, 1), //prawy gorny
                    new IntegrationPoint(p1, p2, 1, 1)  //lewy gorny
                }
            );
            return element;
        }
    }
}