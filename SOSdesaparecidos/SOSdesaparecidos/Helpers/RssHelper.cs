using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SOSdesaparecidos.Helpers
{
    public static class RssHelper
    {
        private const string ImagePattern = @"<img.*?src=[\""'](.+?)[\""'].*?>";
        private const string HiperlinkPattern = @"<a\s+(?:[^>]*?\s+)?href=""([^ ""]*)""";
        private const string HeightPattern = @"height=(?:(['""])(?<height>(?:(?!\1).)*)\1|(?<height>\S+))";
        private const string WidthPattern = @"width=(?:(['""])(?<width>(?:(?!\1).)*)\1|(?<width>\S+))";

        private static readonly Regex RegexImages = new Regex(ImagePattern, RegexOptions.IgnoreCase);
        private static readonly Regex RegexLinks = new Regex(HiperlinkPattern, RegexOptions.IgnoreCase);
        private static readonly Regex RegexHeight = new Regex(HeightPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        private static readonly Regex RegexWidth = new Regex(WidthPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);


        public static string GetSafeElementString(this XElement item, string elementName)
        {
            if (item == null) return string.Empty;
            return GetSafeElementString(item, elementName, item.GetDefaultNamespace());
        }

        public static string GetSafeElementString(this XElement item, string elementName, XNamespace xNamespace)
        {
            if (item == null) return String.Empty;

            XElement value = item.Element(xNamespace + elementName);

            if (value != null)
            {
                return value.Value;
            }

            return string.Empty;
        }

        public static string GetImage(this XElement item)
        {
            string feedDataImage = null;
            try
            {
                feedDataImage = GetImagesInHTMLString(item.Value).FirstOrDefault();
                if (!string.IsNullOrEmpty(feedDataImage) && feedDataImage.EndsWith("'"))
                {
                    feedDataImage = feedDataImage.Remove(feedDataImage.Length - 1);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return feedDataImage;
        }

        private static IEnumerable<string> GetImagesInHTMLString(string htmlString)
        {
            var images = new List<string>();
            foreach (Match match in RegexImages.Matches(htmlString))
            {
                bool include = true;
                string tag = match.Value;

                var matchHeight = RegexHeight.Match(tag);
                if (matchHeight.Success)
                {
                    var heightValue = matchHeight.Groups["height"].Value;
                    int size = 0;
                    if (int.TryParse(heightValue, out size) && size < 10)
                    {
                        include = false;
                    }
                }

                var matchWidth = RegexWidth.Match(tag);
                if (matchWidth.Success)
                {
                    var widthValue = matchWidth.Groups["width"].Value;
                    int size = 0;
                    if (int.TryParse(widthValue, out size) && size < 10)
                    {
                        include = false;
                    }
                }

                if (include)
                {
                    images.Add(match.Groups[1].Value);
                }
            }

            foreach (Match match in RegexLinks.Matches(htmlString))
            {
                var value = match.Groups[1].Value;
                if (value.Contains(".jpg") || value.Contains(".png"))
                {
                    images.Add(value);
                }
            }

            return images;
        }
    }
}
