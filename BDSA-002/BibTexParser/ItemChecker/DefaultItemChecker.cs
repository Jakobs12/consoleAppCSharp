using System.Linq;
using BibliographyParser.FieldValidators;

namespace BibliographyParser.ItemChecker
{
    /// <summary>
    /// Default <see cref="IItemChecker"/> implementation for when no custom checker is specified.
    /// When all fields contained by the item are valid, the item is valid.
    /// </summary>
    public class DefaultItemChecker : IItemChecker
    {
        private readonly IFieldValidator _validator;

        public DefaultItemChecker(IFieldValidator fieldValidator = null)
        {
            _validator = fieldValidator ?? new FieldValidator();
        }

        public bool Validate(Item item)
        {
            return item.Fields.All(field => _validator.IsFieldValid(field.Value, field.Key));
        }
    }
}
