using BibliographyParser.FieldCheckers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BibliographyParser.Tests
{
    [TestClass]
    public class IsbnFieldCheckerTest
    {
        [TestMethod]
        public void IsbnFieldChecker_True()
        {
            var checker = new IsbnFieldChecker();
            Assert.IsTrue(checker.Validate("99921-58-10-7"));
            Assert.IsTrue(checker.Validate("960-425-059-0"));
            Assert.IsTrue(checker.Validate("85-359-0277-5"));
            Assert.IsTrue(checker.Validate("0-9752298-0-X"));
            Assert.IsTrue(checker.Validate("0-9752298-0-x"));
            Assert.IsTrue(checker.Validate("0-943396-04-2"));
            Assert.IsTrue(checker.Validate("0321349601"));
        }

        [TestMethod]
        public void IsbnFieldChecker_False()
        {
            var checker = new IsbnFieldChecker();
            Assert.IsFalse(checker.Validate("90921-58-10-7"));
            Assert.IsFalse(checker.Validate("90921-58-10-7X"));
            Assert.IsFalse(checker.Validate("XXXXX-XX-XX-XX"));
            Assert.IsFalse(checker.Validate("--------------"));
            Assert.IsFalse(checker.Validate("900-425-059-0"));
            Assert.IsFalse(checker.Validate("80-359-0277-5"));
            Assert.IsFalse(checker.Validate("0-9057298-0-X"));
            Assert.IsFalse(checker.Validate("0-9092298-0-x"));
            Assert.IsFalse(checker.Validate("0-903376-04-2"));
            Assert.IsFalse(checker.Validate("0321349901"));
            Assert.IsFalse(checker.Validate("03213499010"));
            Assert.IsFalse(checker.Validate("0321349"));
            Assert.IsFalse(checker.Validate("0"));
            Assert.IsFalse(checker.Validate("Not as ISBN10 string"));
            Assert.IsFalse(checker.Validate("078-5342349603")); //Valid ISBN13 string
        }
    }
}
