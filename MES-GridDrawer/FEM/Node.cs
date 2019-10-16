namespace MES_GridDrawer.FEM {
    public class Node {
        public int Id;
        public int X, Y;
        public double Value; //t
        public int BoundaryCondition;

        public bool IsBoundary => BoundaryCondition != 0;
        
        public override string ToString() {
            return $"Node {Id} ({X}, {Y}): Val={Value} BC={BoundaryCondition}";
        }
    }
}