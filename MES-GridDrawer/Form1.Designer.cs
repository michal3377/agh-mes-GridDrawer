namespace MES_GridDrawer
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
            this.SuspendLayout();
            // 
            // btConstructGrid
            // 
            this.btConstructGrid.Location = new System.Drawing.Point(103, 166);
            this.btConstructGrid.Name = "btConstructGrid";
            this.btConstructGrid.Size = new System.Drawing.Size(149, 47);
            this.btConstructGrid.TabIndex = 0;
            this.btConstructGrid.Text = "Construct Grid";
            this.btConstructGrid.UseVisualStyleBackColor = true;
            this.btConstructGrid.Click += new System.EventHandler(this.btConstructGrid_Click);
            // 
            // tbNodesHeight
            // 
            this.tbNodesHeight.Location = new System.Drawing.Point(103, 91);
            this.tbNodesHeight.Name = "tbNodesHeight";
            this.tbNodesHeight.Size = new System.Drawing.Size(149, 23);
            this.tbNodesHeight.TabIndex = 1;
            this.tbNodesHeight.Text = "5";
            // 
            // tbNodesLength
            // 
            this.tbNodesLength.Location = new System.Drawing.Point(103, 121);
            this.tbNodesLength.Name = "tbNodesLength";
            this.tbNodesLength.Size = new System.Drawing.Size(149, 23);
            this.tbNodesLength.TabIndex = 1;
            this.tbNodesLength.Text = "3";
            // 
            // tbElementId
            // 
            this.tbElementId.Location = new System.Drawing.Point(103, 258);
            this.tbElementId.Name = "tbElementId";
            this.tbElementId.Size = new System.Drawing.Size(149, 23);
            this.tbElementId.TabIndex = 1;
            // 
            // btGetElementInfo
            // 
            this.btGetElementInfo.Enabled = false;
            this.btGetElementInfo.Location = new System.Drawing.Point(103, 303);
            this.btGetElementInfo.Name = "btGetElementInfo";
            this.btGetElementInfo.Size = new System.Drawing.Size(149, 47);
            this.btGetElementInfo.TabIndex = 0;
            this.btGetElementInfo.Text = "Get Info";
            this.btGetElementInfo.UseVisualStyleBackColor = true;
            this.btGetElementInfo.Click += new System.EventHandler(this.btGetElementInfo_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(45, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "nH";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(45, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "nL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 262);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Element Id:";
            // 
            // tbElementInfo
            // 
            this.tbElementInfo.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tbElementInfo.Location = new System.Drawing.Point(285, 29);
            this.tbElementInfo.Multiline = true;
            this.tbElementInfo.Name = "tbElementInfo";
            this.tbElementInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbElementInfo.Size = new System.Drawing.Size(329, 351);
            this.tbElementInfo.TabIndex = 3;
            // 
            // tbNodesRealHeight
            // 
            this.tbNodesRealHeight.Location = new System.Drawing.Point(103, 29);
            this.tbNodesRealHeight.Name = "tbNodesRealHeight";
            this.tbNodesRealHeight.Size = new System.Drawing.Size(149, 23);
            this.tbNodesRealHeight.TabIndex = 1;
            this.tbNodesRealHeight.Text = "1.5";
            // 
            // tbNodesRealLength
            // 
            this.tbNodesRealLength.Location = new System.Drawing.Point(103, 59);
            this.tbNodesRealLength.Name = "tbNodesRealLength";
            this.tbNodesRealLength.Size = new System.Drawing.Size(149, 23);
            this.tbNodesRealLength.TabIndex = 1;
            this.tbNodesRealLength.Text = "0.5";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(45, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "H";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(45, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "L";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 382);
            this.Controls.Add(this.tbElementInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbElementId);
            this.Controls.Add(this.tbNodesRealLength);
            this.Controls.Add(this.tbNodesLength);
            this.Controls.Add(this.tbNodesRealHeight);
            this.Controls.Add(this.tbNodesHeight);
            this.Controls.Add(this.btGetElementInfo);
            this.Controls.Add(this.btConstructGrid);
            this.Name = "Form1";
            this.Text = "Form1";
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
    }
}

