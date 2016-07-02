namespace Marimba.Forms
{
    partial class editSettings
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
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpPref = new System.Windows.Forms.TabPage();
            this.lblEmailPassword = new System.Windows.Forms.Label();
            this.txtEmailPassword = new System.Windows.Forms.TextBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cbAttachSignature = new System.Windows.Forms.CheckBox();
            this.cbAutoSave = new System.Windows.Forms.CheckBox();
            this.cbCurTerm = new System.Windows.Forms.CheckBox();
            this.cbSound = new System.Windows.Forms.CheckBox();
            this.tpAdmin = new System.Windows.Forms.TabPage();
            this.gbEncryption = new System.Windows.Forms.GroupBox();
            this.btnChangeKey = new System.Windows.Forms.Button();
            this.gbEmail = new System.Windows.Forms.GroupBox();
            this.cbSMTP = new System.Windows.Forms.CheckBox();
            this.txtSMTPPort = new System.Windows.Forms.TextBox();
            this.lblSMTPPort = new System.Windows.Forms.Label();
            this.txtSMTP = new System.Windows.Forms.TextBox();
            this.lblSMTP = new System.Windows.Forms.Label();
            this.cbSSL = new System.Windows.Forms.CheckBox();
            this.txtImap = new System.Windows.Forms.TextBox();
            this.lblImap = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tcMain.SuspendLayout();
            this.tpPref.SuspendLayout();
            this.tpAdmin.SuspendLayout();
            this.gbEncryption.SuspendLayout();
            this.gbEmail.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMain.Controls.Add(this.tpPref);
            this.tcMain.Controls.Add(this.tpAdmin);
            this.tcMain.Font = new System.Drawing.Font("Quicksand", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(664, 507);
            this.tcMain.TabIndex = 0;
            // 
            // tpPref
            // 
            this.tpPref.Controls.Add(this.lblEmailPassword);
            this.tpPref.Controls.Add(this.txtEmailPassword);
            this.tpPref.Controls.Add(this.lblPosition);
            this.tpPref.Controls.Add(this.lblName);
            this.tpPref.Controls.Add(this.txtPosition);
            this.tpPref.Controls.Add(this.txtName);
            this.tpPref.Controls.Add(this.cbAttachSignature);
            this.tpPref.Controls.Add(this.cbAutoSave);
            this.tpPref.Controls.Add(this.cbCurTerm);
            this.tpPref.Controls.Add(this.cbSound);
            this.tpPref.Location = new System.Drawing.Point(4, 28);
            this.tpPref.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpPref.Name = "tpPref";
            this.tpPref.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpPref.Size = new System.Drawing.Size(656, 475);
            this.tpPref.TabIndex = 0;
            this.tpPref.Text = "Preferences";
            this.tpPref.UseVisualStyleBackColor = true;
            // 
            // lblEmailPassword
            // 
            this.lblEmailPassword.AutoSize = true;
            this.lblEmailPassword.Location = new System.Drawing.Point(40, 359);
            this.lblEmailPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmailPassword.Name = "lblEmailPassword";
            this.lblEmailPassword.Size = new System.Drawing.Size(152, 20);
            this.lblEmailPassword.TabIndex = 9;
            this.lblEmailPassword.Text = "E-mail Password:";
            // 
            // txtEmailPassword
            // 
            this.txtEmailPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmailPassword.Location = new System.Drawing.Point(223, 356);
            this.txtEmailPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEmailPassword.Name = "txtEmailPassword";
            this.txtEmailPassword.PasswordChar = '♪';
            this.txtEmailPassword.Size = new System.Drawing.Size(331, 26);
            this.txtEmailPassword.TabIndex = 8;
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(40, 302);
            this.lblPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(80, 20);
            this.lblPosition.TabIndex = 7;
            this.lblPosition.Text = "Position:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(40, 251);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(63, 20);
            this.lblName.TabIndex = 6;
            this.lblName.Text = "Name:";
            // 
            // txtPosition
            // 
            this.txtPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPosition.Location = new System.Drawing.Point(173, 298);
            this.txtPosition.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Size = new System.Drawing.Size(381, 26);
            this.txtPosition.TabIndex = 5;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(173, 247);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(381, 26);
            this.txtName.TabIndex = 4;
            // 
            // cbAttachSignature
            // 
            this.cbAttachSignature.AutoSize = true;
            this.cbAttachSignature.Location = new System.Drawing.Point(44, 201);
            this.cbAttachSignature.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbAttachSignature.Name = "cbAttachSignature";
            this.cbAttachSignature.Size = new System.Drawing.Size(357, 24);
            this.cbAttachSignature.TabIndex = 3;
            this.cbAttachSignature.Text = "Attach signature when sending e-mails";
            this.cbAttachSignature.UseVisualStyleBackColor = true;
            this.cbAttachSignature.CheckedChanged += new System.EventHandler(this.cbAttachSignature_CheckedChanged);
            // 
            // cbAutoSave
            // 
            this.cbAutoSave.AutoSize = true;
            this.cbAutoSave.Location = new System.Drawing.Point(44, 146);
            this.cbAutoSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbAutoSave.Name = "cbAutoSave";
            this.cbAutoSave.Size = new System.Drawing.Size(442, 24);
            this.cbAutoSave.TabIndex = 2;
            this.cbAutoSave.Text = "Automatically save file after any change is made";
            this.cbAutoSave.UseVisualStyleBackColor = true;
            // 
            // cbCurTerm
            // 
            this.cbCurTerm.AutoSize = true;
            this.cbCurTerm.Location = new System.Drawing.Point(44, 90);
            this.cbCurTerm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbCurTerm.Name = "cbCurTerm";
            this.cbCurTerm.Size = new System.Drawing.Size(316, 24);
            this.cbCurTerm.TabIndex = 1;
            this.cbCurTerm.Text = "Select current term automatically";
            this.cbCurTerm.UseVisualStyleBackColor = true;
            // 
            // cbSound
            // 
            this.cbSound.AutoSize = true;
            this.cbSound.Location = new System.Drawing.Point(44, 36);
            this.cbSound.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbSound.Name = "cbSound";
            this.cbSound.Size = new System.Drawing.Size(142, 24);
            this.cbSound.TabIndex = 0;
            this.cbSound.Text = "Enable sound";
            this.cbSound.UseVisualStyleBackColor = true;
            // 
            // tpAdmin
            // 
            this.tpAdmin.Controls.Add(this.gbEncryption);
            this.tpAdmin.Controls.Add(this.gbEmail);
            this.tpAdmin.Location = new System.Drawing.Point(4, 28);
            this.tpAdmin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpAdmin.Name = "tpAdmin";
            this.tpAdmin.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpAdmin.Size = new System.Drawing.Size(656, 475);
            this.tpAdmin.TabIndex = 1;
            this.tpAdmin.Text = "Admin";
            this.tpAdmin.UseVisualStyleBackColor = true;
            // 
            // gbEncryption
            // 
            this.gbEncryption.Controls.Add(this.btnChangeKey);
            this.gbEncryption.Location = new System.Drawing.Point(11, 313);
            this.gbEncryption.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbEncryption.Name = "gbEncryption";
            this.gbEncryption.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbEncryption.Size = new System.Drawing.Size(643, 123);
            this.gbEncryption.TabIndex = 1;
            this.gbEncryption.TabStop = false;
            this.gbEncryption.Text = "Encryption";
            // 
            // btnChangeKey
            // 
            this.btnChangeKey.Font = new System.Drawing.Font("Quicksand", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeKey.Location = new System.Drawing.Point(233, 53);
            this.btnChangeKey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnChangeKey.Name = "btnChangeKey";
            this.btnChangeKey.Size = new System.Drawing.Size(177, 28);
            this.btnChangeKey.TabIndex = 3;
            this.btnChangeKey.Text = "Change Key";
            this.btnChangeKey.UseVisualStyleBackColor = true;
            this.btnChangeKey.Click += new System.EventHandler(this.btnChangeKey_Click);
            // 
            // gbEmail
            // 
            this.gbEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEmail.Controls.Add(this.cbSMTP);
            this.gbEmail.Controls.Add(this.txtSMTPPort);
            this.gbEmail.Controls.Add(this.lblSMTPPort);
            this.gbEmail.Controls.Add(this.txtSMTP);
            this.gbEmail.Controls.Add(this.lblSMTP);
            this.gbEmail.Controls.Add(this.cbSSL);
            this.gbEmail.Controls.Add(this.txtImap);
            this.gbEmail.Controls.Add(this.lblImap);
            this.gbEmail.Controls.Add(this.lblEmail);
            this.gbEmail.Controls.Add(this.txtEmail);
            this.gbEmail.Location = new System.Drawing.Point(11, 7);
            this.gbEmail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbEmail.Name = "gbEmail";
            this.gbEmail.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbEmail.Size = new System.Drawing.Size(636, 297);
            this.gbEmail.TabIndex = 0;
            this.gbEmail.TabStop = false;
            this.gbEmail.Text = "Email";
            // 
            // cbSMTP
            // 
            this.cbSMTP.AutoSize = true;
            this.cbSMTP.Checked = true;
            this.cbSMTP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSMTP.Location = new System.Drawing.Point(8, 260);
            this.cbSMTP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbSMTP.Name = "cbSMTP";
            this.cbSMTP.Size = new System.Drawing.Size(223, 24);
            this.cbSMTP.TabIndex = 16;
            this.cbSMTP.Text = "SSL Required for SMTP";
            this.cbSMTP.UseVisualStyleBackColor = true;
            // 
            // txtSMTPPort
            // 
            this.txtSMTPPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSMTPPort.Location = new System.Drawing.Point(195, 213);
            this.txtSMTPPort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSMTPPort.Name = "txtSMTPPort";
            this.txtSMTPPort.Size = new System.Drawing.Size(403, 26);
            this.txtSMTPPort.TabIndex = 15;
            // 
            // lblSMTPPort
            // 
            this.lblSMTPPort.AutoSize = true;
            this.lblSMTPPort.Location = new System.Drawing.Point(8, 217);
            this.lblSMTPPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSMTPPort.Name = "lblSMTPPort";
            this.lblSMTPPort.Size = new System.Drawing.Size(99, 20);
            this.lblSMTPPort.TabIndex = 14;
            this.lblSMTPPort.Text = "SMTP Port:";
            // 
            // txtSMTP
            // 
            this.txtSMTP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSMTP.Location = new System.Drawing.Point(195, 165);
            this.txtSMTP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSMTP.Name = "txtSMTP";
            this.txtSMTP.Size = new System.Drawing.Size(403, 26);
            this.txtSMTP.TabIndex = 13;
            // 
            // lblSMTP
            // 
            this.lblSMTP.AutoSize = true;
            this.lblSMTP.Location = new System.Drawing.Point(8, 169);
            this.lblSMTP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSMTP.Name = "lblSMTP";
            this.lblSMTP.Size = new System.Drawing.Size(119, 20);
            this.lblSMTP.TabIndex = 12;
            this.lblSMTP.Text = "SMTP Server:";
            // 
            // cbSSL
            // 
            this.cbSSL.AutoSize = true;
            this.cbSSL.Checked = true;
            this.cbSSL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSSL.Location = new System.Drawing.Point(12, 123);
            this.cbSSL.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbSSL.Name = "cbSSL";
            this.cbSSL.Size = new System.Drawing.Size(217, 24);
            this.cbSSL.TabIndex = 11;
            this.cbSSL.Text = "SSL Required for IMAP";
            this.cbSSL.UseVisualStyleBackColor = true;
            // 
            // txtImap
            // 
            this.txtImap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImap.Location = new System.Drawing.Point(195, 71);
            this.txtImap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtImap.Name = "txtImap";
            this.txtImap.Size = new System.Drawing.Size(403, 26);
            this.txtImap.TabIndex = 10;
            // 
            // lblImap
            // 
            this.lblImap.AutoSize = true;
            this.lblImap.Location = new System.Drawing.Point(8, 75);
            this.lblImap.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImap.Name = "lblImap";
            this.lblImap.Size = new System.Drawing.Size(113, 20);
            this.lblImap.TabIndex = 9;
            this.lblImap.Text = "IMAP Server:";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(8, 30);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(135, 20);
            this.lblEmail.TabIndex = 8;
            this.lblEmail.Text = "E-Mail Address:";
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.Location = new System.Drawing.Point(195, 26);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(403, 26);
            this.txtEmail.TabIndex = 7;
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Quicksand", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(177, 539);
            this.btnOk.Margin = new System.Windows.Forms.Padding(100, 4, 4, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 28);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Quicksand", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(390, 539);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 100, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // editSettings
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(667, 603);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Marimba.Properties.Resources.Marimba_logo;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "editSettings";
            this.Text = "Marimba";
            this.Load += new System.EventHandler(this.editSettings_Load);
            this.tcMain.ResumeLayout(false);
            this.tpPref.ResumeLayout(false);
            this.tpPref.PerformLayout();
            this.tpAdmin.ResumeLayout(false);
            this.gbEncryption.ResumeLayout(false);
            this.gbEmail.ResumeLayout(false);
            this.gbEmail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpPref;
        private System.Windows.Forms.CheckBox cbSound;
        private System.Windows.Forms.TabPage tpAdmin;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbCurTerm;
        private System.Windows.Forms.CheckBox cbAutoSave;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckBox cbAttachSignature;
        private System.Windows.Forms.Label lblEmailPassword;
        private System.Windows.Forms.TextBox txtEmailPassword;
        private System.Windows.Forms.GroupBox gbEmail;
        private System.Windows.Forms.TextBox txtImap;
        private System.Windows.Forms.Label lblImap;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.CheckBox cbSSL;
        private System.Windows.Forms.CheckBox cbSMTP;
        private System.Windows.Forms.TextBox txtSMTPPort;
        private System.Windows.Forms.Label lblSMTPPort;
        private System.Windows.Forms.TextBox txtSMTP;
        private System.Windows.Forms.Label lblSMTP;
        private System.Windows.Forms.GroupBox gbEncryption;
        private System.Windows.Forms.Button btnChangeKey;
    }
}