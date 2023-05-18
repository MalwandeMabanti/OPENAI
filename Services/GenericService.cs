using Microsoft.EntityFrameworkCore;
using OPENAI.Data;

using Product_Inventory_Management_System.Interface;

namespace OPENAI.Services
{
   
    public class GenericService<T> : IGenericService<T>
        where T : class
    {
        private readonly ApplicationDbContext context;

        public GenericService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<T> GetAll()
        {
            return this.context.Set<T>().ToList();
        }

        public void Add(T entity)
        {
            this.context.Set<T>().Add(entity);
            this.context.SaveChanges();
        }
    }
}
