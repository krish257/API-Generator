public class Customer1Repository : ICustomer1Repository
{
    private readonly DbContext _context;
    public Customer1Repository(DbContext context)
    {
        _context = context;
    }
    public void Add(object entity) => _context.Add(entity);
    public void Save() => _context.SaveChanges();
    public Customer1 GetById(int id) => _context.Set<Customer1>().Find(id);
}
