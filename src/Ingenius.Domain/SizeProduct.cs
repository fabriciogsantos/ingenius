namespace Ingenius.Domain
{
    public class SizeProduct : Entity<SizeProduct>
    {
        public string Size { get; set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}
