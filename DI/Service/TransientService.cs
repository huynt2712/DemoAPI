namespace DI.Service
{
    public class TransientService : ITransientService
    {
        Guid id;
        public TransientService()
        {
            id = Guid.NewGuid();
        }

        public Guid GetID()
        {
            return id;
        }
    }
}
}
