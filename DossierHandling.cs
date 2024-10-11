using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ApplicApp
{
    public enum DossierState { Erzeugt, Offen, Absage, Angenommen };
    public enum JobpostingState { evident, aufgeschoben, verworfen_Profil, verworfen_Distanz, nachhaken, unverständlich, wiedererschienen, entfernt};

    class DossierHandling
    {

        public class ComboBoxItem
        {
            public string DossierNr { get; set; }
            public string Date { get; set; }
            public string Company { get; set; }
            public string Location { get; set; }
            public string JobTitle { get; set; }
            public string DossierState { get; set; }
            public string JobpostingState { get; set; }

            public override string ToString()
            {
                return Date + " - " + Company + " - " + Location + " - " + JobTitle + " - " + JobpostingState + " - " + DossierState;
            }
        }

        private static List<ComboBoxItem> allDossierList = new List<ComboBoxItem>();
        private static List<ComboBoxItem> amorphicDossierList = new List<ComboBoxItem>();
        
        public static List<ComboBoxItem> getAllDossierListForComboBox()
        {
            return allDossierList;
        }

        public static List<ComboBoxItem> getAmorphicDossierListForComboBox()
        {
            return amorphicDossierList;
        }
        
        /// <summary>
        /// Returns a list of path-strings for all dossiers within the given repository-path.
        /// </summary>
        /// <param name="dossierRepositoryPath"></param>
        /// <returns></returns>
        public static List<string> getAllDossiers(string dossierRepositoryPath)
        {
            List<string> allDossiers = new List<string>();
            string[] companies = getCompanies(dossierRepositoryPath);
            string companyPathString = "";
            string dossierPathString = "";
            string[] dossiers = null;
            
            foreach (string company in companies)
            {
                companyPathString = Path.Combine(dossierRepositoryPath, company);
                dossiers = getDossiers(companyPathString);

                foreach (string dossier in dossiers)
                {
                    dossierPathString = Path.Combine(companyPathString, dossier);
                    if(DossierMetadataHandling.hasMetadataFile(dossierPathString))
                    {
                        if(DossierMetadataHandling.isMetadataFileCorrect(dossierPathString))
                        {
                            
                            allDossiers.Add(dossierPathString);
                        }
                    }
                }
            }
            return allDossiers;
        }

        /// <summary>
        /// <para>Refills all dossier-lists with the suitable dossiers from the given repository-path.</para>
        /// <para>One dossier is represented as a dossier-path.</para>
        /// </summary>
        /// <param name="dossierRepositoryPath"></param>
        public static void updateDossierLists(string dossierRepositoryPath)
        {
            allDossierList.Clear();
            amorphicDossierList.Clear();
            
            string[] companies = getCompanies(dossierRepositoryPath);
            string companyPathString = "";
            string dossierPathString = "";
            string[] dossiers = null;
            ComboBoxItem item = null;
            foreach (string company in companies)
            {
                companyPathString = Path.Combine(dossierRepositoryPath, company);

                dossiers = getDossiers(companyPathString);

                foreach (string dossier in dossiers)
                {
                    dossierPathString = Path.Combine(companyPathString, dossier);

                    if(DossierMetadataHandling.hasMetadataFile(dossierPathString))
                    {
                        if(DossierMetadataHandling.isMetadataFileCorrect(dossierPathString))
                        {
                            DossierMetadataHandling.setDefaultDossierPath(dossierPathString);

                            item = new ComboBoxItem();
                            item.DossierNr = DossierMetadataHandling.getNumberFromDefaultDossierPath();
                            item.Date = DossierMetadataHandling.getEntryDateFromDefaultDossierPath();
                            item.Company = DossierMetadataHandling.getCompanyFromDefaultDossierPath();
                            item.Location = DossierMetadataHandling.getTownFromDefaultDossierPath();
                            item.JobTitle = DossierMetadataHandling.getJobTitleFromDefaultDossierPath();
                            item.DossierState = DossierMetadataHandling.getDossierStateFromDefaultDossierPath().ToString();
                            item.JobpostingState = DossierMetadataHandling.getJobpostingStateFromDefaultDossierPath().ToString();
                            
                            allDossierList.Add(item);
                            if( !item.DossierState.Equals(DossierState.Absage.ToString()) || !item.JobpostingState.Equals(JobpostingState.evident.ToString())  )
                            {
                                amorphicDossierList.Add(item);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// <para>Deletes a given dossiers on the basis of its path.</para>
        /// <para>Checks wether it exists or not.</para>
        /// </summary>
        /// <param name="dossierRepositoryPath"></param>
        /// <param name="dossierPartPath"></param>
        public static void deleteDossier(string dossierRepositoryPath, string dossierPartPath)
        {
            string dossierPath = Path.Combine(dossierRepositoryPath, dossierPartPath);

            if (!Directory.Exists(dossierPath))
            {
                throw new Exception("Error: Das Dossier-Verzeichnis ist nicht vorhanden. Löschen wurde abgebrochen. Verzeichnis: " + dossierPath);
            }
         
		    Directory.Delete(dossierPath, true);
        }

        /// <summary>
        /// <para>Creates a new directory.</para>
        /// <para>Each Company is represented with a directory.</para>
        /// <para>Creates a new metadata.xml for the company.</para>
        /// </summary>
        /// <param name="dossierRepositoryPath"></param>
        /// <param name="dossierName"></param>
        /// <param name="entryDate"></param>
        /// <param name="jobTitle"></param>
        /// <param name="company"></param>
        /// <param name="street"></param>
        /// <param name="town"></param>
        /// <param name="salutation"></param>
        /// <param name="forename"></param>
        /// <param name="surname"></param>
        /// <param name="emailAddress"></param>
        /// <param name="emailSubject"></param>
        /// <param name="jobPortal"></param>
        /// <param name="dossierState"></param>
        /// <param name="historyInfos"></param>
        /// <param name="jobpostingState"></param>
        /// <returns></returns>
        public static bool createNewDossier(string dossierRepositoryPath, string dossierName, string entryDate,
                                string jobTitle, string company, string street,
                                string town, string salutation, string forename,
                                string surname, string emailAddress, string emailSubject,
                                string jobPortal, string dossierState, string historyInfos, string jobpostingState)
        {

            if (!Directory.Exists(dossierRepositoryPath))
            {
                throw new Exception("Error: createNewDossier(): Das Repository-Verzeichnis ist ungueltig. Erzeugung abgebrochen.");
            }
            
            string companyPathString = Path.Combine(dossierRepositoryPath, company);
            if (!Directory.Exists(companyPathString))
            {
                Directory.CreateDirectory(companyPathString);
            }

            string dossierPathString = Path.Combine(companyPathString, dossierName);
            if (Directory.Exists(dossierPathString))
            {
                throw new Exception("Error: createNewDossier(): Bewerbungsdossier besteht bereits. Erzeugung abgebrochen.");
            }
            string dossierNr = getNextDossierNr(dossierRepositoryPath);
            Directory.CreateDirectory(dossierPathString);
            
            if (!DossierMetadataHandling.saveMetadata(dossierPathString, dossierNr, entryDate, jobTitle, 
                                                company, street, town, salutation, forename, surname, 
                                                emailAddress, emailSubject, jobPortal, dossierState, historyInfos, jobpostingState))
                {
                    throw new Exception("Error: createNewDossier(): Metadaten konnten nicht gespeichert werden.");
                }

            return true;
        }

        /// <summary>
        /// <para>Creates the dossier-name from the given data.</para>
        /// <para>One name mustn't be longer than 40 characters.</para>
        /// </summary>
        /// <param name="entryDate"></param>
        /// <param name="jobTitle"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string createDossierName(string entryDate, string jobTitle, string status)
        {
            jobTitle.Normalize();
            if (jobTitle.Count() > 40)
            {
                jobTitle = jobTitle.Substring(0, 40);
            }
            return entryDate + " - " + jobTitle + " - " + status;
        }
        
        /// <summary>
        /// <para>Extracts all companies from the given repository-path.</para>
        /// <para>Each company is represented by one directory.</para>
        /// </summary>
        /// <param name="dossierRepositoryPath"></param>
        /// <returns></returns>
        public static string[] getCompanies(string dossierRepositoryPath)
        {
            string[] folderPaths = Directory.GetDirectories(dossierRepositoryPath);
            List<string> companiesFolders = new List<string>();
            foreach(string path in folderPaths)
            {
                companiesFolders.Add(Path.GetFileName(path));
            }
            return companiesFolders.ToArray<string>();
        }

        /// <summary>
        /// Extracts all dossier-paths from one given company.
        /// </summary>
        /// <param name="companyPath"></param>
        /// <returns></returns>
        private static string[] getDossiers(string companyPath)
        {
            string[] folderPaths = Directory.GetDirectories(companyPath);
            List<string> dossiersFolders = new List<string>();
            foreach (string path in folderPaths)
            {
                dossiersFolders.Add(Path.GetFileName(path));
            }
            return dossiersFolders.ToArray<string>();
        }

        /// <summary>
        /// <para>Evaluates the latest dossiernumber and creates the next number.</para>
        /// <para>Loops over all metadata from all dossiers, finds the greatest and increments it.</para>
        /// </summary>
        /// <param name="dossierRepositoryPath"></param>
        /// <returns></returns>
        public static string getNextDossierNr(string dossierRepositoryPath)
        {
            string[] companies = getCompanies(dossierRepositoryPath);

            string[] dossiers;
            string pathToCompany;
            string pathToDossier;
            string str_number;
            int greatestNumber = 0;
            int number = 0;

            foreach (string company in companies)
            {
                pathToCompany = Path.Combine(dossierRepositoryPath, company);
                dossiers = getDossiers(pathToCompany);

                foreach (string dossier in dossiers)
                {
                    pathToDossier = Path.Combine(pathToCompany, dossier);

                    if (DossierMetadataHandling.hasMetadataFile(pathToDossier))
                    {
                        str_number = DossierMetadataHandling.getNumberFrom(pathToDossier);
                        if (str_number != null)
                        {
                            number = Convert.ToInt32(str_number);

                            if (number > greatestNumber)
                            {
                                greatestNumber = number;
                            }
                        }
                    }
                }
            }

            int nextNumber = greatestNumber;
            nextNumber++;
            return nextNumber.ToString();
        }

        /// <summary>
        /// Extracts a defined dossier by its number from the given repository.
        /// </summary>
        /// <param name="dossierRepositoryPath"></param>
        /// <param name="dossierNrSearch"></param>
        /// <returns></returns>
        public static string getRepositoryPathByNr(string dossierRepositoryPath, string dossierNrSearch)
        {
            string[] companies = getCompanies(dossierRepositoryPath);
            string companyPathString = "";
            string dossierPathString = "";
            string[] dossiers = null;
            string dossierNrFound = "";
            
            foreach (string company in companies)
            {
                companyPathString = Path.Combine(dossierRepositoryPath, company);
                dossiers = getDossiers(companyPathString);

                foreach (string dossier in dossiers)
                {
                    dossierPathString = Path.Combine(companyPathString, dossier);

                    if (DossierMetadataHandling.hasMetadataFile(dossierPathString))
                    {
                        if (DossierMetadataHandling.isMetadataFileCorrect(dossierPathString))
                        {
                            dossierNrFound = DossierMetadataHandling.getNumberFrom(dossierPathString);
                            if (dossierNrFound.Equals(dossierNrSearch))
                            {
                                return company + AAConst.SINGLE_BACKSLASH + dossier;
                            }
                        }
                    }
                }
            }           
            return "ERROR: Dossier-Nummer " + dossierNrSearch + " nicht gefunden!";
        }

        /// <summary>
        /// Compares to dates.
        /// </summary>
        public class CBItemSorterByDate : IComparer<ComboBoxItem>
        {
            public int Compare(ComboBoxItem x, ComboBoxItem y)
            {
                return x.Date.CompareTo(y.Date);
            }
        } 

        /// <summary>
        /// Sorts all dossier-lists after the date.
        /// </summary>
        public static void sortDossierListAfterDate()
        {
            allDossierList.Sort(new CBItemSorterByDate());
            amorphicDossierList.Sort(new CBItemSorterByDate());
        }

        /// <summary>
        /// <para>Compares two dossier-state as strings first.</para>
        /// <para>Compares their dates as second, if the strings are equal.</para>
        /// </summary>
        public class CBItemSorterByDossierState : IComparer<ComboBoxItem>
        {
            public int Compare(ComboBoxItem x, ComboBoxItem y)
            {
                int stateCprOut = System.String.Compare(x.DossierState, y.DossierState, StringComparison.CurrentCultureIgnoreCase);
                if (stateCprOut == 0)
                {
                    return x.Date.CompareTo(y.Date);
                }
                return stateCprOut;
            }
        }

        /// <summary>
        /// Sorts all dossier-lists after the dossier-state.
        /// </summary>
        public static void sortDossierListAfterDossierState()
        {
            allDossierList.Sort(new CBItemSorterByDossierState());
            amorphicDossierList.Sort(new CBItemSorterByDossierState());
        }

        /// <summary>
        /// <para>Compares two jobposting-state as strings first.</para>
        /// <para>Compares their dates as second, if the strings are equal.</para>
        /// </summary>
        public class CBItemSorterByJobpostingState : IComparer<ComboBoxItem>
        {
            public int Compare(ComboBoxItem x, ComboBoxItem y)
            {
                int stateCprOut = System.String.Compare(x.JobpostingState, y.JobpostingState, StringComparison.CurrentCultureIgnoreCase);
                if(stateCprOut == 0)
                {
                    return x.Date.CompareTo(y.Date);
                }
                return stateCprOut;
            }
        }

        /// <summary>
        /// Sorts all dossier-lists after the jobposting-state.
        /// </summary>
        public static void sortDossierListAfterJobpostingState()
        {
            allDossierList.Sort(new CBItemSorterByJobpostingState());
            amorphicDossierList.Sort(new CBItemSorterByJobpostingState());
        }

        /// <summary>
        /// <para>Compares two jobtitle-state as strings first.</para>
        /// <para>Compares their dates as second, if the strings are equal.</para>
        /// </summary>
        public class CBItemSorterByJobTitle : IComparer<ComboBoxItem>
        {
            public int Compare(ComboBoxItem x, ComboBoxItem y)
            {
                int jobTitleCprOut = x.JobTitle.CompareTo(y.JobTitle);
                if (jobTitleCprOut == 0)
                {
                    return x.Date.CompareTo(y.Date);
                }
                return jobTitleCprOut;
            }
        }

        /// <summary>
        /// Sorts all dossier-lists after the jobtitle.
        /// </summary>
        public static void sortDossierListAfterJobTitle()
        {
            allDossierList.Sort(new CBItemSorterByJobTitle());
            amorphicDossierList.Sort(new CBItemSorterByJobTitle());
        }

        /// <summary>
        /// Compares two company-name as strings.<br></br>
        /// </summary>
        public class CBItemSorterByCompany : IComparer<ComboBoxItem>
        {
            public int Compare(ComboBoxItem x, ComboBoxItem y)
            {
                return System.String.Compare(x.Company, y.Company, StringComparison.CurrentCultureIgnoreCase);
            }
        }

        /// <summary>
        /// Sorts all dossier-lists after the company-name.
        /// </summary>
        public static void sortDossierListAfterCompany()
        {
            allDossierList.Sort(new CBItemSorterByCompany());
            amorphicDossierList.Sort(new CBItemSorterByCompany());
        }

        /// <summary>
        /// Compares two company-name as strings.<br></br>
        /// </summary>
        public class CBItemSorterByLocation : IComparer<ComboBoxItem>
        {
            public int Compare(ComboBoxItem x, ComboBoxItem y)
            {
                return System.String.Compare(x.Location, y.Location, StringComparison.CurrentCultureIgnoreCase);
            }
        }


        /// <summary>
        /// Sorts all dossier-lists after the location-name.
        /// </summary>
        public static void sortDossierListAfterLocation()
        {
            allDossierList.Sort(new CBItemSorterByLocation());
            amorphicDossierList.Sort(new CBItemSorterByLocation());
        }
    }
}
