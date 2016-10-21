using QuizEngine.Infrastructure;

namespace QuizEngine.Business.Core
{
    public class BusinessObjectContainer : BaseObjectContainer<BusinessContext>
    {
        #region Constructor
        public BusinessObjectContainer(BusinessContext context)
            : base(context)
	    {

	    }
        #endregion Constructor

        #region Properties
        public AnswerBusiness Answer
        {
            get { return GetObject<AnswerBusiness>(); }
        }

        public ArchivedChoiceBusiness ArchivedChoice
        {
            get { return GetObject<ArchivedChoiceBusiness>(); }
        }

        public ArchivedQuestionBusiness ArchivedQuestion
        {
            get { return GetObject<ArchivedQuestionBusiness>(); }
        }

        public CategoryBusiness Category
        {
            get { return GetObject<CategoryBusiness>(); }
        }

        public ChoiceBusiness Choice
        {
            get { return GetObject<ChoiceBusiness>(); }
        }

        public LevelBusiness Level
        {
            get { return GetObject<LevelBusiness>(); }
        }

        public QuestionBusiness Question
        {
            get { return GetObject<QuestionBusiness>(); }
        }

        public QuestionTagBusiness QuestionTag
        {
            get { return GetObject<QuestionTagBusiness>(); }
        }

        public QuizBusiness Quiz
        {
            get { return GetObject<QuizBusiness>(); }
        }

        public QuizUserLinkBusiness QuizUserLink
        {
            get { return GetObject<QuizUserLinkBusiness>(); }
        }

        public TagBusiness Tag
        {
            get { return GetObject<TagBusiness>(); }
        }

        public UserBusiness User
        {
            get { return GetObject<UserBusiness>(); }
        }

        public FreeTextQuizViewBusiness FreeTextQuizView
        {
            get { return GetObject<FreeTextQuizViewBusiness>(); }
        }

        public QuizTemplateBusiness QuizTemplate
        {
            get { return GetObject<QuizTemplateBusiness>(); }
        }

        public SecurityBusiness Security
        {
            get { return GetObject<SecurityBusiness>(); }
        }

        public QuizResultBusiness QuizResult
        {
            get { return GetObject<QuizResultBusiness>(); }
        }

        public ValidationFreeTextQuestionsViewBusiness ValidationFreeTextQuestionsView
        {
            get { return GetObject<ValidationFreeTextQuestionsViewBusiness>(); }
        }

        public LinkPermissionBusiness LinkPermission
        {
            get { return GetObject<LinkPermissionBusiness>(); }
        }

        public AdminDownloadGeneratedTestBusiness AdminDownloadGeneratedTestView
        {
            get { return GetObject<AdminDownloadGeneratedTestBusiness>(); }
        }

        public UsersWithoutQuizViewBusiness UsersWithoutQuizView
        {
            get { return GetObject<UsersWithoutQuizViewBusiness>(); }
        }

        public AdminDownloadPredefinedTestViewBusiness AdminDownloadPredefinedTestView
        {
            get { return GetObject<AdminDownloadPredefinedTestViewBusiness>(); }
        }

        #endregion Properties
    }
}
