using System;
using System.Linq;
using System.Windows.Forms;

namespace MES_GridDrawer.Utils {
    public static class Extensions {
        
        
        public static bool IsNullOrDisposed(this Control control) {
            return control == null || control.Disposing || control.IsDisposed;
        }

        public static object SafeInvoke(this Control control, MethodInvoker method) {
            if (control.IsNullOrDisposed()) return null;
            try {
                return control.Invoke(method);
            } catch (Exception e) {
                return null;
            }
        }

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
        
        public static T[] GetColumn<T>(this T[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
        }

        public static T[] GetRow<T>(this T[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
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
        
        public static void FillWithValue(this double[,] matrix, double value) {
            int hA = matrix.GetLength(0);
            int wA = matrix.GetLength(1);
            for (int i = 0; i < hA; i++) {
                for (int j = 0; j < wA; j++) {
                    matrix[i, j] = value;
                }
            }
        }
        
        /// <summary>
        /// Finds min and max value in given matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>Tuple (min, max) </returns>
        public static Tuple<double, double> FindMinAndMaxValue(this double[,] matrix) {
            int hA = matrix.GetLength(0);
            int wA = matrix.GetLength(1);
            double min = matrix[0, 0];
            double max = matrix[0, 0];
            for (int i = 0; i < hA; i++) {
                for (int j = 0; j < wA; j++) {
                    var value = matrix[i, j];
                    if (value < min) min = value;
                    if (value > max) max = value;
                }
            }
            return new Tuple<double, double>(min, max);
        }
        
        public static double[,] InvertMatrix(this double[,] matrix) {
            int h = matrix.GetLength(0);
            int w = matrix.GetLength(1);
            //assert h == w
            
            int size = w;
            var x = new double[size, size];
            var b = new double[size, size];
            var index = new int[size];
            for (int i=0; i<size; ++i)
                b[i, i] = 1;

            // Transform the matrix into an upper triangle
            MatrixGaussian(matrix, index);

            // Update the matrix b[i][j] with the ratios stored
            for (int i=0; i<size-1; ++i)
                for (int j=i+1; j<size; ++j)
                    for (int k=0; k<size; ++k)
                        b[index[j], k]
                            -= matrix[index[j], i]*b[index[i], k];

            // Perform backward substitutions
            for (int i=0; i<size; ++i)
            {
                x[size-1, i] = b[index[size-1],i]/matrix[index[size-1], size-1];
                for (int j=size-2; j>=0; --j)
                {
                    x[j, i] = b[index[j],i];
                    for (int k=j+1; k<size; ++k)
                    {
                        x[j,i] -= matrix[index[j],k]*x[k, i];
                    }
                    x[j,i] /= matrix[index[j], j];
                }
            }
            return x;
        }
        
        private static void MatrixGaussian(double[,] a, int[] index) {
            int n = index.Length;
            var c = new double[n];

            // Initialize the index
            for (int i = 0; i < n; ++i)
                index[i] = i;

            // Find the rescaling factors, one from each row
            for (int i = 0; i < n; ++i) {
                double c1 = 0;
                for (int j = 0; j < n; ++j) {
                    double c0 = Math.Abs(a[i, j]);
                    if (c0 > c1) c1 = c0;
                }
                c[i] = c1;
            }

            // Search the pivoting element from each column
            int k = 0;
            for (int j = 0; j < n - 1; ++j) {
                double pi1 = 0;
                for (int i = j; i < n; ++i) {
                    double pi0 = Math.Abs(a[index[i], j]);
                    pi0 /= c[index[i]];
                    if (pi0 > pi1) {
                        pi1 = pi0;
                        k = i;
                    }
                }

                // Interchange rows according to the pivoting order
                int itmp = index[j];
                index[j] = index[k];
                index[k] = itmp;
                for (int i = j + 1; i < n; ++i) {
                    double pj = a[index[i],j] / a[index[j],j];

                    // Record pivoting ratios below the diagonal
                    a[index[i],j] = pj;

                    // Modify other elements accordingly
                    for (int l = j + 1; l < n; ++l)
                        a[index[i],l] -= pj * a[index[j],l];
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