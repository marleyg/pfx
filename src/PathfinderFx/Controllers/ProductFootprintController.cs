using Microsoft.AspNetCore.Mvc;
using PathfinderFx.Model;

namespace PathfinderFx.Controllers
{
    [ApiController]
    [Route("footprints")]
    public class ProductFootprintController : ControllerBase
    {

        private readonly ILogger<ProductFootprintController> _logger;

        public ProductFootprintController(ILogger<ProductFootprintController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductFootprints))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ProductFootprints ListFootprints()
        {
            _logger.LogInformation("Getting footprints");
            return ProductFootprints.FromJson(System.IO.File.ReadAllText("Data/pfv2.json"));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductFootprints))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ProductFootprints GetFootprint(string id)
        {
            _logger.LogInformation("Getting footprint, id: {Id}", id);
            return ProductFootprints.FromJson(System.IO.File.ReadAllText("Data/pfv2.json"));
        }
        
    }
}