using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BibliographyParser.FieldCheckers {
    class ISChecker {

        public static bool ParseIssn(string s) {
            string field = s;
            int checkSum = 0;
            var regexString = @"^\d{4}-\d{3}(\d|X|x)$";
            Regex r = new Regex(regexString);
            if (r.Match(field).Success) {
                bool x = false;
                if (field.Contains("X") || field.Contains("x")) {
                    x = true;
                }
                var newField = field.Replace("-", "");

                var fieldArray = newField.ToCharArray();

                var arrayPosition = 0;
                for (int i = 8; i >= 2; i--) {
                    int currentInteger;
                    int.TryParse(fieldArray[arrayPosition].ToString(), out currentInteger);
                    checkSum += currentInteger * i;
                    arrayPosition++;
                }
                int finaleCheckSum;
                if (x) finaleCheckSum = checkSum + 10;
                else {
                    int checkDigit;
                    int.TryParse(fieldArray[7].ToString(), out checkDigit);
                    finaleCheckSum = checkSum + checkDigit;
                }
                if (finaleCheckSum % 11 == 0) return true;
            }

            return false;
        }

        public static bool ParseIsbn(string s) {
            string field = s;
            int checkSum = 0;
            var regexString = @"^(\d\-?){9}(x|X|\d)?$";
            Regex r = new Regex(regexString);
            if (r.Match(field).Success) {
                bool x = false;
                if (field.Contains("X") || field.Contains("x")) {
                    x = true;
                }
                var newField = field.Replace("-", "");

                var fieldArray = newField.ToCharArray();

                var arrayPosition = 0;
                for (int i = 10; i >= 2; i--) {
                    int currentInteger;
                    int.TryParse(fieldArray[arrayPosition].ToString(), out currentInteger);
                    checkSum += currentInteger * i;
                    arrayPosition++;
                }
                int finaleCheckSum;
                if (x) finaleCheckSum = checkSum + 10;
                else {
                    int checkDigit;
                    int.TryParse(fieldArray[9].ToString(), out checkDigit);
                    finaleCheckSum = checkSum + checkDigit;
                }
                if (finaleCheckSum % 11 == 0) return true;
            }

            return false;
        }
    }
}
