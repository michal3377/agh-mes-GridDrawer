using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MES_GridDrawer.FEM;
using MES_GridDrawer.Utils;

//#define CATCH_EXC

namespace MES_GridDrawer
{
    public partial class Form1 : Form {
        private Grid _grid;
        private GlobalData _globalData;

        public Form1()
        {
            InitializeComponent();
        }

        private void btConstructGrid_Click(object sender, EventArgs e) {
#if CATCH_EXC
            try {    
#endif 
                double rH = double.Parse(tbNodesRealHeight.Text.Replace('.',','));
                double rL = double.Parse(tbNodesRealLength.Text.Replace('.',','));
                int h = int.Parse(tbNodesHeight.Text);
                int l = int.Parse(tbNodesLength.Text);
                _globalData = new GlobalData {
                    NodesHeight = h,
                    NodesLength = l,
                    RealHeight = rH,
                    RealLength = rL,
                    
                    InitialTemperature = 100,
                    AmbientTemperature = 1200,
                    Alpha = 300,
                    SpecificHeat = 700,
                    Conductivity = 25,
                    Density = 7800
                };
                _grid = new Grid(_globalData);
                _grid.ConstructGrid();
                _grid.CalculateMatrices();
                btGetElementInfo.Enabled = true;
//                tbElementInfo.Text += $"HGlobal: {_grid.HGlobalMatrix.ToStringMatrix()}";
#if CATCH_EXC
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
#endif
            
            
        }

        private void btGetElementInfo_Click(object sender, EventArgs e) {
            if(_grid == null) return;
#if CATCH_EXC
            try {    
#endif 
                int id = int.Parse(tbElementId.Text);
                var element = _grid.Elements[id];
                tbElementInfo.Text += $"{element}{Environment.NewLine}";
#if CATCH_EXC
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
#endif
        }

        private async void btSimulate_Click(object sender, EventArgs e) {
            var stepTime = 1d;
//            var stepTime = 50d;
            var time = 100d;
//            var time = 500d;
            
            var dof = _grid.Nodes.Length;
            var initTempVector = new double[dof, 1];
            initTempVector.FillWithValue(_globalData.InitialTemperature);

            
            await Task.Run(() => {
                var simulator = new ProcessSimulator(_grid.HGlobalMatrix, _grid.CGlobalMatrix,
                    _grid.PGlobalVector, initTempVector, stepTime, time);
                simulator.LogMessageDelegate = msg => tbElementInfo.SafeInvoke(()=> tbElementInfo.AppendText($"{Environment.NewLine}{msg}"));
                simulator.Simulate();
            });
        }
    }
}
