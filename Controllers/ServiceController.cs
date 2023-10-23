using Microsoft.AspNetCore.Mvc;
using TOVO.Models;

namespace TOVO.Controllers;

public class ServiceController : Controller
{
    public IActionResult Index()
    {
        Service service = new Service();
        List<Service> data = service.GetAllService();
        return View("liste", data);
    }

    public IActionResult renvoie(int nom){
        return RedirectToAction("avec_poste","Service_Poste", new{nom});
    }

}