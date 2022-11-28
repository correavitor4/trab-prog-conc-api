namespace trab_prog_conc_api
{
    public class Asset
    {
        public readonly string CompanyName;
        public readonly string Symbol;
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
