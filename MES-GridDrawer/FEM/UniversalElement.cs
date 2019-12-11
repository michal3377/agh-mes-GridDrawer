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
        
        // [ indeks punktu , wartosc ]
        public double[,] NValuesMatrix, dNdKsiMatrix, dNdEtaMatrix;

        public UniversalElement(IntegrationPoint[] points) {
            Points = points;
            PointsCount = points.Length;

            NValuesMatrix = new double[PointsCount, 4];
            dNdKsiMatrix = new double[PointsCount, 4];
            dNdEtaMatrix = new double[PointsCount, 4];
            CalculateMatrices();
        }

        private void CalculateMatrices() {
            for (int i = 0; i < PointsCount; i++) {
                var ksi = Points[i].Ksi;
                var eta = Points[i].Eta;

                NValuesMatrix[i, 0] = 0.25 * (1 - ksi) * (1 - eta);
                NValuesMatrix[i, 1] = 0.25 * (1 + ksi) * (1 - eta);
                NValuesMatrix[i, 2] = 0.25 * (1 + ksi) * (1 + eta);
                NValuesMatrix[i, 3] = 0.25 * (1 - ksi) * (1 + eta);

                dNdKsiMatrix[i, 0] = -0.25 * (1 - eta);
                dNdKsiMatrix[i, 1] = 0.25 * (1 - eta);
                dNdKsiMatrix[i, 2] = 0.25 * (1 + eta);
                dNdKsiMatrix[i, 3] = -0.25 * (1 + eta);

                dNdEtaMatrix[i, 0] = -0.25 * (1 - ksi);
                dNdEtaMatrix[i, 1] = -0.25 * (1 + ksi);
                dNdEtaMatrix[i, 2] = 0.25 * (1 + ksi);
                dNdEtaMatrix[i, 3] = 0.25 * (1 - ksi);
            }
            NValuesMatrix.Round();
            dNdKsiMatrix.Round();
            dNdEtaMatrix.Round();
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