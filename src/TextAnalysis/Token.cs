namespace TextAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public struct Token : IComparable
    {
        public string Content;
        public TokenKind Kind;
        public int TokenEnd;
        public int TokenStart;

        public int TokenLength
        {
            get
            {
                return TokenEnd - TokenStart;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}: {2}", TokenStart, TokenEnd, Content);
        }
        /// <summary> Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object. </summary>
        /// <remarks> Rene Windegger, 28.12.2014. </remarks>
        /// <param name="obj"> An object to compare with this instance. </param>
        /// <returns> A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref name="obj" /> in the sort order. </returns>
        /// <seealso cref="M:System.IComparable.CompareTo(object)"/>
        int IComparable.CompareTo(object obj)
        {
            if (!(obj is Token))
                return 1;

            Token other = (Token)obj;
            int compare = TokenStart.CompareTo(other.TokenStart);
            if (compare == 0)
            {
                compare = TokenEnd.CompareTo(other.TokenEnd);
            }
            return compare;
        }
    }
}
