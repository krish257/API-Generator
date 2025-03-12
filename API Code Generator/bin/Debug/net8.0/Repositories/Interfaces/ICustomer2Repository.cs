public interface ICustomer2Repository
{
    void Add(Customer2 entity);
    void Save();
    Customer2 GetById(int id);
}
