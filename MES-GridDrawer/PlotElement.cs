using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_GridDrawer
{
    public class PlotElement
    {
        public List<PlotNode> PlotNodes = new List<PlotNode>(4);
        

        public PointF[] Coords => PlotNodes.Select(node => new PointF(node.X, node.Y)).ToArray();
        public Color[] Colors => PlotNodes.Select(node => node.Color).ToArray();
    }

    public class PlotNode
    {
        public float X, Y;
        public Color Color;
    }
}
