using DependenceInjection.Services.Interface;

namespace DependenceInjection.Services
{
    public class SingletonService : ISingletonService
    {
        Guid id;

        public SingletonService() //constructor
        {
            id = Guid.NewGuid(); //return string GUIId random 
        }
        public Guid GetGuid()
        {
            return id;
        }
    }
}
