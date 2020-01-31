namespace CSGOStats.Infrastructure.Core.Tests.SchedulingTests.State
{
    public class SharedState
    {
        public static SharedState Instance { get; } = new SharedState();

        public int Counter { get; private set; }

        private SharedState()
        {
        }

        public void Update() => Counter++;
    }
}