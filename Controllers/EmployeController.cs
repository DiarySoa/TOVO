using Microsoft.AspNetCore.Mvc;
using TOVO.Models;
using System;
using System.IO;
using System.Net.Mail;
using System.Net;


namespace TOVO.Controllers;

public class EmployeController : Controller
{
    public IActionResult Index()
    {
        
        return View("accueilemp");
    }

    public IActionResult formulaire(){
        return View("insert_employe");
    }

    public IActionResult insertEmploye(String nom, String prenom, DateTime dtn, String genre)
    {
        Employe emp = new Employe();
        emp.Insert_Employe(nom,prenom,dtn,genre);
        return View("accueilemp");
    }

    public IActionResult formulaire_embauche(){
        Fiche f = new Fiche();
        List<String> poste = f.GetAllPoste();
        return View("formulaire_embauche", poste);
    }

    public IActionResult embauche(String nom, String prenom, String poste,String numero_employe, String salaire)
    {
        Employe emp = new Employe();
        emp.Insert_embauche(emp.id_emp(nom, prenom), poste , numero_employe, Double.Parse(salaire));

        return View("accueilemp");
    }


}
