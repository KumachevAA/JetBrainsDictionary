using System.Collections.Generic;
using System.IO;

namespace JetBrainsDictionary.Model
{
    class FileDictionarySearch : IDictionarySearch
    {
        public FileDictionarySearch(string Path)
        {
            this.Path = Path;
        }

        public string Path { get; }

        public IEnumerable<string> Find(string Expression)
        {
            using TextReader reader = File.OpenText(Path);

            string line = reader.ReadLine();

            while (line != null)
            {
                if (line.ToLower().Contains(Expression.ToLower()))
                {
                    yield return line;
                }

                line = reader.ReadLine();
            }
        }
    }
}
