namespace MIPT_course;

public class Product : IEntity
{
    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; }

    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Id}: {Name} ({Price})";
    }
}

public class User : IEntity
{
    public int Id { get; }
    public string Name { get; }
    public string Email { get; }

    public User(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public override string ToString()
    {
        return $"{Id}: {Name} <{Email}>";
    }
}
