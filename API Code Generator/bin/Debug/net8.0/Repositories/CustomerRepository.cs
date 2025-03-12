public class CustomerRepository
{
    private readonly DbContext _context;
    public CustomerRepository(DbContext context)
    {
        _context = context;
    }
    // CRUD operations (example)
    public void Add(object entity) => _context.Add(entity);
    public void Save() => _context.SaveChanges();
}
