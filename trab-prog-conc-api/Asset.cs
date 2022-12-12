namespace trab_prog_conc_api
{
    public class Asset
    {
        public Guid Guid { get; set; }
        public string CompanyName { get; set; }
        public string Symbol { get; set; }
        private bool Available { get; set; }
        public Asset(string companyName, string symbol)
        {
            Guid = Guid.NewGuid();
            CompanyName = companyName;
            Symbol = symbol;
            Available = true;
        }

        public void SetRandomDisponibility()
        {
            if(Available is false)
            {
                return;
            }
            var random = new Random();
            Available = random.Next(2) == 1;
        }

        public bool IsAvailable()
        {
            return Available;
        }

    }


}
