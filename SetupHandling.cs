using System;
using System.IO;
using System.Xml;

namespace ApplicApp
{
    class SetupHandling
    {
        //Gibt true zurueck wenn es im angegeben Pfad ein Metadata.xml hat.
        public static bool hasSetupFile() 
        {

            string currentDir = Directory.GetCurrentDirectory();

            string pathString = Path.Combine(currentDir, AAConst.SETUP_XML);

            if (File.Exists(pathString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

 

        //Gibt true zurueck wenn das Metadata alle notwendigen Infos enthaelt.
        public static bool isSetupFileCorrect()
        {
            
            if (getInnerTextFromSetupNode(AAConst.PATH_TO_APPLICATION_REPOSITORY).Equals(""))
            {
                throw new Exception("Error: Setupfile incorrect: " + AAConst.PATH_TO_APPLICATION_REPOSITORY + " Tag ist leer.");
            }
            if (getInnerTextFromSetupNode(AAConst.PATH_TO_VITA_WORKBENCH).Equals(""))
            {
                throw new Exception("Error: Setupfile incorrect: " + AAConst.PATH_TO_VITA_WORKBENCH + " Tag ist leer.");
            }
            if (getInnerTextFromSetupNode(AAConst.NAME_OF_FIRST_VITA_PAGE).Equals(""))
            {
                throw new Exception("Error: Setupfile incorrect: " + AAConst.NAME_OF_FIRST_VITA_PAGE + " Tag ist leer.");
            }
            if (getInnerTextFromSetupNode(AAConst.PATH_TO_MERGETOOL_FOR_PDFS).Equals(""))
            {
                throw new Exception("Error: Setupfile incorrect: " + AAConst.PATH_TO_MERGETOOL_FOR_PDFS + " Tag ist leer.");
            }
            if (getInnerTextFromSetupNode(AAConst.NAME_OF_ODTTOPDF_EXPORTSCRIPT_AU3).Equals(""))
            {
                throw new Exception("Error: Setupfile incorrect: " + AAConst.NAME_OF_ODTTOPDF_EXPORTSCRIPT_AU3 + " Tag ist leer.");
            }
            if (getInnerTextFromSetupNode(AAConst.NAME_OF_PDFS_MERGESCRIPT_AU3).Equals(""))
            {
                throw new Exception("Error: Setupfile incorrect: " + AAConst.NAME_OF_PDFS_MERGESCRIPT_AU3 + " Tag ist leer.");
            }            
            if (getInnerTextFromSetupNode(AAConst.PATH_TO_EMAIL_TEMPLATES).Equals(""))
            {
                throw new Exception("Error: Setupfile incorrect: " + AAConst.PATH_TO_EMAIL_TEMPLATES + " Tag ist leer.");
            }            
            if (getInnerTextFromSetupNode(AAConst.PATH_TO_MOTIVATIONLETTER_TEMPLATES).Equals(""))
            {
                throw new Exception("Error: Setupfile incorrect: " + AAConst.PATH_TO_MOTIVATIONLETTER_TEMPLATES + " Tag ist leer.");
            }            
            if (getInnerTextFromSetupNode(AAConst.PATH_TO_EMPTYODT).Equals(""))
            {
                throw new Exception("Error: Setupfile incorrect: " + AAConst.PATH_TO_EMPTYODT + " Tag ist leer.");
            }            
            if (getInnerTextFromSetupNode(AAConst.NAME_OF_ODTS_SAVESCRIPT_AU3).Equals(""))
            {
                throw new Exception("Error: Setupfile incorrect: " + AAConst.NAME_OF_ODTS_SAVESCRIPT_AU3 + " Tag ist leer.");
            }            
            if (getInnerTextFromSetupNode(AAConst.PATH_TO_EXTANT_PDFS).Equals(""))
            {
                throw new Exception("Error: Setupfile incorrect: " + AAConst.PATH_TO_EXTANT_PDFS + " Tag ist leer.");
            }            
            if (getInnerTextFromSetupNode(AAConst.PATH_TO_AU3_SCRIPTS).Equals(""))
            {
                throw new Exception("Error: Setupfile incorrect: " + AAConst.PATH_TO_AU3_SCRIPTS + " Tag ist leer.");
            }
            if (getInnerTextFromSetupNode(AAConst.PATH_TO_AU3_SCRIPTINGTOOL).Equals(""))
            {
                throw new Exception("Error: Setupfile incorrect: " + AAConst.PATH_TO_AU3_SCRIPTINGTOOL + " Tag ist leer.");
            }
            if (getInnerTextFromSetupNode(AAConst.PATH_TO_QUESTIONARY_TEMPLATES).Equals(""))
            {
                throw new Exception("Error: Setupfile incorrect: " + AAConst.PATH_TO_QUESTIONARY_TEMPLATES + " Tag ist leer.");
            }
            if (getInnerTextFromSetupNode(AAConst.PATH_TO_PREPARED_QA_ODT).Equals(""))
            {
                throw new Exception("Error: Setupfile incorrect: " + AAConst.PATH_TO_PREPARED_QA_ODT + " Tag ist leer.");
            }
            if (getInnerTextFromSetupNode(AAConst.PATH_TO_PHONE_CALL_NOTES_TEMPLATES).Equals(""))
            {
                throw new Exception("Error: Setupfile incorrect: " + AAConst.PATH_TO_PHONE_CALL_NOTES_TEMPLATES + " Tag ist leer.");
            }

            return true;
        }



        public static bool saveSetup(string pathToApplicationRepository, string pathToVitaWorkbench, string nameOfFirstVitaPage,
                                string pathToMergetoolForPDFs, string nameOfOdtToPdfExportScriptAu3, string nameOfPdfsMergeScriptAu3,
                                string pathToEmailTemplates, string pathToMotivationLetterTemplates, string pathToEmptyODT,
                                string nameOfOdtsSaveScriptAu3, string pathToExtantPdfs, string pathToAu3Scripts,
                                string pathToAu3Scriptingtool, string pathToQuestionaryTemplates, string pathToPreparedQaODT,
                                string pathToPhoneCallNotesTemplates)
        {

            try
            {
                string currDir = Directory.GetCurrentDirectory();
                string pathString = Path.Combine(currDir, AAConst.SETUP_XML);
                XmlDocument xmlDoc = new XmlDocument();

                XmlNode rootNode = xmlDoc.CreateElement(AAConst.SETUP);
                xmlDoc.AppendChild(rootNode);

                XmlNode metadataNode = xmlDoc.CreateElement(AAConst.PATH_TO_APPLICATION_REPOSITORY);
                metadataNode.InnerText = pathToApplicationRepository;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.PATH_TO_VITA_WORKBENCH);
                metadataNode.InnerText = pathToVitaWorkbench;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.NAME_OF_FIRST_VITA_PAGE);
                metadataNode.InnerText = nameOfFirstVitaPage;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.PATH_TO_MERGETOOL_FOR_PDFS);
                metadataNode.InnerText = pathToMergetoolForPDFs;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.NAME_OF_ODTTOPDF_EXPORTSCRIPT_AU3);
                metadataNode.InnerText = nameOfOdtToPdfExportScriptAu3;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.NAME_OF_PDFS_MERGESCRIPT_AU3);
                metadataNode.InnerText = nameOfPdfsMergeScriptAu3;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.PATH_TO_EMAIL_TEMPLATES);
                metadataNode.InnerText = pathToEmailTemplates;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.PATH_TO_MOTIVATIONLETTER_TEMPLATES);
                metadataNode.InnerText = pathToMotivationLetterTemplates;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.PATH_TO_EMPTYODT);
                metadataNode.InnerText = pathToEmptyODT;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.NAME_OF_ODTS_SAVESCRIPT_AU3);
                metadataNode.InnerText = nameOfOdtsSaveScriptAu3;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.PATH_TO_EXTANT_PDFS);
                metadataNode.InnerText = pathToExtantPdfs;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.PATH_TO_AU3_SCRIPTS);
                metadataNode.InnerText = pathToAu3Scripts;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.PATH_TO_AU3_SCRIPTINGTOOL);
                metadataNode.InnerText = pathToAu3Scriptingtool;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.PATH_TO_QUESTIONARY_TEMPLATES);
                metadataNode.InnerText = pathToQuestionaryTemplates;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.PATH_TO_PREPARED_QA_ODT);
                metadataNode.InnerText = pathToPreparedQaODT;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.PATH_TO_PHONE_CALL_NOTES_TEMPLATES);
                metadataNode.InnerText = pathToPhoneCallNotesTemplates;
                rootNode.AppendChild(metadataNode);

                xmlDoc.Save(pathString);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Metadata Datei konnte nicht erzeugt werden: " + ex.Message);
            }

        }

        //Gibt null zurueck wenn kein Inhalt gefunden werden konnte.
        private static string getInnerTextFromSetupNode(string nodeName)
        {
            try
            {
                string currDir = Directory.GetCurrentDirectory();
                string pathString = Path.Combine(currDir, AAConst.SETUP_XML);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(pathString);
                XmlNodeList metadataNodes = xmlDoc.SelectNodes(AAConst.DOUBLE_SLASH + AAConst.SETUP
                                                                + AAConst.SINGLE_SLASH + nodeName);
                if (metadataNodes.Count >= 1)
                {
                    return metadataNodes[0].InnerText;
                }
            }
            catch (Exception)
            {
                return "";
            } 
            return "";
        }

        public static string getPathToApplicationRepositoryFromSetup()
        {
            return getInnerTextFromSetupNode(AAConst.PATH_TO_APPLICATION_REPOSITORY);
        }

        public static string getPathToVitaWorkbenchFromSetup()
        {
            return getInnerTextFromSetupNode(AAConst.PATH_TO_VITA_WORKBENCH);
        }

        public static string getNameOfFirstVitaPageFromSetup()
        {
            return getInnerTextFromSetupNode(AAConst.NAME_OF_FIRST_VITA_PAGE);
        }

        public static string getPathToMergetoolForPDFsFromSetup()
        {
            return getInnerTextFromSetupNode(AAConst.PATH_TO_MERGETOOL_FOR_PDFS);
        }

        public static string getNameOfOdtToPdfExportScriptAu3FromSetup()
        {
            return getInnerTextFromSetupNode(AAConst.NAME_OF_ODTTOPDF_EXPORTSCRIPT_AU3);
        }

        public static string getNameOfPdfsMergeScriptAu3FromSetup()
        {
            return getInnerTextFromSetupNode(AAConst.NAME_OF_PDFS_MERGESCRIPT_AU3);
        }

        public static string getPathToEmailTemplatesFromSetup()
        {
            return getInnerTextFromSetupNode(AAConst.PATH_TO_EMAIL_TEMPLATES);
        }

        public static string getPathToMotivationLetterTemplatesFromSetup()
        {
            return getInnerTextFromSetupNode(AAConst.PATH_TO_MOTIVATIONLETTER_TEMPLATES);
        }

        public static string getPathToEmptyODTFromSetup()
        {
            return getInnerTextFromSetupNode(AAConst.PATH_TO_EMPTYODT);
        }

        public static string getNameOfOdtsSaveScriptAu3FromSetup()
        {
            return getInnerTextFromSetupNode(AAConst.NAME_OF_ODTS_SAVESCRIPT_AU3);
        }

        public static string getPathToExtantPdfsFromSetup()
        {
            return getInnerTextFromSetupNode(AAConst.PATH_TO_EXTANT_PDFS);
        }

        public static string getPathToAu3ScriptsFromSetup()
        {
            return getInnerTextFromSetupNode(AAConst.PATH_TO_AU3_SCRIPTS);
        }

        public static string getPathToAu3ScriptingtoolFromSetup()
        {
            return getInnerTextFromSetupNode(AAConst.PATH_TO_AU3_SCRIPTINGTOOL);
        }

        public static string getPathToQuestionaryTemplatesFromSetup()
        {
            return getInnerTextFromSetupNode(AAConst.PATH_TO_QUESTIONARY_TEMPLATES);
        }

        public static string getPathToPreparedQaODTFromSetup()
        {
            return getInnerTextFromSetupNode(AAConst.PATH_TO_PREPARED_QA_ODT);
        }

        public static string getPathToPhoneCallNotesTemplatesFromSetup()
        {
            return getInnerTextFromSetupNode(AAConst.PATH_TO_PHONE_CALL_NOTES_TEMPLATES);
        }



    }



}

