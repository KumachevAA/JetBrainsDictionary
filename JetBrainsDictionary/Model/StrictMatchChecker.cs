namespace JetBrainsDictionary.Model
{
    class StrictMatchChecker : IMatchChecker
    {
        public bool IsMatch(string Expression, string Line)
        {
            return Line.ToLower().Contains(Expression.ToLower());
        }
    }
}
