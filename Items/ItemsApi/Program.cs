using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ItemsDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("ItemsDatabase")));
var app = builder.Build();

app.MapGet("/", async (ItemsDbContext dbContext) => 
Results.Ok(await dbContext.Items.ToListAsync()));

app.Run();
