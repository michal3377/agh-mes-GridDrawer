using System;

namespace MES_GridDrawer.Utils {
    public class Util {
        
        public static double CalculateEdgeLength(double x1, double y1, double x2, double y2) {
            return Math.Sqrt((x2 - x1).Pow(2) + (y2 - y1).Pow(2));
        } 
    }
}