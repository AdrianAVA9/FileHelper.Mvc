namespace FileHelperLibrary.Web.Components.Templates
{
    public static class CtrlImageCardModelTemplate
    {
        public static string Html()
        {
            return @"<div class='images-card-container'>
                         <img alt='Picture' src='/-#controller-/-#action-?imageName=-#image-name-&amp;width=-#width-&amp;height=-#height-'>
                      </div> ";
        }

        public static string Sytle()
        {
            return @"<style>.images-card-container {
                        display: block;width: -#width-css-px;
                        height: -#height-css-px;background: #c6bcbc;margin: 5px;}
                     .images-card-container img {width: 100%;height: 100%}</style>";
        }
    }
}
