namespace ApplicApp
{
    partial class frm_newApplication
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
            this.bt_Create = new System.Windows.Forms.Button();
            this.bt_Cancel = new System.Windows.Forms.Button();
            this.lb_companyName = new System.Windows.Forms.Label();
            this.tb_JobDescription = new System.Windows.Forms.TextBox();
            this.lb_forename = new System.Windows.Forms.Label();
            this.lb_salutation = new System.Windows.Forms.Label();
            this.lb_surname = new System.Windows.Forms.Label();
            this.lb_street = new System.Windows.Forms.Label();
            this.lb_plzTown = new System.Windows.Forms.Label();
            this.lb_emailAddress = new System.Windows.Forms.Label();
            this.lb_jobDescription = new System.Windows.Forms.Label();
            this.lb_emailReference = new System.Windows.Forms.Label();
            this.lb_entryDate = new System.Windows.Forms.Label();
            this.cal_EntryDate = new System.Windows.Forms.MonthCalendar();
            this.tb_CompanyName = new System.Windows.Forms.TextBox();
            this.tb_Street = new System.Windows.Forms.TextBox();
            this.tb_PLZTown = new System.Windows.Forms.TextBox();
            this.tb_Salutation = new System.Windows.Forms.TextBox();
            this.tb_Forename = new System.Windows.Forms.TextBox();
            this.tb_Surname = new System.Windows.Forms.TextBox();
            this.tb_EmailAddress = new System.Windows.Forms.TextBox();
            this.tb_EmailReference = new System.Windows.Forms.TextBox();
            this.lb_jobPortal = new System.Windows.Forms.Label();
            this.tb_JobPortal = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bt_Create
            // 
            this.bt_Create.Location = new System.Drawing.Point(83, 544);
            this.bt_Create.Name = "bt_Create";
            this.bt_Create.Size = new System.Drawing.Size(121, 23);
            this.bt_Create.TabIndex = 12;
            this.bt_Create.Text = "Dossier erzeugen";
            this.bt_Create.UseVisualStyleBackColor = true;
            this.bt_Create.Click += new System.EventHandler(this.bt_Create_Click);
            // 
            // bt_Cancel
            // 
            this.bt_Cancel.Location = new System.Drawing.Point(266, 544);
            this.bt_Cancel.Name = "bt_Cancel";
            this.bt_Cancel.Size = new System.Drawing.Size(121, 23);
            this.bt_Cancel.TabIndex = 0;
            this.bt_Cancel.Text = "Abbrechen";
            this.bt_Cancel.UseVisualStyleBackColor = true;
            this.bt_Cancel.Click += new System.EventHandler(this.bt_Cancel_Click);
            // 
            // lb_companyName
            // 
            this.lb_companyName.AutoSize = true;
            this.lb_companyName.Location = new System.Drawing.Point(18, 242);
            this.lb_companyName.Name = "lb_companyName";
            this.lb_companyName.Size = new System.Drawing.Size(97, 13);
            this.lb_companyName.TabIndex = 2;
            this.lb_companyName.Text = "Firma (SoZei als _):";
            // 
            // tb_JobDescription
            // 
            this.tb_JobDescription.Location = new System.Drawing.Point(121, 207);
            this.tb_JobDescription.Name = "tb_JobDescription";
            this.tb_JobDescription.Size = new System.Drawing.Size(302, 20);
            this.tb_JobDescription.TabIndex = 2;
            // 
            // lb_forename
            // 
            this.lb_forename.AutoSize = true;
            this.lb_forename.Location = new System.Drawing.Point(27, 370);
            this.lb_forename.Name = "lb_forename";
            this.lb_forename.Size = new System.Drawing.Size(88, 13);
            this.lb_forename.TabIndex = 4;
            this.lb_forename.Text = "Kontaktvorname:";
            // 
            // lb_salutation
            // 
            this.lb_salutation.AutoSize = true;
            this.lb_salutation.Location = new System.Drawing.Point(71, 338);
            this.lb_salutation.Name = "lb_salutation";
            this.lb_salutation.Size = new System.Drawing.Size(44, 13);
            this.lb_salutation.TabIndex = 5;
            this.lb_salutation.Text = "Anrede:";
            // 
            // lb_surname
            // 
            this.lb_surname.AutoSize = true;
            this.lb_surname.Location = new System.Drawing.Point(18, 402);
            this.lb_surname.Name = "lb_surname";
            this.lb_surname.Size = new System.Drawing.Size(97, 13);
            this.lb_surname.TabIndex = 6;
            this.lb_surname.Text = "Kontaktnachname:";
            // 
            // lb_street
            // 
            this.lb_street.AutoSize = true;
            this.lb_street.Location = new System.Drawing.Point(53, 274);
            this.lb_street.Name = "lb_street";
            this.lb_street.Size = new System.Drawing.Size(62, 13);
            this.lb_street.TabIndex = 7;
            this.lb_street.Text = "Srasse, Nr.:";
            // 
            // lb_plzTown
            // 
            this.lb_plzTown.AutoSize = true;
            this.lb_plzTown.Location = new System.Drawing.Point(65, 306);
            this.lb_plzTown.Name = "lb_plzTown";
            this.lb_plzTown.Size = new System.Drawing.Size(50, 13);
            this.lb_plzTown.TabIndex = 8;
            this.lb_plzTown.Text = "PLZ, Ort:";
            // 
            // lb_emailAddress
            // 
            this.lb_emailAddress.AutoSize = true;
            this.lb_emailAddress.Location = new System.Drawing.Point(39, 434);
            this.lb_emailAddress.Name = "lb_emailAddress";
            this.lb_emailAddress.Size = new System.Drawing.Size(76, 13);
            this.lb_emailAddress.TabIndex = 9;
            this.lb_emailAddress.Text = "Email-Adresse:";
            // 
            // lb_jobDescription
            // 
            this.lb_jobDescription.AutoSize = true;
            this.lb_jobDescription.Location = new System.Drawing.Point(27, 210);
            this.lb_jobDescription.Name = "lb_jobDescription";
            this.lb_jobDescription.Size = new System.Drawing.Size(88, 13);
            this.lb_jobDescription.TabIndex = 10;
            this.lb_jobDescription.Text = "Jobbezeichnung:";
            // 
            // lb_emailReference
            // 
            this.lb_emailReference.AutoSize = true;
            this.lb_emailReference.Location = new System.Drawing.Point(45, 466);
            this.lb_emailReference.Name = "lb_emailReference";
            this.lb_emailReference.Size = new System.Drawing.Size(70, 13);
            this.lb_emailReference.TabIndex = 11;
            this.lb_emailReference.Text = "Referenz-Nr.:";
            // 
            // lb_entryDate
            // 
            this.lb_entryDate.AutoSize = true;
            this.lb_entryDate.Location = new System.Drawing.Point(24, 103);
            this.lb_entryDate.Name = "lb_entryDate";
            this.lb_entryDate.Size = new System.Drawing.Size(91, 13);
            this.lb_entryDate.TabIndex = 12;
            this.lb_entryDate.Text = "Erfassungsdatum:";
            // 
            // cal_EntryDate
            // 
            this.cal_EntryDate.Location = new System.Drawing.Point(164, 18);
            this.cal_EntryDate.MaxSelectionCount = 1;
            this.cal_EntryDate.Name = "cal_EntryDate";
            this.cal_EntryDate.ShowWeekNumbers = true;
            this.cal_EntryDate.TabIndex = 1;
            // 
            // tb_CompanyName
            // 
            this.tb_CompanyName.Location = new System.Drawing.Point(121, 239);
            this.tb_CompanyName.Name = "tb_CompanyName";
            this.tb_CompanyName.Size = new System.Drawing.Size(302, 20);
            this.tb_CompanyName.TabIndex = 3;
            // 
            // tb_Street
            // 
            this.tb_Street.Location = new System.Drawing.Point(121, 271);
            this.tb_Street.Name = "tb_Street";
            this.tb_Street.Size = new System.Drawing.Size(302, 20);
            this.tb_Street.TabIndex = 4;
            // 
            // tb_PLZTown
            // 
            this.tb_PLZTown.Location = new System.Drawing.Point(121, 303);
            this.tb_PLZTown.Name = "tb_PLZTown";
            this.tb_PLZTown.Size = new System.Drawing.Size(302, 20);
            this.tb_PLZTown.TabIndex = 5;
            // 
            // tb_Salutation
            // 
            this.tb_Salutation.Location = new System.Drawing.Point(121, 335);
            this.tb_Salutation.Name = "tb_Salutation";
            this.tb_Salutation.Size = new System.Drawing.Size(302, 20);
            this.tb_Salutation.TabIndex = 6;
            // 
            // tb_Forename
            // 
            this.tb_Forename.Location = new System.Drawing.Point(121, 367);
            this.tb_Forename.Name = "tb_Forename";
            this.tb_Forename.Size = new System.Drawing.Size(302, 20);
            this.tb_Forename.TabIndex = 7;
            // 
            // tb_Surname
            // 
            this.tb_Surname.Location = new System.Drawing.Point(121, 399);
            this.tb_Surname.Name = "tb_Surname";
            this.tb_Surname.Size = new System.Drawing.Size(302, 20);
            this.tb_Surname.TabIndex = 8;
            // 
            // tb_EmailAddress
            // 
            this.tb_EmailAddress.Location = new System.Drawing.Point(121, 431);
            this.tb_EmailAddress.Name = "tb_EmailAddress";
            this.tb_EmailAddress.Size = new System.Drawing.Size(302, 20);
            this.tb_EmailAddress.TabIndex = 9;
            // 
            // tb_EmailReference
            // 
            this.tb_EmailReference.Location = new System.Drawing.Point(121, 463);
            this.tb_EmailReference.Name = "tb_EmailReference";
            this.tb_EmailReference.Size = new System.Drawing.Size(302, 20);
            this.tb_EmailReference.TabIndex = 10;
            // 
            // lb_jobPortal
            // 
            this.lb_jobPortal.AutoSize = true;
            this.lb_jobPortal.Location = new System.Drawing.Point(47, 498);
            this.lb_jobPortal.Name = "lb_jobPortal";
            this.lb_jobPortal.Size = new System.Drawing.Size(68, 13);
            this.lb_jobPortal.TabIndex = 22;
            this.lb_jobPortal.Text = "Stellenbörse:";
            // 
            // tb_JobPortal
            // 
            this.tb_JobPortal.Location = new System.Drawing.Point(121, 495);
            this.tb_JobPortal.Name = "tb_JobPortal";
            this.tb_JobPortal.Size = new System.Drawing.Size(302, 20);
            this.tb_JobPortal.TabIndex = 11;
            // 
            // frm_newApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 589);
            this.Controls.Add(this.tb_JobPortal);
            this.Controls.Add(this.lb_jobPortal);
            this.Controls.Add(this.tb_EmailReference);
            this.Controls.Add(this.tb_EmailAddress);
            this.Controls.Add(this.tb_Surname);
            this.Controls.Add(this.tb_Forename);
            this.Controls.Add(this.tb_Salutation);
            this.Controls.Add(this.tb_PLZTown);
            this.Controls.Add(this.tb_Street);
            this.Controls.Add(this.tb_CompanyName);
            this.Controls.Add(this.cal_EntryDate);
            this.Controls.Add(this.lb_entryDate);
            this.Controls.Add(this.lb_emailReference);
            this.Controls.Add(this.lb_jobDescription);
            this.Controls.Add(this.lb_emailAddress);
            this.Controls.Add(this.lb_plzTown);
            this.Controls.Add(this.lb_street);
            this.Controls.Add(this.lb_surname);
            this.Controls.Add(this.lb_salutation);
            this.Controls.Add(this.lb_forename);
            this.Controls.Add(this.tb_JobDescription);
            this.Controls.Add(this.lb_companyName);
            this.Controls.Add(this.bt_Cancel);
            this.Controls.Add(this.bt_Create);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frm_newApplication";
            this.Text = "Bewerbungsdossier erstellen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_Create;
        private System.Windows.Forms.Button bt_Cancel;
        private System.Windows.Forms.Label lb_companyName;
        private System.Windows.Forms.TextBox tb_JobDescription;
        private System.Windows.Forms.Label lb_forename;
        private System.Windows.Forms.Label lb_salutation;
        private System.Windows.Forms.Label lb_surname;
        private System.Windows.Forms.Label lb_street;
        private System.Windows.Forms.Label lb_plzTown;
        private System.Windows.Forms.Label lb_emailAddress;
        private System.Windows.Forms.Label lb_jobDescription;
        private System.Windows.Forms.Label lb_emailReference;
        private System.Windows.Forms.Label lb_entryDate;
        private System.Windows.Forms.MonthCalendar cal_EntryDate;
        private System.Windows.Forms.TextBox tb_CompanyName;
        private System.Windows.Forms.TextBox tb_Street;
        private System.Windows.Forms.TextBox tb_PLZTown;
        private System.Windows.Forms.TextBox tb_Salutation;
        private System.Windows.Forms.TextBox tb_Forename;
        private System.Windows.Forms.TextBox tb_Surname;
        private System.Windows.Forms.TextBox tb_EmailAddress;
        private System.Windows.Forms.TextBox tb_EmailReference;
        private System.Windows.Forms.Label lb_jobPortal;
        private System.Windows.Forms.TextBox tb_JobPortal;
    }
}