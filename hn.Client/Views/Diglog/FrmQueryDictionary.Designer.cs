namespace hn.Client
{
    partial class FrmQueryDictionary
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQueryDictionary));
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView名称代码 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn编号 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn名称 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnl跑龙套2 = new DevExpress.XtraEditors.PanelControl();
            this.btn重置 = new DevExpress.XtraEditors.SimpleButton();
            this.btn查询 = new DevExpress.XtraEditors.SimpleButton();
            this.txt关键字 = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigator总记录数 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigator每页多少记录 = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigator状态 = new System.Windows.Forms.ToolStripLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView名称代码)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl跑龙套2)).BeginInit();
            this.pnl跑龙套2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt关键字.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gridControl);
            this.panel1.Controls.Add(this.pnl跑龙套2);
            this.panel1.Controls.Add(this.bindingNavigator1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1109, 650);
            this.panel1.TabIndex = 2;
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 48);
            this.gridControl.LookAndFeel.SkinName = "Office 2010 Silver";
            this.gridControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl.MainView = this.gridView名称代码;
            this.gridControl.Margin = new System.Windows.Forms.Padding(0);
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(1109, 602);
            this.gridControl.TabIndex = 10;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView名称代码});
            this.gridControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridControl_KeyDown);
            // 
            // gridView名称代码
            // 
            this.gridView名称代码.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(158)))), ((int)(((byte)(216)))));
            this.gridView名称代码.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gridView名称代码.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView名称代码.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridView名称代码.Appearance.FooterPanel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.gridView名称代码.Appearance.FooterPanel.Options.UseFont = true;
            this.gridView名称代码.Appearance.GroupButton.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.gridView名称代码.Appearance.GroupButton.Options.UseFont = true;
            this.gridView名称代码.Appearance.GroupFooter.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.gridView名称代码.Appearance.GroupFooter.Options.UseFont = true;
            this.gridView名称代码.Appearance.GroupPanel.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.gridView名称代码.Appearance.GroupPanel.Options.UseFont = true;
            this.gridView名称代码.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.gridView名称代码.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView名称代码.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView名称代码.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView名称代码.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView名称代码.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.gridView名称代码.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gridView名称代码.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.gridView名称代码.Appearance.Row.Options.UseFont = true;
            this.gridView名称代码.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(158)))), ((int)(((byte)(216)))));
            this.gridView名称代码.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridView名称代码.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn编号,
            this.gridColumn名称,
            this.gridColumn6});
            styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Red;
            styleFormatCondition1.Appearance.Options.UseForeColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Value1 = "离线";
            this.gridView名称代码.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this.gridView名称代码.GridControl = this.gridControl;
            this.gridView名称代码.IndicatorWidth = 60;
            this.gridView名称代码.Name = "gridView名称代码";
            this.gridView名称代码.OptionsBehavior.Editable = false;
            this.gridView名称代码.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView名称代码.OptionsMenu.EnableFooterMenu = false;
            this.gridView名称代码.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView名称代码.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView名称代码.OptionsView.ColumnAutoWidth = false;
            this.gridView名称代码.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView名称代码.OptionsView.ShowGroupPanel = false;
            this.gridView名称代码.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView名称代码_CustomDrawRowIndicator);
            this.gridView名称代码.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridView名称代码_MouseDown);
            // 
            // gridColumn编号
            // 
            this.gridColumn编号.Caption = "名称";
            this.gridColumn编号.FieldName = "FNAME";
            this.gridColumn编号.Name = "gridColumn编号";
            this.gridColumn编号.Visible = true;
            this.gridColumn编号.VisibleIndex = 0;
            this.gridColumn编号.Width = 249;
            // 
            // gridColumn名称
            // 
            this.gridColumn名称.Caption = "编码";
            this.gridColumn名称.FieldName = "FNUMBER";
            this.gridColumn名称.Name = "gridColumn名称";
            this.gridColumn名称.Visible = true;
            this.gridColumn名称.VisibleIndex = 1;
            this.gridColumn名称.Width = 132;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "备注";
            this.gridColumn6.FieldName = "FREMARK";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 619;
            // 
            // pnl跑龙套2
            // 
            this.pnl跑龙套2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnl跑龙套2.Appearance.Options.UseBackColor = true;
            this.pnl跑龙套2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl跑龙套2.Controls.Add(this.btn重置);
            this.pnl跑龙套2.Controls.Add(this.btn查询);
            this.pnl跑龙套2.Controls.Add(this.txt关键字);
            this.pnl跑龙套2.Controls.Add(this.label1);
            this.pnl跑龙套2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl跑龙套2.Location = new System.Drawing.Point(0, 0);
            this.pnl跑龙套2.Name = "pnl跑龙套2";
            this.pnl跑龙套2.Size = new System.Drawing.Size(1109, 48);
            this.pnl跑龙套2.TabIndex = 7;
            // 
            // btn重置
            // 
            this.btn重置.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn重置.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn重置.Appearance.Options.UseFont = true;
            this.btn重置.Appearance.Options.UseForeColor = true;
            this.btn重置.AppearanceDisabled.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.btn重置.AppearanceDisabled.Options.UseFont = true;
            this.btn重置.AppearanceHovered.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.btn重置.AppearanceHovered.Options.UseFont = true;
            this.btn重置.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn重置.Location = new System.Drawing.Point(344, 11);
            this.btn重置.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.btn重置.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn重置.Name = "btn重置";
            this.btn重置.Size = new System.Drawing.Size(71, 27);
            this.btn重置.TabIndex = 54;
            this.btn重置.Text = "重置";
            this.btn重置.Click += new System.EventHandler(this.btn重置_Click);
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
            this.btn查询.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn查询.Location = new System.Drawing.Point(267, 11);
            this.btn查询.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.btn查询.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn查询.Name = "btn查询";
            this.btn查询.Size = new System.Drawing.Size(71, 27);
            this.btn查询.TabIndex = 53;
            this.btn查询.Text = "查询";
            this.btn查询.Click += new System.EventHandler(this.btn查询_Click);
            // 
            // txt关键字
            // 
            this.txt关键字.Location = new System.Drawing.Point(51, 11);
            this.txt关键字.Name = "txt关键字";
            this.txt关键字.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txt关键字.Properties.Appearance.Options.UseFont = true;
            this.txt关键字.Size = new System.Drawing.Size(193, 28);
            this.txt关键字.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "查询:";
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bindingNavigator1.AutoSize = false;
            this.bindingNavigator1.BackColor = System.Drawing.Color.Transparent;
            this.bindingNavigator1.CountItem = null;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.None;
            this.bindingNavigator1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigator总记录数,
            this.toolStripLabel1,
            this.bindingNavigator每页多少记录,
            this.bindingNavigator状态});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 625);
            this.bindingNavigator1.MoveFirstItem = null;
            this.bindingNavigator1.MoveLastItem = null;
            this.bindingNavigator1.MoveNextItem = null;
            this.bindingNavigator1.MovePreviousItem = null;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = null;
            this.bindingNavigator1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.bindingNavigator1.Size = new System.Drawing.Size(1109, 25);
            this.bindingNavigator1.TabIndex = 9;
            this.bindingNavigator1.Text = "bindingNavigator1";
            this.bindingNavigator1.Visible = false;
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(24, 22);
            this.bindingNavigatorMoveFirstItem.Text = "首页";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(24, 22);
            this.bindingNavigatorMovePreviousItem.Text = "上一页";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "位置";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "当前页";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(38, 22);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "总页数";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(24, 22);
            this.bindingNavigatorMoveNextItem.Text = "下一页";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(24, 22);
            this.bindingNavigatorMoveLastItem.Text = "尾页";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigator总记录数
            // 
            this.bindingNavigator总记录数.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bindingNavigator总记录数.Enabled = false;
            this.bindingNavigator总记录数.Name = "bindingNavigator总记录数";
            this.bindingNavigator总记录数.RightToLeftAutoMirrorImage = true;
            this.bindingNavigator总记录数.Size = new System.Drawing.Size(90, 22);
            this.bindingNavigator总记录数.Text = "共 0 条记录";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigator每页多少记录
            // 
            this.bindingNavigator每页多少记录.Enabled = false;
            this.bindingNavigator每页多少记录.Name = "bindingNavigator每页多少记录";
            this.bindingNavigator每页多少记录.Size = new System.Drawing.Size(101, 22);
            this.bindingNavigator每页多少记录.Text = "每页 0 条记录";
            // 
            // bindingNavigator状态
            // 
            this.bindingNavigator状态.Enabled = false;
            this.bindingNavigator状态.Name = "bindingNavigator状态";
            this.bindingNavigator状态.Size = new System.Drawing.Size(0, 22);
            // 
            // FrmQueryDictionary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 650);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmQueryDictionary";
            this.ShowInTaskbar = false;
            this.Text = "选择";
            this.Load += new System.EventHandler(this.FrmQueryMarketArea_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmQueryDictionary_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView名称代码)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl跑龙套2)).EndInit();
            this.pnl跑龙套2.ResumeLayout(false);
            this.pnl跑龙套2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt关键字.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PanelControl pnl跑龙套2;
        private DevExpress.XtraEditors.SimpleButton btn重置;
        private DevExpress.XtraEditors.SimpleButton btn查询;
        private DevExpress.XtraEditors.TextEdit txt关键字;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView名称代码;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn编号;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn名称;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton bindingNavigator总记录数;
        private System.Windows.Forms.ToolStripSeparator toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel bindingNavigator每页多少记录;
        private System.Windows.Forms.ToolStripLabel bindingNavigator状态;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
    }
}