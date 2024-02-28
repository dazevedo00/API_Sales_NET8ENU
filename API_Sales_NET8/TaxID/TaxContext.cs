namespace Strategy_Pattern
{
    public class TaxContext
    {

        private ITax _itax;

        public TaxContext(ITax itax)
        {
            _itax = itax;
        }

        public void SetStrategy(ITax itax)
        {
            _itax = itax;
        }

        public bool IsValid(string taxId)
        {
            return _itax.IsValid(taxId);
        }
    }
}
