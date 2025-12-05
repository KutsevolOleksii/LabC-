public class CoffeeMachine : Device, IEnergyConsumer
{
    public string DeviceName => Name;
    public int PowerConsumption => 1000;

    public override void TurnOn()
    {
        IsOn = true;
        Console.WriteLine($"{Name} почала готувати каву.");
    }

    public override void TurnOff()
    {
        IsOn = false;
        Console.WriteLine($"{Name} завершила роботу.");
    }

    public double GetEnergyUsage(int hours)
    {
        if (!IsOn) return 0;
        return PowerConsumption * hours / 1000.0;
    }
}
