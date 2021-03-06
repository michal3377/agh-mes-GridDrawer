﻿namespace MES_GridDrawer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btConstructGrid = new System.Windows.Forms.Button();
            this.tbNodesHeight = new System.Windows.Forms.TextBox();
            this.tbNodesLength = new System.Windows.Forms.TextBox();
            this.tbElementId = new System.Windows.Forms.TextBox();
            this.btGetElementInfo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbElementInfo = new System.Windows.Forms.TextBox();
            this.tbNodesRealHeight = new System.Windows.Forms.TextBox();
            this.tbNodesRealLength = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btSimulate = new System.Windows.Forms.Button();
            this.tbInitTemp = new System.Windows.Forms.TextBox();
            this.tbSimulationTime = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbSimulationStepTime = new System.Windows.Forms.TextBox();
            this.tbAmbientTemp = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbAlpha = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbSpecificHeat = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbConductivity = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbDensity = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.rb2Point = new System.Windows.Forms.RadioButton();
            this.rb3Point = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            this.btConstructGrid.Location = new System.Drawing.Point(12, 438);
            this.btConstructGrid.Name = "btConstructGrid";
            this.btConstructGrid.Size = new System.Drawing.Size(244, 30);
            this.btConstructGrid.TabIndex = 0;
            this.btConstructGrid.Text = "Construct Grid";
            this.btConstructGrid.UseVisualStyleBackColor = true;
            this.btConstructGrid.Click += new System.EventHandler(this.btConstructGrid_Click);
            this.tbNodesHeight.Location = new System.Drawing.Point(152, 123);
            this.tbNodesHeight.Name = "tbNodesHeight";
            this.tbNodesHeight.Size = new System.Drawing.Size(103, 23);
            this.tbNodesHeight.TabIndex = 1;
            this.tbNodesHeight.Text = "31";
            this.tbNodesLength.Location = new System.Drawing.Point(152, 153);
            this.tbNodesLength.Name = "tbNodesLength";
            this.tbNodesLength.Size = new System.Drawing.Size(103, 23);
            this.tbNodesLength.TabIndex = 1;
            this.tbNodesLength.Text = "31";
            this.tbElementId.Location = new System.Drawing.Point(107, 490);
            this.tbElementId.Name = "tbElementId";
            this.tbElementId.Size = new System.Drawing.Size(149, 23);
            this.tbElementId.TabIndex = 1;
            this.tbElementId.Text = "0";
            this.btGetElementInfo.Enabled = false;
            this.btGetElementInfo.Location = new System.Drawing.Point(12, 520);
            this.btGetElementInfo.Name = "btGetElementInfo";
            this.btGetElementInfo.Size = new System.Drawing.Size(244, 31);
            this.btGetElementInfo.TabIndex = 0;
            this.btGetElementInfo.Text = "Get Info";
            this.btGetElementInfo.UseVisualStyleBackColor = true;
            this.btGetElementInfo.Click += new System.EventHandler(this.btGetElementInfo_Click);
            this.label1.Location = new System.Drawing.Point(49, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "nH";
            this.label2.Location = new System.Drawing.Point(49, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "nL";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 494);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Element Id:";
            this.tbElementInfo.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tbElementInfo.Location = new System.Drawing.Point(285, 29);
            this.tbElementInfo.Multiline = true;
            this.tbElementInfo.Name = "tbElementInfo";
            this.tbElementInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbElementInfo.Size = new System.Drawing.Size(399, 610);
            this.tbElementInfo.TabIndex = 3;
            this.tbNodesRealHeight.Location = new System.Drawing.Point(152, 61);
            this.tbNodesRealHeight.Name = "tbNodesRealHeight";
            this.tbNodesRealHeight.Size = new System.Drawing.Size(103, 23);
            this.tbNodesRealHeight.TabIndex = 1;
            this.tbNodesRealHeight.Text = "0.1";
            this.tbNodesRealLength.Location = new System.Drawing.Point(152, 91);
            this.tbNodesRealLength.Name = "tbNodesRealLength";
            this.tbNodesRealLength.Size = new System.Drawing.Size(103, 23);
            this.tbNodesRealLength.TabIndex = 1;
            this.tbNodesRealLength.Text = "0.1";
            this.label4.Location = new System.Drawing.Point(49, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "H";
            this.label5.Location = new System.Drawing.Point(49, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "L";
            this.btSimulate.Enabled = false;
            this.btSimulate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (238)));
            this.btSimulate.Location = new System.Drawing.Point(12, 573);
            this.btSimulate.Name = "btSimulate";
            this.btSimulate.Size = new System.Drawing.Size(244, 55);
            this.btSimulate.TabIndex = 4;
            this.btSimulate.Text = "Simulate";
            this.btSimulate.UseVisualStyleBackColor = true;
            this.btSimulate.Click += new System.EventHandler(this.btSimulate_Click);
            this.tbInitTemp.Location = new System.Drawing.Point(152, 183);
            this.tbInitTemp.Name = "tbInitTemp";
            this.tbInitTemp.Size = new System.Drawing.Size(103, 23);
            this.tbInitTemp.TabIndex = 1;
            this.tbInitTemp.Text = "100";
            this.tbSimulationTime.Location = new System.Drawing.Point(152, 213);
            this.tbSimulationTime.Name = "tbSimulationTime";
            this.tbSimulationTime.Size = new System.Drawing.Size(103, 23);
            this.tbSimulationTime.TabIndex = 1;
            this.tbSimulationTime.Text = "100";
            this.label6.Location = new System.Drawing.Point(9, 187);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "Initial temperature";
            this.tbSimulationStepTime.Location = new System.Drawing.Point(152, 247);
            this.tbSimulationStepTime.Name = "tbSimulationStepTime";
            this.tbSimulationStepTime.Size = new System.Drawing.Size(103, 23);
            this.tbSimulationStepTime.TabIndex = 1;
            this.tbSimulationStepTime.Text = "1";
            this.tbAmbientTemp.Location = new System.Drawing.Point(152, 277);
            this.tbAmbientTemp.Name = "tbAmbientTemp";
            this.tbAmbientTemp.Size = new System.Drawing.Size(103, 23);
            this.tbAmbientTemp.TabIndex = 1;
            this.tbAmbientTemp.Text = "1200";
            this.label7.Location = new System.Drawing.Point(9, 218);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 17);
            this.label7.TabIndex = 2;
            this.label7.Text = "Simulation time";
            this.label8.Location = new System.Drawing.Point(9, 250);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 17);
            this.label8.TabIndex = 2;
            this.label8.Text = "Simulation step time";
            this.label9.Location = new System.Drawing.Point(9, 280);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(136, 17);
            this.label9.TabIndex = 2;
            this.label9.Text = "Ambient temperature";
            this.tbAlpha.Location = new System.Drawing.Point(152, 307);
            this.tbAlpha.Name = "tbAlpha";
            this.tbAlpha.Size = new System.Drawing.Size(103, 23);
            this.tbAlpha.TabIndex = 1;
            this.tbAlpha.Text = "300";
            this.label10.Location = new System.Drawing.Point(9, 310);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(136, 17);
            this.label10.TabIndex = 2;
            this.label10.Text = "Alpha";
            this.label10.Click += new System.EventHandler(this.Label10_Click);
            this.tbSpecificHeat.Location = new System.Drawing.Point(152, 337);
            this.tbSpecificHeat.Name = "tbSpecificHeat";
            this.tbSpecificHeat.Size = new System.Drawing.Size(103, 23);
            this.tbSpecificHeat.TabIndex = 1;
            this.tbSpecificHeat.Text = "700";
            this.label11.Location = new System.Drawing.Point(9, 340);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(136, 17);
            this.label11.TabIndex = 2;
            this.label11.Text = "Specific heat";
            this.tbConductivity.Location = new System.Drawing.Point(152, 367);
            this.tbConductivity.Name = "tbConductivity";
            this.tbConductivity.Size = new System.Drawing.Size(103, 23);
            this.tbConductivity.TabIndex = 1;
            this.tbConductivity.Text = "25";
            this.label12.Location = new System.Drawing.Point(9, 370);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(136, 17);
            this.label12.TabIndex = 2;
            this.label12.Text = "Conductivity";
            this.tbDensity.Location = new System.Drawing.Point(152, 397);
            this.tbDensity.Name = "tbDensity";
            this.tbDensity.Size = new System.Drawing.Size(103, 23);
            this.tbDensity.TabIndex = 1;
            this.tbDensity.Text = "7800";
            this.label13.Location = new System.Drawing.Point(9, 400);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(136, 17);
            this.label13.TabIndex = 2;
            this.label13.Text = "Density";
            this.label14.Location = new System.Drawing.Point(9, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(162, 17);
            this.label14.TabIndex = 2;
            this.label14.Text = "Schemat całkowania:";
            this.rb2Point.Location = new System.Drawing.Point(12, 29);
            this.rb2Point.Name = "rb2Point";
            this.rb2Point.Size = new System.Drawing.Size(93, 21);
            this.rb2Point.TabIndex = 5;
            this.rb2Point.Text = "2 punktowy";
            this.rb2Point.UseVisualStyleBackColor = true;
            this.rb3Point.Checked = true;
            this.rb3Point.Location = new System.Drawing.Point(142, 29);
            this.rb3Point.Name = "rb3Point";
            this.rb3Point.Size = new System.Drawing.Size(93, 21);
            this.rb3Point.TabIndex = 5;
            this.rb3Point.TabStop = true;
            this.rb3Point.Text = "3 punktowy";
            this.rb3Point.UseVisualStyleBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 640);
            this.Controls.Add(this.rb3Point);
            this.Controls.Add(this.rb2Point);
            this.Controls.Add(this.btSimulate);
            this.Controls.Add(this.tbElementInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbElementId);
            this.Controls.Add(this.tbNodesRealLength);
            this.Controls.Add(this.tbDensity);
            this.Controls.Add(this.tbConductivity);
            this.Controls.Add(this.tbSpecificHeat);
            this.Controls.Add(this.tbAlpha);
            this.Controls.Add(this.tbAmbientTemp);
            this.Controls.Add(this.tbSimulationTime);
            this.Controls.Add(this.tbSimulationStepTime);
            this.Controls.Add(this.tbNodesLength);
            this.Controls.Add(this.tbInitTemp);
            this.Controls.Add(this.tbNodesRealHeight);
            this.Controls.Add(this.tbNodesHeight);
            this.Controls.Add(this.btGetElementInfo);
            this.Controls.Add(this.btConstructGrid);
            this.Name = "Form1";
            this.Text = "FEM Simulator";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox tbNodesHeight;
        private System.Windows.Forms.TextBox tbNodesLength;
        private System.Windows.Forms.Button btGetElementInfo;
        private System.Windows.Forms.Button btConstructGrid;
        private System.Windows.Forms.TextBox tbElementId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbElementInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbNodesRealLength;
        private System.Windows.Forms.TextBox tbNodesRealHeight;
        private System.Windows.Forms.Button btSimulate;
        private System.Windows.Forms.TextBox tbInitTemp;
        private System.Windows.Forms.TextBox tbSimulationTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbSimulationStepTime;
        private System.Windows.Forms.TextBox tbAmbientTemp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbAlpha;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbSpecificHeat;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbConductivity;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbDensity;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RadioButton rb3Point;
        private System.Windows.Forms.RadioButton rb2Point;
    }
}

