using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileHelperLibrary
{
    public class FileInfoHelper
    {
        private static FileInfoHelper _instance { get; set; }

        private FileInfoHelper()
        {
        }

        public static FileInfoHelper GetInstance()
        {
            if (_instance == null) _instance = new FileInfoHelper();

            return _instance;
        }

        public ICollection<FileSystemInfo> GetFilesInfo(string path)
        {
            return Process(path, null);
        }

        public ICollection<FileSystemInfo> GetFilesInfo(string path, IEnumerable<string> extensions)
        {
            return Process(path, extensions);
        }

        private ICollection<FileSystemInfo> Process(string path, IEnumerable<string> extensions)
        {
            if (string.IsNullOrEmpty(path)) return new List<FileSystemInfo>();

            var directoryInfo = new DirectoryInfo(path);
            var filesSystemInfo = new List<FileSystemInfo>();

            var filesInfo = directoryInfo.EnumerateFileSystemInfos();

            foreach (var fileInfo in filesInfo)
            {
                if (fileInfo.Attributes == FileAttributes.Archive &&
                    extensions == null || IsFileExtensionAllowed(extensions, fileInfo.Extension))
                {
                    filesSystemInfo.Add(fileInfo);
                }
            }

            return filesSystemInfo;
        }

        private bool IsFileExtensionAllowed(IEnumerable<string> extensions, string fileExtension)
        {
            if (extensions == null) return true;

            return extensions.Contains(fileExtension);
        }
    }
}
