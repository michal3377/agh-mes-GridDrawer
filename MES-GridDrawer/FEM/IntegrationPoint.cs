namespace MES_GridDrawer.FEM {
    
    /// <summary>
    /// Klasa pomocnicza reprezentujaca punkt calkowania 2D.
    /// Laczy wspolrzedne punktow i ich wagi, zeby bylo ladnie czysciutko, bez luzem latajacych
    /// po projekcie tablic.
    /// </summary>
    public class IntegrationPoint {
        public double Ksi, Eta;
        public double WeightKsi, WeightEta;

        public IntegrationPoint(double ksi, double eta, double weightKsi, double weightEta) {
            Ksi = ksi;
            Eta = eta;
            WeightKsi = weightKsi;
            WeightEta = weightEta;
        }
    }
}