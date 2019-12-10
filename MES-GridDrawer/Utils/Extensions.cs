namespace MES_GridDrawer.Utils {
    public static class Extensions {
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
    }
}