using Microsoft.AspNetCore.Mvc;
using TOVO.Models;

namespace TOVO.Controllers;

public class QuestionController : Controller
{
    public IActionResult Index()
    {
        return View("question");
    }
}