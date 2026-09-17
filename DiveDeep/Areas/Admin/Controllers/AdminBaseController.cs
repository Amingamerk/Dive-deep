using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberLangeland.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public abstract class AdminBaseController : Controller
    {


    }
}
