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
    public partial class frm_changeDossierState : Form
    {

        private frm_ApplicApp myParentApp;

        public frm_changeDossierState(frm_ApplicApp parent, string activeDossierPartPath)
        {
            myParentApp = parent;
            InitializeComponent();
            tb_ActiveDossier.Text = activeDossierPartPath;
            initRadioButtons();
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void initRadioButtons()
        {
            string dossierRepositoryPath = myParentApp.getPathToRepository();
            string dossierPath = Path.Combine(dossierRepositoryPath, tb_ActiveDossier.Text);

            DossierState state = DossierMetadataHandling.getDossierStateFrom(dossierPath);
            setStatusChooser(state);
        }

        private void setStatusChooser(DossierState state)
        {
            switch (state)
            {
                case DossierState.Erzeugt:
                {
                    rb_Created.Checked = true;
                    rb_Open.Checked = false;
                    rb_Denied.Checked = false;
                    rb_Hired.Checked = false;
                    break;
                }
                case DossierState.Offen:
                {
                    rb_Created.Checked = false;
                    rb_Open.Checked = true;
                    rb_Denied.Checked = false;
                    rb_Hired.Checked = false;                    
                    break;
                }
                case DossierState.Absage:
                {
                    rb_Created.Checked = false;
                    rb_Open.Checked = false;
                    rb_Denied.Checked = true;
                    rb_Hired.Checked = false;                    
                    break;
                }
                case DossierState.Angenommen:
                {
                    rb_Created.Checked = false;
                    rb_Open.Checked = false;
                    rb_Denied.Checked = false;
                    rb_Hired.Checked = true;
                    break;
                }
                default:
                {
                    rb_Created.Checked = true;
                    rb_Open.Checked = false;
                    rb_Denied.Checked = false;
                    rb_Hired.Checked = false;
                    break;
                }
            }
        }

        private DossierState getStatusChooserState()
        {
            if (rb_Created.Checked)
            {
                return DossierState.Erzeugt;
            }
            else if (rb_Open.Checked)
            {
                return DossierState.Offen;
            }
            else if (rb_Denied.Checked)
            {
                return DossierState.Absage;
            }
            else if (rb_Hired.Checked)
            {
                return DossierState.Angenommen;
            }     
            MessageBox.Show("Error: getStatusChooserState(): Aus den RadioButtons konnte kein Status geschlossen werden.", "Fehler aufgetreten");
            return DossierState.Erzeugt;
        }

        private void bt_saveNewState_Click(object sender, EventArgs e)
        {
            string dossierRepositoryPath = myParentApp.getPathToRepository();
            string oldDossierPath = Path.Combine(dossierRepositoryPath, tb_ActiveDossier.Text);

            DossierState newState = getStatusChooserState();

            DossierMetadataHandling.saveDossierStateToMetadata(oldDossierPath, newState.ToString());

            string entryDate = DossierMetadataHandling.getEntryDateFrom(oldDossierPath);
            string jobTitle = DossierMetadataHandling.getJobTitleFrom(oldDossierPath);
            string company = DossierMetadataHandling.getCompanyFrom(oldDossierPath);

            string newDossierName = DossierHandling.createDossierName(entryDate, jobTitle, newState.ToString());
            string newDossierPath = Path.Combine(dossierRepositoryPath, company, newDossierName);

            //Wenn die Dateien irgendwo geoeffnet sind koennen sie nicht verschoben werden und es tritt ein Fehler auf.
            try
            {
                Directory.Move(oldDossierPath, newDossierPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: bt_saveNewState_Click(): Dossierinhalt kann nicht verschoben werden: " + ex.Message, "Fehler aufgetreten");
                
            }
            

            myParentApp.loadDossierSelector(true);
            Close();
        }
    }
}
