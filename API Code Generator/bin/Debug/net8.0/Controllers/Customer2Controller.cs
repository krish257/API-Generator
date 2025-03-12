[ApiController]
[Route("api/[controller]")]
public class Customer2Controller : ControllerBase
{
    private readonly Customer2Service _service;
    public Customer2Controller(Customer2Service service)
    {
        _service = service;
    }
    [HttpPost]
    public IActionResult Create() { /* Create logic */ return Ok(); }
}
