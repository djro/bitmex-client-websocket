using Bitmex.Client.Websocket.Responses.Trades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Bitmex.Client.Websocket.EFCoreSqlite
{
    public class Repository
    {
        public static void AddTrade(Responses.Trades.Trade x)
        {
            if(x.Size >= 10000)
            {
                var dbTrade = new EFCoreSqlite.Trade{
                    Timestamp = x.Timestamp,
                    Symbol = x.Symbol,
                    Side = x.Side.ToString(),
                    Size = x.Size,
                    Price = x.Price
                };
                using(var db = new BitmexDbContext())
                {
                    var existingTrade = db.Trades.Where(y => y.Timestamp == x.Timestamp && y.Symbol == x.Symbol).FirstOrDefault();

                    if(existingTrade == null)
                    {
                        db.Trades.Add(dbTrade);
                    }
                    else
                    {
                        existingTrade.Size += x.Size;
                    }                   
                    db.SaveChanges();
                }

            }
        }

        public static void AddLiquidation(Responses.Liquidation.Liquidation x, Responses.BitmexAction action)
        {
            var currTime = DateTime.Now.ToUniversalTime();
            if(action == Responses.BitmexAction.Insert){
                var dbLiquidation = new EFCoreSqlite.Liquidation{
                    OrderID = x.OrderID,
                    DateAdded = currTime,
                    Symbol = x.Symbol,
                    Side = x.Side?.ToString(),
                    Price = x.Price,
                    leavesQty = x.leavesQty
                };

                using(var db = new BitmexDbContext()){
                    db.Liquidations.Add(dbLiquidation);
                    db.SaveChanges();
                }
            }
            else if(action == Responses.BitmexAction.Update){
                using (var db = new BitmexDbContext()){
                    var updateDbObj = db.Liquidations.Where(y => y.OrderID == x.OrderID).FirstOrDefault();
                    if(updateDbObj != null){
                        updateDbObj.LatestPrice = x.Price ?? updateDbObj.LatestPrice;
                        updateDbObj.lastLeavesQty = x.leavesQty ?? updateDbObj.lastLeavesQty;
                        updateDbObj.numUpdates = (updateDbObj.numUpdates ?? 0)  + 1;
                        db.SaveChanges();
                    }
                }
            }
            else if(action == Responses.BitmexAction.Delete){
                using (var db = new BitmexDbContext()){
                    var updateDbObj = db.Liquidations.Where(y => y.OrderID == x.OrderID).FirstOrDefault();
                    if(updateDbObj != null){
                        updateDbObj.deletedTime = currTime;
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}