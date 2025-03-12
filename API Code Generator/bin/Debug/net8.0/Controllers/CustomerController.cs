[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _service;
    public CustomerController(CustomerService service)
    {
        _service = service;
    }
    [HttpPost]
    public IActionResult Create() { /* Create logic */ return Ok(); }
}
