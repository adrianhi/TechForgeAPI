using LearningAsp.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200",
         builder => builder.WithOrigins("http://localhost:4200")
                           .AllowAnyMethod()
                           .AllowAnyHeader());
});
builder.Services.AddDbContext<TechForgeContext>();

var app = builder.Build();


app.MapGet("/contacts", (TechForgeContext db ) => db.ContactForms.ToList());
app.MapPost("/contacts", (TechForgeContext db, ContactForm contact) =>
{
    db.ContactForms.Add(contact);
    db.SaveChanges();
    return Results.Created($"contact/{contact.Id}",contact);
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowLocalhost4200");
app.Run();
