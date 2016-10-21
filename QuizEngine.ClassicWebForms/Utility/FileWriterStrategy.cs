using System;

namespace QuizEngine.ClassicWebForms.Utility
{
    /// <summary>
    /// Class that sets the concrete writer, calls the dictionary generator for the writers
    /// Returns the stream of data from the writers
    /// </summary>
    public class FileWriterStrategy
    {
        #region Members
        /// <summary>
        /// Concrete writer
        /// </summary>
        private QuizDocumentWriter _quizDocumentWriter;
        #endregion Members

        /// <summary>
        /// Method that sets the concrete writer and calls its dictionary filling
        /// </summary>
        /// <param name="quizDocumentWriter">Concrete document writer<param>
        /// <param name="quizUID">the id of the quiz</param>
        /// <param name="userName">the name of the creator</param>
        public FileWriterStrategy(QuizDocumentWriter quizDocumentWriter, Guid quizUID, string userName)
        {
            if (quizDocumentWriter != null)
            {
                _quizDocumentWriter = quizDocumentWriter;
                _quizDocumentWriter.GenerateDictionary(quizUID, userName);
            }
        }

        /// <summary>
        /// Method that calls the generate file method from the concrete writer and returns the received stream of data
        /// </summary>
        /// <returns>Stream of data</returns>
        public byte[] CallWriterStrategy()
        {
            if (_quizDocumentWriter != null)
            {
                return _quizDocumentWriter.GenerateFile();
            }
            return null;
        }
    }
}
