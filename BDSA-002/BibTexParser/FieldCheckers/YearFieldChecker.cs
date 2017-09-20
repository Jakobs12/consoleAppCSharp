using System;
using System.Text.RegularExpressions;

namespace BibliographyParser.FieldCheckers
{
    /// <summary>
    /// A field checker to verify whether a string represents a valid year, represented as four digits.
    /// </summary>
    public class YearFieldChecker : IFieldChecker
    {
        public bool Validate(string field)
        {
            var regexString = @"^\d{4}$";
            int yearNumber;
            var result = int.TryParse(field, out yearNumber);
            if (result && DateTime.UtcNow.Year >= yearNumber) {
                Regex r = new Regex(regexString);
                Match m = r.Match(field);
                return m.Success;
            }

            return false;
        }
    }
}
