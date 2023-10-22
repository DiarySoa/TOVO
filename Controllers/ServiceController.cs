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

    public IActionResult renvoie(String nom){
        // String data = nom;
        return RedirectToAction("avec_poste","Service_Poste", new{nom});
    }

}