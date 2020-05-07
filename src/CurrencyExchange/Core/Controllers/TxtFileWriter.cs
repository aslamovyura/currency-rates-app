using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Interfaces;

namespace CurrencyExchange.Core.Controllers
{
    /// <summary>
    /// Define object to write string content to file.
    /// </summary>
    public class TxtFileWriter : IWriter
    {
        // File extension.
        private const string EXT = ".txt";

        /// <summary>
        /// File name without extenstion ('rates' by default).
        /// </summary>
        public string FileName { get; set; } = "rates";

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TxtFileWriter() { }

        /// <summary>
        /// Write string content to file asynchronously.
        /// </summary>
        /// <param name="content">String writing content.</param>
        /// <returns>Task.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task WriteAsync(string content)
        {
            content = content ?? throw new ArgumentNullException(nameof(content));
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), FileName + EXT);

            using (StreamWriter writer = new StreamWriter(filePath, true, System.Text.Encoding.Default))
            {
                await writer.WriteLineAsync(content);
            }
        }
    }
}