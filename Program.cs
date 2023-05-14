using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    static async Task Main(string[] args)
    {
        // Запитати користувача про категорію
        Console.WriteLine("Введiть категорiю (animal, career, celebrity, dev, explicit, fashion, food, history, money, movie, music, political, religion, science, sport, travel):");
        string category = Console.ReadLine();

        // Перевірити, чи введена категорія є в списку доступних категорій
        string[] categories = { "animal", "career", "celebrity", "dev", "explicit", "fashion", "food", "history", "money", "movie", "music", "political", "religion", "science", "sport", "travel" };
        if (!Array.Exists(categories, x => x == category))
        {
            Console.WriteLine("Неправильна категорiя!");
            return;
        }

        // Викликати API та отримати випадковий анекдот вибраної категорії
        HttpClient client = new HttpClient();
        string apiUrl = $"https://api.chucknorris.io/jokes/random?category={category}";
        HttpResponseMessage response = await client.GetAsync(apiUrl);
        string responseBody = await response.Content.ReadAsStringAsync();

        // Розпакувати JSON в об'єкт типу Joke
        Joke joke = JsonConvert.DeserializeObject<Joke>(responseBody);

        // Вивести категорію, дату та анекдот в консоль
        Console.WriteLine($"Категорiя: {category}");
        Console.WriteLine($"Дата створення: {joke.created_at}");
        Console.WriteLine($"Анекдот: {joke.value}");

        Console.ReadLine();
    }
}

class Joke
{
    public string value { get; set; }
    public string created_at { get; set; }
}