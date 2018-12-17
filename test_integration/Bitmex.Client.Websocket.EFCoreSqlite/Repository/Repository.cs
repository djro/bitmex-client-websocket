using Bitmex.Client.Websocket.Responses.Trades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bitmex.Client.Websocket.EFCoreSqlite
{
    public static class Repository
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

        public static void UpdateOrderBook(List<Responses.Books.BookLevel> bookLevels, Responses.BitmexAction action)
        {


            using(var db = new BitmexBookDbContext())
            {

                if(action == Responses.BitmexAction.Partial)
                {
                    //assumming this only occurs at connection initialization
                    var wipeRecords = db.BookLevels.Where(x => true);
                    db.BookLevels.RemoveRange(wipeRecords);
                }
                bookLevels.ForEach(x => 
                {
                    if(action == Responses.BitmexAction.Partial || action == Responses.BitmexAction.Insert)
                    {
                        var dbBookLevel = new BookLevel
                        {
                            Id = x.Id,
                            Symbol = x.Symbol,
                            Side = x.Side.ToString(),
                            Size = x.Size,
                            Price = x.Price
                            
                        };
                        db.BookLevels.Add(dbBookLevel);
                    }
                    else if(action == Responses.BitmexAction.Update)
                    {
                        var updateDbObj = db.BookLevels.Where(y => y.Id == x.Id).FirstOrDefault();

                        if(updateDbObj != null){
                            updateDbObj.Side = x.Side.ToString();
                            updateDbObj.Size = x.Size;
                        }
                    }
                    else if(action == Responses.BitmexAction.Delete)
                    {
                        var deleteDbObj = db.BookLevels.Where(y => y.Id == x.Id).FirstOrDefault();
                        if(deleteDbObj != null)
                        {
                            db.BookLevels.Remove(deleteDbObj);
                        }
                    }
                });
                db.SaveChanges();
            }
        }
    }
}