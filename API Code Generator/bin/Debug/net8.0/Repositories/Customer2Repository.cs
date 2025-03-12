public class Customer2Repository : ICustomer2Repository
{
    private readonly DbContext _context;
    public Customer2Repository(DbContext context)
    {
        _context = context;
    }
    public void Add(object entity) => _context.Add(entity);
    public void Save() => _context.SaveChanges();
    public Customer2 GetById(int id) => _context.Set<Customer2>().Find(id);
}
