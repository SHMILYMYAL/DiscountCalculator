namespace DiscountCalculator.Domain
{
    public class DiscountRate
    {
        public double Rate { get; set; }

        public int EditedBy { get; set; }

        public ProductType ProductType { get; set; }
    }
}
