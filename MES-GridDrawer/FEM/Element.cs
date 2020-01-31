using System;
using MES_GridDrawer.Utils;

namespace MES_GridDrawer.FEM {
    public class Element {

        public const int ELEMENT_NODES_COUNT = 4;
        
        public int Id;
        public int X, Y;
        
        public Node[] Nodes;
        public UniversalElement UniversalElement;


        public double[][,] Jacobians;
        public double[][,] TransformedJacobians;
        public double[] JacobianDeterminals;

        public double[][,] HLocalMatrixPerPoint;
        public double[,] HLocalMatrix;

        public Element(int id, int x, int y, Node[] nodes, UniversalElement universalElement) {
            Id = id;
            X = x;
            Y = y;
            Nodes = nodes;
            UniversalElement = universalElement;
            Init();
        }

        private void Init() {
            int pcCount = UniversalElement.PointsCount;
            
            Jacobians = new double[pcCount][,];
            TransformedJacobians = new double[pcCount][,];
            JacobianDeterminals = new double[pcCount];
            
            HLocalMatrixPerPoint = new double[pcCount][,];
            HLocalMatrix = new double[4,4]; 
            
            for (int i = 0; i < pcCount; i++) {
                Jacobians[i] = new double[2,2];
                TransformedJacobians[i] = new double[2,2];
                HLocalMatrixPerPoint[i] = new double[4,4];
            }
        }


        public void CalculateMatrices() {
            CalculateJacobians();
            TransformJacobians();
            CalculateHLocal();
            RoundMatrices();
        }

        private void RoundMatrices() {
            for (int i = 0; i < UniversalElement.PointsCount; i++) {
                Jacobians[i].Round();
                TransformedJacobians[i].Round();
                HLocalMatrixPerPoint[i].Round();
            }
            HLocalMatrix.Round();
            JacobianDeterminals.Round();
        }
        
        
        private void CalculateJacobians() {
            int index = 0;
            for (var i = 0; i < UniversalElement.PointsCount; i++) {
                var jacobian = Jacobians[i];
                //[↓ →] 
                //dx/dksi
                jacobian[0, 0] = UniversalElement.dNdKsiMatrix[i][0] * Nodes[0].RealX +
                                 UniversalElement.dNdKsiMatrix[i][1] * Nodes[1].RealX +
                                 UniversalElement.dNdKsiMatrix[i][2] * Nodes[2].RealX +
                                 UniversalElement.dNdKsiMatrix[i][3] * Nodes[3].RealX;
                
                //dy/dksi
                jacobian[0, 1] = UniversalElement.dNdKsiMatrix[i][0] * Nodes[0].RealY +
                                 UniversalElement.dNdKsiMatrix[i][1] * Nodes[1].RealY +
                                 UniversalElement.dNdKsiMatrix[i][2] * Nodes[2].RealY +
                                 UniversalElement.dNdKsiMatrix[i][3] * Nodes[3].RealY;
                // dx/deta
                jacobian[1, 0] = UniversalElement.dNdEtaMatrix[i][0] * Nodes[0].RealX +
                                 UniversalElement.dNdEtaMatrix[i][1] * Nodes[1].RealX +
                                 UniversalElement.dNdEtaMatrix[i][2] * Nodes[2].RealX +
                                 UniversalElement.dNdEtaMatrix[i][3] * Nodes[3].RealX;
                
                // dy/deta
                jacobian[1, 1] = UniversalElement.dNdEtaMatrix[i][0] * Nodes[0].RealY +
                                 UniversalElement.dNdEtaMatrix[i][1] * Nodes[1].RealY +
                                 UniversalElement.dNdEtaMatrix[i][2] * Nodes[2].RealY +
                                 UniversalElement.dNdEtaMatrix[i][3] * Nodes[3].RealY;

                JacobianDeterminals[i] = jacobian[0, 0] * jacobian[1, 1] - jacobian[0, 1] * jacobian[1, 0];
            }
        }

        private void TransformJacobians() {
            for (int i = 0; i < 4; i++) {
                var jacobian = Jacobians[i];
                TransformedJacobians[i][0, 0] = jacobian[1, 1];
                TransformedJacobians[i][1, 1] = jacobian[0, 0];
                TransformedJacobians[i][0, 1] = -jacobian[0, 1];
                TransformedJacobians[i][1, 0] = -jacobian[1, 0];
            }
        }

        private double CalculateDnDx(int pointIndex, int nIndex) {
            //nIndex - numer funkcji ksztaltu
            return 1 / JacobianDeterminals[pointIndex] * (TransformedJacobians[pointIndex][0, 0] 
                                                          * UniversalElement.dNdKsiMatrix[pointIndex][nIndex]
                                                          + TransformedJacobians[pointIndex][0, 1] *
                                                          UniversalElement.dNdEtaMatrix[pointIndex][nIndex]);
        }
        
        private double CalculateDnDy(int pointIndex, int nIndex) {
            //nIndex - numer funkcji ksztaltu
            return 1 / JacobianDeterminals[pointIndex] * (TransformedJacobians[pointIndex][1, 0] 
                                                          * UniversalElement.dNdKsiMatrix[pointIndex][ nIndex]
                                                          + TransformedJacobians[pointIndex][1, 1] *
                                                          UniversalElement.dNdEtaMatrix[pointIndex][nIndex]);
        }

        private void CalculateHLocal() {
            //H local per point:
            for (int i = 0; i < UniversalElement.PointsCount; i++) {
                var dNdX = new[] {
                    CalculateDnDx(i, 0),
                    CalculateDnDx(i, 1),
                    CalculateDnDx(i, 2),
                    CalculateDnDx(i, 3)
                };
                var dNdY = new[] {
                    CalculateDnDy(i, 0),
                    CalculateDnDy(i, 1),
                    CalculateDnDy(i, 2),
                    CalculateDnDy(i, 3)
                };
                var dNdXMatrix = dNdX.ToMatrix();
                var dNdXMatrixT = dNdX.Transpose();
            
                var dNdYMatrix = dNdY.ToMatrix();
                var dNdYMatrixT = dNdY.Transpose();

                var dx = MatrixUtils.MultiplyMatrices(dNdXMatrix, dNdXMatrixT);
                var dy = MatrixUtils.MultiplyMatrices(dNdYMatrix, dNdYMatrixT);

                HLocalMatrixPerPoint[i] = MatrixUtils.AddMatrices(dx, dy);
            }
            
            //H local
            var sum = new double[4, 4];
            for (int i = 0; i < UniversalElement.PointsCount; i++) {
                var pc = UniversalElement.Points[i];
                var factor = pc.WeightKsi * pc.WeightEta * JacobianDeterminals[i];
                var matrix = MatrixUtils.MultiplyMatrix(HLocalMatrixPerPoint[i], factor);
                sum = MatrixUtils.AddMatrices(sum, matrix);
            }

            HLocalMatrix = sum;
        }

        public override string ToString() {
            var nl = Environment.NewLine;
            var str = $"Element {Id}. Nodes:" +
                      $"{nl}\t{Nodes[0]}" +
                      $"{nl}\t{Nodes[1]}" +
                      $"{nl}\t{Nodes[2]}" +
                      $"{nl}\t{Nodes[3]}";
            for (int i = 0; i < UniversalElement.PointsCount; i++)
                str += $"{nl} Jacobian [{i}]: {Jacobians[i].ToStringMatrix()}";
            str +=
                   $"{nl} Wyznaczniki: {JacobianDeterminals.ToStringVector()}" +
                   $"{nl} Macierz H lokalna[0]: {HLocalMatrixPerPoint[0].ToStringMatrix()}" +
                   $"{nl} Macierz H globalna: {HLocalMatrix.ToStringMatrix()}";
            return str;
        }
    }
}