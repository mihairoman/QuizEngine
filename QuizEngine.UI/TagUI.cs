using QuizEngine.Business.Core;
using QuizEngine.Infrastructure;
using QuizEngine.Model.Data;
using QuizEngine.UI.Core;
using System;
using System.Collections.Generic;

namespace QuizEngine.UI
{
   public class TagUI : UIObject
    {
        #region Constructors
        public TagUI(UIContext context) : base(context) { }
        #endregion

        #region Methods

        public List<Tag> ReadAll()
        {
            return _context.Business.Objects.Tag.ReadAll();
        }

        public Tag ReadByID(Guid tag)
        {
            return _context.Business.Objects.Tag.ReadByID(tag);
        }

        public void Update(Tag tag)
        {
            _context.Business.Objects.Tag.Update(tag);
        }

        public void Insert(Tag tag)
        {
            _context.Business.Objects.Tag.Insert(tag);
        }

        public void Delete(Tag tag)
        {
            _context.Business.Objects.Tag.Delete(tag);
        }

        public List<Tag> ReadByQuestionID(Guid questionID)
        {
            return _context.Business.Objects.Tag.ReadByQuestionID(questionID);
        }
        #endregion Methods
    }
}
