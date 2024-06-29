using api.ModelViews;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    
    // [HttpGet]
    // public ActionResult Index()
    // {
    //     return Redirect("/swagger");
    // }

    [HttpGet]
    public HomeView Index()
    {
        return new HomeView();
    }
}
