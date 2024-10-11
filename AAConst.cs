using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicApp
{
    static class AAConst
    {
        static public string METADATA_XML = "Metadata.xml";
        static public string SETUP_XML = "Setup.xml";
        static public string DOUBLE_SLASH = "//";
        static public string SINGLE_SLASH = "/";
        static public string DOUBLE_BACKSLASH = "\\\\";
        static public string SINGLE_BACKSLASH = "\\";
        static public string SINGLE_QUOTE = "'";
        static public string DOUBLE_QUOTE = "\"";
        static public string SINGLE_SPACE = " ";
        static public string CSV_SEPARATOR = ",";
        static public string DATESTAMP_FORMAT = "yyyy-MM-dd";
        static public string LETTERDATE_FORMAT = "dd. MMMM yyyy";
        static public string DATESTAMP_CH_FORMAT = "dd.MM.yyyy";
        static public string UNICODE = "Unicode";
        static public string ICH = "Ich";
        static public string NEW_LINE = "\n";
        static public string LINE_FEED = "\r";

        //Filepostfix
        static public string EMAIL_ANSCHREIBEN = "Email-Anschreiben";
        static public string MOTIVATIONSSCHREIBEN = "Motivationsschreiben";
        static public string FRAGEBOGEN = "Fragebogen";
        static public string ANRUFNOTIZEN = "Anrufnotizen";
        static public string COVER_01 = "01";
        static public string EXPORT_ALL_DOSSIERS = "Export - All Dossiers";
        static public string POSTFIX_CSV = ".csv";

        //Metadata-Tags
        static public string DOSSIER = "dossier";
        static public string NUMBER = "number";
        static public string ENTRY_DATE = "entryDate";
        static public string JOB_TITLE = "jobTitle";
        static public string COMPANY = "company";
        static public string STREET_NR = "streetNr";
        static public string PLZ_TOWN = "plzTown";
        static public string SALUTATION = "salutation";
        static public string FORENAME = "forename";
        static public string SURNAME = "surname";
        static public string EMAIL_ADDRESS = "emailAddress";
        static public string EMAIL_REF = "emailReference";
        static public string JOB_PORTAL = "jobPortal";
        static public string DOSSIER_STATE = "dossierState";
        static public string JOBPOSTING_STATE = "jobpostingState";
        static public string HISTORY = "history";


        //Processnames
        static public string PROC_SOFFICE = "soffice.bin";
        static public string PROC_NOTEPAD = "notepad";

        //Setup-Tags

        static public string SETUP = "setup";
        //tb_PathToDossiers
        static public string PATH_TO_APPLICATION_REPOSITORY = "pathToApplicationRepository";

        //tb_PathToVitaWorkbench
        static public string PATH_TO_VITA_WORKBENCH = "pathToVitaWorkbench";

        //tb_NameOfFirstVitaPage
        static public string NAME_OF_FIRST_VITA_PAGE = "nameOfFirstVitaPage";

        //tb_MergetoolForVita
        static public string PATH_TO_MERGETOOL_FOR_PDFS = "pathToMergetoolForPDFs";

        //tb_Au3OdtToPdfExportScript
        static public string NAME_OF_ODTTOPDF_EXPORTSCRIPT_AU3 = "nameOfOdtToPdfExportScriptAu3";
        
        //tb_Au3MergingScript
        static public string NAME_OF_PDFS_MERGESCRIPT_AU3 = "nameOfPdfsMergeScriptAu3";
        
        //tb_PathToEmailTemplates
        static public string PATH_TO_EMAIL_TEMPLATES = "pathToEmailTemplates";

        //tb_PathToMotivationTemplates
        static public string PATH_TO_MOTIVATIONLETTER_TEMPLATES = "pathToMotivationLetterTemplates";

        //tb_PathToEmptyODT
        static public string PATH_TO_EMPTYODT = "pathToEmptyODT";

        //tb_Au3SaveOdt
        static public string NAME_OF_ODTS_SAVESCRIPT_AU3 = "nameOfOdtsSaveScriptAu3";

        //tb_PathToPDFs
        static public string PATH_TO_EXTANT_PDFS = "pathToExtantPdfs";

        //tb_PathToAu3Scripts
        static public string PATH_TO_AU3_SCRIPTS = "pathToAu3Scripts";

        //tb_PathToAutoIT
        static public string PATH_TO_AU3_SCRIPTINGTOOL = "pathToAu3Scriptingtool";

        //tb_PathToQuestionaryTemplates
        static public string PATH_TO_QUESTIONARY_TEMPLATES = "pathToQuestionaryTemplates";
            
        //tb_PathToPreparedQaODT
        static public string PATH_TO_PREPARED_QA_ODT = "pathToPreparedQaODT";

        //tb_PathToPhoneCallNotesTemplates
        static public string PATH_TO_PHONE_CALL_NOTES_TEMPLATES = "pathToPhoneCallNotesTemplates";





        //Function Control Names
        static public string TB_SELECTEDDOSSIER = "tb_selectedDossier";
        static public string CB_DOSSIERSELECTOR = "cb_DossierSelector";


        //Template-Tags
        static public string TMPL_EMAILADDRESS = "<EMAILADDRESS>";
        static public string TMPL_EMAILREF = "<EMAILREF>";
        static public string TMPL_JOBTITLE = "<JOBTITLE>";
        static public string TMPL_JOBPORTAL = "<JOBPORTAL>";
        static public string TMPL_ENTRYDATE = "<ENTRYDATE>";
        static public string TMPL_SALUTATION = "<SALUTATION>";
        static public string TMPL_SURNAME = "<SURNAME>";
        static public string TMPL_COMPANY = "<COMPANY>";
        static public string TMPL_FORENAME = "<FORENAME>";
        static public string TMPL_STREETNR = "<STREETNR>";
        static public string TMPL_PLZTOWN = "<PLZTOWN>";

        static public string TMPL_MEETINGDATE = "<MEETINGDATE>";
        static public string TMPL_PARTICIPANTS = "<PARTICIPANTS>";
        static public string TMPL_QUESTIONS = "<QUESTIONS>";

        static public string TMPL_TFNUMBER = "<TFNUMBER>";
        static public string TMPL_TYPOFLASTCONTACT = "<TYPOFLASTCONTACT>";
        static public string TMPL_CONTACT = "<CONTACT>";

        
        

    }
}
