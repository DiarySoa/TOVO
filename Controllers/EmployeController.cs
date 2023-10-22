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
        return View("formulaire_embauche");
    }

    public IActionResult embaucher(String nom, String numero_cnaps)
    {
        Employe emp = new Employe();
        emp.Insert_embauche(emp.id_emp(nom), numero_cnaps);

        return View("accueilemp");
    }


}
