using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BsaBrasil.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using BsaBrasil.Interfaces;
using BsaBrasil.Extensions.Alerts;
using BsaBrasil.ViewModel;

namespace BsaBrasil.Controllers
{
    public class HomeController : Controller
    {
        public IStringLocalizer<HomeController> Localizer { get; }
        public IEmailSender EmailSender { get; }

        private static string tempText;

        public HomeController(IStringLocalizer<HomeController> localizer,
                              IEmailSender emailSender)
        {
            Localizer = localizer;
            EmailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home(EmailViewModel model)
        {
            if (!String.IsNullOrWhiteSpace(tempText))
            {
                if (tempText.Contains("erro"))
                {
                    return View(model).WithWarning("Erro", tempText);
                }
                else
                {
                    return View(model).WithSuccess("OK!", tempText);
                }
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Send([FromForm] string email, string message, string subject, string name)
        {
            try
            {
                await EmailSender.SendEmailAsync(email, subject, message, name);
                tempText = Localizer["Sua mensagem foi enviada. Obrigado!"];
                return RedirectToAction("Home");
            }
            catch (Exception ex)
            {
                tempText = $"Um erro foi encontrado - {ex.Message}";
                return RedirectToAction("Home");
            }
        }
    }
}
