using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.ServiceModel.Syndication;
using System.Xml.Linq;
using System.Collections;

namespace TestApplication.Sysnet
{
    public partial class Async_jQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            XDocument feedXML = XDocument.Load("http://feeds.encosia.com/Encosia");

            var feeds = from feed in feedXML.Descendants("item")
                        select new
                        {
                            Date = DateTime.Parse(feed.Element("pubDate").Value)
                                           .ToShortDateString(),
                            Title = feed.Element("title").Value,
                            Link = feed.Element("link").Value,
                            Description = feed.Element("description").Value
                        };
        }

        [WebMethod]
        public static IEnumerable GetFeedburnerItems(int Count)
        {
            XDocument feedXML = XDocument.Load("http://feeds.encosia.com/Encosia");

            var feeds = from feed in feedXML.Descendants("item")
                        select new
                        {
                            Date = DateTime.Parse(feed.Element("pubDate").Value)
                                           .ToShortDateString(),
                            Title = feed.Element("title").Value,
                            Link = feed.Element("link").Value,
                            Description = feed.Element("description").Value
                        };

            return feeds.Take(Count);
        }
    }
}