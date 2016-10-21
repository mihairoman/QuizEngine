using QuizEngine.Infrastructure;

namespace QuizEngine.UI.Core
{
    public class UIObject : BaseObject<UIContext>
    {
        #region Constructor
        public UIObject(UIContext uiContext) : base(uiContext) { }
        #endregion Constructor
    }
}
