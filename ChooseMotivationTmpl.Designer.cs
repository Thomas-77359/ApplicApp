namespace ApplicApp
{
    partial class frm_chooseMotivationTmpl
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
            this.cb_TemplateChooser = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_TemplatePreview = new System.Windows.Forms.TextBox();
            this.bt_cancel = new System.Windows.Forms.Button();
            this.bt_choose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cb_TemplateChooser
            // 
            this.cb_TemplateChooser.FormattingEnabled = true;
            this.cb_TemplateChooser.Location = new System.Drawing.Point(145, 46);
            this.cb_TemplateChooser.Name = "cb_TemplateChooser";
            this.cb_TemplateChooser.Size = new System.Drawing.Size(326, 21);
            this.cb_TemplateChooser.TabIndex = 1;
            this.cb_TemplateChooser.SelectedIndexChanged += new System.EventHandler(this.cb_TemplateChooser_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Template auswählen:";
            // 
            // tb_TemplatePreview
            // 
            this.tb_TemplatePreview.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.tb_TemplatePreview.Location = new System.Drawing.Point(34, 95);
            this.tb_TemplatePreview.Multiline = true;
            this.tb_TemplatePreview.Name = "tb_TemplatePreview";
            this.tb_TemplatePreview.ReadOnly = true;
            this.tb_TemplatePreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_TemplatePreview.Size = new System.Drawing.Size(569, 467);
            this.tb_TemplatePreview.TabIndex = 2;
            // 
            // bt_cancel
            // 
            this.bt_cancel.Location = new System.Drawing.Point(350, 592);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(121, 23);
            this.bt_cancel.TabIndex = 0;
            this.bt_cancel.Text = "Abbrechen";
            this.bt_cancel.UseVisualStyleBackColor = true;
            this.bt_cancel.Click += new System.EventHandler(this.bt_cancel_Click);
            // 
            // bt_choose
            // 
            this.bt_choose.Location = new System.Drawing.Point(145, 592);
            this.bt_choose.Name = "bt_choose";
            this.bt_choose.Size = new System.Drawing.Size(121, 23);
            this.bt_choose.TabIndex = 3;
            this.bt_choose.Text = "Auswählen";
            this.bt_choose.UseVisualStyleBackColor = true;
            this.bt_choose.Click += new System.EventHandler(this.bt_choose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(31, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(277, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "TXT-Vorlagen müssen im Unicode-Format hinterlegt sein. ";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // frm_chooseMotivationTmpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 627);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bt_choose);
            this.Controls.Add(this.bt_cancel);
            this.Controls.Add(this.tb_TemplatePreview);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_TemplateChooser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frm_chooseMotivationTmpl";
            this.Text = "Template für Motivationsschreiben aussuchen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_TemplateChooser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_TemplatePreview;
        private System.Windows.Forms.Button bt_cancel;
        private System.Windows.Forms.Button bt_choose;
        private System.Windows.Forms.Label label2;
    }
}