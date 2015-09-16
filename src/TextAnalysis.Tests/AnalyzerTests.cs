namespace TextAnalysis.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AnalyzerTests
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        [DeploymentItem("TestData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestData.xml", "AnalyzerTestData", DataAccessMethod.Sequential)]
        public void AnalyzerContentMatch()
        {
            string input = Convert.ToString(TestContext.DataRow["Text"]);
            string expected = Convert.ToString(TestContext.DataRow["Text"]);

            var result = Analyzer.Parse(input);
            var actual = result.Content;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DeploymentItem("TestData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestData.xml", "AnalyzerTestData", DataAccessMethod.Sequential)]
        public void AnalyzerWordCount()
        {
            string input = Convert.ToString(TestContext.DataRow["Text"]);
            int expected = Convert.ToInt32(TestContext.DataRow["WordCount"]);

            var result = Analyzer.Parse(input);

            int actual = result.WordCount;
            Assert.AreEqual(expected, actual);

            actual = result.WordCount;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DeploymentItem("TestData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestData.xml", "AnalyzerTestData", DataAccessMethod.Sequential)]
        public void AnalyzerWordCloud()
        {
            string input = Convert.ToString(TestContext.DataRow["Text"]);
            string word = Convert.ToString(TestContext.DataRow["WordCloudWord"]);
            int expected = Convert.ToInt32(TestContext.DataRow["WordCloudCount"]);

            var result = Analyzer.Parse(input);
            var actual = result.WordCloud[word];
            Assert.AreEqual(expected, actual);
            actual = result.WordCloud[word];
            Assert.AreEqual(expected, actual);
        }
    }
}
