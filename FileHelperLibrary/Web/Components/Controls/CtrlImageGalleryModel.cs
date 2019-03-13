using FileHelperLibrary.Web.Components.Templates;
using System.Collections.Generic;

namespace FileHelperLibrary.Web.Components.Controls
{
    public class CtrlImageGalleryModel : CtrlBaseModel
    {
        public IEnumerable<CtrlImageCardModel> ImagesCard;
        private string ImageCardTemplate { get; set; }

        public CtrlImageGalleryModel(string action, string controller, IEnumerable<string> imagesName, int height, int width)
        {
            if(imagesName != null)
            {
                List<CtrlImageCardModel> model = new List<CtrlImageCardModel>();

                foreach (var imageName in imagesName)
                {
                    model.Add(new CtrlImageCardModel(action,controller,imageName,height,width));
                }

                ImagesCard = model;
            }
        }

        public CtrlImageGalleryModel(string action, string controller, IEnumerable<string> imagesName):this(action,controller,imagesName,-1,-1)
        {
        }

        public override string GetHtml()
        {
            var html = CtrlImageGalleryModelTemplate.Style() + CtrlImageGalleryModelTemplate.Html();
            
            if(ImagesCard != null)
            {
                LoadImageCardTemplate();
                html = ReplaceTag(this.GetType().GetProperty("ImageCardTemplate",System.Reflection.BindingFlags.Instance|
                    System.Reflection.BindingFlags.NonPublic), html);
            }
            else
            {
                html = RemoveTag(nameof(ImageCardTemplate),html);
            }

            return html;
        }

        private void LoadImageCardTemplate()
        {
            foreach(var imageCard in ImagesCard)
            {
                ImageCardTemplate = ImageCardTemplate +  imageCard.GetHtml();
            }
        }
    }
}
