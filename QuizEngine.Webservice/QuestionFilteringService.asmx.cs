using QuizEngine.Webservice;
using QuizEngine.Library;
using QuizEngine.Model.Data;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Services;


namespace QuizEngine.ClassicWebForms.Services
{
    /// <summary>
    /// Summary description for QuestionFilteringService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class QuestionFilteringService : BaseWebservice
    {

        #region Members
        string[] _existingQuestionTypes = new String[Enum.GetNames(typeof(QuestionType)).Length];
        Guid _selectedCategoryUid;
        Guid _selectedLevelUid;
        Int16 _selectedLevel;
        Int16 _majorityofquestions;
        Int16 _lowerlevel;
        Int16 _higherlevel;
        Int16 _minorityofquestions;
        List<Guid> _selectedTagUid = new List<Guid>();
        List<Int16> _selectedType = new List<Int16>();
        List<string> _selectedQuestionType = new List<string>();
        string _selectedTags = "";
        string _sortExpression;
        string _selectedCategory;
        JavaScriptSerializer _serializer = new JavaScriptSerializer();
        int _pageNumber;
        int _rowsPerPage;
        #endregion Members

        #region Methods
        /// <summary>
        /// This method is used to return all categories from the databse 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetAllCategories()
        {

            List<Category> allCategories = _context.Objects.Category.ReadAll();
            List<KeyValuePairInfo<Guid, String>> allOptions = new List<KeyValuePairInfo<Guid, String>>(0);
            foreach (Category cat in allCategories)
            {
                KeyValuePairInfo<Guid, String> key = new KeyValuePairInfo<Guid, String>
                {
                    Key = cat.CategoryUID,
                    Value = cat.CategoryName
                };
                allOptions.Add(key);
            }
            return _serializer.Serialize(allOptions);
        }

        /// <summary>
        /// This method returns all choices based on the question GUID
        /// </summary>
        /// <param name="questionGUID"></param>
        /// <param name="questionType"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetChoicesAfterQuestionGuid(string questionGUID, string questionType)
        {
            List<Choice> allChoices = _context.Objects.Choice.ReadChoiceByQuestionID(new Guid(questionGUID));
            return _serializer.Serialize(allChoices);
        }

        /// <summary>
        /// This method returns all levels
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public String GetAllLevels()
        {
            List<Level> allLevels = _context.Objects.Level.ReadAll();
            List<KeyValuePairInfo<Guid, String>> allOptions = new List<KeyValuePairInfo<Guid, String>>();
            foreach (Level level in allLevels)
            {
                KeyValuePairInfo<Guid, String> key = new KeyValuePairInfo<Guid, String>
                {
                    Key = level.LevelUID,
                    Value = level.LevelName
                };
                allOptions.Add(key);
            }
            return _serializer.Serialize(allOptions);
        }

        /// <summary>
        /// This method returns all tags
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public String GetAllTags()
        {
            List<Tag> allTags = _context.Objects.Tag.ReadAll();
            List<KeyValuePairInfo<Guid, String>> allOptions = new List<KeyValuePairInfo<Guid, String>>();
            foreach (Tag tag in allTags)
            {
                KeyValuePairInfo<Guid, String> key = new KeyValuePairInfo<Guid, String>
                {
                    Key = tag.TagUID,
                    Value = tag.TagName
                };
                allOptions.Add(key);
            }

            return _serializer.Serialize(allOptions);
        }

        /// <summary>
        /// This method returns all QuestionTypes
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public String GetAllQuestionTypes()
        {
            List<KeyValuePairInfo<Guid, String>> allOptions = new List<KeyValuePairInfo<Guid, String>>();
            var values = Enum.GetValues(typeof(QuestionType));
            foreach (QuestionType type in values)
            {
                KeyValuePairInfo<Guid, String> key = new KeyValuePairInfo<Guid, String>
                {
                    Key = Guid.NewGuid(),
                    Value = type.ToString()
                };
                allOptions.Add(key);
            }

            return _serializer.Serialize(allOptions);
        }

        /// <summary>
        /// This method returns the questions based on the fitlers
        /// </summary>
        /// <param name="categoryGUID"></param>
        /// <param name="levelGUID"></param>
        /// <param name="tagGuidList"></param>
        /// <param name="questionTypeList"></param>
        /// <param name="sortExpression"></param>
        /// <param name="rowsPerPage"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [WebMethod]
        public String GetFilteredQuestions(string categoryGUID, string levelGUID, string tagGuidList, string questionTypeList, string sortExpression, string rowsPerPage, string pageNumber)
        {
            _selectedCategoryUid = _serializer.Deserialize<Guid>(categoryGUID);
            _selectedLevelUid = _serializer.Deserialize<Guid>(levelGUID);
            _selectedTagUid = _serializer.Deserialize<List<Guid>>(tagGuidList);

            List<string> alltypes = new List<string>();
            alltypes = _serializer.Deserialize<List<string>>(questionTypeList);

            foreach (string type in alltypes)
            {
                QuestionType questionType = (QuestionType)Enum.Parse(typeof(QuestionType), type.ToString());
                _selectedType.Add((Int16)questionType.GetHashCode());

            }
            _sortExpression = _serializer.Deserialize<String>(sortExpression);

            _rowsPerPage = _serializer.Deserialize<Int16>(rowsPerPage);
            _pageNumber = _serializer.Deserialize<Int16>(pageNumber);

            return _serializer.Serialize(_context.Objects.Question.ReadAllFiltered(_selectedCategoryUid, _selectedLevelUid, _selectedTagUid, _selectedType, _sortExpression, _rowsPerPage, _pageNumber));
        }

        /// <summary>
        /// This metghod returns the number of questions that respect the fitler information
        /// </summary>
        /// <param name="categoryGUID"></param>
        /// <param name="levelGUID"></param>
        /// <param name="tagGuidList"></param>
        /// <param name="questionTypeList"></param>
        /// <returns></returns>
        [WebMethod]
        public String CountAllFilteredQuestions(string categoryGUID, string levelGUID, string tagGuidList, string questionTypeList)
        {
            _selectedCategoryUid = _serializer.Deserialize<Guid>(categoryGUID);
            _selectedLevelUid = _serializer.Deserialize<Guid>(levelGUID);
            _selectedTagUid = _serializer.Deserialize<List<Guid>>(tagGuidList);

            List<string> alltypes = new List<string>();
            alltypes = _serializer.Deserialize<List<string>>(questionTypeList);

            foreach (string type in alltypes)
            {
                QuestionType questionType = (QuestionType)Enum.Parse(typeof(QuestionType), type.ToString());
                _selectedType.Add((Int16)questionType.GetHashCode());

            }

            return _serializer.Serialize(_context.Objects.Question.CountAllFilteredQuestions(_selectedCategoryUid, _selectedLevelUid, _selectedTagUid, _selectedType));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public String CountAllQuestions() {
            return _serializer.Serialize(_context.Objects.Question.CountAllQuestions());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="levelDifficulty"></param>
        /// <param name="majorityNumber"></param>
        /// <param name="lowerLevel"></param>
        /// <param name="higherLevel"></param>
        /// <param name="minorityNumber"></param>
        /// <param name="categoryName"></param>
        /// <param name="tagList"></param>
        /// <param name="questionTypeList"></param>
        /// <returns></returns>
        [WebMethod]
        public String GetQuestionsByDifficulty(string levelDifficulty, string majorityNumber, string lowerLevel, string higherLevel, string minorityNumber, string categoryName,  string tagList, string questionTypeList)
        {
            _selectedLevel = _serializer.Deserialize<Int16>(levelDifficulty);
            _majorityofquestions = _serializer.Deserialize<Int16>(majorityNumber);
            _lowerlevel = _serializer.Deserialize<Int16>(lowerLevel);
            _higherlevel = _serializer.Deserialize<Int16>(higherLevel);
            _minorityofquestions = _serializer.Deserialize<Int16>(minorityNumber);

            _selectedCategory = _serializer.Deserialize<string>(categoryName);
            _selectedTags = _serializer.Deserialize<string>(tagList);

            string alltypes ;
            alltypes = _serializer.Deserialize<string>(questionTypeList);


            return _serializer.Serialize(_context.Objects.Question.ReadByDifficulty(_selectedLevel, _majorityofquestions, _lowerlevel, _higherlevel, _minorityofquestions, _selectedTags, _selectedCategory, alltypes));
        }


        /// <summary>
        /// This method is sued to delete a certain question
        /// </summary>
        /// <param name="questionGUID"></param>
        [WebMethod]
        public void DeleteQuestion(string questionGUID)
        {
            Guid deleteQuestionGUID = _serializer.Deserialize<Guid>(questionGUID);

            _context.Objects.Question.Delete(new Question { QuestionGUID = deleteQuestionGUID });
        }

        #endregion

    }
}
