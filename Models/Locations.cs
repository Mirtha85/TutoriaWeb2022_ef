using System.Collections.Generic;
namespace webmnv_ef_01.Models

{
    public class Location
    {
        public Location()
        {
            this.Customers = new List<Customer>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Customer> Customers{get;set;}
    }
}