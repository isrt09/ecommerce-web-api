var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("", () => "Welcome to Web API in ASP.NET");

List<Category> categories = new List<Category>();

// GET    : /api/categories
app.MapGet("/api/categories", () =>
{
    return Results.Ok(categories); 
});

// POST    : /api/categories
app.MapPost("/api/categories", () =>
{
    var category = new Category
    {
        CategoryID = Guid.Parse("f78400e8-a2d7-43cd-ad91-ba01f5818ed7"),
        CategoryName = "Electronics",
        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
        CreatedAt = DateTime.Now,
    };
    categories.Add(category);
    return Results.Created($"api/categories/{category.CategoryID}", category);
});

// PUT    : /api/categories
app.MapPut("/api/categories/", () =>
{
    var category = categories.FirstOrDefault(category => category.CategoryID == Guid.Parse("f78400e8-a2d7-43cd-ad91-ba01f5818ed7"));
    if (category == null)
    {
        return Results.NotFound("Category ID does not exists");
    }
    category.CategoryName = "Cosmetics";
    category.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua";
    category.CreatedAt = DateTime.Now;
    return Results.NoContent();
});

// DELETE    : /api/categories
app.MapDelete("/api/categories/", () =>
{
    var categoryById = categories.FirstOrDefault(category => category.CategoryID == Guid.Parse("f78400e8-a2d7-43cd-ad91-ba01f5818ed7"));
    if (categoryById == null)
    {
        return Results.NotFound("Category ID does not exists");
    }
    categories.Remove(categoryById);
    return Results.NoContent();
});

app.Run();

public record Category
{
    public Guid CategoryID { get; set; }
    public string? CategoryName { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}

// GET    : /api/categories
// POST   : /api/categories
// PUT    : /api/categories
// DELETE : /api/categories
