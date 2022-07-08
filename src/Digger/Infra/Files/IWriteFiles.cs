namespace Digger.Infra.Files
{
    public interface IWriteFiles
    {
        /// <summary>
        /// Saves file with slugified filename
        /// </summary>
        /// <param name="path">Relative poath to output directory</param>
        /// <param name="title">Document title</param>
        /// <param name="mdContent">Document content</param>
        /// <returns>Slugified filename</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Task<string> WriteFileSlugified(string path, string title, string mdContent);
    }
}
