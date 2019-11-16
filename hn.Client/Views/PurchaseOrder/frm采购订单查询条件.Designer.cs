namespace hn.Client
{
    partial class FrmOrderQuery
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrderQuery));
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.txt日期结束 = new DevExpress.XtraEditors.DateEdit();
            this.txt日期开始 = new DevExpress.XtraEditors.DateEdit();
            this.txt采购单号 = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.txt重置 = new DevExpress.XtraEditors.SimpleButton();
            this.btn查询 = new DevExpress.XtraEditors.SimpleButton();
            this.chkClose = new DevExpress.XtraEditors.CheckEdit();
            this.cbo状态 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.cbo品牌 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bgw加载数据 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt日期结束.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt日期结束.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt日期开始.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt日期开始.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt采购单号.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkClose.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo状态.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo品牌.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "btnConfirm.png");
            this.imageList2.Images.SetKeyName(1, "btnClose.png");
            this.imageList2.Images.SetKeyName(2, "btnSave.png");
            this.imageList2.Images.SetKeyName(3, "btnSearch.png");
            this.imageList2.Images.SetKeyName(4, "btnSendGoods.png");
            this.imageList2.Images.SetKeyName(5, "btnPageIcon.png");
            this.imageList2.Images.SetKeyName(6, "btnSetting.png");
            this.imageList2.Images.SetKeyName(7, "BtnTitleClose.png");
            this.imageList2.Images.SetKeyName(8, "btnTitleMaxMin.png");
            // 
            // txt日期结束
            // 
            this.txt日期结束.EditValue = null;
            this.txt日期结束.Location = new System.Drawing.Point(332, 133);
            this.txt日期结束.Margin = new System.Windows.Forms.Padding(2);
            this.txt日期结束.Name = "txt日期结束";
            this.txt日期结束.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txt日期结束.Properties.Appearance.Options.UseFont = true;
            this.txt日期结束.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txt日期结束.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txt日期结束.Properties.CalendarTimeProperties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.txt日期结束.Properties.CalendarTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txt日期结束.Properties.CalendarTimeProperties.EditFormat.FormatString = "yyyy/MM/dd";
            this.txt日期结束.Properties.CalendarTimeProperties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txt日期结束.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.txt日期结束.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txt日期结束.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.txt日期结束.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txt日期结束.Size = new System.Drawing.Size(168, 26);
            this.txt日期结束.TabIndex = 69;
            // 
            // txt日期开始
            // 
            this.txt日期开始.EditValue = null;
            this.txt日期开始.Location = new System.Drawing.Point(83, 133);
            this.txt日期开始.Margin = new System.Windows.Forms.Padding(2);
            this.txt日期开始.Name = "txt日期开始";
            this.txt日期开始.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txt日期开始.Properties.Appearance.Options.UseFont = true;
            this.txt日期开始.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txt日期开始.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txt日期开始.Properties.CalendarTimeProperties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.txt日期开始.Properties.CalendarTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txt日期开始.Properties.CalendarTimeProperties.EditFormat.FormatString = "yyyy/MM/dd";
            this.txt日期开始.Properties.CalendarTimeProperties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txt日期开始.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.txt日期开始.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txt日期开始.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.txt日期开始.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txt日期开始.Size = new System.Drawing.Size(142, 26);
            this.txt日期开始.TabIndex = 68;
            // 
            // txt采购单号
            // 
            this.txt采购单号.Location = new System.Drawing.Point(332, 80);
            this.txt采购单号.Name = "txt采购单号";
            this.txt采购单号.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txt采购单号.Properties.Appearance.Options.UseFont = true;
            this.txt采购单号.Size = new System.Drawing.Size(168, 26);
            this.txt采购单号.TabIndex = 57;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(268, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 19);
            this.label5.TabIndex = 56;
            this.label5.Text = "采购单号:";
            // 
            // txt重置
            // 
            this.txt重置.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt重置.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txt重置.Appearance.Options.UseFont = true;
            this.txt重置.Appearance.Options.UseForeColor = true;
            this.txt重置.AppearanceDisabled.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txt重置.AppearanceDisabled.Options.UseFont = true;
            this.txt重置.AppearanceHovered.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txt重置.AppearanceHovered.Options.UseFont = true;
            this.txt重置.ImageList = this.imageList2;
            this.txt重置.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.txt重置.Location = new System.Drawing.Point(245, 253);
            this.txt重置.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.txt重置.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txt重置.Name = "txt重置";
            this.txt重置.Size = new System.Drawing.Size(71, 27);
            this.txt重置.TabIndex = 54;
            this.txt重置.Text = "重置(&C)";
            this.txt重置.Click += new System.EventHandler(this.txt重置_Click);
            // 
            // btn查询
            // 
            this.btn查询.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn查询.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn查询.Appearance.Options.UseFont = true;
            this.btn查询.Appearance.Options.UseForeColor = true;
            this.btn查询.AppearanceDisabled.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.btn查询.AppearanceDisabled.Options.UseFont = true;
            this.btn查询.AppearanceHovered.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.btn查询.AppearanceHovered.Options.UseFont = true;
            this.btn查询.ImageList = this.imageList2;
            this.btn查询.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn查询.Location = new System.Drawing.Point(168, 253);
            this.btn查询.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.btn查询.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn查询.Name = "btn查询";
            this.btn查询.Size = new System.Drawing.Size(71, 27);
            this.btn查询.TabIndex = 53;
            this.btn查询.Text = "查询(&S)";
            this.btn查询.Click += new System.EventHandler(this.btn查询_Click);
            // 
            // chkClose
            // 
            this.chkClose.EditValue = true;
            this.chkClose.Location = new System.Drawing.Point(272, 185);
            this.chkClose.Margin = new System.Windows.Forms.Padding(2);
            this.chkClose.Name = "chkClose";
            this.chkClose.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.chkClose.Properties.Appearance.Options.UseFont = true;
            this.chkClose.Properties.Caption = "不显示已关闭的数据";
            this.chkClose.Size = new System.Drawing.Size(142, 23);
            this.chkClose.TabIndex = 55;
            // 
            // cbo状态
            // 
            this.cbo状态.Location = new System.Drawing.Point(83, 182);
            this.cbo状态.Name = "cbo状态";
            this.cbo状态.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.cbo状态.Properties.Appearance.Options.UseFont = true;
            this.cbo状态.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo状态.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbo状态.Size = new System.Drawing.Size(142, 26);
            this.cbo状态.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(38, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "状态:";
            // 
            // cbo品牌
            // 
            this.cbo品牌.Location = new System.Drawing.Point(85, 25);
            this.cbo品牌.Name = "cbo品牌";
            this.cbo品牌.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.cbo品牌.Properties.Appearance.Options.UseFont = true;
            this.cbo品牌.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo品牌.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbo品牌.Size = new System.Drawing.Size(140, 26);
            this.cbo品牌.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(44, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "品牌:";
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = " ";
            this.gridColumn15.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn15.FieldName = "CheckBox";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 0;
            // 
            // bgw加载数据
            // 
            this.bgw加载数据.WorkerReportsProgress = true;
            this.bgw加载数据.WorkerSupportsCancellation = true;
            this.bgw加载数据.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw加载数据_DoWork);
            this.bgw加载数据.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgw加载数据_ProgressChanged);
            this.bgw加载数据.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw加载数据_RunWorkerCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 19);
            this.label1.TabIndex = 74;
            this.label1.Text = "厂家账户:";
            // 
            // searchControl1
            // 
            this.searchControl1.Location = new System.Drawing.Point(84, 80);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.searchControl1.Properties.Appearance.Options.UseFont = true;
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Properties.NullValuePrompt = " ";
            this.searchControl1.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.searchControl1_Properties_ButtonClick);
            this.searchControl1.Size = new System.Drawing.Size(141, 26);
            this.searchControl1.TabIndex = 75;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(14, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 19);
            this.label7.TabIndex = 76;
            this.label7.Text = "开始日期:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(268, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 19);
            this.label8.TabIndex = 77;
            this.label8.Text = "结束日期:";
            // 
            // FrmOrderQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(517, 351);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt日期结束);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt日期开始);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchControl1);
            this.Controls.Add(this.txt采购单号);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbo品牌);
            this.Controls.Add(this.txt重置);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbo状态);
            this.Controls.Add(this.btn查询);
            this.Controls.Add(this.chkClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "FrmOrderQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " 采购订单过滤";
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt日期结束.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt日期结束.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt日期开始.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt日期开始.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt采购单号.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkClose.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo状态.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo品牌.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.ComboBoxEdit cbo状态;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.ComboBoxEdit cbo品牌;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private System.Windows.Forms.ImageList imageList2;
        private DevExpress.XtraEditors.SimpleButton txt重置;
        private DevExpress.XtraEditors.SimpleButton btn查询;
        private System.ComponentModel.BackgroundWorker bgw加载数据;
        private DevExpress.XtraEditors.CheckEdit chkClose;
        private DevExpress.XtraEditors.TextEdit txt采购单号;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.DateEdit txt日期结束;
        private DevExpress.XtraEditors.DateEdit txt日期开始;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
    }
}