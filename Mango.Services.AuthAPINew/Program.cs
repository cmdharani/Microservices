using Mango.services.AuthAPINew.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



builder.Services.AddDbContext<AppDbContext>(
    options=>options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"))
    );


builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if(app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI();

}



// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
ApplyMigration();
app.Run();


void ApplyMigration()
{
    using (var scope=app.Services.CreateScope())
    {
        var _db=scope.ServiceProvider.GetService<AppDbContext>();
        if(_db != null && _db.Database.GetAppliedMigrations().Count()>0)
        {
            _db.Database.Migrate();
        }
    }
}