using System;
using BibliographyParser.FieldCheckers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BibliographyParser.Tests
{
    [TestClass]
    public class YearFieldCheckerTest
    {
        [TestMethod]
        public void YearFieldChecker_True()
        {
            var checker = new YearFieldChecker();
            Assert.IsTrue(checker.Validate("2014"));
            Assert.IsTrue(checker.Validate("2015"));
            Assert.IsTrue(checker.Validate("1890"));
            Assert.IsTrue(checker.Validate("0000"));
        }

        [TestMethod]
        public void YearFieldChecker_False()
        {
            var checker = new YearFieldChecker();
            Assert.IsFalse(checker.Validate(DateTime.UtcNow.AddYears(1).Year.ToString()));
            Assert.IsFalse(checker.Validate(int.MinValue + ""));
            Assert.IsFalse(checker.Validate(int.MaxValue + ""));
            Assert.IsFalse(checker.Validate("0"));
            Assert.IsFalse(checker.Validate("10"));
            Assert.IsFalse(checker.Validate("100"));
            Assert.IsFalse(checker.Validate("-0001"));
            Assert.IsFalse(checker.Validate("This is not a year"));
        }
    }
}
