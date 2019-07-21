using System.Collections.Generic;
using System.Text;
using System.Web;
using HtmlAgilityPack;

namespace ConsoleApplication2.Browser
{
    public class FormElementCollection : Dictionary<string, string>
    {
        /// <summary>
        /// Constructor. Parses the HtmlDocument to get all form input elements. 
        /// </summary>
        public FormElementCollection(HtmlDocument htmlDoc)
        {
            var inputs = htmlDoc.DocumentNode.Descendants("input");
            foreach (var element in inputs)
            {
                string name = element.GetAttributeValue("name", "undefined");
                string value = element.GetAttributeValue("value", "");
                if (!name.Equals("undefined"))
                {
                    if (!ContainsKey(name))
                    {
                        Add(name, value);
                    }
                    else
                    {
                        this[name] = value;
                    }
                }

                ;
            }
        }

        /// <summary>
        /// Assembles all form elements and values to POST. Also html encodes the values.  
        /// </summary>
        public string AssemblePostPayload()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var element in this)
            {
                string value = HttpUtility.UrlEncode(element.Value);
                sb.Append("&" + element.Key + "=" + value);
            }

            return sb.ToString().Substring(1);
        }
    }
}