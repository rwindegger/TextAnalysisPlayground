namespace TextAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Flags]
    public enum TokenKind
    {
        Unknown = 0x00,
        EoF = 0x01,
        Whitespace = 0x02,
        Number = 0x04,
        Word = 0x08,
        Punctuation = 0x10,
        Html = 0x20,
    }
}
