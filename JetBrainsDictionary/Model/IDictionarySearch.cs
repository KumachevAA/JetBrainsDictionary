using System.Collections.Generic;

namespace JetBrainsDictionary.Model
{
    interface IDictionarySearch
    {
        IEnumerable<string> Find(string Expression);
    }
}
