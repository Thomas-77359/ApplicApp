namespace ApplicApp
{
    partial class frm_changeDossierState
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
            this.rb_Created = new System.Windows.Forms.RadioButton();
            this.rb_Open = new System.Windows.Forms.RadioButton();
            this.rb_Denied = new System.Windows.Forms.RadioButton();
            this.rb_Hired = new System.Windows.Forms.RadioButton();
            this.bt_saveNewState = new System.Windows.Forms.Button();
            this.bt_cancel = new System.Windows.Forms.Button();
            this.tb_ActiveDossier = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rb_Created
            // 
            this.rb_Created.AutoSize = true;
            this.rb_Created.Checked = true;
            this.rb_Created.Location = new System.Drawing.Point(12, 101);
            this.rb_Created.Name = "rb_Created";
            this.rb_Created.Size = new System.Drawing.Size(61, 17);
            this.rb_Created.TabIndex = 1;
            this.rb_Created.TabStop = true;
            this.rb_Created.Text = "Erzeugt";
            this.rb_Created.UseVisualStyleBackColor = true;
            // 
            // rb_Open
            // 
            this.rb_Open.AutoSize = true;
            this.rb_Open.Location = new System.Drawing.Point(152, 101);
            this.rb_Open.Name = "rb_Open";
            this.rb_Open.Size = new System.Drawing.Size(51, 17);
            this.rb_Open.TabIndex = 2;
            this.rb_Open.Text = "Offen";
            this.rb_Open.UseVisualStyleBackColor = true;
            // 
            // rb_Denied
            // 
            this.rb_Denied.AutoSize = true;
            this.rb_Denied.Location = new System.Drawing.Point(308, 101);
            this.rb_Denied.Name = "rb_Denied";
            this.rb_Denied.Size = new System.Drawing.Size(61, 17);
            this.rb_Denied.TabIndex = 4;
            this.rb_Denied.Text = "Absage";
            this.rb_Denied.UseVisualStyleBackColor = true;
            // 
            // rb_Hired
            // 
            this.rb_Hired.AutoSize = true;
            this.rb_Hired.Location = new System.Drawing.Point(458, 101);
            this.rb_Hired.Name = "rb_Hired";
            this.rb_Hired.Size = new System.Drawing.Size(90, 17);
            this.rb_Hired.TabIndex = 5;
            this.rb_Hired.Text = "Angenommen";
            this.rb_Hired.UseVisualStyleBackColor = true;
            // 
            // bt_saveNewState
            // 
            this.bt_saveNewState.Location = new System.Drawing.Point(91, 149);
            this.bt_saveNewState.Name = "bt_saveNewState";
            this.bt_saveNewState.Size = new System.Drawing.Size(121, 23);
            this.bt_saveNewState.TabIndex = 6;
            this.bt_saveNewState.Text = "Status speichern";
            this.bt_saveNewState.UseVisualStyleBackColor = true;
            this.bt_saveNewState.Click += new System.EventHandler(this.bt_saveNewState_Click);
            // 
            // bt_cancel
            // 
            this.bt_cancel.Location = new System.Drawing.Point(322, 149);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(121, 23);
            this.bt_cancel.TabIndex = 0;
            this.bt_cancel.Text = "Abbrechen";
            this.bt_cancel.UseVisualStyleBackColor = true;
            this.bt_cancel.Click += new System.EventHandler(this.bt_cancel_Click);
            // 
            // tb_ActiveDossier
            // 
            this.tb_ActiveDossier.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.tb_ActiveDossier.Location = new System.Drawing.Point(12, 36);
            this.tb_ActiveDossier.Name = "tb_ActiveDossier";
            this.tb_ActiveDossier.ReadOnly = true;
            this.tb_ActiveDossier.Size = new System.Drawing.Size(536, 20);
            this.tb_ActiveDossier.TabIndex = 99;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Aktives Dossier:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Wählen Sie den neuen Status:";
            // 
            // frm_changeDossierState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 184);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_ActiveDossier);
            this.Controls.Add(this.bt_cancel);
            this.Controls.Add(this.bt_saveNewState);
            this.Controls.Add(this.rb_Hired);
            this.Controls.Add(this.rb_Denied);
            this.Controls.Add(this.rb_Open);
            this.Controls.Add(this.rb_Created);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frm_changeDossierState";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Status des aktuellen Dossiers ändern";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rb_Created;
        private System.Windows.Forms.RadioButton rb_Open;
        private System.Windows.Forms.RadioButton rb_Denied;
        private System.Windows.Forms.RadioButton rb_Hired;
        private System.Windows.Forms.Button bt_saveNewState;
        private System.Windows.Forms.Button bt_cancel;
        private System.Windows.Forms.TextBox tb_ActiveDossier;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}