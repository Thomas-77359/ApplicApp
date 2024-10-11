using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ApplicApp
{
    public partial class frm_newPhoneCallNotes : Form
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        private frm_ApplicApp myParentApp;
        private string dossierPathString;

        private static List<ComboBoxItem> dossierList = new List<ComboBoxItem>();
        private string pathToPhoneCallNotesTemplates;
        private string pathToEmptyODT;

        public class ComboBoxItem
        {
            public string fileName { get; set; }
            public string filePath { get; set; }

            public override string ToString()
            {
                return fileName;
            }
        }



        public frm_newPhoneCallNotes(frm_ApplicApp parent, string pathToTemplates, string activeDossierPartPath, string pathToEmptyODTFile)
        {
            myParentApp = parent;
            pathToPhoneCallNotesTemplates = pathToTemplates;
            pathToEmptyODT = pathToEmptyODTFile;
            InitializeComponent();
            if (activeDossierPartPath != null)
            {
                initValuesFromDossier(activeDossierPartPath);
            }
            updateCBTemplateChooser();
        }

        
        private void updateCBTemplateChooser()
        {
            string[] motivTemplates = Directory.GetFiles(pathToPhoneCallNotesTemplates);
            ComboBoxItem item = null;

            foreach (string path in motivTemplates)
            {
                item = new ComboBoxItem();
                item.fileName = Path.GetFileName(path);
                item.filePath = path;
                cb_TemplateChooser.Items.Add(item);
            }

            cb_TemplateChooser.SelectedIndex = 0;
        }
        
        //Initialisiert die Formulareintraege mit den Werten aus dem aktiven Dossier.
        private void initValuesFromDossier(string activeDossierPartPath)
        {
            string dossierRepositoryPath = myParentApp.getPathToRepository();
            dossierPathString = Path.Combine(dossierRepositoryPath, activeDossierPartPath);

            if(DossierMetadataHandling.hasMetadataFile(dossierPathString))
            {
                if (DossierMetadataHandling.isMetadataFileCorrect(dossierPathString))
                {
                    tb_JobDescription.Text = DossierMetadataHandling.getJobTitleFrom(dossierPathString);
                    tb_CompanyName.Text = DossierMetadataHandling.getCompanyFrom(dossierPathString);
                    tb_Street.Text = DossierMetadataHandling.getStreetFrom(dossierPathString);
                    tb_Town.Text = DossierMetadataHandling.getTownFrom(dossierPathString);
                    tb_ContactName.Text = DossierMetadataHandling.getSalutationFrom(dossierPathString) + AAConst.SINGLE_SPACE 
                                        + DossierMetadataHandling.getForenameFrom(dossierPathString) + AAConst.SINGLE_SPACE
                                        + DossierMetadataHandling.getSurnameFrom(dossierPathString);
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
                MessageBox.Show("Info: bt_Create_Click(): Die neue ODT-Datei nur mit \"Speichern unter\" sichern.", "Information");
                createPhoneCallNotes();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: bt_Create_Click(): Exception aufgetreten: " + ex.Message, "Exception");
            }            
        }

        //Erstellt eine Fragebogen-Vorlage
        //Die Textdateien muessen in unicode gespeichert sein.
        private void createPhoneCallNotes()
        {

            if (this.dossierPathString.Equals(""))
            {
                MessageBox.Show("Error: Kein aktives Dossier angegeben. Erzeugung abgebrochen.");
                return;
            }
            
            //Ausgewaehltes Template laden
            string tmplPath = (cb_TemplateChooser.SelectedItem as ComboBoxItem).filePath;
            string strTmpl_phoneNotes = File.ReadAllText(tmplPath, Encoding.GetEncoding(AAConst.UNICODE));

            //Tag fuer das aktuelle Datum erstellen
            //string creationDateTag = DateTime.Today.ToString(AAConst.DATESTAMP_FORMAT, System.Globalization.CultureInfo.InvariantCulture);
            string creationDateTag = DateTime.Today.ToString(AAConst.DATESTAMP_CH_FORMAT, System.Globalization.CultureInfo.InvariantCulture);

            //Das Datum des letzten Kontakts muss erstellt werden.
            DateTime lastContactDate = cal_LastContactDate.SelectionStart;
            string str_meetingDate = lastContactDate.ToString(AAConst.DATESTAMP_CH_FORMAT, System.Globalization.CultureInfo.InvariantCulture);
          
            //Die uebrigen Werten holen
            string jobTitle = tb_JobDescription.Text;
            string company = tb_CompanyName.Text;
            string streetNr = tb_Street.Text;
            string plzTown = tb_Town.Text;
            string tfNumber = tb_PhoneNumber.Text;
            string typeOfLastContact = tb_ArtOfLastContact.Text;
            string contact = tb_ContactName.Text;

            string questions = "";
            if (!tb_Question1.Text.Equals(""))
            {
                questions = questions + tb_Question1.Text + AAConst.LINE_FEED + AAConst.LINE_FEED;
            }
            if (!tb_Question2.Text.Equals(""))
            {
                questions = questions + tb_Question2.Text + AAConst.LINE_FEED + AAConst.LINE_FEED;
            }
            if (!tb_Question3.Text.Equals(""))
            {
                questions = questions + tb_Question3.Text + AAConst.LINE_FEED + AAConst.LINE_FEED;
            }
            if (!tb_Question4.Text.Equals(""))
            {
                questions = questions + tb_Question4.Text + AAConst.LINE_FEED + AAConst.LINE_FEED;
            }


            //Die Tags in den template-strings muessen noch ersetzt werden.
            string str_phoneCallNotes = AAUtilities.replaceTemplateTags(strTmpl_phoneNotes, "", jobTitle, "", creationDateTag, "", "", "", company, 
                                                                    streetNr, plzTown, "", str_meetingDate, "", questions, tfNumber, 
                                                                    typeOfLastContact, contact);
            str_phoneCallNotes = str_phoneCallNotes.Replace(AAConst.NEW_LINE, ""); //\r reicht als Linefeed

            //Process.Start(SetupHandling.getPathToEmptyODTFromSetup());
            Process.Start(SetupHandling.getPathToPreparedQaODTFromSetup());
            Thread.Sleep(15000);
            //TODO: Mehrere Versuche machen, siehe KB sendKeys
            Process p = Process.GetProcessesByName(AAConst.PROC_SOFFICE).FirstOrDefault();
            if (p != null)
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
                //Das Makro muss aktiviert werden.
                SendKeys.SendWait("{LEFT}{ENTER}");
                SendKeys.SendWait(str_phoneCallNotes);
            }
            else
            {
                MessageBox.Show("Error: " + AAConst.PROC_SOFFICE + " ist nicht geöffnet.");
            }       
        }



        //Prueft ob in den allen Feldern ein Inhalt eingegeben wurde.
        private bool isInputOk()
        {

            //Tag fuer das aktuelle Datum erstellen
            string creationDateTag = DateTime.Today.ToString(AAConst.DATESTAMP_CH_FORMAT, System.Globalization.CultureInfo.InvariantCulture);

            //Das Datum des letzten Kontakts muss erstellt werden.
            DateTime lastContactDate = cal_LastContactDate.SelectionStart;
            string str_meetingDate = lastContactDate.ToString(AAConst.DATESTAMP_CH_FORMAT, System.Globalization.CultureInfo.InvariantCulture);

            if (creationDateTag.Equals(str_meetingDate))
            {
                DialogResult dialogResult = MessageBox.Show("Das heutige Datum und das Datum des letzten Kontakts sind identisch. Wollen Sie mit dem Erzeugen fortfahren?", "Rückfrage",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dialogResult.Equals(DialogResult.No))
                {
                    return false;
                }
            }


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

            if (tb_PhoneNumber.Text.Equals(""))
            {
                MessageBox.Show("Error: isInputOk(): Die Telefonnummer fehlt.");
                return false;
            }

            if (tb_ArtOfLastContact.Text.Equals(""))
            {
                MessageBox.Show("Error: isInputOk(): Die Art des letzten Kontakts fehlt.");
                return false;
            }

            if (tb_ContactName.Text.Equals(""))
            {
                MessageBox.Show("Error: isInputOk(): Der Kontakt fehlt.");
                return false;
            }

            return true;
        }




    }
    
}
