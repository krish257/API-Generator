[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _service;
    public ProductController(ProductService service)
    {
        _service = service;
    }
    [HttpPost]
    public IActionResult Create() { /* Create logic */ return Ok(); }
}
