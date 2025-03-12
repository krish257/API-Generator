public interface ICustomer1Repository
{
    void Add(Customer1 entity);
    void Save();
    Customer1 GetById(int id);
}
