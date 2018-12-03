﻿// <auto-generated />
using System;
using EFCoreSqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreSqlite.Migrations
{
    [DbContext(typeof(BitmexDbContext))]
    partial class BitmexDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("EFCoreSqlite.Liquidation", b =>
                {
                    b.Property<string>("OrderID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateAdded");

                    b.Property<double?>("LatestPrice");

                    b.Property<double?>("Price");

                    b.Property<string>("Side");

                    b.Property<string>("Symbol");

                    b.Property<DateTime?>("deletedTime");

                    b.Property<long?>("lastLeavesQty");

                    b.Property<long?>("leavesQty");

                    b.Property<int?>("numUpdates");

                    b.HasKey("OrderID");

                    b.ToTable("Liquidations");
                });

            modelBuilder.Entity("EFCoreSqlite.Trade", b =>
                {
                    b.Property<DateTime>("Timestamp");

                    b.Property<string>("Symbol");

                    b.Property<double?>("ForeignNotional");

                    b.Property<long?>("GrossValue");

                    b.Property<double?>("HomeNotional");

                    b.Property<double>("Price");

                    b.Property<string>("Side");

                    b.Property<long>("Size");

                    b.Property<string>("TickDirection");

                    b.Property<string>("TrdMatchId");

                    b.HasKey("Timestamp", "Symbol");

                    b.ToTable("Trades");
                });
#pragma warning restore 612, 618
        }
    }
}
