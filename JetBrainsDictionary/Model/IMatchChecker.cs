namespace JetBrainsDictionary.Model
{
    /// <summary>
    /// Проверяет слово на совпадение запросу
    /// </summary>
    interface IMatchChecker
    {
        bool IsMatch(string Expression, string Line);
    }
}
