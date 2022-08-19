using System.Text.RegularExpressions;

namespace MS_SQL_Entity_Framework
{
    public class ValidateInputString
    {
        public static List<string>? ValidateFields(string fileData)
        {
            Regex reg = new("[({!@#$%^&*};:'',<.>/?)]");

            string parsedFileData = reg.Replace(fileData, string.Empty);
            if (parsedFileData.Length > 200) return null;

            var stringsList = parsedFileData.Split(" ");
            if (stringsList.Length != 3) return null;

            if (stringsList[0].All(char.IsLetter) && stringsList[1].All(char.IsLetter) && stringsList[2].All(char.IsDigit)) { return stringsList.ToList(); }

            return null;
        }
    }
}
