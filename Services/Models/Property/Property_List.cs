using application.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Services.Models.Property
{
    public class Properties_List
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public Status Status { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public double Area { get; set; }
        public decimal Price { get; set; }
        public DateTime DateAdded { get; set; }
        public string Image { get; set; }
    }
}
