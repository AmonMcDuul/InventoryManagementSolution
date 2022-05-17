using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Models.Dtos
{
    public class ItemToUpdateDto
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
    }
}
