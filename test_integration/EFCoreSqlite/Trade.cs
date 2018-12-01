using System;

namespace EFCoreSqlite
{
    public class Trade
    {
        public DateTime Timestamp { get; set; }
        public string Symbol {get; set; }
        public string Side { get; set; }
        public long Size {get; set; }
        public double Price { get; set; }
        public string TickDirection { get; set; }
        public string TrdMatchId { get; set; }
        public long? GrossValue { get; set; }
        public double? HomeNotional { get; set; }
        public double? ForeignNotional { get; set; }
    }
}