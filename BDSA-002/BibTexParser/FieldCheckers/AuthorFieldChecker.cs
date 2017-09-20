using System;
using System.Text.RegularExpressions;

namespace BibliographyParser.FieldCheckers
{
    /// <summary>
    /// A field checker to verify whether an author field is valid. This can either be a single author, or a list of authors.
    /// First and last name can be separated by a comma. Multiple authors are separated by 'and'.
    /// </summary>
    public class AuthorFieldChecker : IFieldChecker
    {
        public bool Validate(string field)
        {
            var regex = @"^(?! )[\w \.\'\- ]+(?: ?, ?(?:[\w \.\'\- ])+)?$";
            Regex r = new Regex(regex);
            if (String.IsNullOrEmpty(field)) return false;
            string[] andArray = { " and " , "and" };

            if (field.Contains("and")) {
                string[] authors = field.Split(andArray, StringSplitOptions.None);
                var amountofAuthors = authors.Length;
                var countOfActualAuthors = 0;
                foreach (string s in authors) {
                    if (String.IsNullOrEmpty(s)) return false;
                    Match match = r.Match(s);
                    if (match.Success) {
                        countOfActualAuthors++;
                        if (amountofAuthors == countOfActualAuthors) return true;
                    }
                }
                return false;

            }
            Match m = r.Match(field);
            if (m.Success) return true;

            return false;
        }
    }
}

