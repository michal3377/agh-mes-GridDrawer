using System.Collections.Generic;
using MES_GridDrawer.Utils;

namespace MES_GridDrawer {

    public delegate void LogMessage(string msg);
    
    public class ProcessSimulator {

        public LogMessage LogMessageDelegate;
        
        public readonly double[,] HGlobalMatrix;
        public readonly double[,] CGlobalMatrix;
        public readonly double[,] PGlobalVector;
        public readonly double[,] InitialTemperatures;

        public readonly double StepTime;
        public readonly double SimulationTime;

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

        public List<SimulationResult> Simulate() {
            LogMessageDelegate?.Invoke($"Solving the equation...");

            CalculateConstantValues();
            var results = new List<SimulationResult>();

            LogMessageDelegate?.Invoke($"Simulating the process");

            for (double i = StepTime; i <= SimulationTime; i += StepTime) {
                SearchingTemperatures = MatrixUtils.MultiplyMatrices(HMatrixInverted, NewPVector);
                var minMax = SearchingTemperatures.FindMinAndMaxValue();
                var min = minMax.Item1;
                var max = minMax.Item2;
                LogMessageDelegate?.Invoke($"StepTime = {i}; Min temp = {min}; Max temp = {max}");
                results.Add(new SimulationResult {
                    Values = SearchingTemperatures.GetColumn(0),
                    Max = max,
                    Min = min, 
                    Tau = i
                });
                CalculateNewP(SearchingTemperatures);
            }

            return results;
        }
        
        
    }

    public class SimulationResult {
        public double[] Values;
        public double Min, Max, Tau;
    }
}