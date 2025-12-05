public interface ISwitchable
{
    void TurnOn();
    void TurnOff();
    bool IsOn { get; }
}
