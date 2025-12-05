public class Light : Device, IEnergyConsumer
{
    public string DeviceName => Name;
    public int PowerConsumption => 60;

    public override void TurnOn()
    {
        IsOn = true;
        Console.WriteLine($"{Name} засвітилася.");
    }

    public override void TurnOff()
    {
        IsOn = false;
        Console.WriteLine($"{Name} вимкнена.");
    }

    public double GetEnergyUsage(int hours)
    {
        if (!IsOn) return 0;
        return PowerConsumption * hours / 1000.0;
    }
}
