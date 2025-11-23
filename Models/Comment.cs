using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Comment
    { 
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Content { get; set; } = String.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
        public int CompanyStockId { get; set; }
        public string UserId { get; set; }
        public DefaultUser User { get; set; }
    }
}