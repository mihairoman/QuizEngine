using QuizEngine.Infrastructure;

namespace QuizEngine.Repository.Core
{
    public class RepositoryObjectContainer: BaseObjectContainer<RepositoryContext>
    {
        #region Constructor
        public RepositoryObjectContainer(RepositoryContext context) : base(context) { }
        #endregion Constructor

        #region Properties
        public AnswerRepository Answer
        {
            get { return GetObject<AnswerRepository>(); }
        }

        public ArchivedChoiceRepository ArchivedChoice
        {
            get { return GetObject<ArchivedChoiceRepository>(); }
        }

        public ArchivedQuestionRepository ArchivedQuestion
        {
            get { return GetObject<ArchivedQuestionRepository>(); }
        }

        public CategoryRepository Category
        {
            get { return GetObject<CategoryRepository>(); }
        }

        public CategoryViewRepository CategoryView
        {
            get { return GetObject<CategoryViewRepository>(); }
        }

        public ChoiceRepository Choice
        {
            get { return GetObject<ChoiceRepository>(); }
        }

        public LevelRepository Level
        {
            get { return GetObject<LevelRepository>(); }
        }

        public QuestionRepository Question
        {
            get { return GetObject<QuestionRepository>(); }
        }

        public QuestionQuizRepository QuestionQuizView
        {
            get { return GetObject<QuestionQuizRepository>(); }
        }

        public QuestionTagRepository QuestionTag
        {
            get { return GetObject<QuestionTagRepository>(); }
        }

        public QuestionViewRepository QuestionView
        {
            get { return GetObject<QuestionViewRepository>(); }
        }

        public QuizzesPerUserViewRepository QuizesPerUserView
        {
            get { return GetObject<QuizzesPerUserViewRepository>(); }
        }

        public QuizRepository Quiz
        {
            get { return GetObject<QuizRepository>(); }
        }

        public QuizUserLinkRepository QuizUserLink
        {
            get { return GetObject<QuizUserLinkRepository>(); }
        }

        public QuizTemplateRepository QuizTemplate
        {
            get { return GetObject<QuizTemplateRepository>(); }
        }

        public QuizTemplateLinksRepository QuizTemplateLink
        {
            get { return GetObject<QuizTemplateLinksRepository>(); }
        }

        public QuizTemplateViewRepository QuizTemplateView
        {
            get { return GetObject<QuizTemplateViewRepository>();}
        }

        public QuizViewRepository QuizView
        {
            get { return GetObject<QuizViewRepository>(); }
        }

        public TagRepository Tag
        {
            get { return GetObject<TagRepository>(); }
        }

        public UserRepository User
        {
            get { return GetObject<UserRepository>(); }
        }

        public FreeTextQuizViewRepository FreeTextQuizView
        {
            get { return GetObject<FreeTextQuizViewRepository>(); }
        }
        public SecurityRepository Security
        {
            get { return GetObject<SecurityRepository>(); }
        }

        public QuizResultRepository QuizResult
        {
            get { return GetObject<QuizResultRepository>(); }
        }

        public QuizUserViewRepository QuizUserView
        {
            get { return GetObject<QuizUserViewRepository>(); }
        }

        public ValidationFreeTextQuestionsViewRepository ValidationFreeTextQuestionsView
        {
            get { return GetObject<ValidationFreeTextQuestionsViewRepository>(); }
        }

        public LinkPermissionRepository LinkPermission
        {
            get { return GetObject<LinkPermissionRepository>(); }
        }

        public AdminDownloadGeneratedTestRepository AdminDownloadGeneratedTestView
        {
            get { return GetObject<AdminDownloadGeneratedTestRepository>(); }
        }


        public UsersWithoutQuizViewRepository UsersWithoutQuizView 
        {
            get { return GetObject<UsersWithoutQuizViewRepository>(); }
        }

        public AdminDownloadPredefinedTestViewRepository AdminDownloadPredefinedTestView
        {
            get { return GetObject<AdminDownloadPredefinedTestViewRepository>(); }
        }
        #endregion Properties
    }
}
