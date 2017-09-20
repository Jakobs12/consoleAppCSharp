using System;
using BibliographyParser.FieldCheckers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BibliographyParser.Tests
{
    [TestClass]
    public class AuthorFieldCheckerTest
    {
        [TestMethod]
        public void AuthorFieldChecker_True()
        {
            var checker = new AuthorFieldChecker();
            Assert.IsTrue(checker.Validate("Jacob Cholewa"));
            Assert.IsTrue(checker.Validate("Jacob O'Brian"));
            Assert.IsTrue(checker.Validate("Jacob-Benjamin Cholewa"));
            Assert.IsTrue(checker.Validate("Jacob Cholewa and Paolo Tell"));
            Assert.IsTrue(checker.Validate("Cholewa, Jacob"));
            Assert.IsTrue(checker.Validate("Cholewa, Jacob and Tell, Paolo"));
            Assert.IsTrue(checker.Validate("Cholewa, Jacob Benjamin and Tell, Paolo"));
            Assert.IsTrue(checker.Validate("Cholewa, J.B. and Tell, P."));
            Assert.IsTrue(checker.Validate("J.B. Cholewa and P. Tell"));
            Assert.IsTrue(checker.Validate("Cholewa, J.B. and Paolo Tell"));
            Assert.IsTrue(checker.Validate("Jacob"));
            Assert.IsTrue(checker.Validate("Jacob and Paolo"));
            Assert.IsTrue(checker.Validate("Jacob and Paolo Tell"));
            Assert.IsTrue(checker.Validate("And Cholewa"));
            Assert.IsTrue(checker.Validate("And")); //Somebody might have the name And
        }

        [TestMethod]
        public void AuthorFieldChecker_False()
        {
            var checker = new AuthorFieldChecker();
            Assert.IsFalse(checker.Validate(""));
            Assert.IsFalse(checker.Validate(Environment.NewLine));
            Assert.IsFalse(checker.Validate("Cholewa, and and Tell, Paolo "));
            Assert.IsFalse(checker.Validate("Cholewa,"));
            Assert.IsFalse(checker.Validate("and"));
        }
    }
}
