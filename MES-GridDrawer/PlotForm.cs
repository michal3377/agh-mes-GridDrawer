using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MES_GridDrawer.Utils;
using Grid = MES_GridDrawer.FEM.Grid;

namespace MES_GridDrawer
{
    public partial class PlotForm : Form
    {
        Pen pen1 = new System.Drawing.Pen(Color.Black, 2);
        private List<SimulationResult> _results;
        private Bitmap[] bitmaps;
        private Grid _grid;
        private bool _trackbarScrolling = false;
        private int _currentStep = 0;
        private bool _bitmapsPrepared = false;

        public PlotForm()
        {
            InitializeComponent();
        }

        private void PlotForm_Load(object sender, EventArgs e) {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        public void Visualize(Grid grid, List<SimulationResult> results) {
            _grid = grid;
            _results = results;
            _bitmapsPrepared = false;
            bitmaps = new Bitmap[results.Count];
            trackBar1.Maximum = results.Count - 1;
//            ShowPlot(results.Count - 1);
            PrepareBitmaps();
        }

        private void ShowPlot(int step) {
            _currentStep = step;
            RefreshCurrentInfo();
            
            if (bitmaps[step] != null) {
                pictureBox1.Image = bitmaps[step];
                return;
            }
            var bitmap = new Bitmap(1000, 1000);
            pictureBox1.Image = bitmap;
            var graphics = Graphics.FromImage(bitmap);
            var plotElements = ConvertGrid(_grid, _results[step]);
            DrawPlot(plotElements, graphics);
            bitmaps[step] = bitmap;
            pictureBox1.Refresh();

        }

        private List<PlotElement> ConvertGrid(Grid grid, SimulationResult result) {
            var plotElements = new List<PlotElement>();
            foreach (var gridElement in grid.Elements) {
                var plotElement = new PlotElement();
                foreach (var gridElementNode in gridElement.Nodes) {
                    float x = (float) gridElementNode.RealX * 4000 + 20;
                    float y = (float) gridElementNode.RealY * 4000 + 20;
                    double value = result.Values[gridElementNode.Id];
                    var color = HeatMapColor(value, result.Min, result.Max);
                    plotElement.PlotNodes.Add(new PlotNode {
                        Color = color,
                        X = x,
                        Y = y
                    });
                }
                plotElements.Add(plotElement);
            }

            return plotElements;
        }
        
        private Color HeatMapColor(double value, double min, double max)
        {
            double val = (value - min) / (max - min);
            int r = Convert.ToByte(255 * val);
            int g = Convert.ToByte(255 * (1 - val));
            int b = 0;

            return Color.FromArgb(255,r,g,b);                                    
        }


        private void DrawPlot(List<PlotElement> elements, Graphics graphics)
        {
            pictureBox1.Refresh();
            foreach (var element in elements) {
                var polygon = element.Coords;
                graphics.DrawPolygon(pen1, polygon);
                FillPolygon(graphics, polygon, element.Colors);
            }
        }

        void FillPolygon(Graphics g, PointF[] polygon, Color[] colors)
        {
            using (PathGradientBrush pgb = new PathGradientBrush(polygon))
            {
                pgb.CenterColor = medianColor(colors);
                pgb.SurroundColors = colors.ToArray();
                g.FillPolygon(pgb, polygon);
            }
        }

        public void TestPlot() {
            pictureBox1.Refresh();
        }

        private List<PointF> getCorners(RectangleF r)
        {
            return new List<PointF>()
            {
                r.Location, new PointF(r.Right, r.Top),
                new PointF(r.Right, r.Bottom), new PointF(r.Left, r.Bottom)
            };
        }

        private static Color medianColor(Color[] cols)
        {
            int c = cols.Length;
            return Color.FromArgb(cols.Sum(x => x.A) / c, cols.Sum(x => x.R) / c,
                cols.Sum(x => x.G) / c, cols.Sum(x => x.B) / c);
        }

        public void Prepare()
        {
            List<Color> stopColors = new List<Color>()
                {Color.Blue, Color.Cyan, Color.YellowGreen, Color.Orange, Color.Red};
            //colorList = interpolateColors(stopColors, 100);
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            if(_trackbarScrolling) return;
            OnTrackBarChangedVal();
        }

        private void TrackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            _trackbarScrolling = true;
        }

        private void TrackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            if(!_trackbarScrolling) return;
            _trackbarScrolling = false;
            OnTrackBarChangedVal();
        }

        private void OnTrackBarChangedVal() {
            ShowPlot(trackBar1.Value);
        }

        private void RefreshCurrentInfo() {
            var result = _results[_currentStep];
            lbCurrentInfo.Text = $"Krok {_currentStep}: Tau: {result.Tau}, Min: {result.Min}, Max: {result.Max}";
            trackBar1.Value = _currentStep;
        }

        private void PrepareBitmaps() {
            for (int i = 0; i < bitmaps.Length; i++) {
                if (bitmaps[i] == null) ShowPlot(i);
            }

            _bitmapsPrepared = true;
        }

        private void BtPlay_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled) {
                timer1.Stop();
                btPlay.Text = "Play";
            } else {
                if (_currentStep == _results.Count - 1) _currentStep = 0;
                timer1.Start();
                btPlay.Text = "Stop";
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            
            if (_currentStep == _results.Count - 1) {
                timer1.Stop();
                return;
            }

            _currentStep++;
            ShowPlot(_currentStep);
        }
    }
}