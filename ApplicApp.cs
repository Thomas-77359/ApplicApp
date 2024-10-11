using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Globalization;

namespace ApplicApp
{
    public partial class frm_ApplicApp : Form
    {

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        public frm_ApplicApp()
        {
            InitializeComponent();
            try
            {
                loadDossierSelector(true);
                loadSetup();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: frm_ApplicApp(): Beim Start der Applikation ist ein schwerer Fehler aufgetreten: " + ex.Message, "Schwerer Ausnahmefehler");
            }
        }


        public void setActiveDossierPathPart(string firma_Jobtitle)
        {
            tb_selectedDossier.Text = firma_Jobtitle;
        }

        public string getPathToRepository()
        {
            return SetupHandling.getPathToApplicationRepositoryFromSetup();
        }


        //Ein separater Dialog fuer das Erfassen neuer Dossiers wird gestartet.
        //TEST: PASS
        private void bt_newApplication_Click(object sender, EventArgs e)
        {
            frm_newApplication createNewForm = new frm_newApplication(this, null);
            createNewForm.Show();
            
        }

        private void bt_createFromActiveDossier_Click(object sender, EventArgs e)
        {
            MessageBox.Show("INFO: Das Dossier wird aus den Daten des aktiven Dossiers erstellt.", "Information");
            frm_newApplication createNewForm = new frm_newApplication(this, tb_selectedDossier.Text);
            createNewForm.Show();

        }

        //Oeffnet aus der Lebenslauf-Werkstatt das odt mit dem Selbstbeschrieb.
        //TEST: PASS
        private void bt_adoptVita_Click(object sender, EventArgs e)
        {
            string vitaWorkbenchPath = SetupHandling.getPathToVitaWorkbenchFromSetup();
            string vitaTemplatedName = SetupHandling.getNameOfFirstVitaPageFromSetup();
            string filePath = Path.Combine(vitaWorkbenchPath, vitaTemplatedName);

            MessageBox.Show("Achtung! ODT-Dokument nach der Bearbeitung ohne zu Speichern "
                                + "als \"01.pdf\" in folgendes Verzeichnis exportierten: \n" 
                                + SetupHandling.getPathToVitaWorkbenchFromSetup());
            Process.Start(filePath);
        }

        //Oeffnet das PDFsam Basic Tool fuer das Mergen der PDFs.
        private void bt_createCurriculum_Click(object sender, EventArgs e)
        {
            if (isActiveDossierEditable())
            {
                string pathToMergeScript = Path.Combine(SetupHandling.getPathToAu3ScriptsFromSetup(), SetupHandling.getNameOfPdfsMergeScriptAu3FromSetup());
                string pathToMergeTool = SetupHandling.getPathToMergetoolForPDFsFromSetup();
                string dossierPath = Path.Combine(SetupHandling.getPathToApplicationRepositoryFromSetup(), tb_selectedDossier.Text);
                string pathToVitaWorkbench = SetupHandling.getPathToVitaWorkbenchFromSetup();
                string creationDateTag = DossierMetadataHandling.getEntryDateFrom(dossierPath);

                //AutoIt3.exe myScript.au3 param1 "This is a string parameter" 99 
                string paramString = AAConst.DOUBLE_QUOTE + pathToMergeScript + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + pathToMergeTool + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + dossierPath + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + pathToVitaWorkbench + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + creationDateTag + AAConst.DOUBLE_QUOTE;

                Process.Start(SetupHandling.getPathToAu3ScriptingtoolFromSetup(), paramString);
            }
        }

        //Erstellt eine Email-Vorlage
        //Die Textdateien muessen in unicode gespeichert sein.
        private void bt_createEmail_Click(object sender, EventArgs e)
        {
            if (isActiveDossierEditable())
            {

                if (tb_selectedDossier.Text.Equals(""))
                {
                    MessageBox.Show("Error: Kein aktives Dossier angegeben. Erzeugung abgebrochen.");
                    return;
                }

                string dossierPath = Path.Combine(SetupHandling.getPathToApplicationRepositoryFromSetup(), tb_selectedDossier.Text);
                string pathToEmailTemplates = SetupHandling.getPathToEmailTemplatesFromSetup();
                string[] emailTemplates = Directory.GetFiles(pathToEmailTemplates);

                string aEmailTemplate = Path.Combine(pathToEmailTemplates, emailTemplates[0]);
                string strTmpl_email = File.ReadAllText(aEmailTemplate, Encoding.GetEncoding(AAConst.UNICODE));

                //Das Briefdatum muss erstellt werden.
                string entryDateChron = DossierMetadataHandling.getEntryDateFrom(dossierPath);
                DateTime myDate = DateTime.ParseExact(entryDateChron, AAConst.DATESTAMP_FORMAT,
                                           System.Globalization.CultureInfo.InvariantCulture);
                string entryDate = myDate.ToString(AAConst.LETTERDATE_FORMAT);

                //Die uebrigen Werten holen
                string emailAddress = DossierMetadataHandling.getEmailAddressFrom(dossierPath);
                string jobTitle = DossierMetadataHandling.getJobTitleFrom(dossierPath);
                string jobPortal = DossierMetadataHandling.getJobPortalFrom(dossierPath);
                string salutation = DossierMetadataHandling.getSalutationFrom(dossierPath);
                string surname = DossierMetadataHandling.getSurnameFrom(dossierPath);
                string forename = DossierMetadataHandling.getForenameFrom(dossierPath);
                string company = DossierMetadataHandling.getCompanyFrom(dossierPath);
                string streetNr = DossierMetadataHandling.getStreetFrom(dossierPath);
                string plzTown = DossierMetadataHandling.getTownFrom(dossierPath);
                string emailReference = DossierMetadataHandling.getEmailReferenceFrom(dossierPath);

                //Die Tags in den template-strings muessen noch ersetzt werden.
                string str_email = AAUtilities.replaceTemplateTags(strTmpl_email, emailAddress, jobTitle, jobPortal, entryDate,
                                                salutation, surname, forename, company, streetNr, plzTown, emailReference,"","","", "", "", "");

                str_email = str_email.Replace("\n", ""); //\r reicht als Linefeed

                Process.Start(SetupHandling.getPathToEmptyODTFromSetup());
                Thread.Sleep(15000);
                Process p = Process.GetProcessesByName(AAConst.PROC_SOFFICE).FirstOrDefault();
                if (p != null)
                {
                    IntPtr h = p.MainWindowHandle;
                    SetForegroundWindow(h);
                    //Das Makro muss aktiviert werden.
                    SendKeys.SendWait("{LEFT}{ENTER}");
                    SendKeys.SendWait(str_email);
                }
                else
                {
                    MessageBox.Show("Error: " + AAConst.PROC_SOFFICE + " ist nicht geöffnet.");
                }
            }
        }

        //Erstellt eine Briefvorlage.
        //Die Textdateien muessen in unicode gespeichert sein.
        private void bt_createLetter_Click(object sender, EventArgs e)
        {
            if (isActiveDossierEditable())
            {
                string dossierPath = Path.Combine(SetupHandling.getPathToApplicationRepositoryFromSetup(), tb_selectedDossier.Text);
                frm_chooseMotivationTmpl chooseMotiveTmpl = new frm_chooseMotivationTmpl(this, SetupHandling.getPathToMotivationLetterTemplatesFromSetup(), dossierPath, SetupHandling.getPathToEmptyODTFromSetup());
                chooseMotiveTmpl.Show();
            }
        }


        //Die angegebenen PDFs ins Dossier kopieren.
        private void bt_copyPDFs_Click(object sender, EventArgs e)
        {
            if (isActiveDossierEditable())
            {
                if (tb_selectedDossier.Text.Equals(""))
                {
                    MessageBox.Show("Error: Kein aktives Dossier angegeben. Kopieren abgebrochen.");
                    return;
                }

                string dossierPath = Path.Combine(SetupHandling.getPathToApplicationRepositoryFromSetup(), tb_selectedDossier.Text);

                if (SetupHandling.getPathToExtantPdfsFromSetup().Equals(""))
                {
                    MessageBox.Show("Error: Kein PDF Repository angegeben. Kopieren abgebrochen.");
                    return;
                }

                string[] pdfFiles = Directory.GetFiles(SetupHandling.getPathToExtantPdfsFromSetup());

                string targetPath;

                foreach (string sourceFile in pdfFiles)
                {
                    string fileName = Path.GetFileName(sourceFile);
                    targetPath = Path.Combine(dossierPath, fileName);
                    File.Copy(sourceFile, targetPath, true);
                }
            }
        }


        private void loadSetup()
        {
            tb_PathToApplicationRepository.Text = SetupHandling.getPathToApplicationRepositoryFromSetup();
            tb_PathToVitaWorkbench.Text = SetupHandling.getPathToVitaWorkbenchFromSetup();
            tb_NameOfFirstVitaPage.Text = SetupHandling.getNameOfFirstVitaPageFromSetup();
            tb_PathToMergetoolForPDFs.Text = SetupHandling.getPathToMergetoolForPDFsFromSetup();
            tb_NameOfOdtToPdfExportScriptAu3.Text = SetupHandling.getNameOfOdtToPdfExportScriptAu3FromSetup();
            tb_NameOfPdfsMergeScriptAu3.Text = SetupHandling.getNameOfPdfsMergeScriptAu3FromSetup();
            tb_PathToEmailTemplates.Text = SetupHandling.getPathToEmailTemplatesFromSetup();
            tb_PathToMotivationLetterTemplates.Text = SetupHandling.getPathToMotivationLetterTemplatesFromSetup();
            tb_PathToEmptyODT.Text = SetupHandling.getPathToEmptyODTFromSetup();
            tb_NameOfOdtsSaveScriptAu3.Text = SetupHandling.getNameOfOdtsSaveScriptAu3FromSetup();
            tb_PathToExtantPdfs.Text = SetupHandling.getPathToExtantPdfsFromSetup();
            tb_PathToAu3Scripts.Text = SetupHandling.getPathToAu3ScriptsFromSetup();
            tb_PathToAu3Scriptingtool.Text = SetupHandling.getPathToAu3ScriptingtoolFromSetup();
            tb_PathToQuestionaryTemplates.Text = SetupHandling.getPathToQuestionaryTemplatesFromSetup();
            tb_PathToPreparedQaODT.Text = SetupHandling.getPathToPreparedQaODTFromSetup();
            tb_PathToPhoneCallNotesTemplates.Text = SetupHandling.getPathToPhoneCallNotesTemplatesFromSetup();
        }

        private void bt_saveSetup_Click(object sender, EventArgs e)
        {

            string pathToApplicationRepository = tb_PathToApplicationRepository.Text;
            string pathToVitaWorkbench = tb_PathToVitaWorkbench.Text;
            string nameOfFirstVitaPage = tb_NameOfFirstVitaPage.Text;
            string pathToMergetoolForPDFs = tb_PathToMergetoolForPDFs.Text;
            string nameOfOdtToPdfExportScriptAu3 = tb_NameOfOdtToPdfExportScriptAu3.Text;
            string nameOfPdfsMergeScriptAu3 = tb_NameOfPdfsMergeScriptAu3.Text;
            string pathToEmailTemplates = tb_PathToEmailTemplates.Text;
            string pathToMotivationLetterTemplates = tb_PathToMotivationLetterTemplates.Text;
            string pathToEmptyODT = tb_PathToEmptyODT.Text;
            string nameOfOdtsSaveScriptAu3 = tb_NameOfOdtsSaveScriptAu3.Text;
            string pathToExtantPdfs = tb_PathToExtantPdfs.Text;
            string pathToAu3Scripts = tb_PathToAu3Scripts.Text;
            string pathToAu3Scriptingtool = tb_PathToAu3Scriptingtool.Text;
            string pathToQuestionaryTemplates = tb_PathToQuestionaryTemplates.Text;
            string pathToPreparedQaODT = tb_PathToPreparedQaODT.Text;
            string pathToPhoneCallNotesTemplates = tb_PathToPhoneCallNotesTemplates.Text;


            SetupHandling.saveSetup(pathToApplicationRepository, pathToVitaWorkbench, nameOfFirstVitaPage, 
                                    pathToMergetoolForPDFs, nameOfOdtToPdfExportScriptAu3, nameOfPdfsMergeScriptAu3, 
                                    pathToEmailTemplates, pathToMotivationLetterTemplates, pathToEmptyODT, 
                                    nameOfOdtsSaveScriptAu3, pathToExtantPdfs, pathToAu3Scripts, pathToAu3Scriptingtool,
                                    pathToQuestionaryTemplates, pathToPreparedQaODT, pathToPhoneCallNotesTemplates);

        }


        private void cb_DossierSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cb_DossierSelector.SelectedIndex;
            if (cb_DossierSelector.SelectedItem == null) {
                MessageBox.Show("Warn: Kein selektiertes Element im Dossier Selektor vorhanden. Vorgang abgebrochen.");
            } else {
                string dossierNr = (cb_DossierSelector.SelectedItem as DossierHandling.ComboBoxItem).DossierNr;
                string actualDossierPath = DossierHandling.getRepositoryPathByNr(SetupHandling.getPathToApplicationRepositoryFromSetup(), dossierNr);
                setActiveDossierPathPart(actualDossierPath);
            }
        }

        private void cb_AmorphicDossierSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cb_AmorphicDossierSelector.SelectedIndex;

            if (cb_AmorphicDossierSelector.SelectedItem == null)
            {
                MessageBox.Show("Warn: Kein selektiertes Element im amorphischen Dossier Selector vorhanden. Vorgang abgebrochen.");
            }
            else
            {
                string dossierNr = (cb_AmorphicDossierSelector.SelectedItem as DossierHandling.ComboBoxItem).DossierNr;
                string actualDossierPath = DossierHandling.getRepositoryPathByNr(SetupHandling.getPathToApplicationRepositoryFromSetup(), dossierNr);
                setActiveDossierPathPart(actualDossierPath);
            }
        }

        public void loadDossierSelector(bool selectDefault)
        {
            cb_DossierSelector.Items.Clear();
            cb_AmorphicDossierSelector.Items.Clear();

            DossierHandling.updateDossierLists(SetupHandling.getPathToApplicationRepositoryFromSetup());
            kickOnDossierListsSorting();
            List<DossierHandling.ComboBoxItem> itemListAllDossier = DossierHandling.getAllDossierListForComboBox();
            List<DossierHandling.ComboBoxItem> itemListAmorphicDossier = DossierHandling.getAmorphicDossierListForComboBox();
            
            foreach (DossierHandling.ComboBoxItem item in itemListAllDossier)
            {
                cb_DossierSelector.Items.Add(item);
            }
            foreach (DossierHandling.ComboBoxItem item in itemListAmorphicDossier)
            {
                cb_AmorphicDossierSelector.Items.Add(item);
            }
            if (selectDefault)
            {
                try
                {
                    //cb_DossierSelector.SelectedIndex = 0;
                    cb_AmorphicDossierSelector.SelectedIndex = 0;
                } catch(Exception ex) {
                    MessageBox.Show("ERROR: loadDossierSelector(): " + ex.Message, "Fehlermeldung");
                }
            }
        }


        private void rb_SortAfterDate_CheckedChanged(object sender, EventArgs e)
        {
            loadDossierSelector(true);
        }

        private void rb_SortAfterCompany_CheckedChanged(object sender, EventArgs e)
        {
            loadDossierSelector(true);
        }

        private void rb_SortAfterLocation_CheckedChanged(object sender, EventArgs e)
        {
            loadDossierSelector(true);
        }

        private void rb_SortAfterJobTitle_CheckedChanged(object sender, EventArgs e)
        {
            loadDossierSelector(true);
        }

        private void rb_SortAfterDossierState_CheckedChanged(object sender, EventArgs e)
        {
            loadDossierSelector(true);
        }

        private void rb_SortAfterJobpostingState_CheckedChanged(object sender, EventArgs e)
        {
            loadDossierSelector(true);
        }

        public void kickOnDossierListsSorting()
        {
            if (rb_SortAfterDate.Checked)
            {
                DossierHandling.sortDossierListAfterDate();
            }
            if (rb_SortAfterCompany.Checked)
            {
                //MessageBox.Show("INFO: kickOnDossierListsSorting(): sort after company is checked.", "INFO");
                DossierHandling.sortDossierListAfterCompany();
            }
            if (rb_SortAfterLocation.Checked)
            {
                DossierHandling.sortDossierListAfterLocation();
            }
            if (rb_SortAfterJobTitle.Checked)
            {
                DossierHandling.sortDossierListAfterJobTitle();
            }
            if (rb_SortAfterDossierState.Checked)
            {
                DossierHandling.sortDossierListAfterDossierState();
            }
            if (rb_SortAfterJobpostingState.Checked)
            {
                DossierHandling.sortDossierListAfterJobpostingState();
            }
        }

        private void bt_deleteActiveDossier_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Wollen sie das Dossier " + tb_selectedDossier.Text + " wirklich löschen?", "Rückfrage",
                                                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dialogResult.Equals(DialogResult.OK))
            {
                try
                {
                    DossierHandling.deleteDossier(SetupHandling.getPathToApplicationRepositoryFromSetup(), tb_selectedDossier.Text);
                    loadDossierSelector(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: bt_deleteActiveDossier_Click(): " + ex.Message, "Fehlermeldung");
                }
            }
        }


        private void bt_executeTestbench_Click(object sender, EventArgs e)
        {
/*
            string dossierPath = Path.Combine(tb_PathToDossiers.Text, tb_selectedDossier.Text);
            frm_chooseMotivationTmpl chooseMotiveTmpl = new frm_chooseMotivationTmpl(this, tb_PathToMotivationTemplates.Text, dossierPath, tb_PathToEmptyODT.Text);
            chooseMotiveTmpl.Show();
  */
            MessageBox.Show("Das Resultat der Abfrage ist: " + isActiveDossierEditable());
             
        }

        private void bt_changeDossierState_Click(object sender, EventArgs e)
        {
            frm_changeDossierState dossierStateChangerForm = new frm_changeDossierState(this, tb_selectedDossier.Text);
            dossierStateChangerForm.Show();
        }

        private void bt_changeJobpostingState_Click(object sender, EventArgs e)
        {
            frm_changeJobpostingState jobpostingStateChangerForm = new frm_changeJobpostingState(this, tb_selectedDossier.Text);
            jobpostingStateChangerForm.Show();
        }

        private void bt_exportVitaPage_Click(object sender, EventArgs e)
        {
            if (isActiveDossierEditable())
            {
                string pathToExportScript = Path.Combine(SetupHandling.getPathToAu3ScriptsFromSetup(), SetupHandling.getNameOfOdtToPdfExportScriptAu3FromSetup());
                string pathToVitaWorkbench = SetupHandling.getPathToVitaWorkbenchFromSetup();

                //AutoIt3.exe myScript.au3 param1 "This is a string parameter" 99 
                string paramString = AAConst.DOUBLE_QUOTE + pathToExportScript + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + pathToVitaWorkbench + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + AAConst.COVER_01 + AAConst.DOUBLE_QUOTE;
                                    //+ AAConst.SINGLE_SPACE;

                Process.Start(SetupHandling.getPathToAu3ScriptingtoolFromSetup(), paramString);
            }
        }

        private void bt_saveEmail_Click(object sender, EventArgs e)
        {
            if (isActiveDossierEditable())
            {
                string pathToSaveScript = Path.Combine(SetupHandling.getPathToAu3ScriptsFromSetup(), SetupHandling.getNameOfOdtsSaveScriptAu3FromSetup());
                string dossierPath = Path.Combine(SetupHandling.getPathToApplicationRepositoryFromSetup(), tb_selectedDossier.Text);
                string creationDateTag = DossierMetadataHandling.getEntryDateFrom(dossierPath);

                //AutoIt3.exe myScript.au3 param1 "This is a string parameter" 99 
                string paramString = AAConst.DOUBLE_QUOTE + pathToSaveScript + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + AAConst.EMAIL_ANSCHREIBEN + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + dossierPath + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + creationDateTag + AAConst.DOUBLE_QUOTE;

                Process.Start(SetupHandling.getPathToAu3ScriptingtoolFromSetup(), paramString);
            }
        }

        private void bt_saveLetter_Click(object sender, EventArgs e)
        {
            if (isActiveDossierEditable())
            {
                string pathToSaveScript = Path.Combine(SetupHandling.getPathToAu3ScriptsFromSetup(), SetupHandling.getNameOfOdtsSaveScriptAu3FromSetup());
                string dossierPath = Path.Combine(SetupHandling.getPathToApplicationRepositoryFromSetup(), tb_selectedDossier.Text);
                string creationDateTag = DossierMetadataHandling.getEntryDateFrom(dossierPath);

                //AutoIt3.exe myScript.au3 param1 "This is a string parameter" 99 
                string paramString = AAConst.DOUBLE_QUOTE + pathToSaveScript + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + AAConst.MOTIVATIONSSCHREIBEN + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + dossierPath + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + creationDateTag + AAConst.DOUBLE_QUOTE;

                Process.Start(SetupHandling.getPathToAu3ScriptingtoolFromSetup(), paramString);
            }
        }

        private void bt_exportLetter_Click(object sender, EventArgs e)
        {
            if (isActiveDossierEditable())
            {
                string pathToExportScript = Path.Combine(SetupHandling.getPathToAu3ScriptsFromSetup(), SetupHandling.getNameOfOdtToPdfExportScriptAu3FromSetup());
                string dossierPath = Path.Combine(SetupHandling.getPathToApplicationRepositoryFromSetup(), tb_selectedDossier.Text);
                string creationDateTag = DossierMetadataHandling.getEntryDateFrom(dossierPath);
                string fileName = creationDateTag + " - " + AAConst.MOTIVATIONSSCHREIBEN;

                //AutoIt3.exe myScript.au3 param1 "This is a string parameter" 99 
                string paramString = AAConst.DOUBLE_QUOTE + pathToExportScript + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + dossierPath + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + fileName + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE;

                Process.Start(SetupHandling.getPathToAu3ScriptingtoolFromSetup(), paramString);
            }
        }

        private void tabControl1_Selected(object sender, EventArgs e)
        {
            //MessageBox.Show("INFO: Tab wurde selektiert: ", "Information");
            loadSetup();
        }

        private void bt_browsePathToApplicationRepository_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog selectFolderDialog = new FolderBrowserDialog();
            if (selectFolderDialog.ShowDialog() == DialogResult.OK)
            {
                tb_PathToApplicationRepository.Text = selectFolderDialog.SelectedPath;
            }
            selectFolderDialog.Dispose();
        }

        private void bt_browsePathToVitaWorkbench_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("INFO: bt_browsePathToVitaWorkbench_Click() ausgefuehrt. ", "Information");
            FolderBrowserDialog selectFolderDialog = new FolderBrowserDialog();
            if (selectFolderDialog.ShowDialog() == DialogResult.OK)
            {
                tb_PathToVitaWorkbench.Text = selectFolderDialog.SelectedPath;
            }
            selectFolderDialog.Dispose();
        }


        private void bt_browsePathToMergetoolForPDFs_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectFileDialog = new OpenFileDialog();
            if (selectFileDialog.ShowDialog() == DialogResult.OK)
            {
                tb_PathToMergetoolForPDFs.Text = selectFileDialog.FileName;
            }
            selectFileDialog.Dispose();
        }

        private void bt_browsePathToEmailTemplates_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog selectFolderDialog = new FolderBrowserDialog();
            if (selectFolderDialog.ShowDialog() == DialogResult.OK)
            {
                tb_PathToEmailTemplates.Text = selectFolderDialog.SelectedPath;
            } 
        }
        
        private void bt_browsePathToMotivationLetterTemplates_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog selectFolderDialog = new FolderBrowserDialog();
            if (selectFolderDialog.ShowDialog() == DialogResult.OK)
            {
                tb_PathToMotivationLetterTemplates.Text = selectFolderDialog.SelectedPath;
            } 
        }

        private void bt_browsePathToEmptyODT_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectFileDialog = new OpenFileDialog();
            if (selectFileDialog.ShowDialog() == DialogResult.OK)
            {
                tb_PathToEmptyODT.Text = selectFileDialog.FileName;
            } 
        }

        private void bt_browsePathToExtantPdfs_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog selectFolderDialog = new FolderBrowserDialog();
            if (selectFolderDialog.ShowDialog() == DialogResult.OK)
            {
                tb_PathToExtantPdfs.Text = selectFolderDialog.SelectedPath;
            } 
        }

        private void bt_browsePathToAu3Scripts_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog selectFolderDialog = new FolderBrowserDialog();
            if (selectFolderDialog.ShowDialog() == DialogResult.OK)
            {
                tb_PathToAu3Scripts.Text = selectFolderDialog.SelectedPath;
            } 
        }

        private void bt_browsePathToAu3Scriptingtool_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectFileDialog = new OpenFileDialog();
            if (selectFileDialog.ShowDialog() == DialogResult.OK)
            {
                tb_PathToAu3Scriptingtool.Text = selectFileDialog.FileName;
            } 

        }

        private void cbx_deleteAllowed_CheckedChanged(object sender, EventArgs e)
        {
            bt_deleteActiveDossier.Enabled = cbx_deleteAllowed.Checked;
        }

        private void cbx_allowTestbench_CheckedChanged(object sender, EventArgs e)
        {
            bt_executeTestbench.Enabled = cbx_allowTestbench.Checked;
        }

        private void bt_changeDossierDate_Click(object sender, EventArgs e)
        {
            if (isActiveDossierEditable())
            {
                frm_changeDossierData createNewForm = new frm_changeDossierData(this, tb_selectedDossier.Text, ChangeModus.CreationDate);
                createNewForm.Show();
            }
        }

        private void bt_changeJobTitle_Click(object sender, EventArgs e)
        {
            frm_changeDossierData createNewForm = new frm_changeDossierData(this, tb_selectedDossier.Text, ChangeModus.JobDescription);
            createNewForm.Show();
        }

        private void bt_changeCompanyName_Click(object sender, EventArgs e)
        {
            frm_changeDossierData createNewForm = new frm_changeDossierData(this, tb_selectedDossier.Text, ChangeModus.CompanyName);
            createNewForm.Show();
        }

        private void bt_changeDossierInfos_Click(object sender, EventArgs e)
        {
            frm_changeDossierData createNewForm = new frm_changeDossierData(this, tb_selectedDossier.Text, ChangeModus.Informations);
            createNewForm.Show();
        }

        private void bt_exportAllToCSV_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("INFO: Die Informationen aller Dossiers sollen in ein CVS exportiert werden: ", "Information");
            try
            {
                string actDate = DateTime.Now.ToString(AAConst.DATESTAMP_FORMAT);
                string exportFileName = actDate + " - " + AAConst.EXPORT_ALL_DOSSIERS + AAConst.POSTFIX_CSV;

                string dossierRepositoryPath = SetupHandling.getPathToApplicationRepositoryFromSetup();

                string exportFilePathString = Path.Combine(dossierRepositoryPath, exportFileName);

                if (File.Exists(exportFilePathString))
                {
                    File.Delete(exportFilePathString);
                }

                StreamWriter sw = File.CreateText(exportFilePathString);
                
                List<string> allDossiers = DossierHandling.getAllDossiers(dossierRepositoryPath);
                foreach (string dossierPath in allDossiers)
                {
                    string number = DossierMetadataHandling.getNumberFrom(dossierPath);
                    string entryDate = DossierMetadataHandling.getEntryDateFrom(dossierPath);
                    string jobTitle = DossierMetadataHandling.getJobTitleFrom(dossierPath);
                    string company = DossierMetadataHandling.getCompanyFrom(dossierPath);
                    string street = DossierMetadataHandling.getStreetFrom(dossierPath);
                    string town = DossierMetadataHandling.getTownFrom(dossierPath);
                    string salutation = DossierMetadataHandling.getSalutationFrom(dossierPath);
                    string forename = DossierMetadataHandling.getForenameFrom(dossierPath);
                    string surname = DossierMetadataHandling.getSurnameFrom(dossierPath);
                    string emailAddress = DossierMetadataHandling.getEmailAddressFrom(dossierPath);
                    string emailReference = DossierMetadataHandling.getEmailReferenceFrom(dossierPath);
                    string jobPortal = DossierMetadataHandling.getJobPortalFrom(dossierPath);
                    string dossierState = DossierMetadataHandling.getDossierStateFrom(dossierPath).ToString();
                    string historyInfos = DossierMetadataHandling.getHistoryInfosFrom(dossierPath).ToString();
                    string jobpostingState = DossierMetadataHandling.getJobpostingStateFrom(dossierPath).ToString();

                    string csvEntry =   number + AAConst.CSV_SEPARATOR
                                      + entryDate + AAConst.CSV_SEPARATOR
                                      + jobTitle + AAConst.CSV_SEPARATOR
                                      + company + AAConst.CSV_SEPARATOR
                                      + street + AAConst.CSV_SEPARATOR
                                      + town + AAConst.CSV_SEPARATOR
                                      + salutation + AAConst.CSV_SEPARATOR
                                      + forename + AAConst.CSV_SEPARATOR
                                      + surname + AAConst.CSV_SEPARATOR
                                      + emailAddress + AAConst.CSV_SEPARATOR
                                      + emailReference + AAConst.CSV_SEPARATOR
                                      + jobPortal + AAConst.CSV_SEPARATOR
                                      + dossierState + AAConst.CSV_SEPARATOR
                                      + jobpostingState + AAConst.CSV_SEPARATOR
                                      + historyInfos;
                    sw.WriteLine(csvEntry);
                }
                sw.Close();
                MessageBox.Show("INFO: All dossiers successfully exported to: " + exportFilePathString, "Information");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: bt_exportAllToCSV_Click(): " + ex.Message, "Fehlermeldung");
            }
        }

        //Gibt true zurueck wenn das aktive Dossier im Status Erzeugt ist oder der Benutzer eine Aenderung bewusst zulaesst.
        private bool isActiveDossierEditable()
        {
            string dossierPath = Path.Combine(SetupHandling.getPathToApplicationRepositoryFromSetup(), tb_selectedDossier.Text);
            DossierState status = DossierMetadataHandling.getDossierStateFrom(dossierPath);

            if (!status.Equals(DossierState.Erzeugt))
            {
                DialogResult result = MessageBox.Show("Das ausgewählte Dossier wurde bereits abgeschickt. Soll der Vorgang abgebrochen werden?",
                                                        "Warnung", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (result.Equals(DialogResult.Yes))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }





        private void bt_createQuestionary_Click(object sender, EventArgs e)
        {
            if (isActiveDossierEditable())
            {
                MessageBox.Show("INFO: Der Fragebogen wird mit den Daten des aktiven Dossiers initialisiert.", "Information");
                frm_newQuestionary createNewQA = new frm_newQuestionary(this, tb_selectedDossier.Text);
                createNewQA.Show();
            }
        }

        private void bt_saveQuestionary_Click(object sender, EventArgs e)
        {
            if (isActiveDossierEditable())
            {
                string pathToSaveScript = Path.Combine(SetupHandling.getPathToAu3ScriptsFromSetup(), SetupHandling.getNameOfOdtsSaveScriptAu3FromSetup());
                string dossierPath = Path.Combine(SetupHandling.getPathToApplicationRepositoryFromSetup(), tb_selectedDossier.Text);
                
                string creationDateTag = DateTime.Today.ToString(AAConst.DATESTAMP_FORMAT, System.Globalization.CultureInfo.InvariantCulture);

                //AutoIt3.exe myScript.au3 param1 "This is a string parameter" 99 
                string paramString = AAConst.DOUBLE_QUOTE + pathToSaveScript + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + AAConst.FRAGEBOGEN + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + dossierPath + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + creationDateTag + AAConst.DOUBLE_QUOTE;

                Process.Start(SetupHandling.getPathToAu3ScriptingtoolFromSetup(), paramString);
            }
        }

        private void bt_browsePathToQuestionaryTemplates_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("INFO: bt_browsePathToQuestionaryTemplates_Click() ausgefuehrt. ", "Information");
            FolderBrowserDialog selectFolderDialog = new FolderBrowserDialog();
            if (selectFolderDialog.ShowDialog() == DialogResult.OK)
            {
                tb_PathToQuestionaryTemplates.Text = selectFolderDialog.SelectedPath;
            }
            selectFolderDialog.Dispose();
        }

        private void bt_browsePathToPreparedQaODT_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("INFO: bt_browsePathToPreparedQaODT_Click() ausgefuehrt. ", "Information");
            FolderBrowserDialog selectFolderDialog = new FolderBrowserDialog();
            if (selectFolderDialog.ShowDialog() == DialogResult.OK)
            {
                tb_PathToPreparedQaODT.Text = selectFolderDialog.SelectedPath;
            }
            selectFolderDialog.Dispose();
        }

        private void bt_createPhoneCallNotes_Click(object sender, EventArgs e)
        {
            if (isActiveDossierEditable())
            {
                MessageBox.Show("INFO: Der Fragebogen wird mit den Daten des aktiven Dossiers initialisiert.", "Information");

                //string dossierPath = Path.Combine(SetupHandling.getPathToApplicationRepositoryFromSetup(), tb_selectedDossier.Text);
                frm_newPhoneCallNotes createNewPCNotes = new frm_newPhoneCallNotes(this, SetupHandling.getPathToPhoneCallNotesTemplatesFromSetup(), tb_selectedDossier.Text, SetupHandling.getPathToEmptyODTFromSetup());
                
                //SetupHandling.getPathToMotivationLetterTemplatesFromSetup(), dossierPath, SetupHandling.getPathToEmptyODTFromSetup()

                createNewPCNotes.Show();
            }
        }

        private void bt_savePhoneCallNotes_Click(object sender, EventArgs e)
        {   
            if (isActiveDossierEditable())
            {
                string pathToSaveScript = Path.Combine(SetupHandling.getPathToAu3ScriptsFromSetup(), SetupHandling.getNameOfOdtsSaveScriptAu3FromSetup());
                string dossierPath = Path.Combine(SetupHandling.getPathToApplicationRepositoryFromSetup(), tb_selectedDossier.Text);

                string creationDateTag = DateTime.Today.ToString(AAConst.DATESTAMP_FORMAT, System.Globalization.CultureInfo.InvariantCulture);

                //AutoIt3.exe myScript.au3 param1 "This is a string parameter" 99 
                string paramString = AAConst.DOUBLE_QUOTE + pathToSaveScript + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + AAConst.ANRUFNOTIZEN + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + dossierPath + AAConst.DOUBLE_QUOTE + AAConst.SINGLE_SPACE
                                    + AAConst.DOUBLE_QUOTE + creationDateTag + AAConst.DOUBLE_QUOTE;

                Process.Start(SetupHandling.getPathToAu3ScriptingtoolFromSetup(), paramString);
            }
            
        }

        private void bt_browsePathToPhoneCallNotesTemplates_Click(object sender, EventArgs e)
        {
            MessageBox.Show("INFO: bt_browsePathToPhoneCallNotesTemplates_Click() ausgefuehrt. ", "Information");
            
            FolderBrowserDialog selectFolderDialog = new FolderBrowserDialog();
            if (selectFolderDialog.ShowDialog() == DialogResult.OK)
            {
                tb_PathToPhoneCallNotesTemplates.Text = selectFolderDialog.SelectedPath;
            }
            selectFolderDialog.Dispose();
            
        }

        private void gb_NewApplicationCreation_Enter(object sender, EventArgs e)
        {

        }
















    }
}
