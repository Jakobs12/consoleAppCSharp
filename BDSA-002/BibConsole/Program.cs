using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibliographyParser;
using BibliographyParser.BibTex;
using BibliographyParser.FieldCheckers;
using BibliographyParser.FieldValidators;
using BibliographyParser.ItemChecker;
using BibliographyParser.ItemValidators;
using System.IO;

namespace BibConsole
{
    /// <summary>
    /// Console application which allows searching through a preloaded BibTex repository.
    /// </summary>
    static class Program
    {
        public static void Main()
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

            // Populate repository with parsed BibTex data.
            var repo = new InMemoryItemRepo();
            try
            {
                repo.AddRange(parser.Parse(Encoding.Default.GetString(Properties.Resources.bibtex)));
            }
            catch (InvalidDataException)
            {
                Console.WriteLine("Not all loaded BibTeX data is valid. Ensure the field checkers are implemented properly.");
                Console.ReadLine();
                return;
            }

            // Show interface to search.
            Console.WriteLine($"Hi, and welcome to the BibTeX search engine!");
            Console.WriteLine($"Enter a word or sentence to search in the repo (searches on all fields):\n");
            while (true)
            {
                // Search for specified search string.
                Console.Write($">");
                var s = Console.ReadLine();
                var result = repo.Search(s);
                List<Item> items = result.ToList();

                // Print results.
                Console.Write($"\nThere are {items.Count} items containing \"{s}\".\n");
                int curItem = 0;
                const int pageSize = 10;
                bool continuePrinting = true;
                while (continuePrinting && curItem < items.Count)
                {
                    Item item = items[curItem];
                    Console.WriteLine($"\t{item.Fields[Item.FieldType.Title]}");
                    if ((curItem + 1) % pageSize == 0)
                    {
                        Console.Write($"\nEnter enter to get more. Write end to escape > ");
                        if (Console.ReadLine()?.ToLower() == "end")
                        {
                            continuePrinting = false;
                            break;
                        }
                    }
                    curItem++;
                }
                if (items.Count > 0 && continuePrinting)
                {
                    Console.WriteLine($"No more items.");
                }
            }
        }
    }
}
