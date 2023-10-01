using Microsoft.AspNetCore.Mvc;
using TOVO.Models;

namespace TOVO.Controllers;

public class ServiceController : Controller
{
    public IActionResult Index()
    {
        // Service service = new Service();
        // List<Service> data = service.GetAllService();
        // return View("liste", data);

        Service service = new Service();
        List<Poste> data = service.GetAllPoste();
        return View("listeposte", data);
    }

    public IActionResult avec_poste()
    {
        Service service = new Service();
        List<Poste> data = service.GetAllPoste();
        return View("listeposte", data);
    }
}