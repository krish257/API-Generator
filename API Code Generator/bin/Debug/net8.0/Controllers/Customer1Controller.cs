[ApiController]
[Route("api/[controller]")]
public class Customer1Controller : ControllerBase
{
    private readonly Customer1Service _service;
    public Customer1Controller(Customer1Service service)
    {
        _service = service;
    }
    [HttpPost]
    public IActionResult Create() { /* Create logic */ return Ok(); }
}
