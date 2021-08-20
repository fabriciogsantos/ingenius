namespace Ingenius.Domain
{
    public class Inventory : Entity<Inventory>
    {
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public string Size { get; set; }
        public override bool IsValid()
        {
            return true;
        }
    }
}
