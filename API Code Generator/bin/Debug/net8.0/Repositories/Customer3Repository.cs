public class Customer3Repository : ICustomer3Repository
{
    private readonly DbContext _context;
    public Customer3Repository(DbContext context)
    {
        _context = context;
    }
    public void Add(object entity) => _context.Add(entity);
    public void Save() => _context.SaveChanges();
    public Customer3 GetById(int id) => _context.Set<Customer3>().Find(id);
}
