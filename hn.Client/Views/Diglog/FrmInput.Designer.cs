namespace hn.Client.Views.Diglog
{
    partial class FrmInput
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
            this.btn确定 = new DevExpress.XtraEditors.SimpleButton();
            this.btn取消 = new DevExpress.XtraEditors.SimpleButton();
            this.txt内容 = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txt内容.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn确定
            // 
            this.btn确定.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.btn确定.Appearance.Options.UseFont = true;
            this.btn确定.Location = new System.Drawing.Point(277, 227);
            this.btn确定.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn确定.Name = "btn确定";
            this.btn确定.Size = new System.Drawing.Size(80, 30);
            this.btn确定.TabIndex = 1;
            this.btn确定.Text = "确定";
            this.btn确定.Click += new System.EventHandler(this.btn按钮1_Click);
            // 
            // btn取消
            // 
            this.btn取消.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.btn取消.Appearance.Options.UseFont = true;
            this.btn取消.Location = new System.Drawing.Point(378, 227);
            this.btn取消.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn取消.Name = "btn取消";
            this.btn取消.Size = new System.Drawing.Size(80, 30);
            this.btn取消.TabIndex = 2;
            this.btn取消.Text = "关闭";
            this.btn取消.Click += new System.EventHandler(this.btn取消_Click);
            // 
            // txt内容
            // 
            this.txt内容.Location = new System.Drawing.Point(27, 21);
            this.txt内容.Name = "txt内容";
            this.txt内容.Size = new System.Drawing.Size(414, 183);
            this.txt内容.TabIndex = 3;
            // 
            // FrmInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 278);
            this.Controls.Add(this.txt内容);
            this.Controls.Add(this.btn取消);
            this.Controls.Add(this.btn确定);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInput";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "原因";
            ((System.ComponentModel.ISupportInitialize)(this.txt内容.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btn确定;
        private DevExpress.XtraEditors.SimpleButton btn取消;
        private DevExpress.XtraEditors.MemoEdit txt内容;
    }
}