namespace Crawler
{
    using HtmlAgilityPack;
    using JetBrains.Annotations;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class StringObserver
    {
        internal virtual string GetOnlyText([NotNull]HtmlDocument document)
        {
            StringBuilder stringBuilder = new StringBuilder();
            document.DocumentNode.Descendants().Where(n => n.Name == "script" || n.Name == "style").ToList().ForEach(n => n.Remove());

            foreach (HtmlNode node in document.DocumentNode.SelectNodes("//text()[normalize-space(.) != '']"))
            {
                stringBuilder.Append(node.InnerText.Trim()).Append(Environment.NewLine);
            }

            return stringBuilder.ToString();
        }

        internal virtual IEnumerable<string> GetLinks([NotNull]HtmlDocument document)
        {
            List<string> links = new List<string>();
            foreach (HtmlNode link in document.DocumentNode.SelectNodes("//a[@href]"))
            {
                var href = link.GetAttributeValue("href", string.Empty).Trim();
                if (href.StartsWith("/wiki"))
                {
                    links.Add(href);
                }
            }

            return links;
        }
    }
}
