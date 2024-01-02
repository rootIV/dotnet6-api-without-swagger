public static class ProductRepository
{
	public static List<Product> Database { get; set; } = Database = new List<Product>();

	public static void Init(IConfiguration configuration)
	{
		var products = configuration.GetSection("Products").Get<List<Product>>();
		Database = products;
	}
	public static void Add(Product product)
	{
		Database.Add(product);
	}
	public static Product GetBy(int code)
	{
		return Database.FirstOrDefault(p => p.Code == code);
	}
	public static void Remove(Product product) { Database.Remove(product); }
}