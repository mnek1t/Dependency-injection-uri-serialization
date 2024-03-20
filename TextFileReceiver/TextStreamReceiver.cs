using DataReceiving;
using Microsoft.Extensions.Logging;

namespace TextFileReceiver
{
    /// <summary>
    /// The data receiver from text file.
    /// </summary>
    public class TextStreamReceiver : IDataReceiver
    {
        private readonly string? path;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextStreamReceiver"/> class.
        /// </summary>
        /// <param name="path">The path to text file.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentException">Throw if text reader is null or empty.</exception>
        public TextStreamReceiver(string? path, ILogger<TextStreamReceiver>? logger = default)
        {
            this.path = path;
        }

        /// <summary>
        /// Receives lines from text reader.
        /// </summary>
        /// <returns>Strings.</returns>
        public IEnumerable<string> Receive()
        {
            var strings = new List<string>();
            using (FileStream stream = new FileStream(this.path, FileMode.Open, FileAccess.Read)) 
            {
                using (StreamReader reader = new StreamReader(stream)) 
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        strings.Add(line);
                    }
                }
            }

            return strings;
        }
    }
}
