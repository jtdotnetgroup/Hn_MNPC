namespace hn.Client.Views.Diglog
{
    partial class FrmAuditDialog
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
            this.lbl内容 = new System.Windows.Forms.Label();
            this.btn按钮1 = new DevExpress.XtraEditors.SimpleButton();
            this.btn取消 = new DevExpress.XtraEditors.SimpleButton();
            this.btn按钮2 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // lbl内容
            // 
            this.lbl内容.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl内容.Location = new System.Drawing.Point(18, 9);
            this.lbl内容.Name = "lbl内容";
            this.lbl内容.Size = new System.Drawing.Size(621, 68);
            this.lbl内容.TabIndex = 0;
            this.lbl内容.Text = "label1";
            this.lbl内容.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn按钮1
            // 
            this.btn按钮1.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.btn按钮1.Appearance.Options.UseFont = true;
            this.btn按钮1.Location = new System.Drawing.Point(182, 90);
            this.btn按钮1.Name = "btn按钮1";
            this.btn按钮1.Size = new System.Drawing.Size(106, 38);
            this.btn按钮1.TabIndex = 1;
            this.btn按钮1.Text = "通过";
            this.btn按钮1.Click += new System.EventHandler(this.btn按钮1_Click);
            // 
            // btn取消
            // 
            this.btn取消.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.btn取消.Appearance.Options.UseFont = true;
            this.btn取消.Location = new System.Drawing.Point(496, 90);
            this.btn取消.Name = "btn取消";
            this.btn取消.Size = new System.Drawing.Size(106, 38);
            this.btn取消.TabIndex = 2;
            this.btn取消.Text = "取消";
            this.btn取消.Click += new System.EventHandler(this.btn取消_Click);
            // 
            // btn按钮2
            // 
            this.btn按钮2.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.btn按钮2.Appearance.Options.UseFont = true;
            this.btn按钮2.Location = new System.Drawing.Point(339, 90);
            this.btn按钮2.Name = "btn按钮2";
            this.btn按钮2.Size = new System.Drawing.Size(106, 38);
            this.btn按钮2.TabIndex = 3;
            this.btn按钮2.Text = "不通过";
            this.btn按钮2.Click += new System.EventHandler(this.btn按钮2_Click);
            // 
            // FrmAuditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 149);
            this.Controls.Add(this.btn按钮2);
            this.Controls.Add(this.btn取消);
            this.Controls.Add(this.btn按钮1);
            this.Controls.Add(this.lbl内容);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAuditDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAuditDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl内容;
        private DevExpress.XtraEditors.SimpleButton btn按钮1;
        private DevExpress.XtraEditors.SimpleButton btn取消;
        private DevExpress.XtraEditors.SimpleButton btn按钮2;
    }
}