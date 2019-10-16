using System;

namespace MES_GridDrawer.FEM {
    public class Element {

        public const int ELEMENT_NODES_COUNT = 4;
        
        public int Id;
        public int X, Y;
        
        public Node[] Nodes;

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