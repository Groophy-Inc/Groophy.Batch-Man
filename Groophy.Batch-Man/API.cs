using System;
using System.Collections.Generic;
using System.Net;

namespace Groophy.Batch_Man
{
    public class Batch_Man_API
    {
        public static List<article> Get()
        {
            string htmlCode = string.Empty;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("user-agent", "B-M_Api");
                htmlCode = client.DownloadString("https://batch-man.com/");
            }
            List<string> bt = Between(htmlCode, "<article", "</article");
            List<article> a = new List<article>();
            for (int i = 0; i < bt.Count; i++)
            {
                a.Add(filter(bt[i]));
            }

            int c = 2;
            string ht2 = string.Empty;
            for (; ; )
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("user-agent", "B-M_Api");
                    ht2 = client.DownloadString("https://batch-man.com/page/"+c.ToString());
                }
                List<string> b2 = Between(ht2, "<article", "</article");
                for (int i = 0; i < b2.Count; i++)
                {
                    a.Add(filter(b2[i]));
                }
                if (ht2.IndexOf("class=\"next page-numbers\"") != -1)
                {
                    c++;
                }
                else
                {
                    break;
                }
            }
            return a;
        }

        private static string fixtitle(string title)
        {
            title = title.Replace("&#8211;", "-");
            title = title.Replace("&amp;", "&");
            title = title.Replace("&#8217;", "'");
            return title;
        }

        private static List<string> Between(string input, string startMatch, string endMatch)
        {
            List<string> ret = new List<string>();
            try
            {
                string[] arys = input.Split(new string[] { startMatch }, StringSplitOptions.None);
                string[] arye = input.Split(new string[] { endMatch }, StringSplitOptions.None);
                int st = arys[0].Length;
                int se = arye[0].Length;
                for (int i = 1; i < arys.Length; i++)
                {
                    ret.Add(input.Substring(st, se - st));
                    st += arys[i].Length;
                    se += arye[i].Length;
                }
                return ret;
            }
            finally
            {
            }
        }

        private static article filter(string dat)
        {
            article a = new article();
            #region url
            //<span class="posted-on"><a href="
            int siu = dat.IndexOf("<span class=\"posted-on\"><a href=\"") + 33;
            //" rel="bookmark"><time class="entry-date published"
            int eiu = dat.IndexOf("\" rel=\"bookmark\"><time class=\"entry-date published\"");
            a.url = dat.Substring(siu, eiu - siu);
            #endregion
            #region date
            //entry-date published" datetime="
            int sid = dat.IndexOf("entry-date published\" datetime=\"") + 32;
            a.date = dat.Substring(sid, 10);
            #endregion
            #region author
            //</a></span></span>
            int eib = dat.IndexOf("</a></span></span>");
            string by = string.Empty;
            for (int i = 1; i < 25; i++)
            {
                if (dat.Substring(eib - i, 1) == ">")
                {
                    break;
                }
                else
                {
                    by = dat.Substring(eib - i, 1) + by;
                }
            }
            a.by = by;
            #endregion
            #region category
            //rel="category tag">
            int sic = dat.IndexOf("rel=\"category tag\">") + 19;
            string cat = string.Empty;
            for (int i = 0; i < 25; i++)
            {
                if (dat.Substring(sic + i, 1) == "<")
                {
                    break;
                }
                else
                {
                    cat += dat.Substring(sic + i, 1);
                }
            }
            a.categorize = cat;
            #endregion
            #region description
            //post-excerpt entry-content
            int side = dat.IndexOf("post-excerpt entry-content") + 49;
            int eidef = dat.IndexOf("<!-- read more -->");
            int eide = eidef;
            for (int i = 1; i < 25; i++)
            {
                if (dat.Substring(eidef - i, 1) == "<")
                {
                    break;
                }
                else
                {
                    eide--;
                }
            }
            a.desc = fixtitle(dat.Substring(side, eide - side - 1));
            #endregion
            #region article title
            //<!-- .entry-content end -->
            int eiaf = dat.IndexOf("<!-- .entry-content end -->");
            int eia = eiaf;
            int fc = 0;
            for (int i = 1; i < 75; i++)
            {
                if (dat.Substring(eiaf - i, 1) == "<")
                {
                    if (fc == 2) break;
                    else fc++;
                }
                else
                {
                    eia--;
                }
            }
            eia -= 3;
            int sia = eia;
            for (int i = -2; i < 100; i++)
            {
                if (dat.Substring(eia - i, 1) == ">")
                {
                    break;
                }
                else
                {
                    sia--;
                }
            }
            a.articletitle = fixtitle(dat.Substring(sia + 3, eia - sia - 3));

            #endregion
            #region git download link
            try
            {
                string htmlCode = string.Empty;
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("user-agent", "B-M_Api");
                    htmlCode = client.DownloadString(a.url);
                }
                int sig = htmlCode.IndexOf("github.com");
                a.gitlink = "github link not found.";
                if (sig != -1)
                {
                    string link = string.Empty;
                    for (int i = 0; i < 75; i++)
                    {
                        if (htmlCode.Substring(sig + i, 1) == "\"")
                        {
                            break;
                        }
                        else
                        {
                            link += htmlCode.Substring(sig + i, 1);
                        }
                    }
                    a.gitlink = link;
                }
            }
            catch { }

            #endregion
            return a;
        }
    }

    public class article
    {
        public string articletitle { get; set; }
        public string url { get; set; }
        public string date { get; set; }
        public string by { get; set; }
        public string categorize { get; set; }
        public string desc { get; set; }
        public string gitlink { get; set; }
    }
}
