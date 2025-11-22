using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Portfolio")]
    public class Portfolio
    {
        public string DefaultUserId { get; set; }
        public int CompanyStockId { get; set; }
        public DefaultUser DefaultUser { get; set; }
        public CompanyStock CompanyStock { get; set; }
    }
}