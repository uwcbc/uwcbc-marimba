namespace Marimba.Forms
{
    partial class ResettedPasswordsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResettedPasswordsForm));
            this.tlpResettedPasswords = new System.Windows.Forms.TableLayoutPanel();
            this.btnExport = new System.Windows.Forms.Button();
            this.lvResettedPasswords = new System.Windows.Forms.ListView();
            this.userHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.passwordHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlpResettedPasswords.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpResettedPasswords
            // 
            this.tlpResettedPasswords.ColumnCount = 2;
            this.tlpResettedPasswords.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpResettedPasswords.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpResettedPasswords.Controls.Add(this.btnExport, 1, 0);
            this.tlpResettedPasswords.Controls.Add(this.lvResettedPasswords, 0, 1);
            this.tlpResettedPasswords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpResettedPasswords.Location = new System.Drawing.Point(0, 0);
            this.tlpResettedPasswords.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tlpResettedPasswords.Name = "tlpResettedPasswords";
            this.tlpResettedPasswords.RowCount = 2;
            this.tlpResettedPasswords.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tlpResettedPasswords.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpResettedPasswords.Size = new System.Drawing.Size(437, 379);
            this.tlpResettedPasswords.TabIndex = 0;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.18868F);
            this.btnExport.Location = new System.Drawing.Point(265, 9);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(125, 34);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export to CSV";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lvResettedPasswords
            // 
            this.lvResettedPasswords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.userHeader,
            this.passwordHeader});
            this.tlpResettedPasswords.SetColumnSpan(this.lvResettedPasswords, 2);
            this.lvResettedPasswords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvResettedPasswords.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.18868F);
            this.lvResettedPasswords.HideSelection = false;
            this.lvResettedPasswords.Location = new System.Drawing.Point(2, 54);
            this.lvResettedPasswords.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lvResettedPasswords.Name = "lvResettedPasswords";
            this.lvResettedPasswords.Size = new System.Drawing.Size(433, 323);
            this.lvResettedPasswords.TabIndex = 1;
            this.lvResettedPasswords.UseCompatibleStateImageBehavior = false;
            this.lvResettedPasswords.View = System.Windows.Forms.View.Details;
            // 
            // userHeader
            // 
            this.userHeader.Text = "User";
            this.userHeader.Width = 330;
            // 
            // passwordHeader
            // 
            this.passwordHeader.Text = "Password";
            this.passwordHeader.Width = 535;
            // 
            // ResettedPasswordsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(437, 379);
            this.Controls.Add(this.tlpResettedPasswords);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ResettedPasswordsForm";
            this.Text = "Resetted Passwords";
            this.tlpResettedPasswords.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpResettedPasswords;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ListView lvResettedPasswords;
        private System.Windows.Forms.ColumnHeader userHeader;
        private System.Windows.Forms.ColumnHeader passwordHeader;
    }
}