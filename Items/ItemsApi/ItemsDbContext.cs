using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
public sealed class ItemsDbContext : DbContext
{
public ItemsDbContext(DbContextOptions<ItemsDbContext> options) : base(options){}
public DbSet<Item> Items => Set<Item>();

}
[Table("items")]
public sealed record Item
{
    public int id {get;init;}
    public string itemname {get;init;} = string.Empty;
    public decimal price {get;init;}

    public DateTime created {get;init;}

    public bool active {get;init;}
}