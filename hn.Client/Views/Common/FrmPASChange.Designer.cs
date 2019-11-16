namespace hn.Client
{
    partial class FrmPASChange
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
            this.txtOld = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNew1 = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNew2 = new DevExpress.XtraEditors.TextEdit();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.btnViewPAS = new DevExpress.XtraEditors.CheckButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtOld.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNew1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNew2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtOld
            // 
            this.txtOld.Location = new System.Drawing.Point(103, 59);
            this.txtOld.Name = "txtOld";
            this.txtOld.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOld.Properties.Appearance.Options.UseFont = true;
            this.txtOld.Properties.PasswordChar = '*';
            this.txtOld.Size = new System.Drawing.Size(195, 26);
            this.txtOld.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.label1.Location = new System.Drawing.Point(46, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 19);
            this.label1.TabIndex = 14;
            this.label1.Text = "旧密码:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.label2.Location = new System.Drawing.Point(46, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 19);
            this.label2.TabIndex = 15;
            this.label2.Text = "新密码:";
            // 
            // txtNew1
            // 
            this.txtNew1.Location = new System.Drawing.Point(103, 97);
            this.txtNew1.Name = "txtNew1";
            this.txtNew1.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNew1.Properties.Appearance.Options.UseFont = true;
            this.txtNew1.Properties.PasswordChar = '*';
            this.txtNew1.Size = new System.Drawing.Size(195, 26);
            this.txtNew1.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.label3.Location = new System.Drawing.Point(20, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 19);
            this.label3.TabIndex = 17;
            this.label3.Text = "确认新密码:";
            // 
            // txtNew2
            // 
            this.txtNew2.Location = new System.Drawing.Point(103, 135);
            this.txtNew2.Name = "txtNew2";
            this.txtNew2.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNew2.Properties.Appearance.Options.UseFont = true;
            this.txtNew2.Properties.PasswordChar = '*';
            this.txtNew2.Size = new System.Drawing.Size(195, 26);
            this.txtNew2.TabIndex = 18;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Appearance.Options.UseFont = true;
            this.btnConfirm.Location = new System.Drawing.Point(223, 181);
            this.btnConfirm.LookAndFeel.SkinName = "Office 2010 Silver";
            this.btnConfirm.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 31);
            this.btnConfirm.TabIndex = 20;
            this.btnConfirm.Text = "确认修改";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnViewPAS
            // 
            this.btnViewPAS.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewPAS.Appearance.Options.UseFont = true;
            this.btnViewPAS.Location = new System.Drawing.Point(103, 181);
            this.btnViewPAS.LookAndFeel.SkinName = "Office 2010 Silver";
            this.btnViewPAS.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnViewPAS.Name = "btnViewPAS";
            this.btnViewPAS.Size = new System.Drawing.Size(75, 31);
            this.btnViewPAS.TabIndex = 22;
            this.btnViewPAS.Text = "显示密码";
            this.btnViewPAS.CheckedChanged += new System.EventHandler(this.btnViewPAS_CheckedChanged);
            // 
            // FrmPASChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 246);
            this.Controls.Add(this.btnViewPAS);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtNew2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNew1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOld);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "FrmPASChange";
            this.ShowInTaskbar = false;
            this.Text = "修改密码";
            ((System.ComponentModel.ISupportInitialize)(this.txtOld.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNew1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNew2.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtOld;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtNew1;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtNew2;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.CheckButton btnViewPAS;
    }
}