namespace trab_prog_conc_api
{
    public class PaginetedReturn
    {
        public int count { get; set; }
        public int page { get; set; }
        public int rollsPerPage { get; set; }
        public List<Asset> assets { get; set; }
    }
}
