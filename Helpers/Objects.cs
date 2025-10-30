using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Helpers.Objects
{
    public class queryStockObject{
        public string? Symbol { get; set; }= null;
        public string? CompanyName { get; set; }= null;
        public bool isDescending { get; set; } = false;
        public int? PageIndex { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        public int LastId { get; set; } = 0;
        public string LastStringId { get; set; } = "";
    }
}