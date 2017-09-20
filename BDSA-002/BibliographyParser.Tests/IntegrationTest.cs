using System.Collections.Generic;
using System.Text;
using BibliographyParser.BibTex;
using BibliographyParser.FieldCheckers;
using BibliographyParser.FieldValidators;
using BibliographyParser.ItemChecker;
using BibliographyParser.ItemValidators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BibliographyParser.Tests
{
    [TestClass]
    public class IntegrationTest
    {
        /// <summary>
        /// Verifies whether all expected BibTex items are parsed correctly, when the necessary custom field checkers work as expected.
        /// </summary>
        [TestMethod]
        public void BibTex_Parse_Successful()
        {
            // Set up BibTexParser.
            var customFieldCheckers = new Dictionary<Item.FieldType, IFieldChecker>
            {
                [Item.FieldType.ISSN] = new IssnFieldChecker(),
                [Item.FieldType.ISBN] = new IsbnFieldChecker(),
                [Item.FieldType.Author] = new AuthorFieldChecker(),
                [Item.FieldType.Year] = new YearFieldChecker(),
                [Item.FieldType.URL] = new UrlFieldChecker()
            };
            var itemChecker = new DefaultItemChecker(new FieldValidator(new DefaultFieldChecker(), customFieldCheckers));
            var parser = new BibTexParser(new ItemValidator(itemChecker));

            var bib = parser.Parse(Encoding.Default.GetString(Properties.Resources.bibtex));
            Assert.AreEqual(100, bib.Count);
        }
    }
}
