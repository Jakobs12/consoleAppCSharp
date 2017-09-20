using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

// ReSharper disable LoopCanBeConvertedToQuery

namespace BibliographyParser
{
    /// <summary>
    /// A collection of bibliographic <see cref="Item" />'s which are held in memory.
    /// </summary>
    public class InMemoryItemRepo
    {
        private readonly ICollection<Item> _items;

        public InMemoryItemRepo() {
            _items = new HashSet<Item>();
        }

        /// <summary>
        /// Add an item to the repo.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(Item item)
        {
            if (item == null) throw new ArgumentNullException();
            if (!_items.Contains(item))
                _items.Add(item);
            else Console.WriteLine("Ohhh noes, dublicates");
        }

        /// <summary>
        /// Add a range of items to the repo.
        /// </summary>
        /// <param name="items">The items to add.</param>
        public void AddRange(IEnumerable<Item> items)
        {
            if (items == null) throw new ArgumentNullException();
            foreach (Item item in items) {
                Add(item);
            }
        }

        /// <summary>
        /// Returns an IEnumerable of all items in the repo. This method is lazily evaluated.
        /// </summary>
        /// <returns>An IEnumerable of the results.</returns>
        public IEnumerable<Item> GetAll()
        {
            return _items;
        }

        /// <summary>
        /// Returns an IEnumerable of all items matching a given search string. This method is lazily evaluated.
        /// </summary>
        /// <param name="searchString">The search string is a plain non-null and whitespace string that is matehced against all fields of an item.</param>
        /// <param name="take">The number of items to take. If null, return all results.</param>
        /// <returns>An IEnumerable of the results.</returns>
        public IEnumerable<Item> Search(string searchString, int? take = null)
        {
            var amountOftakes = 1;
            var nameValidator = @"\w";
            Regex r = new Regex(nameValidator);
            Match m = r.Match(searchString);
            if(m.Success) {
                foreach(Item item in _items) {
                    var fieldValues = item.Fields.Values;
                    foreach (string value in fieldValues) {
                        if (value.ToLower().Contains(searchString.ToLower())) {
                            if (take < amountOftakes || searchString == "") yield break;
                            amountOftakes++;
                            yield return item;
                            break;
                        }
                    }
                    
                }
            }
            yield break;
        }
    }
}
