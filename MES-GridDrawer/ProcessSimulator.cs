using MES_GridDrawer.Utils;

namespace MES_GridDrawer {

    public delegate void LogMessage(string msg);
    
    public class ProcessSimulator {

        public LogMessage LogMessageDelegate;
        
        public double[,] HGlobalMatrix;
        public double[,] CGlobalMatrix;
        public double[,] PGlobalVector;
        public double[,] InitialTemperatures;

        public double StepTime;
        public double SimulationTime;
        
        public double[,] CMatrixByTau;
        public double[,] HMatrixInverted;
        public double[,] NewPVector;
        public double[,] SearchingTemperatures;


        public ProcessSimulator(double[,] hGlobalMatrix, double[,] cGlobalMatrix, double[,] pGlobalVector, double[,] initialTemperatures, double stepTime, double simulationTime) {
            HGlobalMatrix = hGlobalMatrix;
            CGlobalMatrix = cGlobalMatrix;
            PGlobalVector = pGlobalVector;
            InitialTemperatures = initialTemperatures;
            StepTime = stepTime;
            SimulationTime = simulationTime;
            CalculateConstantValues();
        }

        public void CalculateConstantValues() {
            CMatrixByTau = MatrixUtils.MultiplyMatrix(CGlobalMatrix, 1 / StepTime);
            var t1Matrix = MatrixUtils.AddMatrices(HGlobalMatrix, CMatrixByTau);
            HMatrixInverted = t1Matrix.InvertMatrix();
            
            CalculateNewP(InitialTemperatures);
        }
        
        private void CalculateNewP(double[,] temperature)  {
            NewPVector = MatrixUtils.MultiplyMatrices(CMatrixByTau, temperature);
            NewPVector = MatrixUtils.AddMatrices(NewPVector, PGlobalVector);
        }

        public void Simulate() {
            for (double i = StepTime; i <= SimulationTime; i += StepTime) {
                SearchingTemperatures = MatrixUtils.MultiplyMatrices(HMatrixInverted, NewPVector);
                var minMax = SearchingTemperatures.FindMinAndMaxValue();
                var min = minMax.Item1;
                var max = minMax.Item2;
                LogMessageDelegate?.Invoke($"StepTime = {i}; Min temp = {min}; Max temp = {max}");
                CalculateNewP(SearchingTemperatures);
            }
        }
        
        
    }
}