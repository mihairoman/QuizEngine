using QuizEngine.Infrastructure;

namespace QuizEngine.UI.Core
{
    public class UIObjectContainer : BaseObjectContainer<UIContext>
    {
        #region Constructor
        public UIObjectContainer(UIContext context) : base(context) { }
        #endregion Constructor

        #region Properties
        public AnswerUI Answer
        {
            get { return GetObject<AnswerUI>(); }
        }

        public ArchivedChoiceUI ArchivedChoice
        {
            get { return GetObject<ArchivedChoiceUI>(); }
        }

        public ArchivedQuestionUI ArchivedQuestion
        {
            get { return GetObject<ArchivedQuestionUI>(); }
        }

        public CategoryUI Category
        {
            get { return GetObject<CategoryUI>(); }
        }

        public ChoiceUI Choice
        {
            get { return GetObject<ChoiceUI>(); }
        }

        public LevelUI Level
        {
            get { return GetObject<LevelUI>(); }
        }

        public QuestionUI Question
        {
            get { return GetObject<QuestionUI>(); }
        }

        public QuestionTagUI QuestionTag
        {
            get { return GetObject<QuestionTagUI>(); }
        }

        public QuizUI Quiz
        {
            get { return GetObject<QuizUI>(); }
        }

        public QuizUserLinkUI QuizUserLink
        {
            get { return GetObject<QuizUserLinkUI>(); }
        }

        public TagUI Tag
        {
            get { return GetObject<TagUI>(); }
        }

        public UserUI User
        {
            get { return GetObject<UserUI>(); }
        }

        public FreeTextQuizViewUI FreeTextQuizView
        {
            get { return GetObject<FreeTextQuizViewUI>(); }
        }

        public QuizTemplateUI QuizTemplate
        {
            get { return GetObject<QuizTemplateUI>(); }
        }

        public QuizResultUI QuizResult
        {
            get { return GetObject<QuizResultUI>(); }
        }

        public SecurityUI Security
        {
            get { return GetObject<SecurityUI>(); }
        }

        public ValidationFreeTextQuestionsViewUI ValidationFreeTextQuestionsView
        {
            get { return GetObject<ValidationFreeTextQuestionsViewUI>(); }
        }

        public LinkPermissionUI LinkPermission
        {
            get { return GetObject<LinkPermissionUI>(); }
        }

        public AdminDownloadGeneratedTestUI AdminDownloadGeneratedTestView
        {
            get { return GetObject<AdminDownloadGeneratedTestUI>(); }
        }

        public UsersWithoutQuizViewUI UsersWithoutQuizView
        {
            get { return GetObject<UsersWithoutQuizViewUI>(); }
        }

        public AdminDownloadPredefinedTestViewUI AdminDownloadPredefinedTestView
        {
            get { return GetObject<AdminDownloadPredefinedTestViewUI>(); }
        }
        #endregion Properties
    }
}
