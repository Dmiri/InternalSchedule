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
using System.Text.RegularExpressions;

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

        [HttpGet]
        public ActionResult UsersProfile()
        {
            var id = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(u => u.Id == id);
            var empl = context.Employee.FirstOrDefault(u => u.UserId == id);

            if (user == null) return RedirectToAction("Login", "Account");

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
            string patternText = @"(^[\w\s-+_.,;:?()]*$)";
            string patternPhone = @"(\+?[0-9]{3}-*[0-9]{2}-*[0-9]{3}-*[0-9]{2}-*[0-9]{2})";
            //string patternEmail = @"(^(\w[-._+\w]*\w@\w[-._\w]*\w\.\w{2,4})$)";

            var id = User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(id)) model.UserId = id;
            var user = context.Users.FirstOrDefault(u => u.Id == id);
            var empl = context.Employee.FirstOrDefault(u => u.UserId == id);
            model.Email = model.Email;
            model.PhoneNumber = Regex.Match(model.PhoneNumber, patternText).Value;
            model.EmailConfirmed = user.EmailConfirmed;
            if (model.Birthday == new DateTime())
                model.Birthday = DateTime.Today.AddYears(-100);

            if (user.EmailConfirmed &&
                empl != null)
            {
                empl.Birthday = model.Birthday;
                empl.Name = model.Name;
                empl.Patronymic = model.Patronymic;
                empl.Surname = model.Surname;
                empl.Nickname = model.Nickname;
                empl.Introduction = model.Introduction;
                empl.Description = model.Description;

                model.TwoFactorEnabled = user.TwoFactorEnabled;
                model.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            }
            verificationOfUserData(model, patternText, patternPhone);
            if ( ModelState.IsValid )
            {
                context.SaveChanges();
                TempData["Message"] = string.Format($"Changes saved.");
            }
            else
            {
                //return RedirectToAction("UsersProfile", model);
                return View(model);
            }
            return RedirectToAction("UsersProfile");
        }


        bool verificationOfUserData(ProfileViewModels model, string patternText, string patternPhone)
        {
            //string patternEmail = @"(^(\w[-._+\w]*\w@\w[-._\w]*\w\.\w{2,4})$)";

            bool result = true;
            if (model.Name != null
                && (!Regex.IsMatch(model.Name, patternText)
                //|| Regex.Matches(model.Name, patternText).Count < 2)
                //|| Regex.Matches(model.Name, patternText).Count > 2
                ))
            {
                ModelState.AddModelError("Name", "Field \"Name\" must include only letter, number and symbols: \"-+_.,;:?()\"");
                result = result && false;
            }

            if (model.Patronymic != null
                && Regex.Matches(model.Patronymic, patternText).Count != 2)
            {
                ModelState.AddModelError("Patronymic", "Field \"Patronymic\" must include only letter, number and symbols: \"-+_.,;:?()\"");
                result = result && false;
            }

            if (model.Surname != null
                && Regex.Matches(model.Surname, patternText).Count != 2)
            {
                ModelState.AddModelError("Surname", "Field \"Surname\" must include only letter, number and symbols: \"-+_.,;:?()\"");
                result = result && false;
            }

            if (model.Nickname != null
                && Regex.Matches(model.Nickname, patternText).Count != 2)
            {
                ModelState.AddModelError("Nickname", "Field \"Nickname\" must include only letter, number and symbols: \"-+_.,;:?()\"");
                result = result && false;
            }

            if (model.Introduction != null
                && Regex.Matches(model.Introduction, patternText).Count != 2)
            {
                ModelState.AddModelError("Introduction", "Field \"Introduction\" must include only letter, number and symbols: \"-+_.,;:?()\"");
                result = result && false;
            }

            if (model.Description != null
                && Regex.Matches(model.Description, patternText).Count != 2)
            {
                ModelState.AddModelError("Description", "Field \"Description\" must include only letter, number and symbols: \"-+_.,;:?()\"");
                result = result && false;
            }

            //if (model.Email != null
            //    && !Regex.IsMatch(model.Email, patternEmail, RegexOptions.IgnoreCase))
            //{
            //    ModelState.AddModelError("Email", "");
            //    result = result && false;
            //}

            if (model.PhoneNumber != null
                && !Regex.IsMatch(model.PhoneNumber, patternPhone, RegexOptions.IgnoreCase))
            {
                ModelState.AddModelError("PhoneNumber", "Not correct phone number");
                result = result && false;
            }

            if (model.UserId != null
                && !Regex.IsMatch(model.UserId, patternPhone, RegexOptions.IgnoreCase))
            {
                ModelState.AddModelError("UserId", "User error!");
                result = result && false;
            }

            if (model.Birthday > DateTime.Today.AddYears(-14)
                || model.Birthday < DateTime.Today.AddYears(-120)
                || model.Birthday != new DateTime())
            {
                ModelState.AddModelError("Birthday", "Not correct birthday");
                result = result && false;
            }


            return result;
        }

    }
}
