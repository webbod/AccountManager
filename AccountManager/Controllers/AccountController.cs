using AccountManager.Data;
using AccountManager.Interfaces.Accounts.Repository;
using AccountManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace AccountManager.Controllers
{
    public class AccountController : Controller
    {

        private IAccountInserter _InserterService; 
        private IAccountFinder _FinderService;

        public AccountController(IAccountInserter inserterService, IAccountFinder finderService,  IOptions<SqlDataStoreConfigurationSettings> options)
        {
            _InserterService = inserterService;
            _InserterService.Initalise(options.Value);
            _FinderService = finderService;
            _FinderService.Initalise(options.Value);
        }
        public IActionResult Index()
        { 
            return View(new CredentialsViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CredentialsViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _InserterService.Insert(model);
                    model.HasBeenSaved();

                    return View(model);
                }
            }
            catch (NotSupportedException)
            {
                // reloading the success screen should return a blank form
                model = new CredentialsViewModel();
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

