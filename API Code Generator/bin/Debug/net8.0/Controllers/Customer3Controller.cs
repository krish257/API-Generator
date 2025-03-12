[ApiController]
[Route("api/[controller]")]
public class Customer3Controller : ControllerBase
{
    private readonly Customer3Service _service;
    public Customer3Controller(Customer3Service service)
    {
        _service = service;
    }
    [HttpPost]
    public IActionResult Create() { /* Create logic */ return Ok(); }
}
