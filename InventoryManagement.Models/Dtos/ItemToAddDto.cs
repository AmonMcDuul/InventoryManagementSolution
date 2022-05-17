using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Models.Dtos
{
    public class ItemToAddDto
    {
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public int LocationId { get; set; }
    }
}
