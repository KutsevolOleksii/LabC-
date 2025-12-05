public interface IEnergyConsumer
{
    string DeviceName { get; }
    int PowerConsumption { get; }
    double GetEnergyUsage(int hours);
}
