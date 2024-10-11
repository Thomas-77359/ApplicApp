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
    public partial class frm_chooseMotivationTmpl : Form
    {

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        private frm_ApplicApp myParentApp;
        private string pathToMoveLetterTemplates;
        private string dossierPath;
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


        public frm_chooseMotivationTmpl(frm_ApplicApp parent, string pathToTemplates, string activeDossierPath, string pathToEmptyODTFile)
        {
            pathToMoveLetterTemplates = pathToTemplates;
            myParentApp = parent;
            dossierPath = activeDossierPath;
            pathToEmptyODT = pathToEmptyODTFile;
            InitializeComponent();
            updateCBTemplateChooser();
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cb_TemplateChooser_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_TemplatePreview.Clear();
            string tmplPath = (cb_TemplateChooser.SelectedItem as ComboBoxItem).filePath;
            tb_TemplatePreview.Text = File.ReadAllText(tmplPath, Encoding.GetEncoding(AAConst.UNICODE));

        }

        private void updateCBTemplateChooser()
        {
            string[] motivTemplates = Directory.GetFiles(pathToMoveLetterTemplates);
            ComboBoxItem item = null;

            foreach(string path in motivTemplates)
            {
                item = new ComboBoxItem();
                item.fileName = Path.GetFileName(path);
                item.filePath = path;
                cb_TemplateChooser.Items.Add(item);
            }

            cb_TemplateChooser.SelectedIndex = 0;
        }

        private void bt_choose_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Info: bt_choose_Click(): Die neue ODT-Datei nur mit \"Speichern unter\" sichern, allenfalls als PDF exportieren.", "Information");
            
            string tmplPath = (cb_TemplateChooser.SelectedItem as ComboBoxItem).filePath;
            string strTmpl_motiv = File.ReadAllText(tmplPath, Encoding.GetEncoding(AAConst.UNICODE));

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
            string str_motiv = AAUtilities.replaceTemplateTags(strTmpl_motiv, emailAddress, jobTitle, jobPortal, entryDate,
                                            salutation, surname, forename, company, streetNr, plzTown, emailReference, "", "", "", "", "", "");

            str_motiv = str_motiv.Replace("\n", ""); //Allfaellige Zeilenumbrueche muessen entfernt werden.

            Process.Start(pathToEmptyODT);
            Thread.Sleep(15000);
            //TODO: Mehrere Versuche machen, siehe KB sendKeys
            Process p = Process.GetProcessesByName(AAConst.PROC_SOFFICE).FirstOrDefault();
            if (p != null)
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
                //Das Makro muss aktiviert werden.
                SendKeys.SendWait("{LEFT}{ENTER}");
                //Wenn es hier zu Fehlern kommt hat es meistens mit ausgeschriebenen Sonderzeichen zu tun.
                SendKeys.SendWait(str_motiv);
            }
            else
            {
                MessageBox.Show("Error: " + AAConst.PROC_SOFFICE + " ist nicht geöffnet.");
            }

            Close();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }







    }
}
