namespace Strategy_Pattern
{
    internal class TaxSP : ITax
    {
        public bool IsValid(string taxId)
        {
            // For demo only
            return true;
        }
    }
}
