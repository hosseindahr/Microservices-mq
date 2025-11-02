using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }=DateTime.Now;
        public int ProductId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
