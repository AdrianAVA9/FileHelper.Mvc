using FileHelperLibrary.Web.Components.Templates;
using System.Text;

namespace FileHelperLibrary.Web.Components.Controls
{
    public class CtrlFormModel : CtrlBaseModel
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string LabelText { get; set; }
        public string AllowedFilesExtension { get; set; }
        public string FilesExtensionForJs { get; set; }
        public string ImgTitle { get; set; }
        public string BtnValue { get; set; }
        public long MaxFileSize { get; set; }
        public string MsgForMaxSize { get; set; }
        public string MsgForExtensionNotAllowed { get; set; }
        public string ErrorMessageId { get; set; }

        public CtrlFormModel(string action, string controller,
         string labelText, string allowedFilesExtension, string imgTitle, string btnValue, int maxSizeFile, string msgMaxSize,
         string msgForExtensionNotAllowed, string errorMessageId)
        {
            Controller = controller;
            Action = action;
            LabelText = labelText;
            AllowedFilesExtension = allowedFilesExtension;
            FilesExtensionForJs = FormatExtensionsForJS(allowedFilesExtension);
            ImgTitle = imgTitle;
            BtnValue = btnValue;
            MaxFileSize = ConvertMbToBytes(maxSizeFile);
            MsgForMaxSize = msgMaxSize;
            MsgForExtensionNotAllowed = msgForExtensionNotAllowed;
            ErrorMessageId = errorMessageId;
        }

        private string FormatExtensionsForJS(string allowedFilesExtension)
        {
            if (string.IsNullOrEmpty(allowedFilesExtension)) return string.Empty;

            var extensions = allowedFilesExtension.Split(',');
            var builder = new StringBuilder();

            foreach (var extension in extensions)
            {
                builder.Append(string.Format("'{0}',", extension));
            }

            return builder.ToString().Replace(" ", "");
        }

        private long ConvertMbToBytes(int maxSizeFile)
        {
            return maxSizeFile * 1048576;
        }

        public override string GetHtml()
        {
            var html = CtrlFormModelTemplate.Style() + CtrlFormModelTemplate.Html() +
                CtrlFormModelTemplate.Logic();

            html = ReplaceTags( this.GetType().GetProperties(System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.Public),html);

            return html;
        }
    }
}
