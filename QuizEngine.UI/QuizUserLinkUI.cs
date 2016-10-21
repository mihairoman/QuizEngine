using QuizEngine.Model.Data;
using System;
using System.Collections.Generic;
using QuizEngine.Infrastructure;
using QuizEngine.Business.Core;
using QuizEngine.UI.Core;

namespace QuizEngine.UI
{
    public class QuizUserLinkUI : UIObject
    {
        #region Constructors
        public QuizUserLinkUI(UIContext context) : base(context) { }
        #endregion

        #region Methods
        public void Update(QuizUserLink quizUserLink)
        {
            _context.Business.Objects.QuizUserLink.Update(quizUserLink);
        }
  
        public void Insert(QuizUserLink quizUserLink)
        {
            _context.Business.Objects.QuizUserLink.Insert(quizUserLink);
        }

        public void InsertForGeneratingNewLink(QuizUserLink quizUserLink)
        {
            _context.Business.Objects.QuizUserLink.InsertForGeneratingNewLink(quizUserLink);
        }


        public List<QuizUserLink> ReadAll()
        {
            return _context.Business.Objects.QuizUserLink.ReadAll();
        }

        public List<QuizUserLink> ReadByQuizUID(Guid quizUID)
        {
            return _context.Business.Objects.QuizUserLink.ReadByQuizUID(quizUID);
        }

        public List<QuizUserLink> ReadByUserUID(Guid quizUID)
        {
            return _context.Business.Objects.QuizUserLink.ReadByUserUID(quizUID);
        }
        public void UpdateFinalResult(QuizUserLink quizUserLink)
        {
            _context.Business.Objects.QuizUserLink.UpdateFinalResult(quizUserLink);
        }
        #endregion
    }
}
