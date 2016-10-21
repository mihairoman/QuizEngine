using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.Library
{
    /// <summary>
    /// Describes the type of file to be downloaded
    /// </summary>
    public enum FileType : int
    {
        /// <summary>
        /// PDF file: It's integer Value is 0
        /// </summary>
        PDF = 0,
        /// <summary>
        /// PDF file: It's integer Value is 1
        /// </summary>
        WORD = 1        
    }
}
