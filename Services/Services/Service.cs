using Database.interfaces;

namespace Services.Services
{
    public class Service
    {
        public Service(IApplicationDBContext context)
        {
            this.Context = context;
        }

        protected IApplicationDBContext Context { get; set; }
    }
}
