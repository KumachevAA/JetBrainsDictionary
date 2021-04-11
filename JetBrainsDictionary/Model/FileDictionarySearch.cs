using System.Collections.Generic;
using System.IO;

namespace JetBrainsDictionary.Model
{
    class FileDictionarySearch : IDictionarySearch
    {
        public FileDictionarySearch(string Path, IMatchChecker Checker)
        {
            this.Path = Path;
            this.Checker = Checker;
        }

        public string Path { get; }
        public IMatchChecker Checker { get; }

        public IEnumerable<string> Find(string Expression)
        {
            using TextReader reader = File.OpenText(Path);

            string line = reader.ReadLine();

            while (line != null)
            {
                if (Checker.IsMatch(Expression.ToLower(), line.ToLower()))
                {
                    yield return line;
                }

                line = reader.ReadLine();
            }
        }
    }
}
