// System
using Hnatob.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// Embed

// Project
using Hnatob.WebUI.Models;
using Microsoft.AspNet.Identity;

namespace Hnatob.WebUI.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext context;

        public ProfileController(ApplicationDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("No arguments");
            }
            this.context = context;

        }

        public ActionResult UsersProfile()
        {
            //string id = "38f3e55a-f99f-4184-b737-b441bb9f8833";
            var id = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(u => u.Id == id);
            var empl = context.Employee.FirstOrDefault(u => u.UserId == id);

            if (user == null) return RedirectToAction("", "");

            ProfileViewModels model = new ProfileViewModels
            {
                UserId = id,
                Name = empl?.Name ?? "",
                Patronymic = empl?.Patronymic ?? "",
                Surname = empl?.Surname ?? "",
                Birthday = empl?.Birthday ?? DateTime.Now,

                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Nickname = empl?.Nickname ?? "",

                Introduction = empl?.Introduction ?? "",
                Description = empl?.Description ?? "",

                TwoFactorEnabled = user.TwoFactorEnabled,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UsersProfile(ProfileViewModels model)
        {
            var id = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(u => u.Id == id);
            var empl = context.Employee.FirstOrDefault(u => u.UserId == id);

            user.PhoneNumber = model.PhoneNumber;

            if (user.EmailConfirmed && empl != null)
            {
                empl.Birthday = model.Birthday;
                empl.Name = model.Name;
                empl.Patronymic = model.Patronymic;
                empl.Surname = model.Surname;
                empl.Nickname = model.Nickname;
                empl.Introduction = model.Introduction;
                empl.Description = model.Description;
            }

            context.SaveChanges();

            return RedirectToAction("", "");
        }

    }
}
