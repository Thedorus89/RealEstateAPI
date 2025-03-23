namespace RealEstateAPI.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Type { get; set; } // House, Apartment, Land
        public decimal Price { get; set; }
    }
}
