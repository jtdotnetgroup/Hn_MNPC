namespace HN.Client
{
    partial class FrmIndex
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
        private void InitializeComponent()
        {
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.kIndexView1 = new HN.Client.Core.KIndexView();
            this.kIndexView3 = new HN.Client.Core.KIndexView();
            this.kIndexView2 = new HN.Client.Core.KIndexView();
            this.xtraScrollableControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.xtraScrollableControl1.Appearance.Options.UseBackColor = true;
            this.xtraScrollableControl1.Controls.Add(this.kIndexView1);
            this.xtraScrollableControl1.Controls.Add(this.kIndexView3);
            this.xtraScrollableControl1.Controls.Add(this.kIndexView2);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl1.LookAndFeel.SkinName = "Office 2010 Silver";
            this.xtraScrollableControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(1150, 623);
            this.xtraScrollableControl1.TabIndex = 3;
            // 
            // kIndexView1
            // 
            this.kIndexView1.BackColor = System.Drawing.Color.White;
            this.kIndexView1.K_Text = "待确认";
            this.kIndexView1.Location = new System.Drawing.Point(3, 3);
            this.kIndexView1.Name = "kIndexView1";
            this.kIndexView1.Size = new System.Drawing.Size(381, 253);
            this.kIndexView1.TabIndex = 0;
            // 
            // kIndexView3
            // 
            this.kIndexView3.BackColor = System.Drawing.Color.White;
            this.kIndexView3.K_Text = "待发货";
            this.kIndexView3.Location = new System.Drawing.Point(765, 3);
            this.kIndexView3.Name = "kIndexView3";
            this.kIndexView3.Size = new System.Drawing.Size(381, 253);
            this.kIndexView3.TabIndex = 2;
            // 
            // kIndexView2
            // 
            this.kIndexView2.BackColor = System.Drawing.Color.White;
            this.kIndexView2.K_Text = "待派车";
            this.kIndexView2.Location = new System.Drawing.Point(384, 3);
            this.kIndexView2.Name = "kIndexView2";
            this.kIndexView2.Size = new System.Drawing.Size(381, 253);
            this.kIndexView2.TabIndex = 1;
            // 
            // FrmIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1150, 623);
            this.Controls.Add(this.xtraScrollableControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmIndex";
            this.Text = " 首页 ";
            this.xtraScrollableControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Core.KIndexView kIndexView1;
        private Core.KIndexView kIndexView2;
        private Core.KIndexView kIndexView3;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
    }
}