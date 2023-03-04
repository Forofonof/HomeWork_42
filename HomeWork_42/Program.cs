using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Seller seller = new Seller();
        Player player = new Player(5000);
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
                    seller.ViewInventory();
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

class Person
{
    protected List<Item> _inventory = new List<Item> { };

    public int Balance { get; protected set; }

    public void ViewInventory()
    {
        if (false == _inventory.Any())
        {
            Console.WriteLine("Товаров нет!");
            return;
        }
        else
        {
            Console.WriteLine("Товары: ");

            foreach (var item in _inventory)
            {
                Console.WriteLine($"{item.Name}. Цена - {item.Price}.");
            }
        }
    }
}

class Player : Person
{
    public Player(int balance)
    {
        Balance = balance;
        _inventory = new List<Item>();
    }

    public void BuyProduct(Item item)
    {
        if (Balance >= item.Price)
        {
            _inventory.Add(item);
            Balance -= item.Price;

            Console.WriteLine($"Вы купили: {item.Name}\nВаш баланс: {Balance}");
        }
        else
        {
            Console.WriteLine("Недостаточно средств на вашем балансе.");
        }
    }
}

class Seller : Person
{
    public Seller()
    {
        _inventory = new List<Item>
            {
                new Item("Хлеб", 35),
                new Item("Сыр", 65),
                new Item("Молоко", 75),
                new Item("Колбаса", 150)
            };
    }

    public bool TryGetProduct(out Item product)
    {
        product = null;

        Console.WriteLine("Введите название товара:");
        string productName = Console.ReadLine();
        product = _inventory.Find(item => item.Name == productName);
        _inventory.Remove(product);

        return true;
    }
}