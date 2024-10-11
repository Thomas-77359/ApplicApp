using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ApplicApp
{
    public partial class frm_newApplication : Form
    {
        private frm_ApplicApp myParentApp;
        

        public frm_newApplication(frm_ApplicApp parent, string activeDossierPartPath)
        {
            myParentApp = parent;
            InitializeComponent();
            if (activeDossierPartPath != null)
            {
                initValuesFromDossier(activeDossierPartPath);
            }
        }

        //Initialisiert die Formulareintraege mit den Werten aus dem aktiven Dossier.
        private void initValuesFromDossier(string activeDossierPartPath)
        {
            string dossierRepositoryPath = myParentApp.getPathToRepository();
            string dossierPathString = Path.Combine(dossierRepositoryPath, activeDossierPartPath);

            if(DossierMetadataHandling.hasMetadataFile(dossierPathString))
            {
                if (DossierMetadataHandling.isMetadataFileCorrect(dossierPathString))
                {
                    string dossierCreationDate = DossierMetadataHandling.getEntryDateFrom(dossierPathString);
                    DateTime creationDate = DateTime.ParseExact(dossierCreationDate, AAConst.DATESTAMP_FORMAT, System.Globalization.CultureInfo.InvariantCulture);
                    cal_EntryDate.SetDate(creationDate);

                    tb_JobDescription.Text = DossierMetadataHandling.getJobTitleFrom(dossierPathString);
                    tb_CompanyName.Text = DossierMetadataHandling.getCompanyFrom(dossierPathString);
                    tb_Street.Text = DossierMetadataHandling.getStreetFrom(dossierPathString);
                    tb_PLZTown.Text = DossierMetadataHandling.getTownFrom(dossierPathString);
                    tb_Salutation.Text = DossierMetadataHandling.getSalutationFrom(dossierPathString);
                    tb_Forename.Text = DossierMetadataHandling.getForenameFrom(dossierPathString);
                    tb_Surname.Text = DossierMetadataHandling.getSurnameFrom(dossierPathString);
                    tb_EmailAddress.Text = DossierMetadataHandling.getEmailAddressFrom(dossierPathString);
                    tb_EmailReference.Text = DossierMetadataHandling.getEmailReferenceFrom(dossierPathString);
                    tb_JobPortal.Text = DossierMetadataHandling.getJobPortalFrom(dossierPathString);
                }
            }
        }

        

        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bt_Create_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isInputOk())
                {
                    return;
                }

                string dossierRepositoryPath = myParentApp.getPathToRepository();

                DateTime entryDate = cal_EntryDate.SelectionStart;
                string str_entryDate = entryDate.ToString(AAConst.DATESTAMP_FORMAT, System.Globalization.CultureInfo.InvariantCulture);

                string dossierStatus = DossierState.Erzeugt.ToString();
                string jobpostingState = JobpostingState.evident.ToString();
                string dossierName = DossierHandling.createDossierName(str_entryDate, tb_JobDescription.Text, dossierStatus);

                myParentApp.setActiveDossierPathPart(tb_CompanyName.Text + AAConst.SINGLE_BACKSLASH + dossierName);

                DossierHandling.createNewDossier(dossierRepositoryPath, dossierName, str_entryDate, tb_JobDescription.Text,
                                                tb_CompanyName.Text, tb_Street.Text, tb_PLZTown.Text,
                                                tb_Salutation.Text, tb_Forename.Text, tb_Surname.Text,
                                                tb_EmailAddress.Text, tb_EmailReference.Text, tb_JobPortal.Text, dossierStatus, "", jobpostingState);
                myParentApp.loadDossierSelector(false);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: bt_Create_Click(): Exception aufgetreten: " + ex.Message, "Exception");
            }            
        }

        //Prueft ob in den allen Feldern ein Inhalt eingegeben wurde.
        private bool isInputOk()
        {
            if (tb_JobDescription.Text.Equals(""))
            {
                MessageBox.Show("Error: isInputOk(): Die Jobbezeichnung fehlt.");
                return false;
            }

            if (tb_CompanyName.Text.Equals(""))
            {
                MessageBox.Show("Error: isInputOk(): Der Firmenname fehlt.");
                return false;
            }
            if (tb_Street.Text.Equals(""))
            {
                MessageBox.Show("Error: isInputOk(): Die Strasse und die Hausnummer fehlen.");
                return false;
            }
            if (tb_PLZTown.Text.Equals(""))
            {
                MessageBox.Show("Error: isInputOk(): Die Postleitzahl und der Ort fehlen.");
                return false;
            }
            if (tb_Salutation.Text.Equals(""))
            {
                MessageBox.Show("Error: isInputOk(): Die Anrede fehlt.");
                return false;
            }
            if (tb_EmailAddress.Text.Equals(""))
            {
                MessageBox.Show("Error: isInputOk(): Die Email Adresse fehlt.");
                return false;
            }
            if (tb_JobPortal.Text.Equals(""))
            {
                MessageBox.Show("Error: isInputOk(): Die Stellenbörse fehlt.");
                return false;
            }
            return true;
        }
    }
}
