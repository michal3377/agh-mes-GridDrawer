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
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btConstructGrid_Click(object sender, EventArgs e) {
            try {
                double rH = double.Parse(tbNodesRealHeight.Text.Replace('.',','));
                double rL = double.Parse(tbNodesRealLength.Text.Replace('.',','));
                int h = int.Parse(tbNodesHeight.Text);
                int l = int.Parse(tbNodesLength.Text);
                var data = new GlobalData {
                    NodesHeight = h,
                    NodesLength = l,
                    RealHeight = rH,
                    RealLength = rL
                };
                _grid = new Grid(data);
                _grid.ConstructGrid();
                _grid.CalculateMatrices();
                btGetElementInfo.Enabled = true;
                tbElementInfo.Text += $"HGlobal: {_grid.HGlobalMatrix.ToStringMatrix()}";
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
            
            
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
    }
}
