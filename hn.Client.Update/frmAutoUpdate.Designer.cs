namespace hn.Client.Update
{
    partial class frmAutoUpdate
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBar更新进度 = new System.Windows.Forms.ProgressBar();
            this.txt更新进度记录 = new System.Windows.Forms.TextBox();
            this.backgroundWorker下载 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // progressBar更新进度
            // 
            this.progressBar更新进度.Location = new System.Drawing.Point(20, 15);
            this.progressBar更新进度.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar更新进度.Name = "progressBar更新进度";
            this.progressBar更新进度.Size = new System.Drawing.Size(542, 23);
            this.progressBar更新进度.TabIndex = 5;
            // 
            // txt更新进度记录
            // 
            this.txt更新进度记录.Location = new System.Drawing.Point(20, 57);
            this.txt更新进度记录.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt更新进度记录.Multiline = true;
            this.txt更新进度记录.Name = "txt更新进度记录";
            this.txt更新进度记录.ReadOnly = true;
            this.txt更新进度记录.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt更新进度记录.Size = new System.Drawing.Size(542, 102);
            this.txt更新进度记录.TabIndex = 8;
            // 
            // backgroundWorker下载
            // 
            this.backgroundWorker下载.WorkerReportsProgress = true;
            this.backgroundWorker下载.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker下载_DoWork);
            this.backgroundWorker下载.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker下载_ProgressChanged);
            this.backgroundWorker下载.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker下载_RunWorkerCompleted);
            // 
            // frmAutoUpdate
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(578, 169);
            this.Controls.Add(this.txt更新进度记录);
            this.Controls.Add(this.progressBar更新进度);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAutoUpdate";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更新软件";
            this.Load += new System.EventHandler(this.frmAutoUpdate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar更新进度;
        private System.Windows.Forms.TextBox txt更新进度记录;
        private System.ComponentModel.BackgroundWorker backgroundWorker下载;
    }
}

