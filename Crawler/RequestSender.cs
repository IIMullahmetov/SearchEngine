namespace Crawler
{
    using HtmlAgilityPack;

    internal sealed class RequestSender
    {
        internal HtmlDocument Send(string url)
        {
            HtmlWeb hw = new HtmlWeb();
            return hw.Load(url);
        }
    }
}
