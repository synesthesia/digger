using Microsoft.Extensions.Logging;
using System.IO.Abstractions;

namespace Digger.Infra.Files
{
    public class BookmarkFileWriter : IWriteFiles
    {
        private readonly IFileSystem _fileSystem;
        ILogger<BookmarkFileWriter> _log;

        public BookmarkFileWriter(IFileSystem fileSystem, ILogger<BookmarkFileWriter> log)
        {
            _fileSystem = fileSystem;
            _log = log;
        }

        /// <inheritdoc />
        public async Task<string> WriteFileSlugified(string path, string title, string mdContent)
        {
            var folderPath = _fileSystem.Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(folderPath))
            {
                throw new InvalidOperationException($"Cannot write to {path} - if this is the root of a drive please try a subfolder");
            }
            if (!_fileSystem.Directory.Exists(folderPath))
            {
                _fileSystem.Directory.CreateDirectory(folderPath);
            }

            var fileName = Slugify(title);

            if (!fileName.EndsWith(".md"))
            {
                fileName = fileName + ".md";
            }

            await _fileSystem.File.WriteAllTextAsync($"{path}/{fileName}", mdContent);

            _log.LogInformation("Wrote {path}/{fileName}", path, fileName);

            return fileName;

        }

        private string Slugify(string src, int maxLength = 20)
        {
            var slug = src.Replace(' ', '-').ToLower().Substring(0, maxLength);

            slug = slug.Replace(':', '-').Replace(';', '-').Replace('\\', '-').Replace('/', '-');
            slug = slug.TrimEnd('-');
            return slug;
        }

    }
}
