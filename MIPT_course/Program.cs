using MIPT_course;

var productRepository = new Repository<Product>();
productRepository.Add(new Product(1, "Laptop", 2500m));
productRepository.Add(new Product(2, "Mouse", 40m));
productRepository.Add(new Product(3, "Monitor", 1200m));

Console.WriteLine("Products in repository:");
foreach (var product in productRepository.GetAll())
{
    Console.WriteLine(product);
}
Console.WriteLine($"Product count: {productRepository.Count}");

var expensiveProducts = productRepository.Find(product => product.Price > 1000m);
Console.WriteLine("Products with price > 1000:");
foreach (var product in expensiveProducts)
{
    Console.WriteLine(product);
}

var productById = productRepository.GetById(2);
Console.WriteLine($"GetById(2): {(productById is null ? "not found" : productById)}");

Console.WriteLine($"Remove(2): {productRepository.Remove(2)}");
Console.WriteLine($"Remove(999): {productRepository.Remove(999)}");
Console.WriteLine($"Product count after remove: {productRepository.Count}");

try
{
    productRepository.Add(new Product(1, "Duplicate Laptop", 2600m));
}
catch (InvalidOperationException exception)
{
    Console.WriteLine($"Duplicate add handled: {exception.Message}");
}

var userRepository = new Repository<User>();
userRepository.Add(new User(1, "Alice", "alice@example.com"));
userRepository.Add(new User(2, "Bob", "bob@example.com"));
Console.WriteLine("Users in repository:");
foreach (var user in userRepository.GetAll())
{
    Console.WriteLine(user);
}

var distinctInts = CollectionUtils.Distinct(new List<int> { 1, 2, 2, 3, 1, 4, 4 });
Console.WriteLine($"Distinct int: {string.Join(", ", distinctInts)}");

var distinctStrings = CollectionUtils.Distinct(new List<string> { "cat", "dog", "cat", "bird", "dog" });
Console.WriteLine($"Distinct string: {string.Join(", ", distinctStrings)}");

var words = new List<string> { "sun", "code", "ai", "star", "cloud", "sky" };
var groupedByLength = CollectionUtils.GroupBy(words, word => word.Length);
Console.WriteLine("GroupBy word length:");
foreach (var pair in groupedByLength)
{
    Console.WriteLine($"{pair.Key}: {string.Join(", ", pair.Value)}");
}

var firstCounters = new Dictionary<string, int>
{
    ["apple"] = 2,
    ["banana"] = 1,
    ["orange"] = 5
};
var secondCounters = new Dictionary<string, int>
{
    ["banana"] = 3,
    ["orange"] = 1,
    ["melon"] = 7
};
var mergedCounters = CollectionUtils.Merge(firstCounters, secondCounters, (left, right) => left + right);
Console.WriteLine("Merged dictionary:");
foreach (var pair in mergedCounters)
{
    Console.WriteLine($"{pair.Key}: {pair.Value}");
}

var mostExpensive = CollectionUtils.MaxBy(productRepository.GetAll().ToList(), product => product.Price);
Console.WriteLine($"Most expensive product: {mostExpensive}");

try
{
    CollectionUtils.MaxBy(new List<Product>(), product => product.Price);
}
catch (InvalidOperationException exception)
{
    Console.WriteLine($"MaxBy empty list handled: {exception.Message}");
}
