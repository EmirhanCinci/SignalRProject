namespace MySignalRWebApi.Models
{
    public enum Product
    {
        Kalem = 1,
        Silgi = 2,
        Defter = 3,
        Kitap = 4,
        Kağıt = 5
    }

    public class Sale
    {
        public int Id { get; set; }
        public Product ProductName { get; set; }
        public int Price { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
