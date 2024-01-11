using BLL.Forms;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Cloud_APIDemo.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_userService.GetAll());
        }

        [HttpGet("getById/{id}")]
        public IActionResult Details(int id)
        {

            User? u = _userService.GetById(id);

            if (u != null)
            {
                return View(u);
            }

            return View("NotFound");


        }

        [HttpGet("Create")]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RegisterForm form)
        {

            if (ModelState.IsValid)
            {
                _userService.Create(form);
            }
            return View();
        }

        [HttpGet("Update")]
        public IActionResult Update(int id)
        {
            UpdatePasswordForm form = new UpdatePasswordForm()
            {
                Id = id,
                ConfirmationPassword = "",
                Password = ""
            };
            return View(form);
        }

        [HttpPost]
        public IActionResult Update(UpdatePasswordForm form)
        {
            if (ModelState.IsValid)
            {

                if (_userService.UpdatePassword(form))
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Le Mot de passe actuel ne correspond pas.");

            }

            return View();
        }

        [HttpGet("Delete")]
        public IActionResult Delete(int id)
        {
            if (_userService.Delete(id))
            {
                return RedirectToAction("Index");
            }

            return View("NotFound");

        }
    }
}

