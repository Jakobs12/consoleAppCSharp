using System;
using System.Text.RegularExpressions;

namespace BibliographyParser.FieldCheckers
{
    /// <summary>
    /// A field checker to verify whether a field represents a valid URL.
    /// </summary>
    public class UrlFieldChecker : IFieldChecker
    {
        public bool Validate(string field)
        {
            var regexString = @"^https?:\/\/(www.)?[\w]*(\.(\w){2,6}){1,2}(\/([\w\.\/])*)*$";
            Regex r = new Regex(regexString);
            Match m = r.Match(field);
            if (m.Success) return true;
            return false;
        }
    }
}
