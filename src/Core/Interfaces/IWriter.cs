using System.Threading.Tasks;

namespace Core.Interfaces
{
    /// <summary>
    /// Define interface to write string content to file.
    /// </summary>
    public interface IWriter
    {
        /// <summary>
        /// Write string content to file asynchronously.
        /// </summary>
        /// <param name="content">String writing content.</param>
        public Task WriteAsync(string content);
    }
}