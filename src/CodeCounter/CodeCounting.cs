using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace CodeCounter
{
    public enum CodingLanguage {
        Csharp,
        Cplus,
        C,
        Java,
        Python,
        VHDL,
        MATLAB,
        HTML
    }

    public class CodeCounting
    {
        private readonly Dictionary<CodingLanguage, bool> desiredLanguages;
        private readonly Dictionary<CodingLanguage, long> numLines;
        private readonly Dictionary<CodingLanguage, string> extensions = new Dictionary<CodingLanguage, string> {
            {CodingLanguage.Csharp, ".cs" },
            {CodingLanguage.Cplus, ".cpp" },
            {CodingLanguage.C, ".c" },
            {CodingLanguage.Java, ".java" },
            {CodingLanguage.Python, ".py" },
            {CodingLanguage.VHDL, ".vhd" },
            {CodingLanguage.MATLAB, ".m" },
            {CodingLanguage.HTML, ".html" },
        };

        public CodeCounting() {

            // Initialize mappings of desired coding languages
            // One for whether or not it is being checked (bool)
            // One for counting the number of lines (long)
            desiredLanguages = new Dictionary<CodingLanguage, bool>();
            numLines = new Dictionary<CodingLanguage, long>();

            var languages = Enum.GetValues(typeof(CodingLanguage));
            foreach (CodingLanguage lang in languages) {
                desiredLanguages.Add(lang,false);
                numLines.Add(lang, 0);
            }

        }

        private void ResetNumLines() {
            var languages = Enum.GetValues(typeof(CodingLanguage));
            foreach (CodingLanguage lang in languages)
                numLines[lang] = 0;
        }

        public void SetLanguage(CodingLanguage lang, bool isSet) {
            desiredLanguages[lang] = isSet;
        }

        public bool CountLines(string folder)
        {
            if (!Directory.Exists(folder))
                return false;

            ResetNumLines();

            // Loop through each coding language
            var languages = Enum.GetValues(typeof(CodingLanguage));
            foreach (CodingLanguage lang in languages)
            {
                // Check if we want to count this language
                if (!desiredLanguages[lang])
                    continue;

                // Get all the files matching our extension. This search INCLUDES sub-directories
                var filepaths = Directory.GetFiles(folder, "*"+ extensions[lang], SearchOption.AllDirectories);

                // Count the number of lines in each file, add it to our counter
                foreach (var path in filepaths) {
                    var lineCount = File.ReadLines(path).Count();
                    numLines[lang] += lineCount;
                }
            }

            return true;
        }

        public String GetLastResult() {
            string output = "";
            var languages = Enum.GetValues(typeof(CodingLanguage));
            foreach (CodingLanguage lang in languages)
            {
                // Check if we want to count this language
                if (!desiredLanguages[lang])
                    continue;

                output += lang.ToString("G") + ": " + numLines[lang] + "\n";
            }
            return output;
        }
    }
}
