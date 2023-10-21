using Microsoft.AspNetCore.Mvc;
using TOVO.Models;
using System;
using System.IO;
using System.Net.Mail;
using System.Net;


namespace TOVO.Controllers;

public class EmpbauchersController : Controller
{

    public IActionResult liste_des_embauchers()
    {
        Embauchers em = new Embauchers();
        List<Embauchers> lem = em.getAllEmbauchers();
        return View("Liste_embauchers");
    }


}
