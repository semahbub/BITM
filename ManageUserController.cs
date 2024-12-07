using App18_ClaimBased.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App18_ClaimBased.Controllers
{
    [Authorize]
    public class ManageUserController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public ManageUserController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            ViewBag.userlist = _userManager.Users;
            List<string> claims = ClaimStore.GetAllClaims();

            if (TempData["msg"] != null)
            {
                ViewBag.msg = TempData["msg"].ToString();
            }

            return View(claims);
        }

        
        public async Task<JsonResult> getuserclaims(string email)
        {
            ViewBag.userlist = _userManager.Users;
            List<string> claims = ClaimStore.GetAllClaims();

            if(string.IsNullOrEmpty(email))
            {
                return Json(new { result = "failed", msg = "User Email address could not be null or empty" });
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(new { result = "failed", msg = "Sorry ! Could not find the User." });
            }

            var uclaims =await _userManager.GetClaimsAsync(user);
            if(uclaims.Count <= 0) {
                return Json(new { result = "failed", msg = "Sorry ! No claim is found for this User." });
            }

            var user_claims = uclaims.Select(x => x.Value).ToList();
            return Json(new { result = "ok", msg = "User claim is found for this User.", mydata=user_claims });
        }


        [HttpPost]
        public async Task<ActionResult> SetPermission(string uid, string[] perm)
        {
            if (string.IsNullOrEmpty(uid))
            {
                TempData["msg"] = "User Email address could not be null or empty";
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByEmailAsync(uid);
            if (user == null)
            {
                TempData["msg"] = "Sorry ! Could not find the User.";
                return RedirectToAction("Index");
            }

            var uclaims = await _userManager.GetClaimsAsync(user);
            if (uclaims.Count > 0)
            {
                foreach (var item in uclaims)
                {
                    await _userManager.RemoveClaimAsync(user, item);
                }
            }

            if (perm.Length <= 0) {
                TempData["msg"] = "Sorry ! No permission is checked.";
                return RedirectToAction("Index");
            }

            foreach (var item in perm)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    Claim c = new Claim(item, item);
                    await _userManager.AddClaimAsync(user, c);
                }
            }

            TempData["msg"] = "User permission has been granted Successfully.";
            return RedirectToAction("Index");
        }
    }
}
