using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace FileHelperLibrary.Web.Components.Helpers
{
    ///<Summary>
    /// HtmlExtensionFile
    ///</Summary>
    public static class HtmlExtensionFile
    {
        /// <summary>
        /// Upload Image helper
        /// </summary>
        /// <param name="htmlHelper">The helper</param>
        /// <param name="action">The name of the action method</param>
        /// <param name="controller">The name of the controller</param>
        /// <param name="labelText">The text to display to choose a file</param>
        /// <param name="allowedFilesExtension">Allowed files exentension.
        /// <example> Example: ".jpg, .png, .jpeg, .gif" </example>
        /// </param>
        /// <param name="imgTitle">Message to display when the image is not chosen</param>
        /// <param name="btnValue">The text of the button</param>
        /// <param name="maxSizeFile">Maximum file size allowed</param>
        /// <param name="msgMaxSize">Message to display when the file size is exceeded</param>
        /// <param name="msgForExtensionNotAllowed">Message to display when the file extension is not allowed</param>
        /// <param name="errorMessageId">Element id to display any message</param>
        /// <returns>A HTML element with the appropriate properties set</returns>

        public static MvcHtmlString UploadImageHelper(this HtmlHelper htmlHelper, string action, string controller,
         string labelText, string allowedFilesExtension, string imgTitle, string btnValue, int maxSizeFile, string msgMaxSize,
         string msgForExtensionNotAllowed, string errorMessageId)
        {
            var ctrlForm = new Controls.CtrlFormModel( action,  controller, labelText,  allowedFilesExtension,  
                imgTitle,  btnValue,  maxSizeFile,  msgMaxSize, msgForExtensionNotAllowed,  errorMessageId);

            return new MvcHtmlString(ctrlForm.GetHtml());
        }

        /// <summary>
        /// Image Gallery helper
        /// <remarks>
        /// Remember to put in the method these 3 parameters in the method that will be called ,and in the same order
        /// <code>
        ///     public ActionResult methodname(string imageName, int width, int height)
        /// </code>
        /// </remarks>
        /// </summary>
        /// <param name="htmlHelper">The helper</param>
        /// <param name="action">The name of the action method</param>
        /// <param name="controller">The name of the controller</param>
        /// <param name="imagesName">The List of images name, each image must include the extension to be found</param>
        /// <param name="width">The width of the image that will be resized</param>
        /// <param name="height">The width of the image that will be resized</param>
        /// <returns>A HTML element with the appropriate properties set</returns>
        public static MvcHtmlString ImageGallery(this HtmlHelper htmlHelper, string action, string controller, IEnumerable<string> imagesName,int width, int height)
        {
            var ctrlImageGallery = new Controls.CtrlImageGalleryModel(action, controller, imagesName, width, height);

            return new MvcHtmlString(ctrlImageGallery.GetHtml());
        }

        /// <summary>
        /// Image Gallery helper
        /// <remarks>
        /// Remember to put in the method this parameters which will be called
        /// <code>
        ///     public ActionResult methodname(string imageName)
        /// </code>
        /// </remarks>
        /// </summary>
        /// <param name="htmlHelper">The helper</param>
        /// <param name="action">The name of the action method</param>
        /// <param name="controller">The name of the controller</param>
        /// <param name="imagesName">The List of images name, each image must include the extension to be found</param>
        /// <returns>A HTML element with the appropriate properties set</returns>
        public static MvcHtmlString ImageGallery(this HtmlHelper htmlHelper, string action, string controller, IEnumerable<string> imagesName)
        {
            return ImageGallery(htmlHelper,action,controller,imagesName,-1,-1);
        }
    }
}
