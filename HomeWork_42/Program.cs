using System;
using System.Collections.Generic;
using System.Linq;


class Program
{
    static void Main(string[] args)
    {
        Seller seller = new Seller();
        Player player = new Player("forofonof", 5000);
        Store store = new Store();

        store.Work(seller, player);
    }
}

class Store
{
    public void Work(Seller seller, Player player)
    {
        const string CommandViewInventory = "1";
        const string CommandShowInventory = "2";
        const string CommandBuyProduct = "3";
        const string CommandExit = "4";

        bool isWork = true;

        while (isWork)
        {
            Console.WriteLine($"{CommandViewInventory} - Посмотреть свой инвентарь.\n{CommandShowInventory} - Показать инвентарь продавца.\n{CommandBuyProduct} - Купить товар.\n{CommandExit} - Завершить работу.");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case CommandViewInventory:
                    player.ViewInventory();
                    break;

                case CommandShowInventory:
                    seller.ShowAllProducts();
                    break;

                case CommandBuyProduct:
                    TradeProduct(seller, player);
                    break;

                case CommandExit:
                    isWork= false;
                    break;

                default:
                    Console.WriteLine("Ошибка! Нет такой команды.");
                    break;
            }
        }
    }

    private void TradeProduct(Seller seller, Player player)
    {
        seller.TryGetProduct(out Item product);

        if (product != null)
        {
            player.BuyProduct(product);
        }
        else
        {
            Console.WriteLine("Товар не найден.");
        }
    }
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

class Player
{
    private List<Item> Inventory;

    private string Name;

    private int Balance;

    public Player(string name, int balance)
    {
        Name = name;
        Balance = balance;
        Inventory = new List<Item>();
    }

    public void ViewInventory()
    {
        if (false == Inventory.Any())
        {
            Console.WriteLine("Ваш инвентарь пустой!");
            return;
        }
        else
        {
            Console.WriteLine("Ваш инвентарь:");

            foreach (var item in Inventory)
            {
                Console.WriteLine($"{item.Name}. Цена - {item.Price}.");
            }
        }
    }

    public void BuyProduct(Item item)
    {
        if (Balance >= item.Price)
        {
            Inventory.Add(item);
            Balance -= item.Price;

            Console.WriteLine($"Вы купили: {item.Name}\nВаш баланс: {Balance}");
        }
        else
        {
            Console.WriteLine("Недостаточно средств на вашем балансе.");
        }
    }
}

class Seller
{
    private List<Item> _items = new List<Item>();

    public Seller()
    {
        _items.Add(new Item("Хлеб", 35));
        _items.Add(new Item("Сыр", 65));
        _items.Add(new Item("Молоко", 75));
        _items.Add(new Item("Колбаса", 150));
    }

    public void ShowAllProducts()
    {
        int minimumNumberItems = 0;

        if (_items.Count > minimumNumberItems)
        {
            Console.WriteLine("Товары доступные к покупке:");

            for (int i = 0; i < _items.Count; i++)
            {
                Console.WriteLine($"{_items[i].Name}. Цена - {_items[i].Price}");
            }
        }
        else
        {
            Console.WriteLine("Товары закончились.");
        }
    }

    public bool TryGetProduct(out Item product)
    {
        product = null;

        Console.WriteLine("Введите название товара:");
        string productName = Console.ReadLine();
        product = _items.Find(item => item.Name == productName);
        _items.Remove(product);

        return true;
    }
}