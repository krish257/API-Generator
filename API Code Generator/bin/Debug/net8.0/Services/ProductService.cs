public class ProductService
{
    private readonly ProductRepository _repository;
    public ProductService(ProductRepository repository)
    {
        _repository = repository;
    }
    // Service methods (example)
    public void Add() { /* Add logic */ }
}
