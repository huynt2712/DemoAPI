using DependenceInjection.Services.Interface;

namespace DependenceInjection.Services
{
    public class TransientService: ITransientService
    {
        Guid id;

        public TransientService() //constructor
        {
            id = Guid.NewGuid(); //return string GUIId random 
        }
        public Guid GetGuid()
        {
            return id;
        }
    }
}
