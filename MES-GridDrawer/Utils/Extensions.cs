using System;

namespace MES_GridDrawer.Utils {
    public static class Extensions {

        public static int ToInt(this double val) {
            return Convert.ToInt32(val);
        }
        
        public static double[,] Transpose(this double[] vect) {
            var result = new double[1, vect.Length];
            for (var i = 0; i < vect.Length; i++) {
                result[0, i] = vect[i];
            }

            return result;
        }

        public static double[,] ToMatrix(this double[] vect) {
            var result = new double[vect.Length, 1];
            for (var i = 0; i < vect.Length; i++) {
                result[i, 0] = vect[i];
            }

            return result;
        }

        public static string ToStringMatrix(this double[,] matrix) {
            var nl = Environment.NewLine;
            var str = $"[{nl}";
            int hA = matrix.GetLength(0);
            int wA = matrix.GetLength(1);
            for (int i = 0; i < hA; i++) {
                for (int j = 0; j < wA; j++) {
                    str += $"{matrix[i, j]}   ";
                }

                if (i != hA - 1) str += nl;
            }

            return str + $"{nl}]";
        }

        public static void Round(this double[,] matrix) {
            int hA = matrix.GetLength(0);
            int wA = matrix.GetLength(1);
            for (int i = 0; i < hA; i++) {
                for (int j = 0; j < wA; j++) {
                    matrix[i, j] = Math.Round(matrix[i, j], 5);
                }
            }
        }
        
        public static void Round(this double[][] matrix) {
            int hA = matrix.Length;
            for (int i = 0; i < hA; i++) {
                int wA = matrix[i].Length;
                for (int j = 0; j < wA; j++) {
                    matrix[i][j] = Math.Round(matrix[i][j], 5);
                }
            }
        }

        public static void Round(this double[] vector) {
            for (int i = 0; i < vector.Length; i++) {
                vector[i] = Math.Round(vector[i], 5);
            }
        }

        public static string ToStringVector(this double[] vect) {
            var str = "{ ";
            foreach (var t in vect) {
                str += $"{t}  ";
            }

            return str + "}";
        }

        public static double Pow(this double val, int power) {
            return Math.Pow(val, power);
        }
    }
}