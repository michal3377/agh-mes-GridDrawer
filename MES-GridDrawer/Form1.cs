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

namespace MES_GridDrawer
{
    public partial class Form1 : Form {
        private Grid _grid;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btConstructGrid_Click(object sender, EventArgs e) {
            int h = int.Parse(tbNodesHeight.Text);
            int l = int.Parse(tbNodesLength.Text);
            var data = new GlobalData {
                NodesHeight = h,
                NodesLength = l
            };
            _grid = new Grid(data);
            _grid.ConstructGrid();
            btGetElementInfo.Enabled = true;
        }

        private void btGetElementInfo_Click(object sender, EventArgs e) {
            if(_grid == null) return;
            int id = int.Parse(tbElementId.Text);
            var element = _grid.Elements[id];
            MessageBox.Show(element.ToString());
        }
    }
}
