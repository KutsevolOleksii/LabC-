using System;
using System.Collections.Generic;
using System.Linq;

enum OrderStatus
{
    New,
    InProgress,
    Ready,
    Paid
}

// Абстрактний клас для елементів меню
abstract class MenuItem
{
    public int ID { get; private set; }
    private static int nextId = 1;
    public string Name { get; set; }
    public decimal Price { get; set; }

    public MenuItem(string name, decimal price)
    {
        ID = nextId++;
        Name = name;
        Price = price;
    }

    public abstract string GetInfo();
}

// Страва
class Dish : MenuItem
{
    public string Category { get; set; }

    public Dish(string name, decimal price, string category) : base(name, price)
    {
        Category = category;
    }

    public override string GetInfo()
    {
        return $"[{ID}] {Name} ({Category}) - {Price} грн";
    }
}

// Напій
class Drink : MenuItem
{
    public int Volume { get; set; }
    public bool IsAlcoholic { get; set; }

    public Drink(string name, decimal price, int volume, bool isAlcoholic) : base(name, price)
    {
        Volume = volume;
        IsAlcoholic = isAlcoholic;
    }

    public override string GetInfo()
    {
        string alc = IsAlcoholic ? "алкогольний" : "без алкоголю";
        return $"[{ID}] {Name} ({Volume} мл, {alc}) - {Price} грн";
    }
}

// Клас замовлення
class Order
{
    private static int nextId = 101;
    public int ID { get; private set; }
    public int TableNumber { get; set; }
    public List<MenuItem> Items { get; private set; } = new List<MenuItem>();
    public OrderStatus Status { get; private set; } = OrderStatus.New;

    public Order(int tableNumber)
    {
        ID = nextId++;
        TableNumber = tableNumber;
    }

    public void AddItem(MenuItem item)
    {
        Items.Add(item);
        Console.WriteLine($"Додано позицію: [{item.ID}] {item.Name}");
    }

    public void RemoveItem(int itemId)
    {
        MenuItem item = Items.FirstOrDefault(i => i.ID == itemId);
        if (item != null)
        {
            Items.Remove(item);
            Console.WriteLine($"Видалено позицію: [{item.ID}] {item.Name}");
        }
        else
            Console.WriteLine($"Позиція з ID {itemId} не знайдена у замовленні");
    }

    public decimal GetTotal() => Items.Sum(i => i.Price);

    public void SetStatus(OrderStatus newStatus)
    {
        Status = newStatus;
        Console.WriteLine($"> Змінено статус: {Status}");
    }

    public void ShowOrder()
    {
        Console.WriteLine($"ID замовлення: {ID} | Стіл: {TableNumber} | Статус: {Status} | Сума: {GetTotal()} грн");
        if (Items.Count > 0)
        {
            Console.WriteLine("Позиції у замовленні:");
            foreach (var item in Items)
                Console.WriteLine($" - [{item.ID}] {item.GetInfo()}");
        }
    }
}

// Клас ресторану
class Restaurant
{
    public List<MenuItem> Menu { get; private set; } = new List<MenuItem>();
    public List<Order> Orders { get; private set; } = new List<Order>();

    public void AddMenuItem(MenuItem item) => Menu.Add(item);

    public void ShowMenu()
    {
        Console.WriteLine("--- МЕНЮ РЕСТОРАНУ ---");
        foreach (var item in Menu)
            Console.WriteLine(item.GetInfo());
        Console.WriteLine("-----------------------");
    }

    public Order CreateOrder(int tableNumber)
    {
        Order order = new Order(tableNumber);
        Orders.Add(order);
        Console.WriteLine($"\nСтворено нове замовлення для столика №{tableNumber}");
        return order;
    }

    public Order FindOrder(int id) => Orders.FirstOrDefault(o => o.ID == id);

    // Пошук меню за назвою або категорією
    public List<MenuItem> SearchMenu(string keyword)
    {
        return Menu.Where(item =>
        {
            if (item.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                return true;

            // Downcast для доступу до Category
            if (item is Dish dish && dish.Category.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }).ToList();
    }

    public void ShowAllOrders()
    {
        Console.WriteLine("\n--- УСІ ЗАМОВЛЕННЯ ---");
        foreach (var order in Orders)
            order.ShowOrder();
        if (Orders.Count == 0)
            Console.WriteLine("Замовлень ще немає.");
    }
}

// Основна програма з пошуком
class Program
{
    static void Main()
    {
        Restaurant restaurant = new Restaurant();

        // Початкове меню
        restaurant.AddMenuItem(new Dish("Борщ", 120, "Перше"));
        restaurant.AddMenuItem(new Dish("Котлета по-київськи", 150, "Друге"));
        restaurant.AddMenuItem(new Drink("Кава", 60, 200, false));
        restaurant.AddMenuItem(new Drink("Сік апельсиновий", 70, 250, false));

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n--- Меню дій ---");
            Console.WriteLine("1. Показати меню");
            Console.WriteLine("2. Створити замовлення");
            Console.WriteLine("3. Додати позицію до замовлення");
            Console.WriteLine("4. Видалити позицію з замовлення");
            Console.WriteLine("5. Змінити статус замовлення");
            Console.WriteLine("6. Показати всі замовлення");
            Console.WriteLine("7. Пошук замовлення за ID");
            Console.WriteLine("8. Пошук у меню (назва/категорія)");
            Console.WriteLine("0. Вихід");
            Console.Write("Виберіть дію: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    restaurant.ShowMenu();
                    break;

                case "2":
                    Console.Write("Введіть номер столика: ");
                    if (int.TryParse(Console.ReadLine(), out int table))
                        restaurant.CreateOrder(table);
                    else
                        Console.WriteLine("Невірний номер столика");
                    break;

                case "3":
                    Console.Write("Введіть ID замовлення: ");
                    if (int.TryParse(Console.ReadLine(), out int orderIdAdd))
                    {
                        Order orderAdd = restaurant.FindOrder(orderIdAdd);
                        if (orderAdd != null)
                        {
                            restaurant.ShowMenu();
                            Console.Write("Введіть ID позиції з меню для додавання: ");
                            if (int.TryParse(Console.ReadLine(), out int menuId))
                            {
                                MenuItem menuItem = restaurant.Menu.FirstOrDefault(m => m.ID == menuId);
                                if (menuItem != null)
                                {
                                    orderAdd.AddItem(menuItem);
                                    Console.WriteLine($"Поточна сума: {orderAdd.GetTotal()} грн");
                                }
                                else Console.WriteLine("Позиція з таким ID не знайдена у меню");
                            }
                        }
                        else Console.WriteLine("Замовлення не знайдено");
                    }
                    break;

                case "4":
                    Console.Write("Введіть ID замовлення: ");
                    if (int.TryParse(Console.ReadLine(), out int orderIdRemove))
                    {
                        Order orderRemove = restaurant.FindOrder(orderIdRemove);
                        if (orderRemove != null)
                        {
                            Console.Write("Введіть ID позиції для видалення: ");
                            if (int.TryParse(Console.ReadLine(), out int itemId))
                                orderRemove.RemoveItem(itemId);
                            else Console.WriteLine("Невірний ID позиції");
                        }
                        else Console.WriteLine("Замовлення не знайдено");
                    }
                    break;

                case "5":
                    Console.Write("Введіть ID замовлення: ");
                    if (int.TryParse(Console.ReadLine(), out int orderIdStatus))
                    {
                        Order orderStatus = restaurant.FindOrder(orderIdStatus);
                        if (orderStatus != null)
                        {
                            Console.WriteLine("Доступні статуси: 0-New, 1-InProgress, 2-Ready, 3-Paid");
                            Console.Write("Введіть номер нового статусу: ");
                            if (int.TryParse(Console.ReadLine(), out int statusNum) &&
                                statusNum >= 0 && statusNum <= 3)
                            {
                                orderStatus.SetStatus((OrderStatus)statusNum);
                            }
                            else Console.WriteLine("Невірний статус");
                        }
                        else Console.WriteLine("Замовлення не знайдено");
                    }
                    break;

                case "6":
                    restaurant.ShowAllOrders();
                    break;

                case "7":
                    Console.Write("Введіть ID замовлення для пошуку: ");
                    if (int.TryParse(Console.ReadLine(), out int searchId))
                    {
                        Order foundOrder = restaurant.FindOrder(searchId);
                        if (foundOrder != null)
                            foundOrder.ShowOrder();
                        else
                            Console.WriteLine("Замовлення не знайдено");
                    }
                    break;

                case "8":
                    Console.Write("Введіть ключове слово для пошуку у меню: ");
                    string keyword = Console.ReadLine();
                    var results = restaurant.SearchMenu(keyword);
                    if (results.Count > 0)
                    {
                        Console.WriteLine("Знайдено позиції:");
                        foreach (var item in results)
                            Console.WriteLine(item.GetInfo());
                    }
                    else Console.WriteLine("Нічого не знайдено");
                    break;

                case "0":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Невірна команда");
                    break;
            }
        }

        Console.WriteLine("Дякуємо, що скористалися системою ресторану!");
    }
}
