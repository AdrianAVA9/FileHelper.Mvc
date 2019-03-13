using System.Reflection;
using System.Text;

namespace FileHelperLibrary.Web.Components.Controls
{
    public abstract class CtrlBaseModel
    {
        public abstract string GetHtml();

        public string ReplaceTag(PropertyInfo prop, string html)
        {
            if (prop != null)
            {
                var value = prop.GetValue(this).ToString();

                var tag = string.Format("-#{0}-", FormatParameter(prop.Name));
                html = html.Replace(tag, value);
            }

            return html;
        }

        public string ReplaceTags(PropertyInfo[]properties, string html)
        {
            foreach (var prop in properties)
            {
                html = ReplaceTag(prop, html);   
            }

            return html;
        }

        public string RemoveTag(string propertyName, string template)
        {
            return template.Replace(FormatParameter(propertyName),"");
        }

        public string FormatParameter(string name)
        {
            var builder = new StringBuilder();
            var letters = name.ToCharArray();

            foreach (var letter in letters)
            {
                if (char.IsUpper(letter) && builder.Length > 0) builder.Append("-");

                builder.Append(letter);
            }

            return builder.ToString().ToLower();
        }
    }
}
