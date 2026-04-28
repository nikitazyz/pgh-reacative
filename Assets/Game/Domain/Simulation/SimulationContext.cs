namespace Reacative.Domain.Simulation
{
    public class SimulationContext
    {
        public double DeltaTime { get; }
        public long TimeStamp { get; }
        public double ProducedEnergy { get; set; }
        public double TemperatureDelta { get; set; }
        
        public bool ShouldStopTurbine { get; set; }

        public SimulationContext(double deltaTime, long timeStamp)
        {
            DeltaTime = deltaTime;
            TimeStamp = timeStamp;
        }
    }
}