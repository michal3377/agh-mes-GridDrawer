using System;
using MES_GridDrawer.Utils;

namespace MES_GridDrawer.FEM {
    public class Element {

        public const int ELEMENT_NODES_COUNT = 4;

        private GlobalData _globalData;
        public int Id;
        public int X, Y;
        
        public Node[] Nodes;
        public UniversalElement UniversalElement;


        public double[][,] Jacobians;
        public double[][,] TransformedJacobians;
        public double[] JacobianDeterminals;

        public double[][,] HLocalMatrixPerPoint;
        public double[,] HLocalMatrix;
        public double[,] HBcLocalMatrix;
        public double[,] CLocalMatrix;
        public double[,] PVector;

        public Element(int id, int x, int y, Node[] nodes, UniversalElement universalElement, GlobalData globalData) {
            Id = id;
            X = x;
            Y = y;
            Nodes = nodes;
            UniversalElement = universalElement;
            _globalData = globalData;
            Init();
        }

        private void Init() {
            int pcCount = UniversalElement.PointsCount;
            
            Jacobians = new double[pcCount][,];
            TransformedJacobians = new double[pcCount][,];
            JacobianDeterminals = new double[pcCount];
            
            HLocalMatrixPerPoint = new double[pcCount][,];
            HLocalMatrix = new double[4,4]; 
            HBcLocalMatrix = new double[4,4]; 
            CLocalMatrix = new double[4,4]; 
            PVector = new double[4,1];
            
            for (int i = 0; i < pcCount; i++) {
                Jacobians[i] = new double[2,2];
                TransformedJacobians[i] = new double[2,2];
                HLocalMatrixPerPoint[i] = new double[4,4];
            }
        }


        public void CalculateMatrices() {
            CalculateJacobians();
            TransformJacobians();
            CalculateHLocal(_globalData.Conductivity);
            CalculateCLocal(_globalData.SpecificHeat, _globalData.Density);
            CalculateBoundaryConditions(_globalData.Alpha, _globalData.AmbientTemperature);
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
                jacobian[0, 0] = UniversalElement.dNdKsiValues[i][0] * Nodes[0].RealX +
                                 UniversalElement.dNdKsiValues[i][1] * Nodes[1].RealX +
                                 UniversalElement.dNdKsiValues[i][2] * Nodes[2].RealX +
                                 UniversalElement.dNdKsiValues[i][3] * Nodes[3].RealX;
                
                //dy/dksi
                jacobian[0, 1] = UniversalElement.dNdKsiValues[i][0] * Nodes[0].RealY +
                                 UniversalElement.dNdKsiValues[i][1] * Nodes[1].RealY +
                                 UniversalElement.dNdKsiValues[i][2] * Nodes[2].RealY +
                                 UniversalElement.dNdKsiValues[i][3] * Nodes[3].RealY;
                // dx/deta
                jacobian[1, 0] = UniversalElement.dNdEtaValues[i][0] * Nodes[0].RealX +
                                 UniversalElement.dNdEtaValues[i][1] * Nodes[1].RealX +
                                 UniversalElement.dNdEtaValues[i][2] * Nodes[2].RealX +
                                 UniversalElement.dNdEtaValues[i][3] * Nodes[3].RealX;
                
                // dy/deta
                jacobian[1, 1] = UniversalElement.dNdEtaValues[i][0] * Nodes[0].RealY +
                                 UniversalElement.dNdEtaValues[i][1] * Nodes[1].RealY +
                                 UniversalElement.dNdEtaValues[i][2] * Nodes[2].RealY +
                                 UniversalElement.dNdEtaValues[i][3] * Nodes[3].RealY;

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
                                                          * UniversalElement.dNdKsiValues[pointIndex][nIndex]
                                                          + TransformedJacobians[pointIndex][0, 1] *
                                                          UniversalElement.dNdEtaValues[pointIndex][nIndex]);
        }
        
        private double CalculateDnDy(int pointIndex, int nIndex) {
            //nIndex - numer funkcji ksztaltu
            return 1 / JacobianDeterminals[pointIndex] * (TransformedJacobians[pointIndex][1, 0] 
                                                          * UniversalElement.dNdKsiValues[pointIndex][ nIndex]
                                                          + TransformedJacobians[pointIndex][1, 1] *
                                                          UniversalElement.dNdEtaValues[pointIndex][nIndex]);
        }

        private void CalculateHLocal(double conductivity) {
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
                var factor = pc.WeightKsi * pc.WeightEta * JacobianDeterminals[i] * conductivity;
                var matrix = MatrixUtils.MultiplyMatrix(HLocalMatrixPerPoint[i], factor);
                sum = MatrixUtils.AddMatrices(sum, matrix);
            }

            HLocalMatrix = sum;
        }
        
        private void CalculateCLocal(double c, double ro) {
            //H local per point:
            for (int i = 0; i < UniversalElement.PointsCount; i++) {
                var NValues = UniversalElement.NValues[i].ToMatrix();
                var NValuesT = UniversalElement.NValues[i].Transpose();

                var NxN = MatrixUtils.MultiplyMatrices(NValues, NValuesT);
                var pc = UniversalElement.Points[i];
                var factor = pc.WeightKsi * pc.WeightEta * JacobianDeterminals[i] * c * ro;
                var cPartial = MatrixUtils.MultiplyMatrix(NxN, factor);
                CLocalMatrix = MatrixUtils.AddMatrices(CLocalMatrix, cPartial);
            }
        }      
        
        private void CalculateBoundaryConditions(double alpha, double ambientTemperature) {

            for (int i = 0; i < ELEMENT_NODES_COUNT; i++) {
                var current = Nodes[i];
                var next = Nodes[(i + 1) % ELEMENT_NODES_COUNT];
                if (current.IsBoundary && next.IsBoundary) {
                    //boundary edge
                    int edgeIndex = i;
                    var jacobianDet = CalculateEdgeJacobian1DDeterminal(edgeIndex);
                    var pointsProjectedOntoEdge = UniversalElement.GetIntegrationPointsProjectedOntoEdge(edgeIndex);
                    for (int j = 0; j < pointsProjectedOntoEdge.Length; j++) {
                        var point = pointsProjectedOntoEdge[j];
                        var NValues = UniversalElement.CalculateNValues(point);
                        var NValuesT = NValues.Transpose();
                        
                        // [Hbc]
                        var NxN = MatrixUtils.MultiplyMatrices(NValues.ToMatrix(), NValuesT);
                        var factor = point.WeightKsi * point.WeightEta * jacobianDet * alpha;
                        var HBcPartial = MatrixUtils.MultiplyMatrix(NxN, factor);
                        HBcLocalMatrix = MatrixUtils.AddMatrices(HBcLocalMatrix, HBcPartial);

                        // {P}
                        var vectPFactor = point.WeightKsi * point.WeightEta * jacobianDet * alpha * ambientTemperature;
                        var vectorPPartial = MatrixUtils.MultiplyMatrix(NValues.ToMatrix(), vectPFactor);
                        PVector = MatrixUtils.AddMatrices(PVector, vectorPPartial);
                    }
                }
            }
        }

        private double CalculateEdgeJacobian1DDeterminal(int edgeIndex) {
            //  realLength / localLength
            var start = Nodes[edgeIndex];
            var end = Nodes[(edgeIndex + 1) % ELEMENT_NODES_COUNT];
            var realLength = Util.CalculateEdgeLength(start.RealX, start.RealY, end.RealX, end.RealY);
            return realLength / 2d;
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
                   $"{nl} Macierz H lokalna: {HLocalMatrix.ToStringMatrix()}" +
                   $"{nl} Macierz HBc lokalna: {HBcLocalMatrix.ToStringMatrix()}" +
                   $"{nl} Wektor P: {PVector.ToStringMatrix()}" +
                   $"{nl} Macierz C lokalna: {CLocalMatrix.ToStringMatrix()}";
            return str;
        }
    }
}