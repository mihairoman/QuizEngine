using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.Library
{
    public class MailSender
    {
        #region Constants
        private const string MAIL_SMTP = "172.16.0.3";
        private const string SITE_LINK = "https://wss.umtsoftware.com";
        #endregion


        #region Methods
        /// <summary>
        /// This method handles the process of sedning mails.
        /// </summary>
        /// <param name="emailAddr"></param>
        /// <param name="userGUID"></param>
        /// <param name="quizGUID"></param>
        public void SendMail(Guid userGUID, Guid quizGUID, string emailToAddr, string emailFromAddr, string realEmailSubject, string realEmailBody)
        {
            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(emailToAddr);
                message.Subject = realEmailSubject;
                message.From = new System.Net.Mail.MailAddress(emailFromAddr);
                message.Body = realEmailBody;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(MAIL_SMTP);
                smtp.Send(message);
            }
            catch (Exception) { }
        }


        /// <summary>
        /// Transforms the username intro an email respecting the domain name. If it has errors at at reference Microsoft.Sharepoint.Client and Microsoft.Sharepoint.Client.Runtime
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public String GetEmailAfterWindowsUserName(string username)
        {
            string response = "";
            // Starting with ClientContext, the constructor requires a URL to the 
            // server running SharePoint. 
            ClientContext context = new ClientContext(SITE_LINK);

            // Assume the web has a list named "Announcements". 
            List announcementsList = context.Web.Lists.GetByTitle("Team Contacts");

            // This creates a CamlQuery that has a RowLimit of 100, and also specifies Scope="RecursiveAll" 
            // so that it grabs all list items, regardless of the folder they are in. 

            CamlQuery query = CamlQuery.CreateAllItemsQuery();
            // set the query in order to get the email that begins with the given username
            query.ViewXml = string.Format("<View><Query> <Where> <BeginsWith><FieldRef Name='Email'/><Value Type='Text'>{0}</Value></BeginsWith> </Where> </Query></View>", username);
            ListItemCollection items = announcementsList.GetItems(query);

            // Retrieve all items in the ListItemCollection from List.GetItems(Query). 
            context.Load(items);
            context.ExecuteQuery();

            foreach (ListItem listItem in items)
            {
                // We have all the list item data. And we retrieve the Email. 
                response = response + listItem["Email"];
            }
            return response;


        }
        #endregion
    }
}
