namespace TextAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Analyzer
    {
        public static AnalyzerResult Parse(string text)
        {
            return new AnalyzerResult(text);
        }
    }
}