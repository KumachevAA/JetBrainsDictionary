using System.Collections.Generic;

namespace JetBrainsDictionary.Model
{

    /// <summary>
    /// Ищет совпадения слову в словаре
    /// </summary>
    interface IDictionarySearch
    {
        IEnumerable<string> Find(string Expression);
    }
}
