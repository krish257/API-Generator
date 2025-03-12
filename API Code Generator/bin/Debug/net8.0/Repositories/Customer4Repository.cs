public class Customer4Repository : ICustomer4Repository
{
    private readonly DbContext _context;
    public Customer4Repository(DbContext context)
    {
        _context = context;
    }
    public void Add(object entity) => _context.Add(entity);
    public void Save() => _context.SaveChanges();
    public Customer4 GetById(int id) => _context.Set<Customer4>().Find(id);
}
