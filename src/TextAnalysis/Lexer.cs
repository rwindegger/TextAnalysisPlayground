namespace TextAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Lexer
    {
        private int m_CurrentPosition = 0;
        private readonly string m_Source;

        public bool IsEoF
        {
            get
            {
                return m_Source.Length <= m_CurrentPosition;
            }
        }

        internal Lexer(string source)
        {
            m_Source = source;
        }

        private void ProcessToken(ref Token token, TokenKind kind, Func<string, int, bool> validator)
        {
            do
            {
                ++m_CurrentPosition;
            } while (!IsEoF && validator(m_Source, m_CurrentPosition));
            token.Kind = kind;
            token.TokenEnd = m_CurrentPosition;
        }

        public Token NextToken()
        {
            Token result = new Token();
            result.TokenStart = m_CurrentPosition;

            if (IsEoF)
            {
                result.TokenEnd = m_CurrentPosition;
                result.Kind = TokenKind.EoF;
            }
            else if (m_Source[m_CurrentPosition] == '<')
            {
                ProcessToken(ref result, TokenKind.Html, (s, o) => s[o - 1] != '>');
            }
            else if (char.IsWhiteSpace(m_Source, m_CurrentPosition))
            {
                ProcessToken(ref result, TokenKind.Whitespace, char.IsWhiteSpace);
            }
            else if (char.IsDigit(m_Source, m_CurrentPosition))
            {
                ProcessToken(ref result, TokenKind.Number | TokenKind.Word, (s, o) => char.IsDigit(s, o) || s[o] == '.' || s[o] == ',');
            }
            else if (char.IsPunctuation(m_Source, m_CurrentPosition))
            {
                ProcessToken(ref result, TokenKind.Punctuation, char.IsPunctuation);
            }
            else if (char.IsLetter(m_Source, m_CurrentPosition))
            {
                ProcessToken(ref result, TokenKind.Word, char.IsLetter);
            }
            else
            {
                ++m_CurrentPosition;
                result.TokenEnd = m_CurrentPosition;
                result.Kind = TokenKind.Unknown;
            }
            result.Content = m_Source.Substring(result.TokenStart, result.TokenLength);
            return result;
        }
    }
}
