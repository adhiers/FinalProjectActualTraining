namespace FinalProject.MVC.Models
{
    public class DealerInsert
    {
        public required string DealerId { get; set; }

        public required string DealerName { get; set; }

        public required string DealerAddress { get; set; }

        public required string City { get; set; }

        public required string Province { get; set; }

        public int TaxRate { get; set; }
    }
}
