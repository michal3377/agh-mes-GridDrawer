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

        private List<SimulationResult> _lastResults;
        private PlotForm _plotForm;

        public Form1()
        {
            InitializeComponent();
            _plotForm = new PlotForm();
        }

        private void btConstructGrid_Click(object sender, EventArgs e) {
#if CATCH_EXC
            try {    
#endif 
                double rH = double.Parse(tbNodesRealHeight.Text.Replace('.',','));
                double rL = double.Parse(tbNodesRealLength.Text.Replace('.',','));
                int h = int.Parse(tbNodesHeight.Text);
                int l = int.Parse(tbNodesLength.Text);
                double initTemp = double.Parse(tbInitTemp.Text.Replace('.', ','));
                double simulTime = double.Parse(tbSimulationTime.Text.Replace('.', ','));
                double simulStepTime = double.Parse(tbSimulationStepTime.Text.Replace('.', ','));
                double ambientTemp= double.Parse(tbAmbientTemp.Text.Replace('.', ','));
                double alpha = double.Parse(tbAlpha.Text.Replace('.', ','));
                double specificHeat = double.Parse(tbSpecificHeat.Text.Replace('.', ','));
                double conductivity= double.Parse(tbConductivity.Text.Replace('.', ','));
                double density = double.Parse(tbDensity.Text.Replace('.', ','));
            _globalData = new GlobalData {
                    NodesHeight = h,
                    NodesLength = l,
                    RealHeight = rH,
                    RealLength = rL,
                    
                    InitialTemperature = initTemp,
                    AmbientTemperature = ambientTemp,
                    Alpha = alpha,
                    SpecificHeat = specificHeat,
                    Conductivity = conductivity,
                    Density = density,
                    SimulationTime = simulTime,
                    SimulationStepTime = simulStepTime
                };
                _grid = new Grid(_globalData, rb3Point.Checked);
                _grid.ConstructGrid();
                _grid.CalculateMatrices();
                btGetElementInfo.Enabled = true;
                btSimulate.Enabled = true;
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

        private async void btSimulate_Click(object sender, EventArgs e)
        {
            btSimulate.Enabled = false;
            var dof = _grid.Nodes.Length;
            var initTempVector = new double[dof, 1];
            initTempVector.FillWithValue(_globalData.InitialTemperature);

            var simulator = new ProcessSimulator(_grid.HGlobalMatrix, _grid.CGlobalMatrix,
                _grid.PGlobalVector, initTempVector, _globalData.SimulationStepTime, _globalData.SimulationTime);
            simulator.LogMessageDelegate = msg => tbElementInfo.SafeInvoke(()=> tbElementInfo.AppendText($"{Environment.NewLine}{msg}"));

            await Task.Run(() => {
                _lastResults = simulator.Simulate();
            });
            btSimulate.Enabled = true;
            _plotForm.Show();
            _plotForm.Visualize(_grid, _lastResults);
        }

        private void Label10_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _plotForm.Show();
            _plotForm.TestPlot();
        }
    }
}
