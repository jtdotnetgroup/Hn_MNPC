namespace hn.Client
{
    partial class frm库存查询
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm库存查询));
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.panel右 = new System.Windows.Forms.Panel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnl跑龙套2 = new DevExpress.XtraEditors.PanelControl();
            this.label3 = new System.Windows.Forms.Label();
            this.numPerPage = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numPageIndex = new System.Windows.Forms.NumericUpDown();
            this.labInfo = new System.Windows.Forms.Label();
            this.tGGXH = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.tColorNo = new DevExpress.XtraEditors.TextEdit();
            this.txt重置 = new DevExpress.XtraEditors.SimpleButton();
            this.btn查询 = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo品牌 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bgw加载数据 = new System.ComponentModel.BackgroundWorker();
            this.label5 = new System.Windows.Forms.Label();
            this.comGG = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.panel右.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl跑龙套2)).BeginInit();
            this.pnl跑龙套2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPerPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPageIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tGGXH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tColorNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo品牌.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comGG.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "等级";
            this.gridColumn6.FieldName = "cpdj";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            // 
            // repositoryItemCheckEdit1
            // 
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
            // panel右
            // 
            this.panel右.Controls.Add(this.gridControl1);
            this.panel右.Controls.Add(this.pnl跑龙套2);
            this.panel右.Controls.Add(this.cbo品牌);
            this.panel右.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel右.Location = new System.Drawing.Point(0, 0);
            this.panel右.Name = "panel右";
            this.panel右.Size = new System.Drawing.Size(1034, 600);
            this.panel右.TabIndex = 4;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Location = new System.Drawing.Point(0, 36);
            this.gridControl1.LookAndFeel.SkinName = "Office 2010 Silver";
            this.gridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(0);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1034, 564);
            this.gridControl1.TabIndex = 47;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FixedLine.BackColor = System.Drawing.Color.Red;
            this.gridView1.Appearance.FixedLine.Options.UseBackColor = true;
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(158)))), ((int)(((byte)(216)))));
            this.gridView1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridView1.Appearance.FooterPanel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.gridView1.Appearance.FooterPanel.Options.UseFont = true;
            this.gridView1.Appearance.GroupButton.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.gridView1.Appearance.GroupButton.Options.UseFont = true;
            this.gridView1.Appearance.GroupFooter.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.gridView1.Appearance.GroupFooter.Options.UseFont = true;
            this.gridView1.Appearance.GroupPanel.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.gridView1.Appearance.GroupPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView1.Appearance.HideSelectionRow.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gridView1.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.gridView1.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gridView1.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn6,
            this.gridColumn11,
            this.gridColumn13,
            this.gridColumn5,
            this.gridColumn7,
            this.gridColumn8});
            styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Red;
            styleFormatCondition1.Appearance.Options.UseForeColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Column = this.gridColumn6;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Value1 = "离线";
            this.gridView1.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 60;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator_1);
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "厂家代码/品种";
            this.gridColumn2.FieldName = "cppz";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 125;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "厂家名称/型号";
            this.gridColumn3.FieldName = "cpgg";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 97;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "厂家规格";
            this.gridColumn4.FieldName = "cpxh";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 100;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.Caption = "仓库";
            this.gridColumn11.FieldName = "cpcm";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 4;
            this.gridColumn11.Width = 125;
            // 
            // gridColumn13
            // 
            this.gridColumn13.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn13.Caption = "色号";
            this.gridColumn13.FieldName = "cpsh";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 5;
            this.gridColumn13.Width = 81;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "批号";
            this.gridColumn5.FieldName = "package";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 6;
            this.gridColumn5.Width = 87;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "单位";
            this.gridColumn7.FieldName = "dw";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 57;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "数量";
            this.gridColumn8.FieldName = "bysl";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            // 
            // pnl跑龙套2
            // 
            this.pnl跑龙套2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnl跑龙套2.Appearance.Options.UseBackColor = true;
            this.pnl跑龙套2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl跑龙套2.Controls.Add(this.comGG);
            this.pnl跑龙套2.Controls.Add(this.label5);
            this.pnl跑龙套2.Controls.Add(this.label3);
            this.pnl跑龙套2.Controls.Add(this.numPerPage);
            this.pnl跑龙套2.Controls.Add(this.label1);
            this.pnl跑龙套2.Controls.Add(this.numPageIndex);
            this.pnl跑龙套2.Controls.Add(this.labInfo);
            this.pnl跑龙套2.Controls.Add(this.tGGXH);
            this.pnl跑龙套2.Controls.Add(this.label4);
            this.pnl跑龙套2.Controls.Add(this.tColorNo);
            this.pnl跑龙套2.Controls.Add(this.txt重置);
            this.pnl跑龙套2.Controls.Add(this.btn查询);
            this.pnl跑龙套2.Controls.Add(this.label2);
            this.pnl跑龙套2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl跑龙套2.Location = new System.Drawing.Point(0, 0);
            this.pnl跑龙套2.Name = "pnl跑龙套2";
            this.pnl跑龙套2.Size = new System.Drawing.Size(1034, 36);
            this.pnl跑龙套2.TabIndex = 1;
            this.pnl跑龙套2.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl跑龙套2_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(479, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 19);
            this.label3.TabIndex = 67;
            this.label3.Text = "每页记录数:";
            // 
            // numPerPage
            // 
            this.numPerPage.Location = new System.Drawing.Point(562, 8);
            this.numPerPage.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numPerPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPerPage.Name = "numPerPage";
            this.numPerPage.Size = new System.Drawing.Size(48, 22);
            this.numPerPage.TabIndex = 66;
            this.numPerPage.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(386, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 19);
            this.label1.TabIndex = 65;
            this.label1.Text = "页码:";
            // 
            // numPageIndex
            // 
            this.numPageIndex.Location = new System.Drawing.Point(428, 8);
            this.numPageIndex.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numPageIndex.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPageIndex.Name = "numPageIndex";
            this.numPageIndex.Size = new System.Drawing.Size(45, 22);
            this.numPageIndex.TabIndex = 64;
            this.numPageIndex.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labInfo
            // 
            this.labInfo.AutoSize = true;
            this.labInfo.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labInfo.Location = new System.Drawing.Point(788, 10);
            this.labInfo.Name = "labInfo";
            this.labInfo.Size = new System.Drawing.Size(35, 19);
            this.labInfo.TabIndex = 63;
            this.labInfo.Text = "提示";
            // 
            // tGGXH
            // 
            this.tGGXH.EditValue = "";
            this.tGGXH.Location = new System.Drawing.Point(173, 5);
            this.tGGXH.Name = "tGGXH";
            this.tGGXH.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.tGGXH.Properties.Appearance.Options.UseFont = true;
            this.tGGXH.Size = new System.Drawing.Size(79, 26);
            this.tGGXH.TabIndex = 62;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(130, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 19);
            this.label4.TabIndex = 61;
            this.label4.Text = "规格:";
            // 
            // tColorNo
            // 
            this.tColorNo.EditValue = "";
            this.tColorNo.Location = new System.Drawing.Point(297, 5);
            this.tColorNo.Name = "tColorNo";
            this.tColorNo.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.tColorNo.Properties.Appearance.Options.UseFont = true;
            this.tColorNo.Size = new System.Drawing.Size(88, 26);
            this.tColorNo.TabIndex = 56;
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
            this.txt重置.Location = new System.Drawing.Point(697, 4);
            this.txt重置.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.txt重置.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txt重置.Name = "txt重置";
            this.txt重置.Size = new System.Drawing.Size(71, 27);
            this.txt重置.TabIndex = 54;
            this.txt重置.Text = "重置(F6)";
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
            this.btn查询.Location = new System.Drawing.Point(620, 4);
            this.btn查询.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.btn查询.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn查询.Name = "btn查询";
            this.btn查询.Size = new System.Drawing.Size(71, 27);
            this.btn查询.TabIndex = 53;
            this.btn查询.Text = "查询(F5)";
            this.btn查询.Click += new System.EventHandler(this.btn查询_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(254, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "型号:";
            // 
            // cbo品牌
            // 
            this.cbo品牌.Location = new System.Drawing.Point(214, 156);
            this.cbo品牌.Name = "cbo品牌";
            this.cbo品牌.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.cbo品牌.Properties.Appearance.Options.UseFont = true;
            this.cbo品牌.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo品牌.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbo品牌.Size = new System.Drawing.Size(100, 26);
            this.cbo品牌.TabIndex = 58;
            this.cbo品牌.Visible = false;
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
            this.bgw加载数据.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw加载数据_RunWorkerCompleted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 19);
            this.label5.TabIndex = 68;
            this.label5.Text = "选择:";
            // 
            // comGG
            // 
            this.comGG.Location = new System.Drawing.Point(46, 4);
            this.comGG.Name = "comGG";
            this.comGG.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.comGG.Properties.Appearance.Options.UseFont = true;
            this.comGG.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comGG.Properties.Items.AddRange(new object[] {
            "全部",
            "直发仓库",
            "直发工地"});
            this.comGG.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comGG.Size = new System.Drawing.Size(79, 26);
            this.comGG.TabIndex = 143;
            this.comGG.SelectedIndexChanged += new System.EventHandler(this.comGG_SelectedIndexChanged);
            // 
            // frm库存查询
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1034, 600);
            this.Controls.Add(this.panel右);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frm库存查询";
            this.Text = "库存查询";
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.panel右.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl跑龙套2)).EndInit();
            this.pnl跑龙套2.ResumeLayout(false);
            this.pnl跑龙套2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPerPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPageIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tGGXH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tColorNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo品牌.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comGG.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel右;
        private DevExpress.XtraEditors.PanelControl pnl跑龙套2;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private System.Windows.Forms.ImageList imageList2;
        private DevExpress.XtraEditors.SimpleButton txt重置;
        private DevExpress.XtraEditors.SimpleButton btn查询;
        private System.ComponentModel.BackgroundWorker bgw加载数据;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.TextEdit tColorNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.ComboBoxEdit cbo品牌;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.TextEdit tGGXH;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numPerPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numPageIndex;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.ComboBoxEdit comGG;
    }
}