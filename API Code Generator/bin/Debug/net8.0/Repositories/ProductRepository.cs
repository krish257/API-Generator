public class ProductRepository
{
    private readonly DbContext _context;
    public ProductRepository(DbContext context)
    {
        _context = context;
    }
    // CRUD operations (example)
    public void Add(object entity) => _context.Add(entity);
    public void Save() => _context.SaveChanges();
}
