using QuizEngine.Repository;
using QuizEngine.Model.Data;
using System;
using System.Collections.Generic;
using QuizEngine.Infrastructure;
using QuizEngine.Business.Core;
using QuizEngine.AssemblyResources;
using Microsoft.SharePoint.Client;
using QuizEngine.Library;

namespace QuizEngine.Business
{
    public class QuizUserLinkBusiness : BusinessObject
    {
        #region Constructors
        public QuizUserLinkBusiness(BusinessContext context)
            : base(context)
        { }
        #endregion

        #region Methods
        public void Update(QuizUserLink quizUserLink)
        {
            _context.Repository.Objects.QuizUserLink.Update(quizUserLink);
        }

        public void UpdateFinalResult(QuizUserLink quizUserLink)
        {
            _context.Repository.Objects.QuizUserLink.UpdateFinalResult(quizUserLink);
        }
        public void Insert(QuizUserLink quizUserLink)
        {
            _context.Repository.Objects.QuizUserLink.Insert(quizUserLink);
        }

        public void InsertForGeneratingNewLink(QuizUserLink quizUserLink)
        {
            _context.Repository.Objects.QuizUserLink.InsertForGeneratingNewLink(quizUserLink);
        }
        public List<QuizUserLink> ReadAll()
        {
            return _context.Repository.Objects.QuizUserLink.ReadAll();
        }

        public List<QuizUserLink> ReadByQuizUID(Guid quizUID)
        {
            return _context.Repository.Objects.QuizUserLink.ReadByQuizId(quizUID);
        }

        public List<QuizUserLink> ReadByUserUID(Guid quizUID)
        {
            return _context.Repository.Objects.QuizUserLink.ReadByUserId(quizUID);
        }

        /// <summary>
        /// Sends the email to the desired set of people. This method sends a default email with all data already filled.
        /// </summary>
        /// <param name="emailAddr"></param>
        /// <param name="userGUID"></param>
        /// <param name="quizGUID"></param>
        public void SendEmail(List<string> selectedEmails, List<Guid> selectedGUID, Guid quizGUID)
        {
            MailSender mailSender = new MailSender();

            for (int i = 0; i < selectedEmails.Count; i++)
            {
                if (selectedEmails[i].Contains("\\"))
                {
                    selectedEmails[i] = mailSender.GetEmailAfterWindowsUserName(selectedEmails[i].Split('\\')[1]);
                }
                // this is the link with the information that will be created
                string link = string.Format(BusinessResources.QuizLink, quizGUID, selectedGUID[i]);
                mailSender.SendMail(selectedGUID[i], quizGUID, selectedEmails[i], BusinessResources.SenderMail, BusinessResources.EmailSubject, BusinessResources.MailBody + " " + link);
            }


        }
        /// <summary>
        /// This method sends a custom e-mail. This means that all thed ata needs to be provided in order for the email to be sent succesfully.
        /// </summary>
        /// <param name="selectedEmails"></param>
        /// <param name="selectedGUID"></param>
        /// <param name="quizGUID"></param>
        /// <param name="emailToAddr"></param>
        /// <param name="emailFromAddr"></param>
        /// <param name="realEmailSubject"></param>
        /// <param name="realEmailBody"></param>
        public void SendEmail(List<Guid> selectedGUID, Guid quizGUID, List<string> selectedEmails, string emailFromAddr, string realEmailSubject, string realEmailBody)
        {
            MailSender mailSender = new MailSender();

            for (int i = 0; i < selectedEmails.Count; i++)
            {
                if (selectedEmails[i].Contains("\\"))
                {
                    selectedEmails[i] = mailSender.GetEmailAfterWindowsUserName(selectedEmails[i].Split('\\')[1]);
                }

                if (emailFromAddr.Contains("\\"))
                {
                    emailFromAddr = mailSender.GetEmailAfterWindowsUserName(emailFromAddr.Split('\\')[1]);
                }


                mailSender.SendMail(selectedGUID[i], quizGUID, selectedEmails[i], emailFromAddr, realEmailSubject, realEmailBody);
            }


        }



        #endregion
    }
}
