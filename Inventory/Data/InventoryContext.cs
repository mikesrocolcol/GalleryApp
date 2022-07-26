using Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Data
{
    public class InventoryContext : DbContext

    {

        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {

        }
        public DbSet<ProductEntities> Inventories { get; set; }
        public object ProductEntities { get; internal set; }
    }
}
