using System;

// Абстрактный класс доставки
public abstract class Delivery
{
    public string Address { get; protected set; }
    
    protected Delivery(string address)
    {
        Address = address;
    }

    public abstract void DisplayDeliveryInfo();
}

// Доставка на дом
public class HomeDelivery : Delivery
{
    public string CourierName { get; }
    
    public HomeDelivery(string address, string courierName) : base(address)
    {
        CourierName = courierName;
    }

    public override void DisplayDeliveryInfo()
    {
        Console.WriteLine($"Доставка на дом по адресу: {Address}");
        Console.WriteLine($"Курьер: {CourierName}");
    }
}

// Доставка в пункт выдачи
public class PickPointDelivery : Delivery
{
    public string CompanyName { get; }
    
    public PickPointDelivery(string address, string companyName) : base(address)
    {
        CompanyName = companyName;
    }

    public override void DisplayDeliveryInfo()
    {
        Console.WriteLine($"Доставка в пункт выдачи {CompanyName}");
        Console.WriteLine($"Адрес пункта: {Address}");
    }
}

// Класс продукта
public class Product
{
    public string Name { get; }
    public decimal Price { get; }
    
    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"{Name} - {Price:C}");
    }
}

// Класс заказа с обобщением
public class Order<TDelivery> where TDelivery : Delivery
{
    public TDelivery Delivery { get; }
    public int Number { get; }
    public Product[] Products { get; }
    
    public Order(int number, TDelivery delivery, Product[] products)
    {
        Number = number;
        Delivery = delivery;
        Products = products;
    }

    public void DisplayAddress()
    {
        Console.WriteLine(Delivery.Address);
    }

    public void DisplayOrderInfo()
    {
        Console.WriteLine($"Заказ #{Number}");
        Console.WriteLine("Состав заказа:");
        foreach (var product in Products)
        {
            product.DisplayInfo();
        }
        Console.WriteLine("Информация о доставке:");
        Delivery.DisplayDeliveryInfo();
    }
}

// Пример использования
class Program
{
    static void Main(string[] args)
    {
        // Создаем продукты
        var laptop = new Product("Ноутбук", 1200);
        var phone = new Product("Смартфон", 999);
        
        // Создаем доставки
        var homeDelivery = new HomeDelivery("ул. Ленина, 10, кв. 42", "Иванов Иван");
        var pickPointDelivery = new PickPointDelivery("ул. Пушкина, 5", "PickPoint");
        
        // Создаем заказы
        var homeOrder = new Order<HomeDelivery>(1, homeDelivery, new Product[] { laptop, phone });
        var pickPointOrder = new Order<PickPointDelivery>(2, pickPointDelivery, new Product[] { phone });
        
        // Выводим информацию о заказах
        homeOrder.DisplayOrderInfo();
        Console.WriteLine();
        pickPointOrder.DisplayOrderInfo();
    }
}
