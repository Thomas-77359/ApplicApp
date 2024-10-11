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
    public partial class frm_newQuestionary : Form
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        private frm_ApplicApp myParentApp;
        private string dossierPathString;

        public frm_newQuestionary(frm_ApplicApp parent, string activeDossierPartPath)
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
            dossierPathString = Path.Combine(dossierRepositoryPath, activeDossierPartPath);

            if(DossierMetadataHandling.hasMetadataFile(dossierPathString))
            {
                if (DossierMetadataHandling.isMetadataFileCorrect(dossierPathString))
                {
                    //string dossierCreationDate = DossierMetadataHandling.getEntryDateFrom(dossierPathString);
                    
                    //DateTime creationDate = DateTime.ParseExact(dossierCreationDate, AAConst.DATESTAMP_FORMAT, System.Globalization.CultureInfo.InvariantCulture);
                    //cal_MeetingDate.SetDate(creationDate);

                    tb_JobDescription.Text = DossierMetadataHandling.getJobTitleFrom(dossierPathString);
                    tb_CompanyName.Text = DossierMetadataHandling.getCompanyFrom(dossierPathString);
                    tb_Town.Text = DossierMetadataHandling.getTownFrom(dossierPathString);
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

                if (createQuestionary())
                {
                    this.Close();
                }
 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: bt_Create_Click(): Exception aufgetreten: " + ex.Message, "Exception");
            }            
        }

        //Erstellt eine Fragebogen-Vorlage
        //Die Textdateien muessen in unicode gespeichert sein.
        private bool createQuestionary()
        {

            if (this.dossierPathString.Equals(""))
            {
                MessageBox.Show("Error: Kein aktives Dossier angegeben. Erzeugung abgebrochen.");
                return false;
            }
                
            string pathToQuestionaryTemplates = SetupHandling.getPathToQuestionaryTemplatesFromSetup();
            string[] questionaryTemplates = Directory.GetFiles(pathToQuestionaryTemplates);

            string aQuestionaryTemplate = Path.Combine(pathToQuestionaryTemplates, questionaryTemplates[0]);
            string strTmpl_questionary = File.ReadAllText(aQuestionaryTemplate, Encoding.GetEncoding(AAConst.UNICODE));

            //Das Briefdatum muss erstellt werden.
            DateTime meetingDate = cal_MeetingDate.SelectionStart;
            string str_meetingDate = meetingDate.ToString(AAConst.DATESTAMP_CH_FORMAT, System.Globalization.CultureInfo.InvariantCulture);

            
            //Die uebrigen Werten holen
            string jobTitle = tb_JobDescription.Text;
            string company = tb_CompanyName.Text;
            string plzTown = tb_Town.Text;

            string participants = AAConst.ICH + AAConst.CSV_SEPARATOR;
            if (!tb_Participant1.Text.Equals(""))
            {
                participants = participants + AAConst.SINGLE_SPACE + tb_Participant1.Text + AAConst.CSV_SEPARATOR;
            }
            if (!tb_Participant2.Text.Equals(""))
            {
                participants = participants + AAConst.SINGLE_SPACE + tb_Participant2.Text + AAConst.CSV_SEPARATOR;
            }
            if (!tb_Participant3.Text.Equals(""))
            {
                participants = participants + AAConst.SINGLE_SPACE + tb_Participant3.Text + AAConst.CSV_SEPARATOR;
            }
            if (!tb_Participant4.Text.Equals(""))
            {
                participants = participants + AAConst.SINGLE_SPACE + tb_Participant4.Text + AAConst.CSV_SEPARATOR;
            }

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
            if (!tb_Question5.Text.Equals(""))
            {
                questions = questions + tb_Question5.Text + AAConst.LINE_FEED;
            }

            //Die Tags in den template-strings muessen noch ersetzt werden.
            string str_questionary = AAUtilities.replaceTemplateTags(strTmpl_questionary, "", jobTitle, "", "",
                                                "", "", "", company, "", plzTown, "",
                                                str_meetingDate, participants, questions, "", "", "");

            str_questionary = str_questionary.Replace(AAConst.NEW_LINE, ""); //\r reicht als Linefeed

            Process.Start(SetupHandling.getPathToPreparedQaODTFromSetup());
            //Der Start des Prozesses kann sich bis zu mehrere Sekunden verzoegern.
            int tries = 0;
            Process p = null;
            do
            {
                if(tries >= 10)
                {
                    MessageBox.Show("Error: " + AAConst.PROC_SOFFICE + " wurde nicht geöffnet, ich warte nicht länger.");
                    return false;
                }

                Thread.Sleep(15000);
                p = Process.GetProcessesByName(AAConst.PROC_SOFFICE).FirstOrDefault();
                if (p != null)
                {
                    IntPtr h = p.MainWindowHandle;
                    SetForegroundWindow(h);
                    //Das Makro muss aktiviert werden.
                    SendKeys.SendWait("{LEFT}{ENTER}");
                    SendKeys.SendWait(str_questionary);
                    return true;
                }

                tries++;
            } while(p == null);

            return false;
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

            if (tb_Town.Text.Equals(""))
            {
                MessageBox.Show("Error: isInputOk(): Der Ort fehlen.");
                return false;
            }
            return true;
        }




    }
    
}
