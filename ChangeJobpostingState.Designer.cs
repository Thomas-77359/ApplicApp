namespace ApplicApp
{
    partial class frm_changeJobpostingState
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
            this.bt_saveNewState = new System.Windows.Forms.Button();
            this.bt_cancel = new System.Windows.Forms.Button();
            this.tb_ActiveDossier = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rb_clear = new System.Windows.Forms.RadioButton();
            this.rb_digDeeper = new System.Windows.Forms.RadioButton();
            this.rb_inscrutable = new System.Windows.Forms.RadioButton();
            this.rb_reappearance = new System.Windows.Forms.RadioButton();
            this.rb_suspended = new System.Windows.Forms.RadioButton();
            this.rb_rejectedDistance = new System.Windows.Forms.RadioButton();
            this.rb_rejectedProfile = new System.Windows.Forms.RadioButton();
            this.rb_withdrawn = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // bt_saveNewState
            // 
            this.bt_saveNewState.Location = new System.Drawing.Point(91, 164);
            this.bt_saveNewState.Name = "bt_saveNewState";
            this.bt_saveNewState.Size = new System.Drawing.Size(121, 23);
            this.bt_saveNewState.TabIndex = 6;
            this.bt_saveNewState.Text = "Status speichern";
            this.bt_saveNewState.UseVisualStyleBackColor = true;
            this.bt_saveNewState.Click += new System.EventHandler(this.bt_saveNewState_Click);
            // 
            // bt_cancel
            // 
            this.bt_cancel.Location = new System.Drawing.Point(322, 164);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(121, 23);
            this.bt_cancel.TabIndex = 1;
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
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 100;
            this.label1.Text = "Aktives Dossier:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 13);
            this.label2.TabIndex = 101;
            this.label2.Text = "Wählen Sie den neuen Status:";
            // 
            // rb_clear
            // 
            this.rb_clear.AutoSize = true;
            this.rb_clear.Checked = true;
            this.rb_clear.Location = new System.Drawing.Point(12, 101);
            this.rb_clear.Name = "rb_clear";
            this.rb_clear.Size = new System.Drawing.Size(60, 17);
            this.rb_clear.TabIndex = 102;
            this.rb_clear.TabStop = true;
            this.rb_clear.Text = "evident";
            this.rb_clear.UseVisualStyleBackColor = true;
            // 
            // rb_digDeeper
            // 
            this.rb_digDeeper.AutoSize = true;
            this.rb_digDeeper.Location = new System.Drawing.Point(314, 124);
            this.rb_digDeeper.Name = "rb_digDeeper";
            this.rb_digDeeper.Size = new System.Drawing.Size(79, 17);
            this.rb_digDeeper.TabIndex = 103;
            this.rb_digDeeper.Text = "nachhaken";
            this.rb_digDeeper.UseVisualStyleBackColor = true;
            // 
            // rb_inscrutable
            // 
            this.rb_inscrutable.AutoSize = true;
            this.rb_inscrutable.Location = new System.Drawing.Point(314, 101);
            this.rb_inscrutable.Name = "rb_inscrutable";
            this.rb_inscrutable.Size = new System.Drawing.Size(94, 17);
            this.rb_inscrutable.TabIndex = 104;
            this.rb_inscrutable.Text = "unverständlich";
            this.rb_inscrutable.UseVisualStyleBackColor = true;
            // 
            // rb_reappearance
            // 
            this.rb_reappearance.AutoSize = true;
            this.rb_reappearance.Location = new System.Drawing.Point(439, 101);
            this.rb_reappearance.Name = "rb_reappearance";
            this.rb_reappearance.Size = new System.Drawing.Size(108, 17);
            this.rb_reappearance.TabIndex = 105;
            this.rb_reappearance.Text = "wiedererschienen";
            this.rb_reappearance.UseVisualStyleBackColor = true;
            // 
            // rb_suspended
            // 
            this.rb_suspended.AutoSize = true;
            this.rb_suspended.Location = new System.Drawing.Point(12, 124);
            this.rb_suspended.Name = "rb_suspended";
            this.rb_suspended.Size = new System.Drawing.Size(93, 17);
            this.rb_suspended.TabIndex = 106;
            this.rb_suspended.Text = "aufgeschoben";
            this.rb_suspended.UseVisualStyleBackColor = true;
            // 
            // rb_rejectedDistance
            // 
            this.rb_rejectedDistance.AutoSize = true;
            this.rb_rejectedDistance.Location = new System.Drawing.Point(137, 124);
            this.rb_rejectedDistance.Name = "rb_rejectedDistance";
            this.rb_rejectedDistance.Size = new System.Drawing.Size(110, 17);
            this.rb_rejectedDistance.TabIndex = 107;
            this.rb_rejectedDistance.Text = "verworfen Distanz";
            this.rb_rejectedDistance.UseVisualStyleBackColor = true;
            // 
            // rb_rejectedProfile
            // 
            this.rb_rejectedProfile.AutoSize = true;
            this.rb_rejectedProfile.Location = new System.Drawing.Point(137, 101);
            this.rb_rejectedProfile.Name = "rb_rejectedProfile";
            this.rb_rejectedProfile.Size = new System.Drawing.Size(98, 17);
            this.rb_rejectedProfile.TabIndex = 108;
            this.rb_rejectedProfile.Text = "verworfen Profil";
            this.rb_rejectedProfile.UseVisualStyleBackColor = true;
            // 
            // rb_withdrawn
            // 
            this.rb_withdrawn.AutoSize = true;
            this.rb_withdrawn.Location = new System.Drawing.Point(439, 124);
            this.rb_withdrawn.Name = "rb_withdrawn";
            this.rb_withdrawn.Size = new System.Drawing.Size(61, 17);
            this.rb_withdrawn.TabIndex = 109;
            this.rb_withdrawn.Text = "entfernt";
            this.rb_withdrawn.UseVisualStyleBackColor = true;
            // 
            // frm_changeJobpostingState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 200);
            this.Controls.Add(this.rb_withdrawn);
            this.Controls.Add(this.rb_rejectedProfile);
            this.Controls.Add(this.rb_rejectedDistance);
            this.Controls.Add(this.rb_suspended);
            this.Controls.Add(this.rb_reappearance);
            this.Controls.Add(this.rb_inscrutable);
            this.Controls.Add(this.rb_digDeeper);
            this.Controls.Add(this.rb_clear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_ActiveDossier);
            this.Controls.Add(this.bt_cancel);
            this.Controls.Add(this.bt_saveNewState);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frm_changeJobpostingState";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inseratstatus des aktuellen Dossiers ändern";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_saveNewState;
        private System.Windows.Forms.Button bt_cancel;
        private System.Windows.Forms.TextBox tb_ActiveDossier;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rb_clear;
        private System.Windows.Forms.RadioButton rb_digDeeper;
        private System.Windows.Forms.RadioButton rb_inscrutable;
        private System.Windows.Forms.RadioButton rb_reappearance;
        private System.Windows.Forms.RadioButton rb_suspended;
        private System.Windows.Forms.RadioButton rb_rejectedDistance;
        private System.Windows.Forms.RadioButton rb_rejectedProfile;
        private System.Windows.Forms.RadioButton rb_withdrawn;
    }
}