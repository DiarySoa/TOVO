using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using TOVO.Models;
using System;
using System.IO;
using System.Net.Mail;
using System.Net;


namespace TOVO.Controllers;

public class CandidatController : Controller
{
    public IActionResult Index()
    {
        Candidat c = new Candidat();
        c.service_poste = new Service_Poste();
        List<Service_Poste> data = c.service_poste.getAllServicePoste();

        // return View("liste2");
        return View("accueil", data);
    }

    public IActionResult inscription(String id_annonce)
    {
        ViewBag.Id_annonce = id_annonce;
        return View("Register");
    }

    public IActionResult insertCandidat(String nom, String prenom, DateTime dtn, String email, String sexe, String tel, String situation, String adresse, String region, String province, String n)
    {
        Candidat ca = new Candidat();
        ca.Insert_Candidat(nom,prenom,dtn,email,sexe,tel,situation,adresse,region,province);
        int data = ca.id_last_register();
        ViewBag.Data = data;
        ViewBag.N = n;
        return View("Import");
    }
    public IActionResult insertCandidat_diplome(String niveau, IFormFile diplome, IFormFile cv, IFormFile lm, int experience,String n)
    {
        Candidat ca = new Candidat();
        ca.Insert_Candidat_diplome(niveau, diplome.FileName, cv.FileName, lm.FileName,experience);
        int id_last = ca.lastId();
        ca.insertcand_poste(id_last,int.Parse(n));

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
                return RedirectToAction("Index");
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

    public IActionResult liste_candidat(int id_annonce)
    {
        Candidat c = new Candidat();
        ViewData.Add("Id_annonce", id_annonce);
        List<Candidat> liste = c.getAllCandidatSelectionne(id_annonce);
        Console.WriteLine(liste.Count());
        return View("liste_candidat", liste);
    }

    public IActionResult mail_envoyer(String nom_c, String em)
    {
        String data = nom_c;
        try
        {
            string smtpServer = "mail.stack-x.mg";
            int smtpPort = 465;
            string smtpUsername = "rajaoharisaona@stack-x.mg";
            string smtpPassword = "rajaoharisaona";

        // Créez un client SMTP
        SmtpClient client = new SmtpClient(smtpServer, smtpPort)
        {
            Credentials = new NetworkCredential(smtpUsername, smtpPassword),
            EnableSsl = true, // Utilisez SSL si votre serveur SMTP le prend en charge
        };

            // Créez le message e-mail
            MailMessage message = new MailMessage()
            {
                From = new MailAddress(smtpUsername),
                Subject = "Sujet de l'e-mail",
                Body = "Contenu de l'e-mail",
                IsBodyHtml = true, // Vous pouvez utiliser du HTML dans le corps de l'e-mail
            };

            message.To.Add("manjakarasolomanana@gmail.com"); // Ajoutez l'adresse e-mail du destinataire

            // Envoyez l'e-mail
            client.Send(message);

            // Traitement réussi, redirigez l'utilisateur ou affichez un message de confirmation
            return View("envoie",data);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur lors de l'envoi de l'e-mail : " + ex.ToString());
            return View("envoie", data);
            // En cas d'erreur, gérez l'exception ici (par exemple, enregistrer dans un journal)
            // Affichez une vue d'erreur ou effectuez d'autres actions appropriées
        }
    }
}
