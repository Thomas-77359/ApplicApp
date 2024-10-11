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

    public enum ChangeModus { CreationDate, JobDescription, CompanyName, Informations };
    

    public partial class frm_changeDossierData : Form
    {
 
        private frm_ApplicApp myParentApp;
        private string activeDossierPathString;
        private string dossierRepositoryPath;
        private ChangeModus changeModus;
        


        public frm_changeDossierData(frm_ApplicApp parent, string activeDossierPartPath, ChangeModus modus)
        {
            this.myParentApp = parent;
            this.changeModus = modus;
            
            InitializeComponent();
            if(!couldCreateActiveDossierPath(activeDossierPartPath))
            {
                MessageBox.Show("Error: frm_changeDossierData(): Der Pfad des aktuellen Dossiers konnte nicht erstellt werden. Teilpfad: " + activeDossierPartPath);
                this.Close();
                return;
            }
            if(!couldCallInitMethod())
            {
                MessageBox.Show("Error: frm_changeDossierData(): Die init-Methode konnte nicht aufgerufen werden. Modus: " + changeModus.ToString());
            }
        }


        //Erzeugt den Pfad des aktiven Dossiers und prüft ob es gültige Metadaten hat.
        private bool couldCreateActiveDossierPath(string activeDossierPartPath)
        {
            this.dossierRepositoryPath = myParentApp.getPathToRepository();
            activeDossierPathString = Path.Combine(dossierRepositoryPath, activeDossierPartPath);

            if (DossierMetadataHandling.hasMetadataFile(activeDossierPathString))
            {
                if (DossierMetadataHandling.isMetadataFileCorrect(activeDossierPathString))
                {
                    return true;
                }
            }
            return false;
        }

        private bool couldCallInitMethod()
        {

            switch (this.changeModus)
            {
                case ChangeModus.CreationDate:
                    initChangeOfCreationDate();
                    return true;
                case ChangeModus.JobDescription:
                    initChangeOfJobDescription();
                    return true;
                case ChangeModus.CompanyName:
                    initChangeOfCompanyName(false);
                    return true;
                case ChangeModus.Informations:
                    initChangeOfInformations();
                    return true;
                default:
                    MessageBox.Show("Error: couldCallInitMethod(): Angegebener Änderungsmodus ungültig: " + changeModus.ToString());
                    return false;
            }
        }

        private void initChangeOfCreationDate()
        {
            cal_EntryDate.Enabled = true;
            string dossierCreationDate = DossierMetadataHandling.getEntryDateFrom(activeDossierPathString);
            DateTime creationDate = DateTime.ParseExact(dossierCreationDate, AAConst.DATESTAMP_FORMAT, System.Globalization.CultureInfo.InvariantCulture);
            cal_EntryDate.SetDate(creationDate);
        }

        private void initChangeOfJobDescription()
        {
            tb_JobDescription.Enabled = true;
            tb_JobDescription.Text = DossierMetadataHandling.getJobTitleFrom(activeDossierPathString);
        }

        //Die ComboBox wird mit den verfügbaren Firmennamen gefüllt.
        private void initChangeOfCompanyName(bool selectDefault)
        {
                       
            List<string> companyNameList = new List<string>(DossierHandling.getCompanies(dossierRepositoryPath));

            companyNameList.Sort();

            cb_CompanyName.Enabled = true;
            cb_CompanyName.Items.Clear();

            foreach (string company in companyNameList)
            {
                cb_CompanyName.Items.Add(company);
            }
            if (selectDefault)
            {
                cb_CompanyName.SelectedIndex = 0;
            }
            else
            {
                int indexActCompany = cb_CompanyName.FindString(DossierMetadataHandling.getCompanyFrom(activeDossierPathString));
                cb_CompanyName.SelectedIndex = indexActCompany;
            }
        }

        private void initChangeOfInformations()
        {            
            tb_Street.Enabled = true;
            tb_Street.Text = DossierMetadataHandling.getStreetFrom(activeDossierPathString);

            tb_PLZTown.Enabled = true;
            tb_PLZTown.Text = DossierMetadataHandling.getTownFrom(activeDossierPathString);

            tb_Salutation.Enabled = true;
            tb_Salutation.Text = DossierMetadataHandling.getSalutationFrom(activeDossierPathString);

            tb_Forename.Enabled = true;
            tb_Forename.Text = DossierMetadataHandling.getForenameFrom(activeDossierPathString);

            tb_Surname.Enabled = true;
            tb_Surname.Text = DossierMetadataHandling.getSurnameFrom(activeDossierPathString);

            tb_EmailAddress.Enabled = true;
            tb_EmailAddress.Text = DossierMetadataHandling.getEmailAddressFrom(activeDossierPathString);

            tb_EmailReference.Enabled = true;
            tb_EmailReference.Text = DossierMetadataHandling.getEmailReferenceFrom(activeDossierPathString);

            tb_JobPortal.Enabled = true;
            tb_JobPortal.Text = DossierMetadataHandling.getJobPortalFrom(activeDossierPathString);

            tb_HistoryInfos.Enabled = true;
            tb_HistoryInfos.Text = DossierMetadataHandling.getHistoryInfosFrom(activeDossierPathString);
        }

        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_Save_Click(object sender, EventArgs e)
        {
            if (!couldCallSaveMethod())
            {
                MessageBox.Show("Error: bt_Save_Click(): Aus irgendeinem Grund konnte nicht gespeichert werden.");
            }
        }


        private bool couldCallSaveMethod()
        {
            switch (this.changeModus)
            {
                case ChangeModus.CreationDate:
                    saveCreationDate();
                    return true;
                case ChangeModus.JobDescription:
                    saveJobDescription();
                    return true;
                case ChangeModus.CompanyName:
                    saveCompanyName();
                    return true;
                case ChangeModus.Informations:
                    saveInformations();
                    return true;
                default:
                    MessageBox.Show("Error: couldCallSaveMethod(): Angegebener Änderungsmodus ungültig: " + changeModus.ToString());
                    return false;
            }
        }

        //Aendert die Metadaten
        //Erzeugt ein neues Dossier und verschiebt die Dateien des alten Dossiers dorthin. 
        //Das Aendern von Dateinamen ist keine gute Idee solange der Inhalt nicht auch geaendert wird.
        private void saveCreationDate()
        {
            DateTime entryDate = cal_EntryDate.SelectionStart;
            string str_entryDate = entryDate.ToString(AAConst.DATESTAMP_FORMAT, System.Globalization.CultureInfo.InvariantCulture);

            if (str_entryDate.Equals(""))
            {
                MessageBox.Show("Error: saveCreationDate(): Speichern unmöglich, das Datumstag ist ein Leerstring.");
                return;
            }
            
            DossierMetadataHandling.saveEntryDateToMetadata(activeDossierPathString, str_entryDate);

            string jobTitle = DossierMetadataHandling.getJobTitleFrom(activeDossierPathString);
            string status = DossierMetadataHandling.getDossierStateFrom(activeDossierPathString).ToString();
            string company = DossierMetadataHandling.getCompanyFrom(activeDossierPathString);

            string newDossierName = DossierHandling.createDossierName(str_entryDate, jobTitle, status);
            string newDossierPath = Path.Combine(dossierRepositoryPath, company, newDossierName);

            //Wenn die Dateien irgendwo geoeffnet sind koennen sie nicht verschoben werden und es tritt ein Fehler auf.
            try
            {
                Directory.Move(activeDossierPathString, newDossierPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: saveCreationDate(): Dossierinhalt kann nicht verschoben werden: " + ex.Message, "Fehler aufgetreten");
            }
            
            myParentApp.loadDossierSelector(true);
            Close();

        }

        //Aendert die Metadaten
        //Erzeugt ein neues Dossier und verschiebt die Dateien des alten Dossiers dorthin. 
        private void saveJobDescription()
        {
            if (tb_JobDescription.Text.Equals(""))
            {
                MessageBox.Show("Error: saveJobDescription(): Speichern unmöglich, die Job-Beschreibung ist ein Leerstring.");
                return;
            }
            
            DossierMetadataHandling.saveJobTitleToMetadata(activeDossierPathString, tb_JobDescription.Text);

            string entryDate = DossierMetadataHandling.getEntryDateFrom(activeDossierPathString);
            string status = DossierMetadataHandling.getDossierStateFrom(activeDossierPathString).ToString();
            string company = DossierMetadataHandling.getCompanyFrom(activeDossierPathString);

            string newDossierName = DossierHandling.createDossierName(entryDate, tb_JobDescription.Text, status);
            string newDossierPath = Path.Combine(dossierRepositoryPath, company, newDossierName);

            //Wenn die Dateien irgendwo geoeffnet sind koennen sie nicht verschoben werden und es tritt ein Fehler auf.
            try
            {
                Directory.Move(activeDossierPathString, newDossierPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: saveJobDescription(): Dossierinhalt kann nicht verschoben werden: " + ex.Message, "Fehler aufgetreten");
            }

            myParentApp.loadDossierSelector(true);
            Close();
        }

        //Aendert die Metadaten
        //Erzeugt ein neues Dossier und verschiebt die Dateien des alten Dossiers dorthin. 
        private void saveCompanyName()
        {

            if (cb_CompanyName.Text.Equals(""))
            {
                MessageBox.Show("Error: saveCompanyName(): Speichern unmöglich, der Firmenname ist ein Leerstring.");
                return;
            }

            DossierMetadataHandling.saveCompanyToMetadata(activeDossierPathString, cb_CompanyName.Text);

            string companyPathString = Path.Combine(dossierRepositoryPath, cb_CompanyName.Text);
            if (!Directory.Exists(companyPathString))
            {
                Directory.CreateDirectory(companyPathString);
            }

            string entryDate = DossierMetadataHandling.getEntryDateFrom(activeDossierPathString);
            string jobTitle = DossierMetadataHandling.getJobTitleFrom(activeDossierPathString);
            string status = DossierMetadataHandling.getDossierStateFrom(activeDossierPathString).ToString();

            string newDossierName = DossierHandling.createDossierName(entryDate, jobTitle, status);
            string newDossierPath = Path.Combine(dossierRepositoryPath, cb_CompanyName.Text, newDossierName);

            //Wenn die Dateien irgendwo geoeffnet sind koennen sie nicht verschoben werden und es tritt ein Fehler auf.
            try
            {
                Directory.Move(activeDossierPathString, newDossierPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: saveCompanyName(): Dossierinhalt kann nicht verschoben werden: " + ex.Message, "Fehler aufgetreten");
            }

            myParentApp.loadDossierSelector(true);
            Close();
        }

        //Aendert das Metadata-File
        private void saveInformations()
        {
            
            if (tb_Street.Text.Equals(""))
            {
                MessageBox.Show("Error: saveInformations(): Speichern unmöglich, die Strassenangabe ist ein Leerstring.");
                return;
            }
            if (tb_PLZTown.Text.Equals(""))
            {
                MessageBox.Show("Error: saveInformations(): Speichern unmöglich, die Ortsangabe ist ein Leerstring.");
                return;
            }
            if (tb_Salutation.Text.Equals(""))
            {
                MessageBox.Show("Error: saveInformations(): Speichern unmöglich, die Anrede ist ein Leerstring.");
                return;
            }
            if (tb_EmailAddress.Text.Equals(""))
            {
                MessageBox.Show("Error: saveInformations(): Speichern unmöglich, die Email-Adresse ist ein Leerstring.");
                return;
            }
            if (tb_JobPortal.Text.Equals(""))
            {
                MessageBox.Show("Error: saveInformations(): Speichern unmöglich, die JobPortal-URL ist ein Leerstring.");
                return;
            } 

            DossierMetadataHandling.saveInformationsToMetadata(activeDossierPathString, tb_Street.Text, tb_PLZTown.Text, 
                                                        tb_Salutation.Text, tb_Forename.Text, tb_Surname.Text, 
                                                        tb_EmailAddress.Text, tb_EmailReference.Text, tb_JobPortal.Text, 
                                                        tb_HistoryInfos.Text);

            Close();
            
        }


        
    }
}
