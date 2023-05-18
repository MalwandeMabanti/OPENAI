namespace Product_Inventory_Management_System.Interface
{
    using System.Collections.Generic;
    public interface IGenericService<T>
        where T : class
    {
        List<T> GetAll();
        void Add(T entity);
    }
}
