using DependenceInjection.Services.Interface;

namespace DependenceInjection.Services
{
    public class ScopeService: IScopedService
    {
        Guid id;

        public ScopeService() //constructor
        {
            id = Guid.NewGuid(); //return string GUIId random 
        }
        public Guid GetGuid()
        {
            return id;
        }
    }
}
