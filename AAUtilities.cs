using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicApp
{
    class AAUtilities
    {
        public static string replaceTemplateTags(string tmpString, string emailAddress, string jobTitle,
                                                string jobPortal, string entryDate, string salutation, string surname,
                                                string forename, string company, string streetNr, string plzTown,
                                                string emailReference, string meetingDate, string participants, string questions,
                                                string tfNumber, string typeOfLastContact, string contact )
        {
            tmpString = tmpString.Replace(AAConst.TMPL_EMAILADDRESS, emailAddress);
            tmpString = tmpString.Replace(AAConst.TMPL_JOBTITLE, jobTitle);
            tmpString = tmpString.Replace(AAConst.TMPL_JOBPORTAL, jobPortal);
            tmpString = tmpString.Replace(AAConst.TMPL_ENTRYDATE, entryDate);
            tmpString = tmpString.Replace(AAConst.TMPL_SALUTATION, salutation);
            tmpString = tmpString.Replace(AAConst.TMPL_SURNAME, surname);
            tmpString = tmpString.Replace(AAConst.TMPL_FORENAME, forename);
            tmpString = tmpString.Replace(AAConst.TMPL_COMPANY, company);
            tmpString = tmpString.Replace(AAConst.TMPL_STREETNR, streetNr);
            tmpString = tmpString.Replace(AAConst.TMPL_PLZTOWN, plzTown);
            tmpString = tmpString.Replace(AAConst.TMPL_EMAILREF, emailReference);

            tmpString = tmpString.Replace(AAConst.TMPL_MEETINGDATE, meetingDate);
            tmpString = tmpString.Replace(AAConst.TMPL_PARTICIPANTS, participants);
            tmpString = tmpString.Replace(AAConst.TMPL_QUESTIONS, questions);

            tmpString = tmpString.Replace(AAConst.TMPL_TFNUMBER, tfNumber);
            tmpString = tmpString.Replace(AAConst.TMPL_TYPOFLASTCONTACT, typeOfLastContact);
            tmpString = tmpString.Replace(AAConst.TMPL_CONTACT, contact);
            return tmpString;
        }

    }
}
