namespace Klir.TechChallenge.Web.Api.Entities
{
    public class CartProduct
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantidy { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Saved { get; set; }
        public string Promotion { get; set; }
        public string PromotionApplied { get; set; }
    }
}