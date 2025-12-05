public abstract class Device : ISwitchable
{
    public string Name { get; set; }
    public bool IsOn { get; protected set; }

    public abstract void TurnOn();
    public abstract void TurnOff();

    public void PrintStatus()
    {
        if (IsOn)
            Console.WriteLine($"{Name}: увімкнено");
        else
            Console.WriteLine($"{Name}: вимкнено");
    }
}
