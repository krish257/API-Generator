public class CustomerService
{
    private readonly CustomerRepository _repository;
    public CustomerService(CustomerRepository repository)
    {
        _repository = repository;
    }
    // Service methods (example)
    public void Add() { /* Add logic */ }
}
