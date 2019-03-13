using FileHelperLibrary.AppException;
using FileHelperLibrary.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileHelperLibrary
{

    public abstract class FileHelper
    {
        public static ImageHelper Image = ImageHelper.GetInstance();

        public static ICollection<FileSystemInfo> GetFilesInfo(string path)
        {
            return GetFilesInfo(path, null);
        }

        public static ICollection<FileSystemInfo> GetFilesInfo(string path, IEnumerable<string> extensions)
        {
            return FileInfoHelper.GetInstance().GetFilesInfo(path, extensions);
        }

        public static FileHelperActionResult SaveFile(string path, Stream stream, string fileExtension, IEnumerable<string> AllowedFilesExtensions, int maxFileSize)
        {
            return TrySavingFile(path, stream, fileExtension, AllowedFilesExtensions, maxFileSize);
        }

        public static FileHelperActionResult SaveFile(string path, Stream stream, string fileExtension, IEnumerable<string> AllowedFilesExtensions)
        {
            return TrySavingFile(path, stream, fileExtension, AllowedFilesExtensions, -1);
        }

        public static FileHelperActionResult SaveFile(string path, Stream stream, string fileExtension)
        {
            return TrySavingFile(path, stream, fileExtension, null, -1);
        }

        private static FileHelperActionResult TrySavingFile(string path, Stream stream, string fileExtension, IEnumerable<string> allowedFilesExtensions, int maxFileSize)
        {
            if (stream == null) throw new NullReferenceException("stream");

            if (!IsMaxFileSizeExceeded(stream.Length,maxFileSize))
            {
                if (IsFileExtensionAllowed(fileExtension, allowedFilesExtensions))
                {
                    return Save(path, stream, fileExtension);
                }
                else
                {
                    return new FileHelperActionResult(FileHelperActionStatus.Error, FileHelperException.GetInstance("uex-2"));
                }
            }
            else
            {
                return new FileHelperActionResult(FileHelperActionStatus.Error, FileHelperException.GetInstance("uex-3"));
            }
        }

        private static FileHelperActionResult Save(string path, Stream stream, string fileExtension)
        {
            try
            {
                var filename = Guid.NewGuid().ToString();
                fileExtension = GetValidExtension(fileExtension);

                filename = filename + fileExtension;

                var route = path + "/" + filename;

                var file = new byte[stream.Length];
                stream.Read(file, 0, file.Length);

                File.WriteAllBytes(route, file);

                return new FileHelperActionResult(FileHelperActionStatus.Successful,
                    new Entities.FileInfo
                    {
                        Extension = fileExtension,
                        Name = filename,
                        Path = route,
                        Size = file.Length
                    });
            }
            catch (Exception)
            {
                return new FileHelperActionResult(FileHelperActionStatus.Undefine, FileHelperException.GetInstance("uex-1"));
            }
        }

        private static string GetValidExtension(string extension)
        {
            if (string.IsNullOrEmpty(extension)) return string.Empty;

            if (!extension[0].Equals('.'))
            {
                return string.Format(".{0}", extension);
            }

            return extension;
        }

        private static bool IsFileExtensionAllowed(string extension, IEnumerable<string> extensions)
        {
            if (extensions == null) return true;

            return extensions.Contains(extension) ? true : false;
        }

        private static bool IsMaxFileSizeExceeded(long size, int maxFileSize)
        {
            if (maxFileSize == -1) return false;

            return (size <= ConvertMbToBytes(maxFileSize)) ? false : true;
        }

        private static long ConvertMbToBytes(int mb)
        {
            return mb * 1048576;
        }
    }
}
