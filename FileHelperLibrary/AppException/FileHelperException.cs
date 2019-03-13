using FileHelperLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileHelperLibrary.AppException
{
    public class FileHelperException:Exception
    {
        private static ICollection<ErrorMessage> ErrorMessages { get; set; }
        public string Code { get; set; }

        private FileHelperException(string message) : base(message)
        {
        }

        public static FileHelperException GetInstance(string code)
        {
            LoadMessages();

            var exception = new FileHelperException(ErrorMessages
                .FirstOrDefault(em => em.Code.Equals(code)).Message)
            {
                Code = code
            };

            return exception;
        }

        private static void LoadMessages()
        {
            if (ErrorMessages == null)
            {
                ErrorMessages = new List<ErrorMessage>
                {
                    new ErrorMessage("An error occurred", "uex-1"),
                    new ErrorMessage("Extension not allowed", "uex-2"),
                    new ErrorMessage("Maximum size exceeded", "uex-3"),
                    new ErrorMessage("No file found", "uex-4")
                };
            }
        }
    }
}
