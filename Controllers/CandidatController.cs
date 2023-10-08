using Microsoft.AspNetCore.Mvc;
using TOVO.Models;
using System;
using System.IO;

namespace TOVO.Controllers;

public class CandidatController : Controller
{
    public IActionResult Index(String id_annonce)
    {
        ViewBag.Id_annonce = id_annonce;
        return View("Register");
    }

    public IActionResult insertCandidat(String nom, String prenom, DateTime dtn, String email, String sexe, String tel, String situation, String adresse, String region, String province)
    {
        Candidat ca = new Candidat();
        ca.Insert_Candidat(nom,prenom,dtn,email,sexe,tel,situation,adresse,region,province);
        int data = ca.id_last_register();
        return View("Import",data);
    }
    public IActionResult insertCandidat_diplome(String niveau, IFormFile diplome, IFormFile cv, IFormFile lm, int experience)
    {
        Candidat ca = new Candidat();
        ca.Insert_Candidat_diplome(niveau, diplome.FileName, cv.FileName, lm.FileName,experience);

        if (diplome != null && diplome.Length > 0)
        {
            try
            {
                string fileName = Path.GetFileName(diplome.FileName);
                string filePath = Path.Combine("~/Uploads", fileName);

                // Assurez-vous que le dossier de destination existe, sinon créez-le
                Directory.CreateDirectory("~/Uploads");

                // Sauvegardez le fichier sur le serveur
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    diplome.CopyTo(stream);
                }

                // Le fichier a été téléchargé avec succès
                return RedirectToAction("Import");
            }
            catch (Exception ex)
            {
                // Gérer les erreurs en fonction de vos besoins
                Console.WriteLine("Erreur lors de l'upload du fichier : " + ex.Message);
                return View("Erreur");
            }
        }
        else
        {
            // Aucun fichier sélectionné
            return View("Erreur");
        }

        return View();
    }
}
