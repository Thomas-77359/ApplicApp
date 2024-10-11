using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicApp
{
    public partial class frm_changeJobpostingState : Form
    {

        private frm_ApplicApp myParentApp;

        public frm_changeJobpostingState(frm_ApplicApp parent, string activeDossierPartPath)
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

            JobpostingState state = DossierMetadataHandling.getJobpostingStateFrom(dossierPath);
            setStatusChooser(state);
        }

        private void setStatusChooser(JobpostingState state)
        {
            switch (state)
            {
                case JobpostingState.evident:
                {
                    rb_clear.Checked = true;
                    rb_suspended.Checked = false;
                    rb_rejectedProfile.Checked = false;
                    rb_rejectedDistance.Checked = false;
                    rb_digDeeper.Checked = false;
                    rb_inscrutable.Checked = false;
                    rb_reappearance.Checked = false;
                    rb_withdrawn.Checked = false;
                    break;
                }
                case JobpostingState.aufgeschoben:
                {
                    rb_clear.Checked = false;
                    rb_suspended.Checked = true;
                    rb_rejectedProfile.Checked = false;
                    rb_rejectedDistance.Checked = false;
                    rb_digDeeper.Checked = false;
                    rb_inscrutable.Checked = false;
                    rb_reappearance.Checked = false;
                    rb_withdrawn.Checked = false;
                    break;
                }
                case JobpostingState.verworfen_Profil:
                {
                    rb_clear.Checked = false;
                    rb_suspended.Checked = false;
                    rb_rejectedProfile.Checked = true;
                    rb_rejectedDistance.Checked = false;
                    rb_digDeeper.Checked = false;
                    rb_inscrutable.Checked = false;
                    rb_reappearance.Checked = false;
                    rb_withdrawn.Checked = false;
                    break;
                }
                case JobpostingState.verworfen_Distanz:
                {
                    rb_clear.Checked = false;
                    rb_suspended.Checked = false;
                    rb_rejectedProfile.Checked = false;
                    rb_rejectedDistance.Checked = true;
                    rb_digDeeper.Checked = false;
                    rb_inscrutable.Checked = false;
                    rb_reappearance.Checked = false;
                    rb_withdrawn.Checked = false;
                    break;
                }
                case JobpostingState.nachhaken:
                {
                    rb_clear.Checked = false;
                    rb_suspended.Checked = false;
                    rb_rejectedProfile.Checked = false;
                    rb_rejectedDistance.Checked = false;
                    rb_digDeeper.Checked = true;
                    rb_inscrutable.Checked = false;
                    rb_reappearance.Checked = false;
                    rb_withdrawn.Checked = false;
                    break;
                }
                case JobpostingState.unverständlich:
                {
                    rb_clear.Checked = false;
                    rb_suspended.Checked = false;
                    rb_rejectedProfile.Checked = false;
                    rb_rejectedDistance.Checked = false;
                    rb_digDeeper.Checked = false;
                    rb_inscrutable.Checked = true;
                    rb_reappearance.Checked = false;
                    rb_withdrawn.Checked = false;
                    break;
                }
                case JobpostingState.wiedererschienen:
                {
                    rb_clear.Checked = false;
                    rb_suspended.Checked = false;
                    rb_rejectedProfile.Checked = false;
                    rb_rejectedDistance.Checked = false;
                    rb_digDeeper.Checked = false;
                    rb_inscrutable.Checked = false;
                    rb_reappearance.Checked = true; 
                    rb_withdrawn.Checked = false;
                    break;
                }
                case JobpostingState.entfernt:
                {
                    rb_clear.Checked = false;
                    rb_suspended.Checked = false;
                    rb_rejectedProfile.Checked = false;
                    rb_rejectedDistance.Checked = false;
                    rb_digDeeper.Checked = false;
                    rb_inscrutable.Checked = false;
                    rb_reappearance.Checked = false;
                    rb_withdrawn.Checked = true;
                    break;
                }
                default:
                {
                    rb_clear.Checked = true;
                    rb_suspended.Checked = false;
                    rb_rejectedProfile.Checked = false;
                    rb_rejectedDistance.Checked = false;
                    rb_digDeeper.Checked = false;
                    rb_inscrutable.Checked = false;
                    rb_reappearance.Checked = false;
                    rb_withdrawn.Checked = false;
                    break;
                }
            }
        }

        private JobpostingState getStatusChooserState()
        {
            if (rb_clear.Checked)
            {
                return JobpostingState.evident;
            }
            if (rb_suspended.Checked) 
            {
                return JobpostingState.aufgeschoben;
            }
            if (rb_rejectedProfile.Checked) 
            {
                return JobpostingState.verworfen_Profil;
            }
            if (rb_rejectedDistance.Checked) 
            {
                return JobpostingState.verworfen_Distanz;
            }
            if (rb_digDeeper.Checked)
            {
                return JobpostingState.nachhaken;
            }
            if (rb_inscrutable.Checked)
            {
                return JobpostingState.unverständlich;
            }
            if (rb_reappearance.Checked)
            {
                return JobpostingState.wiedererschienen;
            }
            if (rb_withdrawn.Checked)
            {
                return JobpostingState.entfernt;
            }
            MessageBox.Show("Error: getStatusChooserState(): Aus den RadioButtons konnte kein Status geschlossen werden.", "Fehler aufgetreten");
            return JobpostingState.evident;
        }

        private void bt_saveNewState_Click(object sender, EventArgs e)
        {
            string dossierRepositoryPath = myParentApp.getPathToRepository();
            string dossierPath = Path.Combine(dossierRepositoryPath, tb_ActiveDossier.Text);

            JobpostingState newState = getStatusChooserState();

            DossierMetadataHandling.saveJobpostingStateToMetadata(dossierPath, newState.ToString());

            //myParentApp.loadDossierSelector(true);
            Close();
        }
    }
}
