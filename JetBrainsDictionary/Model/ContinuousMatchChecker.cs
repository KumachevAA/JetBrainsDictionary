namespace JetBrainsDictionary.Model
{
    class ContinuousMatchChecker : IMatchChecker
    {
        public bool IsMatch(string expression, string line)
        {
            int position = 0;

            for (int i = 0; i < line.Length; i++)
            {
                if (expression[position] == line[i])
                    position++;

                if (position == expression.Length)
                    return true;
            }

            return false;
        }
    }
}
