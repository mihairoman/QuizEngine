using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.Model.View
{
    public class QuizesPerUserView
    {
        #region Members
        private Guid _quizUID;
        private DateTime _quizDate;
        private string _categoryName;
        private string _levelName;
        private string _onlineOrDownloaded;
        private string _takenOrNot;
        private string _result;
        #endregion Members

        #region Constructors
        public QuizesPerUserView()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quizUID"></param>
        /// <param name="quizDate"></param>
        /// <param name="categoryName"></param>
        /// <param name="levelName"></param>
        /// <param name="onlineOrDownloaded"></param>
        /// <param name="takenOrNot"></param>
        /// <param name="result"></param>
        public QuizesPerUserView(Guid quizUID, DateTime quizDate, string categoryName, string levelName, string onlineOrDownloaded, string takenOrNot, string result)
        {
            _quizUID = quizUID;
            _quizDate = quizDate;
            _categoryName = categoryName;
            _levelName = levelName;
            _onlineOrDownloaded = onlineOrDownloaded;
            _takenOrNot = takenOrNot;
            _result = result;
        }
        #endregion Constructors

        #region Properties
        public Guid QuizUID
        {
            get { return _quizUID; }
            set { _quizUID = value; }
        }

        public DateTime QuizDate
        {
            get { return _quizDate; }
            set { _quizDate = value; }
        }
        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        public string LevelName
        {
            get { return _levelName; }
            set { _levelName = value; }
        }
        public string OnlineOrDownloaded
        {
            get { return _onlineOrDownloaded; }
            set { _onlineOrDownloaded = value; }
        }

        public string TakenOrNot
        {
            get { return _takenOrNot; }
            set { _takenOrNot = value; }
        }
        public string Result
        {
            get { return _result; }
            set { _result = value; }
        }
        #endregion Properties
    }
}
