using System;
using System.Collections.Generic;

internal class Program
{
    static void Main(string[] args)
    {
        const string ViewInventoryPlayer = "1";
        const string ShowInventory = "2";
        const string BuyProduct = "3";
        const string Exit = "4";

        Seller seller = new Seller();
        Player player = new Player("forofonof", 5000);

        bool isWork = true;

        while (isWork)
        {
            Console.WriteLine($"{ViewInventoryPlayer} - Посмотреть свой инвентарь.\n{ShowInventory} - Показать инвентарь продавца.\n{BuyProduct} - Купить товар.\n{Exit} - Завершить работу.");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case ViewInventoryPlayer:
                    player.ViewInventory();
                    break;
                case ShowInventory:
                    seller.ShowAllProducts();
                    break;
                case BuyProduct:
                    seller.TradeProducts(player);
                    break;
                case Exit:
                    isWork= false;
                    break;
                default:
                    Console.WriteLine("Ошибка! Нет такой команды.");
                    break;
            }
        }
    }
}

class Player
{
    public string Name;

    public List<Item> Inventory;

    public int Balance;

    public Player(string name, int balance)
    {
        Name = name;
        Balance = balance;
        Inventory = new List<Item>();
    }

    public void ViewInventory()
    {
        if (Inventory.Count == 0)
        {
            Console.WriteLine("Ваш инвентарь пустой!");
            return;
        }
        else
        {
            Console.WriteLine("Ваш инвентарь:");

            foreach (var item in Inventory)
            {
                Console.WriteLine($"Название - {item.Name}. Цена - ${item.Price}.");
            }
        }
    }
}

class Seller
{
    public void ShowAllProducts()
    {
        int minimumNumberItems = 0;

        if (_items.Count > minimumNumberItems)
        {
            Console.WriteLine("Товары доступные к покупке:");

            for (int i = 0; i < _items.Count; i++)
            {
                Console.WriteLine($"{_items[i].Name} - {_items[i].Price}");
            }
        }
        else
        {
            Console.WriteLine("Товары закончились.");
        }
    }

    public void TradeProducts(Player player)
    {
        Console.WriteLine("Введите название товара:");
        string productName = Console.ReadLine();
        Item product = _items.Find(item => item.Name == productName);

        if (product != null)
        {
            SellProduct(product, player);
        }
        else
        {
            Console.WriteLine("Товар не найден.");
        }
    }

    private void SellProduct(Item item, Player player)
    {
        if (player.Balance >= item.Price)
        {
            _items.Remove(item);
            player.Inventory.Add(item);
            player.Balance -= item.Price;

            Console.WriteLine($"Вы купили: {item.Name}\nВаш баланс: {player.Balance}");
        }
        else
        {
            Console.WriteLine("Недостаточно средств на вашем балансе.");
        }
    }

    private List<Item> _items = new List<Item> {
        new Item("Хлеб", 35),
        new Item("Молоко", 65),
        new Item("Сыр", 170),
        new Item("Колбаса", 150)};
}

class Item
{
    public string Name { get; private set; }

    public int Price { get; private set; }

    public Item(string name, int price)
    {
        Name = name;
        Price = price;
    }
}