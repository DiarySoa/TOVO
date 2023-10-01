using Microsoft.AspNetCore.Mvc;
using TOVO.Models;

namespace TOVO.Controllers;

public class Service_PosteController : Controller
{
    // public IActionResult Index()
    // {
    //     Service_Poste service = new Service_Poste();
    //     List<Service> data = service.GetAllService();
    //     return View("liste1", data);
    // }

    // public IActionResult avec_poste()
    // {
    //     Service_Poste service = new Service_Poste();
    //     List<Poste> data = service.GetAllPoste();
    //     return View("liste1", data);
    // }



    public IActionResult Index()
    {
        Service_Poste servicep = new Service_Poste();
        servicep.services = servicep.GetAllService();
        servicep.postes = servicep.GetAllPoste(); // Initialiser avec une liste vide ou null si nécessaire


        return View("liste1", servicep);
    }

    public IActionResult avec_poste()
    {
        Service_Poste servicep = new Service_Poste();
        servicep.services = new List<Service>(); // Initialiser avec une liste vide ou null si nécessaire
        servicep.postes = servicep.GetAllPoste();

        return View("liste1", servicep);
    }

    public IActionResult ValiderListe1(string nom_service, string nom_poste, double heure, string diplome)
    {
        Service_Poste servicep = new Service_Poste();
        servicep.InsertSP(nom_service, nom_poste, heure, diplome);

        return RedirectToAction("liste1");
    }

    // public IActionResult ValiderListe1(string nom_service)
    // {
    //     Service_Poste servicep = new Service_Poste();
    //     int data = servicep.GetIdService(nom_service);

    //     return View("liste2", data);
    // }

    // public IActionResult Lister(string nom_service)
    // {
    //     Service_Poste servicep = new Service_Poste();
    //     int data = servicep.GetIdService(nom_service);
    //     return View("liste2", data);
    // }
}