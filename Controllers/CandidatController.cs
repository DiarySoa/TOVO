using Microsoft.AspNetCore.Mvc;
using TOVO.Models;
using System;
using System.IO;

namespace TOVO.Controllers;

public class CandidatController : Controller
{
    public IActionResult Index()
    {
        return View("Register");
    }

    public IActionResult insertCandidat(String nom, String prenom, DateTime dtn, String email, String sexe, String tel)
    {
        Candidat ca = new Candidat();
        ca.Insert_Candidat(nom,prenom,dtn,email,sexe,tel);
        int data = ca.id_last_register();
        return View("Import",data);
    }

    // public IActionResult insertCandidat_diplome(String niveau, String diplome, String cv, String lm)
    // {
    //     Candidat ca = new Candidat();
    //     ca.Insert_Candidat_diplome(niveau,diplome,cv,lm);

    //     if (diplome != null && cv != null && lm != null)
    //     {
    //         // Assurez-vous que les fichiers ont été correctement sélectionnés
    //         // Ensuite, vous pouvez enregistrer ces fichiers où vous le souhaitez.
    //         // Par exemple, vous pouvez utiliser Server.MapPath pour obtenir un chemin sur le serveur.

    //         string diplomePath = Server.MapPath("~/Uploads/" + diplome.FileName);
    //         string cvPath = Server.MapPath("~/Uploads/" + cv.FileName);
    //         string lmPath = Server.MapPath("~/Uploads/" + lm.FileName);

    //         // Sauvegardez les fichiers sur le serveur
    //         diplome.SaveAs(diplomePath);
    //         cv.SaveAs(cvPath);
    //         lm.SaveAs(lmPath);

    //         // Après avoir sauvegardé les fichiers, vous pouvez effectuer d'autres opérations
    //         // telles que l'ajout des informations dans une base de données.

    //         // Redirigez l'utilisateur vers une page de confirmation ou une autre page.
    //         return RedirectToAction("Confirmation");
    //     }
    //     else
    //     {
    //         // Gérez le cas où les fichiers n'ont pas été correctement sélectionnés
    //         return View("Erreur");
    //     }
    //     return View("Import");
    // }


    public IActionResult insertCandidat_diplome(String niveau, IFormFile diplome, IFormFile cv, IFormFile lm)
    {
        Candidat ca = new Candidat();
        ca.Insert_Candidat_diplome(niveau, diplome.FileName, cv.FileName, lm.FileName);

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
    }
}
