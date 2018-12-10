    
using System;
using System.ComponentModel.DataAnnotations;

namespace Bitmex.Client.Websocket.EFCoreSqlite
{
    public class BookLevel
    {
        /// <summary>
        /// Order book level id (combination of price and symbol)
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Target symbol
        /// </summary>
        public string Symbol { get; set; }

        public string Side { get; set; }

        /// <summary>
        /// Available only for 'partial', 'insert' and 'update' action
        /// </summary>
        public long? Size { get; set; }

        /// <summary>
        /// Available only for 'partial' and 'insert' action, use Id otherwise
        /// </summary>
        public double? Price { get; set; }
    }
}