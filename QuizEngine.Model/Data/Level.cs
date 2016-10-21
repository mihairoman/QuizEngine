using System;

namespace QuizEngine.Model.Data
{   
    /// <summary>
    /// 
    /// Class Level
    /// </summary>
    [Serializable]
    public class Level
    {
        #region Members
        private Guid _levelUID;
        private string _levelName;
        private Int16 _difficulty;
        #endregion Members

        #region Constructors
        /// <summary>
        /// Instantiates a <see cref="QuizEngine.Model.Data.Level"/>.
        /// </summary>
        public Level()
        {

        }
        /// <summary>
        /// Instantiates a <see cref="QuizEngine.Model.Data.Level"/>.
        /// </summary>
        /// <param name="levelUID"></param>
        /// <param name="levelName"></param>
        /// <param name="levelDifficulty"></param>
        public Level(Guid levelUID, string levelName, Int16 levelDifficulty)
        {
            _levelUID = levelUID;
            _levelName = levelName;
            _difficulty = levelDifficulty;
        }
        #endregion Constructors

        #region Properties
        public Guid LevelUID
        {
            get { return _levelUID; }
            set { _levelUID = value; }
        }

        public string LevelName
        {
            get { return _levelName; }
            set { _levelName = value; }
        }

        public Int16 Difficulty
        {
            get { return _difficulty; }
            set { _difficulty = value; }
        }
        #endregion Properties


    }
}
