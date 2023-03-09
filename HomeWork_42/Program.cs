using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Seller seller = new Seller(400);
        Player player = new Player(100);
        Item item = new Item(null, 0);
        Store store = new Store();
        Menu menu = new Menu();

        menu.Work(seller, player, item, store);
    }
}

class Menu
{
    public void Work(Seller seller, Player player, Item item, Store store)
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
                    store.TradeProduct(seller, player, item);
                    break;

                case CommandExit:
                    isWork = false;
                    break;

                default:
                    Console.WriteLine("Ошибка! Нет такой команды.");
                    break;
            }
        }
    }
}

class Store
{
    public void TradeProduct(Seller seller, Player player, Item item)
    {
        if (seller.TryGetProduct(out Item product) == true)
        {
            if (player.Balance >= product.Price)
            {
                seller.SellProduct(product);
                player.BuyProduct(product);
            }
            else
            {
                Console.WriteLine($"У вас недостаточно денег. Ваш баланс: {player.Balance}");
                Console.WriteLine($"{product.Name}. Стоит: {product.Price}");
            }
        }
        else
        {
            Console.WriteLine("Товар не найден."); 
        }
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
            Console.WriteLine($"Деньги: {Balance}. Товары: ");

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
        _inventory.Add(item);
        Balance -= item.Price;

        Console.WriteLine($"Вы купили: {item.Name}. Ваш баланс: {Balance}.");
    }
}

class Seller : Person
{
    public Seller(int balance)
    {
        Balance = balance;

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
        Console.WriteLine("Введите название товара:");
        string productName = Console.ReadLine();

        product = _inventory.Find(item => item.Name == productName);

        if (product == null) return false;

        return true;
    }

    public void SellProduct(Item product)
    {
        _inventory.Remove(product);
        Balance += product.Price;
    }
}

class Item
{
    public Item(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public string Name { get; private set; }

    public int Price { get; private set; }

}