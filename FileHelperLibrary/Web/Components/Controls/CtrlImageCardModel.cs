using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHelperLibrary.Web.Components.Controls
{
    public class CtrlImageCardModel : CtrlBaseModel
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int HeightCss { get; set; }
        public int WidthCss { get; set; }
        public string ImageName { get; set; }

        public CtrlImageCardModel(string action, string controller,string imageName, int height, int width)
        {
            ImageName = imageName;
            Action = action;
            Controller = controller;
            Height = height;
            Width = width;
            HeightCss = Height == -1 ? 170 : Height;
            WidthCss = Width == -1 ? 190 : Width;
        }

        public CtrlImageCardModel(string action, string controller, string imageName):this(action, controller, imageName,-1,-1)
        {
        }

        public override string GetHtml()
        {
            var html = Templates.CtrlImageCardModelTemplate.Sytle() +
                Templates.CtrlImageCardModelTemplate.Html();

            if(Height == -1 && Width == -1)
            {
                html = html.Replace("width=-#width-&amp;height=-#height-","");
            }

            html = ReplaceTags(this.GetType().GetProperties(System.Reflection.BindingFlags.Instance | 
                System.Reflection.BindingFlags.Public), html);

            return html;   
        }
    }
}
