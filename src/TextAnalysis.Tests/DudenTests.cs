using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAnalysis.LanguageAnalysis.German;

namespace TextAnalysis.Tests
{
    [TestClass]
    public class DudenTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Duden x = new Duden();
            x.FindWord("der");
            x.FindWord("Passagier");
            x.FindWord("Autodeck");
            x.FindWord("übernommen");
        }
    }
}
