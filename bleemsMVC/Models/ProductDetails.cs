namespace bleemsMVC.Models
{
    public class ProductDetails
    {
        // Define the properties that match the columns returned by the stored procedure
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPriceWithCurrency { get; set; }
       
        
    }
}
