namespace TaskHub.Utils;

public static class InputReader
{
    public static string ReadRequiredText(string caption)
    {
        while (true)
        {
            Console.Write(caption);
            string? value = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Trim();
            }

            Console.WriteLine("Input cannot be empty.");
        }
    }

    public static DateTime ReadDateTime(string caption)
    {
        while (true)
        {
            Console.Write(caption);
            string? value = Console.ReadLine();
            if (DateTime.TryParse(value, out DateTime date))
            {
                return date;
            }

            Console.WriteLine("Invalid date format.");
        }
    }

    public static TEnum ReadEnum<TEnum>(string caption) where TEnum : struct, Enum
    {
        while (true)
        {
            Console.Write(caption);
            string? value = Console.ReadLine();
            if (Enum.TryParse(value, true, out TEnum result) && Enum.IsDefined(result))
            {
                return result;
            }

            string allowed = string.Join(", ", Enum.GetNames<TEnum>());
            Console.WriteLine($"Allowed values: {allowed}");
        }
    }

    public static Guid ReadGuid(string caption)
    {
        while (true)
        {
            Console.Write(caption);
            string? value = Console.ReadLine();
            if (Guid.TryParse(value, out Guid id))
            {
                return id;
            }

            Console.WriteLine("Invalid Guid format.");
        }
    }
}
