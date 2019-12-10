using System;

namespace MES_GridDrawer.FEM {
    public class Element {

        public const int ELEMENT_NODES_COUNT = 4;
        
        public int Id;
        public int X, Y;
        
        public Node[] Nodes;
        
        public double[][,] Jacobians = new double[4][,];
        public double[][,] TransformedJacobians = new double[4][,];
        public double[] JacobianDeterminals = new double[4];

        public UniversalElement UniversalElement;
        
        
        public void CalculateJacobians() {
            int index = 0;
            for (var i = 0; i < UniversalElement.Points.Length; i++) {
                var jacobian = Jacobians[i];
                //[↓ →] 
                //dx/dksi
                jacobian[0, 0] = UniversalElement.KsiValues[i, 0] * Nodes[0].X +
                                 UniversalElement.KsiValues[i, 1] * Nodes[1].X +
                                 UniversalElement.KsiValues[i, 2] * Nodes[2].X +
                                 UniversalElement.KsiValues[i, 3] * Nodes[3].X;
                
                //dy/dksi
                jacobian[0, 1] = UniversalElement.KsiValues[i, 0] * Nodes[0].Y +
                                 UniversalElement.KsiValues[i, 1] * Nodes[1].Y +
                                 UniversalElement.KsiValues[i, 2] * Nodes[2].Y +
                                 UniversalElement.KsiValues[i, 3] * Nodes[3].Y;
                // dx/deta
                jacobian[1, 0] = UniversalElement.EtaValues[i, 0] * Nodes[0].X +
                                 UniversalElement.EtaValues[i, 1] * Nodes[1].X +
                                 UniversalElement.EtaValues[i, 2] * Nodes[2].X +
                                 UniversalElement.EtaValues[i, 3] * Nodes[3].X;
                
                // dy/deta
                jacobian[1, 1] = UniversalElement.EtaValues[i, 0] * Nodes[0].Y +
                                 UniversalElement.EtaValues[i, 1] * Nodes[1].Y +
                                 UniversalElement.EtaValues[i, 2] * Nodes[2].Y +
                                 UniversalElement.EtaValues[i, 3] * Nodes[3].Y;

                JacobianDeterminals[i] = jacobian[0, 0] * jacobian[1, 1] - jacobian[0, 1] * jacobian[1, 0];
            }
        }

        public void TransformJacobians() {
            for (int i = 0; i < 4; i++) {
                var jacobian = Jacobians[i];
                TransformedJacobians[i][0, 0] = jacobian[1, 1];
                TransformedJacobians[i][1, 1] = jacobian[0, 0];
                TransformedJacobians[i][0, 1] = -jacobian[0, 1];
                TransformedJacobians[i][1, 0] = -jacobian[1, 0];
            }
        }

        public double CalculateDnDx(int pointIndex, int nIndex) {
            //nIndex - no. of shape function 
            return 1 / JacobianDeterminals[pointIndex] * (TransformedJacobians[pointIndex][0, 0] 
                                                          * UniversalElement.KsiValues[pointIndex, nIndex]
                                                          + TransformedJacobians[pointIndex][0, 1] *
                                                          UniversalElement.EtaValues[pointIndex, nIndex]);
        }
        
        public double CalculateDnDy(int pointIndex, int nIndex) {
            //nIndex - no. of shape function 
            return 1 / JacobianDeterminals[pointIndex] * (TransformedJacobians[pointIndex][1, 0] 
                                                          * UniversalElement.KsiValues[pointIndex, nIndex]
                                                          + TransformedJacobians[pointIndex][1, 1] *
                                                          UniversalElement.EtaValues[pointIndex, nIndex]);
        }


        public override string ToString() {
            var nl = Environment.NewLine;
            return $"Element {Id}. Nodes:" +
                   $"{nl}\t{Nodes[0]}" +
                   $"{nl}\t{Nodes[1]}" +
                   $"{nl}\t{Nodes[2]}" +
                   $"{nl}\t{Nodes[3]}";
        }
    }
}