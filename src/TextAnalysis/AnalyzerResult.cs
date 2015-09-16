namespace TextAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    public class AnalyzerResult
    {
        private readonly SortedSet<Token> m_Tokens = new SortedSet<Token>();

        private Dictionary<string, int> m_WordCloud;
        private int? m_WordCount;

        public string Content { get; set; }
        public Dictionary<string, int> WordCloud
        {
            get
            {
                if (m_WordCloud == null)
                    m_WordCloud = m_Tokens.Where(t => t.Kind == TokenKind.Word).GroupBy(t => t.Content.ToLowerInvariant()).ToDictionary(t => t.Key, t => t.Count());
                return m_WordCloud;
            }
        }
        public int WordCount
        {
            get
            {
                if (m_WordCount == null || !m_WordCount.HasValue)
                    m_WordCount = m_Tokens.Where(t => t.Kind.HasFlag(TokenKind.Word)).Count();
                return m_WordCount.Value;
            }
        }

        public IEnumerable<string> PossibleKeyWords
        {
            get
            {
                return WordCloud.OrderByDescending(k => k.Value).Select(k => k.Key).Take(5);
            }
        }

        internal AnalyzerResult(string input)
        {
            Content = input;
            Lexer lexer = new Lexer(input);
            Token t;
            do
            {
                t = lexer.NextToken();
                m_Tokens.Add(t);
            } while (t.Kind != TokenKind.EoF);
        }
    }
}
