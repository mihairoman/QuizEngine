using QuizEngine.ClassicWebForms.Utility;
using QuizEngine.Infrastructure;
using QuizEngine.Library;
using QuizEngine.UI.Core;
using System;
using System.Web;

namespace QuizEngine.ClassicWebForms.Pages
{
    /// <summary>
    /// Handles generate file requests
    /// </summary>
    public class GenerateFileHandler : IHttpHandler
    {
        #region Methods
        /// <summary>
        /// Method to process the requests
        /// </summary>
        /// <param name="context">Current context of the request</param>
        public void ProcessRequest(HttpContext context)
        {
            UIContext uiContext = new UIContext(InitializeSeed());
            var fileTypeRequest = context.Request.QueryString["FileType"];
            var quizUIDRequest = context.Request.QueryString["QuizUID"];
            string userName="";
            if (HttpContext.Current.Request.Cookies["currentUser"]["email"] != null)
            {
                userName = HttpContext.Current.Request.Cookies["currentUser"]["email"].ToString();
            }
            if (!string.IsNullOrEmpty(fileTypeRequest) && !string.IsNullOrEmpty(quizUIDRequest) && !string.IsNullOrEmpty(userName))
            {
                try
                {
                    string fileType = Enum.GetName(typeof(FileType),Convert.ToInt32(fileTypeRequest)).ToUpper();
                    Guid quizUID;
                    Guid.TryParse(quizUIDRequest, out quizUID);

                    if (quizUID != null)
                    {                    
                        switch (fileType)
                        {
                            case "PDF":
                                IssueFileResponse(context, "application/pdf", new FileWriterStrategy(new PDFDocumentWriter(uiContext), quizUID, userName));
                                break;
                            case "WORD":
                                IssueFileResponse(context, "application/msword", new FileWriterStrategy(new WordDocumentWriter(uiContext), quizUID, userName));
                                break;
                            default:
                                IssueInvalidDataResponse(context);
                                break;
                        }
                    }
                    else
                    {
                        IssueInvalidDataResponse(context);
                    }
                }
                catch (Exception)
                {
                    IssueInvalidDataResponse(context);
                }
            }
            else
            {
                IssueInvalidDataResponse(context);
            } 
        }

        private static void IssueFileResponse(HttpContext context, string contentType, FileWriterStrategy strategy)
        {
            context.Response.ContentType = contentType;
            var strategyCall = strategy.CallWriterStrategy();
            if (strategyCall != null)
            {
                context.Response.BinaryWrite(strategyCall);
            }
            else
            {
                IssueInvalidDataResponse(context);
            }
        }

        private static void IssueInvalidDataResponse(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Invalid Data");
        }

        public ContextSeed InitializeSeed()
        {
            Guid userGUID = Guid.Empty;

            if (HttpContext.Current.Request.Cookies["currentUser"]["userID"] != null)
            {
                userGUID = new Guid(HttpContext.Current.Request.Cookies["currentUser"]["userID"]);
            }

            return new ContextSeed
            {
                UserID = userGUID
            };
        }
        #endregion Methods

        #region IHttpHandler Implementation
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion 
    }
}