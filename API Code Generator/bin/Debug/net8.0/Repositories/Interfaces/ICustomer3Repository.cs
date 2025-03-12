public interface ICustomer3Repository
{
    void Add(Customer3 entity);
    void Save();
    Customer3 GetById(int id);
}
