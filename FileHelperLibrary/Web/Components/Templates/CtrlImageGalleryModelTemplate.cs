namespace FileHelperLibrary.Web.Components.Templates
{
    public class CtrlImageGalleryModelTemplate
    {
        public static string Style()
        {
            return @"<style>.image-gallery-container {
                        display: flex;justify-content: center;
                        flex-flow: row wrap;align-content: flex-end;}</style>";
        }

        public static string Html()
        {
            return @"<div class='image-gallery-container'> -#image-card-template- </div>";
        }
    }
}
