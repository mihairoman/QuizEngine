using System;

namespace QuizEngine.Model.Data
{
    /// <summary>
    /// Represents an answer choice for a specific <see cref="QuizEngine.Model.Data.Question"/>
    /// </summary>
    public class Choice
    {
        /// <summary>
        /// Unique Identifier for this <see cref="QuizEngine.Model.Data.Choice"/>
        /// </summary>
        public Guid ChoiceGUID { get; set; }

        /// <summary>
        /// The <see cref="QuizEngine.Model.Data.Question"/> to which
        /// this <see cref="QuizEngine.Model.Data.Choice"/> belons
        /// </summary>
        public Guid QuestionGUID { get; set; }

        public string AnswerText { get; set; }

        /// <summary>
        /// It is a values between 0 and 1 which will be 0 if this is a wrong answer,
        /// 1 if this is a correct answer and a value between them if it is a partially correct answer
        /// </summary>
        public decimal? Value { get; set; }

        /// <summary>
        /// The value is 1 if the Choice is correct and 0 if it is not.
        /// It cand also have a null value.
        /// </summary>
        public bool? IsCorrect { get; set; }

        /// <summary>
        /// This is the position of thre choice in order for the questions to be displayed in the correct order.
        /// </summary>
        public int ChoicePosition { get; set; }
    }
}
