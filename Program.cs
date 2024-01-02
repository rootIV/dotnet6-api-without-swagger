using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:SqlServer"]);

var app = builder.Build();
var configuration = app.Configuration;
ProductRepository.Init(configuration);

app.MapPost("/products", (ProductRequest productRequest, ApplicationDbContext context) =>
{
	var category = context.Categories.Where(c => c.Id == productRequest.CategoryId).First();
	var product = new Product{ 
		Code = productRequest.Code,
		Name = productRequest.Name,
		Description = productRequest.Description,
		Category = category
	};
	
	if(productRequest.Tags != null)
	{
		product.Tags = new List<Tag>();
		foreach(var tag in productRequest.Tags)
		{
			product.Tags.Add(new Tag{ Name = tag });
		}
	}
	
	context.Products.Add(product);
	context.SaveChanges();
	
	return Results.Created($"/products/{product.Code}", product.Code);
});

app.MapGet("/products/{code}", ([FromRoute] int code, ApplicationDbContext context) =>
{
	var product = context.Products
	.Include(p => p.Category)
	.Include(p => p.Tags)
	.Where(p => p.Code == code).First();
	if (product != null)
		return Results.Ok(product);
	
	return Results.NotFound();
});

app.MapPut("/products/{code}", ([FromRoute] int code, ProductRequest productRequest, ApplicationDbContext context) =>
{
	var product = context.Products
	.Include(p => p.Tags)
	.Where(p => p.Code == code).First();
	
	var category = context.Categories.Where(c => c.Id == productRequest.CategoryId).First();
	
	product.Code = product.Code;
	product.Name = productRequest.Name;
	product.Description = productRequest.Description;
	product.Category = category;
	
	if(productRequest.Tags != null)
	{
		product.Tags = new List<Tag>();
		foreach(var tag in productRequest.Tags)
		{
			product.Tags.Add(new Tag{ Name = tag });
		}
	}
	
	context.SaveChanges();
	return Results.Ok();
});

app.MapDelete("/products/{code}", ([FromRoute] int code, ApplicationDbContext context) =>
{
	var product = context.Products.Where(p => p.Code == code).First();

	context.Products.Remove(product);
	context.SaveChanges();
	return Results.Ok();
});

//if(app.Environment.IsStaging())
app.MapGet("/configuration/database", (IConfiguration configuration) =>
{
	return Results.Ok($"{configuration["database:connection"]}/{configuration["database:port"]}");
});

app.Run();
