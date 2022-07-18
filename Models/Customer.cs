
namespace webmnv_ef_01.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }

        public int LocationId{get;set;}
    }
}