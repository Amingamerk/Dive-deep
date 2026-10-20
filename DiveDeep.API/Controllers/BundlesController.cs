using DiveDeep.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.API.Controllers
{
    [Route("api/bundles")]
    [ApiController]
    public class BundlesController : ControllerBase
    {
        private readonly IBundleRepository _bundleRepository;

        public BundlesController(IBundleRepository bundleRepository)
        {
            _bundleRepository = bundleRepository;
        }

        // Generelle "værktøjsmetoder" herunder
    }
}
