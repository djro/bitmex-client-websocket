using System;
using System.ComponentModel.DataAnnotations;

namespace EFCoreSqlite
{
    public class Liquidation
    {
        [Key]
        public string OrderID { get; set; }
        public DateTime? DateAdded {get;set;}
        public string Symbol { get; set; }
        public string Side { get; set; }
        public double? Price { get; set; }
        public long? leavesQty { get; set; }
        public double? LatestPrice {get; set;}
        public long? lastLeavesQty {get;set;}
        public DateTime? deletedTime {get; set;}
    }
}
