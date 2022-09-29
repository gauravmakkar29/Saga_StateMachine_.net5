using Microsoft.EntityFrameworkCore;
using Saga_BlockSeat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saga_BlockSeat.Infra
{
    public class OrderDbContext : DbContext
    {
        public DbSet<OrderModel> OrderData { get; set; }

        public OrderDbContext()
        {
        }

        public OrderDbContext(DbContextOptions
<OrderDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSqlLocalDb; initial catalog=OrderDb;integrated security=true;");
        }
    }
}
