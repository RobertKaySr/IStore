namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Product>>> GetProducts(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return Ok(product);
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

    }
}