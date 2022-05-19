namespace DI.Service
{
    public class SingletonService: ISingletonService
    {
        Guid id;
        public SingletonService()
        {
            id = Guid.NewGuid();
        }

        public Guid GetID()
        {
            return id;
        }
    }
}
