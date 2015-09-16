namespace TextAnalysis.LanguageAnalysis.German
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Telerik.OpenAccess;

    public class Duden
    {
        private readonly Regex m_SearchRegex = new Regex(@"<div class='search-result first (?:lemma|regular)-hit'>\s+<h3><a href='(?<relativeLink>.*?)'>(?<word>.*?)</a></h3>", RegexOptions.Compiled);
        private readonly Regex m_PageRegex = new Regex(@"<span class=""wortart"">(?<wortart>.*?)</span>|<span class=""bi-artikel-span-frequenzklasse bi-artikel-span-frequenzklasse_(?<frequency>\d)""></span>|<a.*?meta-ref-id="".*?"".*?meta-topic=""Synonym"".*?>(?<synonym>.*?)</a>", RegexOptions.Compiled);

        public StringCollection ParsePage(string subjectString, string groupName)
        {
            StringCollection resultList = new StringCollection();
            Match matchResult = m_PageRegex.Match(subjectString);
            while (matchResult.Success)
            {
                resultList.Add(matchResult.Groups[groupName].Value);
                matchResult = matchResult.NextMatch();
            }
            return resultList;
        }

        public StringCollection ParseSearchResults(string subjectString, string groupName)
        {
            StringCollection resultList = new StringCollection();
            Match matchResult = m_SearchRegex.Match(subjectString);
            while (matchResult.Success)
            {
                resultList.Add(matchResult.Groups[groupName].Value);
                matchResult = matchResult.NextMatch();
            }
            return resultList;
        }

        public void FindWord(string word)
        {
            using (DudenCacheModel mdl = new DudenCacheModel())
            {
                WordMatch match = mdl.WordMatches.Where(wm => wm.Word == word).FirstOrDefault();
                if (match == null)
                {
                    match = new WordMatch() { Word = word };
                    mdl.Add(match);
                    mdl.SaveChanges();
                }

                if (match.WordDefinition == null)
                {
                    try
                    {   
                        WebClient client = new WebClient();
                        string searchResults = client.DownloadString(string.Format("http://www.duden.de/suchen/dudenonline/{0}", word));
                        var link = ParseSearchResults(searchResults, "relativeLink").OfType<string>().First(s => !string.IsNullOrWhiteSpace(s));
                        var worddef = ParseSearchResults(searchResults, "word").OfType<string>().First(s => !string.IsNullOrWhiteSpace(s));
                        string resultPage = client.DownloadString(string.Format("http://www.duden.de{0}", link));
                        var synonyms = ParsePage(resultPage, "synonym").OfType<string>().Where(s => !string.IsNullOrWhiteSpace(s));
                        var wortart = ParsePage(resultPage, "wortart").OfType<string>().First(s => !string.IsNullOrWhiteSpace(s));
                        var frequency = int.Parse(ParsePage(resultPage, "frequency").OfType<string>().First(s => !string.IsNullOrWhiteSpace(s)));
                    }
                    catch (WebException wex)
                    {

                    }
                }
            }
        }
    }
}
