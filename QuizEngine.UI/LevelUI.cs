using QuizEngine.Model.Data;
using QuizEngine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizEngine.Business.Core;
using QuizEngine.UI.Core;

namespace QuizEngine.UI
{
   public class LevelUI : UIObject
    {
        #region Constructors
        public LevelUI(UIContext context) : base(context) { }
        #endregion

        #region Methods

        public List<Level> ReadAll()
        {
            return _context.Business.Objects.Level.ReadAll();
        }

        public Level ReadByID(Guid level)
        {
            return _context.Business.Objects.Level.ReadByID(level);
        }

        public void Update(Level level)
        {
            _context.Business.Objects.Level.Update(level);
        }

        public void Insert(Level level)
        {
            _context.Business.Objects.Level.Insert(level);
        }

        public void Delete(Level level)
        {
            _context.Business.Objects.Level.Delete(level);
        }

        #endregion Methods
    }
}
