namespace DI.Service
{
    public class ScopedService: IScopedService
    {
        Guid id;
        public ScopedService()
        {
            id = Guid.NewGuid();
        }

        public Guid GetID()
        {
            return id;
        }
    }
}
