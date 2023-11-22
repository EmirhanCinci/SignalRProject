namespace MySignalRWebApi.Models
{
    public class SaleChart
    {
        public SaleChart()
        {
            Prices = new List<int>();
        }

        public string SaleDate { get; set; }
        public List<int> Prices { get; set; }
    }
}
