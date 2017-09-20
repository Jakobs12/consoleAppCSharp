using BibliographyParser.FieldCheckers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BibliographyParser.Tests
{
    [TestClass]
    public class IssnFieldCheckerTest
    {
        [TestMethod]
        public void IssnFieldChecker_True()
        {
            var checker = new IssnFieldChecker();
            Assert.IsTrue(checker.Validate("0378-5955"));
            Assert.IsTrue(checker.Validate("1091-0050"));
        }

        [TestMethod]
        public void IssnFieldChecker_False()
        {
            var checker = new IssnFieldChecker();
            Assert.IsFalse(checker.Validate("0378-5956"));
            Assert.IsFalse(checker.Validate("0321349601"));
            Assert.IsFalse(checker.Validate("03785955"));
        }
    }
}
