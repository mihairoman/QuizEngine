using QuizEngine.Webservice.Utility;
using QuizEngine.Infrastructure;
using QuizEngine.Library;
using QuizEngine.UI.Core;
using System;
using System.Web;
using QuizEngine.Model.Data;

namespace QuizEngine.Webservice
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
            
            var fileTypeRequest = context.Request.QueryString["FileType"];
            var quizUIDRequest = context.Request.QueryString["QuizUID"];
            var userUIDRequest = context.Request.QueryString["UserUID"];
            Guid userUID;
            Guid.TryParse(userUIDRequest, out userUID);   
            UIContext uiContext = new UIContext(InitializeSeed(userUID)); 
            User currentUser = uiContext.Objects.User.ReadByID(userUID);
            if (!string.IsNullOrEmpty(fileTypeRequest) && !string.IsNullOrEmpty(quizUIDRequest) && !string.IsNullOrEmpty(currentUser.UserName))
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
                                IssueFileResponse(context, "application/pdf", new FileWriterStrategy(new PDFDocumentWriter(uiContext), quizUID, (currentUser.UserName)));
                                break;
                            case "WORD":
                                IssueFileResponse(context, "application/msword", new FileWriterStrategy(new WordDocumentWriter(uiContext), quizUID, currentUser.UserName));
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

        public ContextSeed InitializeSeed(Guid userUID)
        {          

            return new ContextSeed
            {
                UserID = userUID
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