namespace trab_prog_conc_api
{
    public class Asset
    {
        public string CompanyName { get; set; }
        public string Symbol { get; set; }
        private Owner? Owner { get; set; }
        public Asset(string companyName, string symbol)
        {
            CompanyName = companyName;
            Symbol = symbol;
        }

        public void BuyAsset(Guid OwnerId, string OwnerName)
        {
            Owner.Id = OwnerId;
            Owner.Name = OwnerName;
        }

    }

    public class Owner
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
