using System.ComponentModel.Design;
using Microsoft.AspNetCore.Mvc;
using TOVO.Models;

namespace TOVO.Controllers;

public class Service_PosteController : Controller
{
    public IActionResult Index()
    {
        Service_Poste servicep = new Service_Poste();
        servicep.services = servicep.GetAllService();
        // servicep.postes = servicep.GetAllPoste(); // Initialiser avec une liste vide ou null si nécessaire
        return View("liste1", servicep);
    }

    public IActionResult avec_poste(int nom)
    {
        List<Post> data = Post.getByServ(nom);
        ViewData.Add("Nom",nom);
        return View("liste1", data);
    }

    public IActionResult ValiderListe1(int nom_service, int nom_poste, double heure, string diplome, string sexe, string d_debut, string d_fin, string lieu)
    {
        Service_Poste servicep = new Service_Poste();
        servicep.InsertSP(nom_service, nom_poste, heure, diplome, sexe, d_debut, d_fin, lieu);

        return RedirectToAction("detail_annonce");
        // List<Service_Poste> data = servicep.getAllCandidatSelectionne(nom_poste);
        // return View("liste2",data);
    }

    public IActionResult detail_annonce(){
        Service_Poste servicep = new Service_Poste();
        List<Service_Poste> data = servicep.getAllServicePoste();
        return View("liste2",data);
    }

    public IActionResult vers_candidat(string id_annonce)
    {
        return RedirectToAction("inscription", "Candidat", new { id_annonce });
    }
    
    public IActionResult vers_liste_candidat(int id_annonce)
    {
        return RedirectToAction("liste_candidat", "Candidat", new { id_annonce });
    }
}