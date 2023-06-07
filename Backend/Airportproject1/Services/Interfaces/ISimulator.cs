namespace Airportproject.Client.Services.Interfaces
{
    public interface ISimulator
    {
        bool IsStarted { get; }

        void Start(int interval = 5000);
    }
}