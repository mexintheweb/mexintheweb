using mexintheweb.Data;
using mexintheweb.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

WebApplicationOptions options = new()
{
    ContentRootPath = AppContext.BaseDirectory,
    Args = args
};

var builder = WebApplication.CreateBuilder(options);

// ConnectionString festlegen und auslesen
var connectionStringValue = "ProductionConnection";
#if DEBUG
connectionStringValue = "DefaultConnection";
#endif
var connectionString = builder.Configuration.GetConnectionString(connectionStringValue);

// DbContext setzen
builder.Services.AddDbContext<AuthIdentityDbContext>(options => {
    options.UseSqlServer(connectionString);
});
builder.Services.AddEntityFrameworkSqlServer();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AuthIdentityDbContext>();

// Authentication setzen
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "https://www.mexintheweb.net",
        ValidateAudience = true,
        ValidAudience = "https://www.mexintheweb.net",
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SECURITYMEXWEB21SECURITYMEXWEB21SECURITYMEXWEB21SECURITYMEXWEB21SECURITYMEXWEB21SECURITYMEXWEB21"))
    };
});


// Standards setzen
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


// WebApp Einstellungen
var app = builder.Build();
app.MapControllers();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("corsapp");

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<AuthIdentityDbContext>();
    dataContext.Database.Migrate();
}

//app.MapGet("/", () => "Hello World!");

app.Run();
