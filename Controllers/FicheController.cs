using Microsoft.AspNetCore.Mvc;
using TOVO.Models;
using System;
using System.IO;
using System.Net.Mail;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;



namespace TOVO.Controllers;

public class FicheController : Controller
{
    public IActionResult Index()
    {
        
        Fiche f = new Fiche();
        List<String> les_emp = f.numero_des_employes_existant();
        return View("fiche_de_l_employe", les_emp);
    }

    public IActionResult fiche_de_paie(String numero_employe)
    {
        Fiche f = new Fiche();
        Embauchers anakiray = f.getemploye_embaucher(numero_employe);
        String anciennete = f.CalculerAnciennete(numero_employe);
        double calcul_cnaps = f.Calcul_cnaps(numero_employe);
        double calcul_ostie = f.Calcul_ostie(numero_employe);
        double base_imposable = anakiray.salaire - (calcul_cnaps + calcul_ostie);
        double irsa_inf_350 = f.Calcul_IRSA_inf_350(numero_employe, base_imposable );
        double irsa_btw_350_400 = f.Calcul_IRSA_btw_350_400(numero_employe, base_imposable );
        double irsa_btw_400_500 = f.Calcul_IRSA_btw_400_500(numero_employe, base_imposable );
        double irsa_btw_500_600 = f.Calcul_IRSA_btw_500_600(numero_employe, base_imposable );
        double irsa_sup_600 = f.Calcul_IRSA_sup_600(numero_employe, base_imposable );

        double IRSA = irsa_inf_350 + irsa_btw_350_400 + irsa_btw_400_500 + irsa_btw_500_600 + irsa_sup_600;
        double retenue = calcul_cnaps+ calcul_ostie + IRSA;

        double salaire_net = anakiray.salaire - (retenue);

        HeureSup hs = new HeureSup();
        double hs18 = hs.Calcul_heure_sup_81(numero_employe);


         var data = new
    {
        Anakiray = anakiray,
        Anciennete = anciennete,
        Calcul_cnaps = calcul_cnaps,
        Calcul_ostie = calcul_ostie,
        IRSA_inf_350 = irsa_inf_350,
        IRSA_btw_350_400 = irsa_btw_350_400,
        IRSA_btw_400_500 = irsa_btw_400_500,
        IRSA_btw_500_600 = irsa_btw_500_600,
        IRSA_sup_600 = irsa_sup_600,
        baseI = base_imposable,
        IRSA = IRSA,
        net = salaire_net,
        retenue = retenue,
        hs18 = hs18


    };

        return View("fiche_de_paie", data);
    }


    public async Task<IActionResult> ExportToPdf()
{
    // Générez le contenu de la page Fiche.cshtml
    string content = "Contenu de la page Fiche.cshtml à générer en PDF.";

    // Créez un document PDF et ajoutez le contenu
    Document doc = new Document();
    MemoryStream ms = new MemoryStream();
    PdfWriter writer = PdfWriter.GetInstance(doc, ms);
    doc.Open();
    doc.Add(new Paragraph(content));
    doc.Close();

    // Renvoyez le fichier PDF en réponse de manière asynchrone
    Response.ContentType = "application/pdf";
    Response.Headers.Add("content-disposition", "attachment;filename=Fiche.pdf");
    await Response.Body.WriteAsync(ms.ToArray());

    return new EmptyResult();
}








}
