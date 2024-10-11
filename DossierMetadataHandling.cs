using System;
using System.IO;
using System.Xml;


namespace ApplicApp
{
    class DossierMetadataHandling
    {

        private static XmlDocument defaultXmlDoc;

        //Gibt true zurueck wenn es im angegeben Pfad ein Metadata.xml hat.
        public static bool hasMetadataFile(string dossierPath) 
        {
            string pathString = Path.Combine(dossierPath, AAConst.METADATA_XML);

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
        public static bool isMetadataFileCorrect(string dossierPath)
        {
            if (getNumberFrom(dossierPath).Equals(""))
            {
                throw new Exception("Error: Metadata incorrect: " + AAConst.NUMBER + " Tag ist leer. Dossier: " + dossierPath);
            }
            if (getJobTitleFrom(dossierPath).Equals(""))
            {
                throw new Exception("Error: Metadata incorrect: " + AAConst.JOB_TITLE + " Tag ist leer. Dossier: " + dossierPath);
            }
            if (getCompanyFrom(dossierPath).Equals(""))
            {
                throw new Exception("Error: Metadata incorrect: " + AAConst.COMPANY + " Tag ist leer. Dossier: " + dossierPath);
            }
            if (getStreetFrom(dossierPath).Equals(""))
            {
                throw new Exception("Error: Metadata incorrect: " + AAConst.STREET_NR + " Tag ist leer. Dossier: " + dossierPath);
            }
            if (getTownFrom(dossierPath).Equals(""))
            {
                throw new Exception("Error: Metadata incorrect: " + AAConst.PLZ_TOWN + " Tag ist leer. Dossier: " + dossierPath);
            }
            if (getSalutationFrom(dossierPath).Equals(""))
            {
                throw new Exception("Error: Metadata incorrect: " + AAConst.SALUTATION + " Tag ist leer. Dossier: " + dossierPath);
            }
            if (getEmailAddressFrom(dossierPath).Equals(""))
            {
                throw new Exception("Error: Metadata incorrect: " + AAConst.EMAIL_ADDRESS + " Tag ist leer. Dossier: " + dossierPath);
            }
            if (getJobPortalFrom(dossierPath).Equals(""))
            {
                throw new Exception("Error: Metadata incorrect: "+AAConst.JOB_PORTAL+" Tag ist leer. Dossier: " + dossierPath);
            }
            if (getDossierStateFrom(dossierPath).Equals(""))
            {
                throw new Exception("Error: Metadata incorrect: " + AAConst.DOSSIER_STATE + " Tag ist leer. Dossier: " + dossierPath);
            }
            if (getJobpostingStateFrom(dossierPath).Equals(""))
            {
                throw new Exception("Error: Metadata incorrect: " + AAConst.JOBPOSTING_STATE + " Tag ist leer. Dossier: " + dossierPath);
            }

            return true;

        }


        public static bool saveEntryDateToMetadata(string dossierPath, string entryDate)
        {
            string number = getNumberFrom(dossierPath);
            //string entryDate = getEntryDateFrom(dossierPath);
            string jobTitle = getJobTitleFrom(dossierPath);
            string company = getCompanyFrom(dossierPath);
            string street = getStreetFrom(dossierPath);
            string town = getTownFrom(dossierPath);
            string salutation = getSalutationFrom(dossierPath);
            string forename = getForenameFrom(dossierPath);
            string surname = getSurnameFrom(dossierPath);
            string emailAddress = getEmailAddressFrom(dossierPath);
            string emailReference = getEmailReferenceFrom(dossierPath);
            string jobPortal = getJobPortalFrom(dossierPath);
            string dossierState = getDossierStateFrom(dossierPath).ToString();
            string historyInfos = getHistoryInfosFrom(dossierPath).ToString();
            string jobpostingState = getJobpostingStateFrom(dossierPath).ToString();

            return saveMetadata(dossierPath, number, entryDate, jobTitle, company, street, town,
                            salutation, forename, surname, emailAddress, emailReference, jobPortal, dossierState, historyInfos, jobpostingState);

        }

        public static bool saveJobTitleToMetadata(string dossierPath, string jobTitle)
        {
            string number = getNumberFrom(dossierPath);
            string entryDate = getEntryDateFrom(dossierPath);
            //string jobTitle = getJobTitleFrom(dossierPath);
            string company = getCompanyFrom(dossierPath);
            string street = getStreetFrom(dossierPath);
            string town = getTownFrom(dossierPath);
            string salutation = getSalutationFrom(dossierPath);
            string forename = getForenameFrom(dossierPath);
            string surname = getSurnameFrom(dossierPath);
            string emailAddress = getEmailAddressFrom(dossierPath);
            string emailReference = getEmailReferenceFrom(dossierPath);
            string jobPortal = getJobPortalFrom(dossierPath);
            string dossierState = getDossierStateFrom(dossierPath).ToString();
            string historyInfos = getHistoryInfosFrom(dossierPath).ToString();
            string jobpostingState = getJobpostingStateFrom(dossierPath).ToString();

            return saveMetadata(dossierPath, number, entryDate, jobTitle, company, street, town,
                            salutation, forename, surname, emailAddress, emailReference, jobPortal, dossierState, historyInfos, jobpostingState);

        }

        public static bool saveCompanyToMetadata(string dossierPath, string company)
        {
            string number = getNumberFrom(dossierPath);
            string entryDate = getEntryDateFrom(dossierPath);
            string jobTitle = getJobTitleFrom(dossierPath);
            //string company = getCompanyFrom(dossierPath);
            string street = getStreetFrom(dossierPath);
            string town = getTownFrom(dossierPath);
            string salutation = getSalutationFrom(dossierPath);
            string forename = getForenameFrom(dossierPath);
            string surname = getSurnameFrom(dossierPath);
            string emailAddress = getEmailAddressFrom(dossierPath);
            string emailReference = getEmailReferenceFrom(dossierPath);
            string jobPortal = getJobPortalFrom(dossierPath);
            string dossierState = getDossierStateFrom(dossierPath).ToString();
            string historyInfos = getHistoryInfosFrom(dossierPath).ToString();
            string jobpostingState = getJobpostingStateFrom(dossierPath).ToString();

            return saveMetadata(dossierPath, number, entryDate, jobTitle, company, street, town,
                            salutation, forename, surname, emailAddress, emailReference, jobPortal, dossierState, historyInfos, jobpostingState);

        }

        public static bool saveInformationsToMetadata(string dossierPath, string street, string town,
                                                string salutation, string forename, string surname,
                                                string emailAddress, string emailReference, string jobPortal, string historyInfos)
        {
            string number = getNumberFrom(dossierPath);
            string entryDate = getEntryDateFrom(dossierPath);
            string jobTitle = getJobTitleFrom(dossierPath);
            string company = getCompanyFrom(dossierPath);
            //string street = getStreetFrom(dossierPath);
            //string town = getTownFrom(dossierPath);
            //string salutation = getSalutationFrom(dossierPath);
            //string forename = getForenameFrom(dossierPath);
            //string surname = getSurnameFrom(dossierPath);
            //string emailAddress = getEmailAddressFrom(dossierPath);
            //string emailReference = getEmailReferenceFrom(dossierPath);
            //string jobPortal = getJobPortalFrom(dossierPath);
            string dossierState = getDossierStateFrom(dossierPath).ToString();
            string jobpostingState = getJobpostingStateFrom(dossierPath).ToString();

            return saveMetadata(dossierPath, number, entryDate, jobTitle, company, street, town,
                            salutation, forename, surname, emailAddress, emailReference, jobPortal, dossierState, historyInfos, jobpostingState);
        }





        public static bool saveDossierStateToMetadata(string dossierPath, string dossierState)
        {

            string number = getNumberFrom(dossierPath);
            string entryDate = getEntryDateFrom(dossierPath);
            string jobTitle = getJobTitleFrom(dossierPath);
            string company = getCompanyFrom(dossierPath);
            string street = getStreetFrom(dossierPath);
            string town = getTownFrom(dossierPath);
            string salutation = getSalutationFrom(dossierPath);
            string forename = getForenameFrom(dossierPath);
            string surname = getSurnameFrom(dossierPath);
            string emailAddress = getEmailAddressFrom(dossierPath);
            string emailReference = getEmailReferenceFrom(dossierPath);
            string jobPortal = getJobPortalFrom(dossierPath);
            string historyInfos = getHistoryInfosFrom(dossierPath);
            string jobpostingState = getJobpostingStateFrom(dossierPath).ToString();

            return saveMetadata(dossierPath, number, entryDate, jobTitle, company, street, town, 
                            salutation, forename, surname, emailAddress, emailReference, jobPortal, dossierState, historyInfos, jobpostingState);
        }

        public static bool saveJobpostingStateToMetadata(string dossierPath, string jobpostingState)
        {

            string number = getNumberFrom(dossierPath);
            string entryDate = getEntryDateFrom(dossierPath);
            string jobTitle = getJobTitleFrom(dossierPath);
            string company = getCompanyFrom(dossierPath);
            string street = getStreetFrom(dossierPath);
            string town = getTownFrom(dossierPath);
            string salutation = getSalutationFrom(dossierPath);
            string forename = getForenameFrom(dossierPath);
            string surname = getSurnameFrom(dossierPath);
            string emailAddress = getEmailAddressFrom(dossierPath);
            string emailReference = getEmailReferenceFrom(dossierPath);
            string jobPortal = getJobPortalFrom(dossierPath);
            string historyInfos = getHistoryInfosFrom(dossierPath);
            string dossierState = getDossierStateFrom(dossierPath).ToString();

            return saveMetadata(dossierPath, number, entryDate, jobTitle, company, street, town,
                            salutation, forename, surname, emailAddress, emailReference, jobPortal, dossierState, historyInfos, jobpostingState);
        }


        public static bool saveMetadata(string dossierPath, string number, string entryDate, 
                                string jobTitle, string company, string street, 
                                string town, string salutation, string forename, 
                                string surname, string emailAddress, string emailReference,
                                string jobPortal, string dossierState, string historyInfos, string jobpostingState)
        {

            try
            {
                string pathString = Path.Combine(dossierPath, AAConst.METADATA_XML);
                XmlDocument xmlDoc = new XmlDocument();

                XmlNode rootNode = xmlDoc.CreateElement(AAConst.DOSSIER);
                xmlDoc.AppendChild(rootNode);

                XmlNode metadataNode = xmlDoc.CreateElement(AAConst.NUMBER);
                metadataNode.InnerText = number;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.ENTRY_DATE);
                metadataNode.InnerText = entryDate;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.JOB_TITLE);
                metadataNode.InnerText = jobTitle;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.COMPANY);
                metadataNode.InnerText = company;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.STREET_NR);
                metadataNode.InnerText = street;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.PLZ_TOWN);
                metadataNode.InnerText = town;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.SALUTATION);
                metadataNode.InnerText = salutation;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.FORENAME);
                metadataNode.InnerText = forename;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.SURNAME);
                metadataNode.InnerText = surname;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.EMAIL_ADDRESS);
                metadataNode.InnerText = emailAddress;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.EMAIL_REF);
                metadataNode.InnerText = emailReference;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.JOB_PORTAL);
                metadataNode.InnerText = jobPortal;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.DOSSIER_STATE);
                metadataNode.InnerText = dossierState;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.JOBPOSTING_STATE);
                metadataNode.InnerText = jobpostingState;
                rootNode.AppendChild(metadataNode);

                metadataNode = xmlDoc.CreateElement(AAConst.HISTORY);
                metadataNode.InnerText = historyInfos;
                rootNode.AppendChild(metadataNode);

                xmlDoc.Save(pathString);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Metadata Datei konnte nicht erzeugt werden: " + ex.Message);
            }

        }


        public static void setDefaultDossierPath(string defaultDossierPath)
        {
            string pathString = Path.Combine(defaultDossierPath, AAConst.METADATA_XML);
            defaultXmlDoc = new XmlDocument();
            defaultXmlDoc.Load(pathString);
        }

        //vorher muss das XML-File mit setDefaultDossierPath() definiert werden.
        private static string getInnerTextFromDefaultFileNode(string nodeName)
        {
            if (defaultXmlDoc == null)
            {
                throw new Exception("Error: kein XML-Dokument geladen. Wahrscheinlich kein Defaultpfad gesetzt.");
            }

            try
            {
                XmlNodeList metadataNodes = defaultXmlDoc.SelectNodes(AAConst.DOUBLE_SLASH + AAConst.DOSSIER
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

        public static string getNumberFromDefaultDossierPath()
        {
            return getInnerTextFromDefaultFileNode(AAConst.NUMBER);
        }

        public static string getEntryDateFromDefaultDossierPath()
        {
            return getInnerTextFromDefaultFileNode(AAConst.ENTRY_DATE);
        }

        public static string getJobTitleFromDefaultDossierPath()
        {
            return getInnerTextFromDefaultFileNode(AAConst.JOB_TITLE);
        }

        public static string getCompanyFromDefaultDossierPath()
        {
            return getInnerTextFromDefaultFileNode(AAConst.COMPANY);
        }

        public static string getStreetFromDefaultDossierPath()
        {
            return getInnerTextFromDefaultFileNode(AAConst.STREET_NR);
        }

        public static string getTownFromDefaultDossierPath()
        {
            return getInnerTextFromDefaultFileNode(AAConst.PLZ_TOWN);
        }

        public static string getSalutationFromDefaultDossierPath()
        {
            return getInnerTextFromDefaultFileNode(AAConst.SALUTATION);
        }

        public static string getForenameFromDefaultDossierPath()
        {
            return getInnerTextFromDefaultFileNode(AAConst.FORENAME);
        }

        public static string getSurnameFromDefaultDossierPath()
        {
            return getInnerTextFromDefaultFileNode(AAConst.SURNAME);
        }

        public static string getEmailAddressFromDefaultDossierPath()
        {
            return getInnerTextFromDefaultFileNode(AAConst.EMAIL_ADDRESS);
        }

        public static string getEmailReferenceFromDefaultDossierPath()
        {
            return getInnerTextFromDefaultFileNode(AAConst.EMAIL_REF);
        }

        public static string getJobPortalFromDefaultDossierPath()
        {
            return getInnerTextFromDefaultFileNode(AAConst.JOB_PORTAL);
        }

        public static DossierState getDossierStateFromDefaultDossierPath()
        {
            string state = getInnerTextFromDefaultFileNode(AAConst.DOSSIER_STATE);
            return convertStringToDossierState(state);
        }

        public static JobpostingState getJobpostingStateFromDefaultDossierPath()
        {
            string state = getInnerTextFromDefaultFileNode(AAConst.JOBPOSTING_STATE);
            return convertStringToJobpostingState(state);
        }

        public static string getHistoryInfosFromDefaultDossierPath()
        {
            return getInnerTextFromDefaultFileNode(AAConst.HISTORY);
        }






        //Gibt null zurueck wenn kein Inhalt gefunden werden konnte.
        private static string getInnerTextFromNode(string dossierPath, string nodeName)
        {
            try
            {
                string pathString = Path.Combine(dossierPath, AAConst.METADATA_XML);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(pathString);
                XmlNodeList metadataNodes = xmlDoc.SelectNodes(AAConst.DOUBLE_SLASH + AAConst.DOSSIER
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


        public static string getNumberFrom(string dossierPath)
        {
            return getInnerTextFromNode(dossierPath, AAConst.NUMBER);
        }

        public static string getEntryDateFrom(string dossierPath)
        {
            return getInnerTextFromNode(dossierPath, AAConst.ENTRY_DATE);
        }

        public static string getJobTitleFrom(string dossierPath)
        {
            return getInnerTextFromNode(dossierPath, AAConst.JOB_TITLE);
        }

        public static string getCompanyFrom(string dossierPath)
        {
            return getInnerTextFromNode(dossierPath, AAConst.COMPANY);
        }

        public static string getStreetFrom(string dossierPath)
        {
            return getInnerTextFromNode(dossierPath, AAConst.STREET_NR);
        }

        public static string getTownFrom(string dossierPath)
        {
            return getInnerTextFromNode(dossierPath, AAConst.PLZ_TOWN);
        }

        public static string getSalutationFrom(string dossierPath)
        {
            return getInnerTextFromNode(dossierPath, AAConst.SALUTATION);
        }

        public static string getForenameFrom(string dossierPath)
        {
            return getInnerTextFromNode(dossierPath, AAConst.FORENAME);
        }

        public static string getSurnameFrom(string dossierPath)
        {
            return getInnerTextFromNode(dossierPath, AAConst.SURNAME);
        }

        public static string getEmailAddressFrom(string dossierPath)
        {
            return getInnerTextFromNode(dossierPath, AAConst.EMAIL_ADDRESS);
        }

        public static string getEmailReferenceFrom(string dossierPath)
        {
            return getInnerTextFromNode(dossierPath, AAConst.EMAIL_REF);
        }

        public static string getJobPortalFrom(string dossierPath)
        {
            return getInnerTextFromNode(dossierPath, AAConst.JOB_PORTAL);
        }
        
        public static DossierState getDossierStateFrom(string dossierPath)
        {
            string state = getInnerTextFromNode(dossierPath, AAConst.DOSSIER_STATE);
            return convertStringToDossierState(state);
        }

        public static JobpostingState getJobpostingStateFrom(string dossierPath)
        {
            string state = getInnerTextFromNode(dossierPath, AAConst.JOBPOSTING_STATE);
            return convertStringToJobpostingState(state);
        }

        public static string getHistoryInfosFrom(string dossierPath)
        {
            return getInnerTextFromNode(dossierPath, AAConst.HISTORY);
        }

        private static JobpostingState convertStringToJobpostingState(string strJobpostingState)
        {
            if (strJobpostingState.Equals(JobpostingState.evident.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return JobpostingState.evident;
            }
            else if (strJobpostingState.Equals(JobpostingState.aufgeschoben.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return JobpostingState.aufgeschoben;
            }
            else if (strJobpostingState.Equals(JobpostingState.verworfen_Profil.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return JobpostingState.verworfen_Profil;
            }
            else if (strJobpostingState.Equals(JobpostingState.verworfen_Distanz.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return JobpostingState.verworfen_Distanz;
            }
            else if (strJobpostingState.Equals(JobpostingState.nachhaken.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return JobpostingState.nachhaken;
            }
            else if (strJobpostingState.Equals(JobpostingState.unverständlich.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return JobpostingState.unverständlich;
            }
            else if (strJobpostingState.Equals(JobpostingState.wiedererschienen.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return JobpostingState.wiedererschienen;
            }
            else if (strJobpostingState.Equals(JobpostingState.entfernt.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return JobpostingState.entfernt;
            }
            else
            {
                throw new Exception("ERROR: getJobpostingStateFrom: No valid state found for string-state: " + strJobpostingState);
            }
        }

        private static DossierState convertStringToDossierState(string strDossierState)
        {
            if (strDossierState.Equals(DossierState.Erzeugt.ToString()))
            {
                return DossierState.Erzeugt;
            }
            else if (strDossierState.Equals(DossierState.Offen.ToString()))
            {
                return DossierState.Offen;
            }
            else if (strDossierState.Equals(DossierState.Absage.ToString()))
            {
                return DossierState.Absage;
            }
            else if (strDossierState.Equals(DossierState.Angenommen.ToString()))
            {
                return DossierState.Angenommen;
            }
                //TODO: Klausel ausbauen
            //else if (strDossierState.Equals(DossierState.Abgelehnt.ToString()))
            //{
            //    return DossierState.Angenommen;
            //}
            else
            {
                throw new Exception("ERROR: convertStringToDossierState: No valid state found for string-state: " + strDossierState);
            }
        }

    }
}
