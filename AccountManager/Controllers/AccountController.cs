using AccountManager.Data;
using AccountManager.Interfaces.Accounts.Repository;
using AccountManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace AccountManager.Controllers
{
    public class AccountController : Controller
    {

        private IAccountFinder _FinderService;
        private IAccountUpdater _UpdaterService;

        public AccountController(IAccountFinder finderService, IAccountUpdater updaterService, IOptions<SqlDataStoreConfigurationSettings> options)
        {
            _FinderService = finderService;
            _FinderService.Initalise(options.Value);
            _UpdaterService = updaterService;
            _UpdaterService.Initalise(options.Value);
        }
        public IActionResult Index()
        { 
            return View(new CredentialsViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CredentialsViewModel model)
        {
            if (ModelState.IsValid)
            {
                _UpdaterService.Update(model);
                model.WasSaved = true;
                
                // we don't need this anymore
                model.PlainTextPassword = string.Empty;

                return View(model);
            }
            
            return View(model);
        }

        [HttpPost]
        public JsonResult CheckIfEmailIsNotInUse(string emailAddress)
        {
            bool isAvailable = false;
            try
            {
                _FinderService.Find(emailAddress);
            }
            catch (KeyNotFoundException)
            {
                isAvailable = true;
            }

            return Json(isAvailable);
        }
    }
}

