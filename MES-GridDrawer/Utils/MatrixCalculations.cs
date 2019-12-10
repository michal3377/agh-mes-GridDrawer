using System;

namespace MES_GridDrawer.Utils {
    public class MatrixCalculations {
        
        public static double[,] MultiplyMatrices(double[,] m1, double[,] m2) {
            int hA = m1.GetLength(0);
            int wA = m1.GetLength(1);
            int hB = m2.GetLength(0);
            int wB = m2.GetLength(1);
            if (wA != hB) throw new ArgumentException("Can't multiply matrices");
            
            double[,] result = new double[hA, wB];

            for (int i = 0; i < hA; i++) {
                for (int j = 0; j < wB; j++) {
                    double temp = 0;
                    for (int k = 0; k < wA; k++) {
                        temp += m1[i, k] * m2[k, j];
                    }
                    result[i, j] = temp;
                }
            }

            return result;
        }    
        
        public static double[,] MultiplyMatrix(double[,] m1, double scalar) {
            int hA = m1.GetLength(0);
            int wA = m1.GetLength(1);
            double[,] result = new double[hA, wA];

            for (int i = 0; i < hA; i++) {
                for (int j = 0; j < wA; j++) {
                    result[i, j] = m1[i,j] * scalar;
                }
            }

            return result;
        }

        public static double[,] AddMatrices(double[,] m1, double[,] m2) {
            int hA = m1.GetLength(0);
            int wA = m1.GetLength(1);
            int hB = m2.GetLength(0);
            int wB = m2.GetLength(1);
            if(hA != hB || wA != wB)  throw new ArgumentException("Can't add matrices");
            double[,] result = new double[hA, wB];
            for (int i = 0; i < hA; i++) {
                for (int j = 0; j < wB; j++) {
                    result[i, j] = m1[i,j] + m2[i, j];
                }
            }

            return result;
        }
    }
}