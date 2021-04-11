namespace JetBrainsDictionary.Model
{
    interface IMatchChecker
    {
        bool IsMatch(string Expression, string Line);
    }
}
