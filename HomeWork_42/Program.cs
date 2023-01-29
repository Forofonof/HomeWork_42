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

        bool isWork = true;

        Item bread = new Item("Хлеб", 35);
        Item milk = new Item("Молоко", 65);
        Item cheese = new Item("Сыр", 170);
        Item sausage = new Item("Колбаса", 150);

        Seller seller = new Seller();
        seller.Items.Add(bread);
        seller.Items.Add(milk);
        seller.Items.Add(cheese);
        seller.Items.Add(sausage);

        Player player = new Player("forofonof", 5000);

        Console.WriteLine("Добро пожаловать в магазин, что желаете купить?");

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
                    seller.ShowProducts();
                    break;
                case BuyProduct:
                    seller.ProductSearch(seller, player);
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
    public string Name { get; set; }

    public List<Item> Inventory { get; set; }

    public int Balance { get; set; }

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
    public List<Item> Items { get; set; }

    public Seller()
    {
        Items = new List<Item>();
    }

    public void ShowProducts()
    {
        Console.WriteLine("Товары доступные к покупке:");
        for (int i = 0; i < Items.Count; i++)
        {
            Console.WriteLine(Items[i].Name + " - " + Items[i].Price);
        }
    }

    public void SellProduct(Item item, Player player)
    {
        if (Items.Contains(item))
        {
            if (player.Balance >= item.Price)
            {
                Items.Remove(item);
                player.Inventory.Add(item);
                player.Balance -= item.Price;

                Console.WriteLine($"Вы купили: {item.Name}");
                Console.WriteLine($"Ваш баланс: {player.Balance}");
            }
            else
            {
                Console.WriteLine("Недостаточно средств на вашем балансе.");
            }
        }
        else
        {
            Console.WriteLine("Товар уже продан.");
        }
    }

    public void ProductSearch(Seller seller, Player player)
    {
        Console.WriteLine("Введите название товара:");
        string productName = Console.ReadLine();
        Item product = seller.Items.Find(item => item.Name == productName);

        if (product != null)
        {
            seller.SellProduct(product, player);
        }
        else
        {
            Console.WriteLine("Товар не найден.");
        }
    }
}

class Item
{
    public string Name { get; set; }

    public int Price { get; set; }

    public Item(string name, int price)
    {
        Name = name;
        Price = price;
    }
}
