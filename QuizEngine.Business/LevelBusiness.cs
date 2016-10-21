using QuizEngine.Infrastructure;
using QuizEngine.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizEngine.Business.Core;

namespace QuizEngine.Business
{
    /// <summary>
    /// Class Business
    /// </summary>
   public class LevelBusiness : BusinessObject
    {
        #region Constructors
        public LevelBusiness(BusinessContext repositoryContext) : base(repositoryContext) { }
        #endregion

        #region Methods

        public List<Level> ReadAll()
        {
            return _context.Repository.Objects.Level.ReadAll();
        }

        public Level ReadByID(Guid level)
        {
            return _context.Repository.Objects.Level.ReadById(level);
        }

        public void Update(Level level)
        {
            _context.Repository.Objects.Level.Update(level);
        }

        public void Insert(Level level)
        {
            _context.Repository.Objects.Level.Insert(level);
        }

        public void Delete(Level level)
        {
            _context.Repository.Objects.Level.Delete(level);
        }
        #endregion Methods
    }
}
