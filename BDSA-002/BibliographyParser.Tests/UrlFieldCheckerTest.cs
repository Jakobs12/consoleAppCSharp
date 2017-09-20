using BibliographyParser.FieldCheckers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BibliographyParser.Tests
{
    [TestClass]
    public class UrlFieldCheckerTest
    {
        [TestMethod]
        public void UrlFieldChecker_True()
        {
            var checker = new UrlFieldChecker();
            Assert.IsTrue(checker.Validate("http://itu.dk"));
            Assert.IsTrue(checker.Validate("http://www.itu.dk"));
            Assert.IsTrue(checker.Validate("https://itu.dk"));
            Assert.IsTrue(checker.Validate("https://itu.co.uk"));
            Assert.IsTrue(checker.Validate("https://itu.dk/"));
            Assert.IsTrue(checker.Validate("https://itu.dk/somepage"));
            Assert.IsTrue(checker.Validate("https://itu.dk/somepage/somefile.pdf"));
            Assert.IsTrue(checker.Validate("https://itu.dk/index.html"));
        }

        [TestMethod]
        public void UrlFieldChecker_False()
        {
            var checker = new UrlFieldChecker();
            Assert.IsFalse(checker.Validate("www.itu.dk"));
            Assert.IsFalse(checker.Validate("itu.dk"));
            Assert.IsFalse(checker.Validate("itu"));
            Assert.IsFalse(checker.Validate("www.itu"));
            Assert.IsFalse(checker.Validate("http://itu"));
            Assert.IsFalse(checker.Validate("This is not a URL"));
        }
    }
}
