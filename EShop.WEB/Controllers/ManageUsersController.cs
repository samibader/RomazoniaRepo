using AutoMapper;
using EShop.BLL.DTO;
using EShop.BLL.Services;
using EShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using EShop.WEB.Models;
using Microsoft.AspNet.Identity;
using EShop.BLL.Infrastructure;

namespace EShop.WEB.Controllers
{
    [Authorize(Roles = "AdminRole")]
    public class ManageUsersController : BaseController
    {
        private UserService _userService;

        public ManageUsersController(UserService _userService)
        {
            this._userService = _userService;
        }
        //
        // GET: /ManageUsers/
        public ActionResult Index(String fName, string eml, int PageSize = PAGE_SIZE, int page = 1, Sorts sb = Sorts.CustomerNameUp)
        {
            ViewBag.FullName = User.Identity.GetUserName();
            page = page > 1 ? page : 1;
            PageSize = PageSize > 0 ? PageSize : PAGE_SIZE;
            //var usersVMs = new List<UserDetailsVm>();
            var usersDtos = _userService.Filter(fName, eml, sb);
            Mapper.Initialize(c => c.CreateMap<UserDTO, UserDetailsVm>());
            var usersVMs = Mapper.Map<List<UserDTO>, List<UserDetailsVm>>(usersDtos);
            ManageUsersVM users = new ManageUsersVM
            {
                Users = usersVMs.ToPagedList(page, PageSize),
                filters = new UserManageFilter
                {

                    PageNum = page,
                    PageSize = PageSize,
                    SortBy = sb,
                    Email = eml,
                    FullName = fName

                }
            };
            return View(users);
        }

        public ActionResult Edit(string Id)
        {
            var userDto = _userService.getuserById(Id);
            Mapper.Initialize(c => c.CreateMap<UserDTO, UserDetailsVm>());
            var userVM = Mapper.Map<UserDTO, UserDetailsVm>(userDto);


            return View(userVM);
        }

        [HttpPost]
        public ActionResult EditPersonal(UserDetailsVm user)
        {
            if (ModelState.IsValid)
            {
                var op = _userService.EditPersonalInformations(user.Id, user.FirstName, user.LastName);
                return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });
            }
            else
                return View("Edit",user);
        }

        [HttpPost]
        public async Task<ActionResult> EditLogin(UserDetailsVm user)
        {
            var op = _userService.EditLoginInformations(user.Id, user.Email);

            if (op.Succedeed)
            {
                if (!String.IsNullOrWhiteSpace(user.Password) && !String.IsNullOrWhiteSpace(user.ConfirmPassword))
                {
                    if (user.Password != user.ConfirmPassword)
                        return Json(new { Succedeed = false, message = "كلمة المرور وتأكيدها غير متطابقتان", prop = op.Property });
                    String hashedNewPassword = UserManager.PasswordHasher.HashPassword(user.Password);
                    var result = await UserManager.ChangePassword(UserManager.Users.ToList().Where(c => c.Id == user.Id).FirstOrDefault(), hashedNewPassword);
                    if (result)
                    {
                        return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });

                    }
                    else
                    {
                        return Json(new { Succedeed = false, message = "لم يتم تغيير كلمة المرور" });
                    }
                }
                return Json(new { Succedeed = true, message = "تم تغيير البريد الالكتروني", prop = op.Property });

            }

            return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });
        }

        public ActionResult ChangeAdminPassword()
        {
            AdminPasswordVM vm = new AdminPasswordVM();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ChangeAdminPassword(AdminPasswordVM admin)
        {
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrWhiteSpace(admin.Password) && !String.IsNullOrWhiteSpace(admin.ConfirmPassword))
                {
                    if (admin.Password != admin.ConfirmPassword)
                        return Json(new { Succedeed = false, message = "كلمة المرور وتأكيدها غير متطابقتان" });
                    String hashedNewPassword = UserManager.PasswordHasher.HashPassword(admin.Password);
                    var result = await UserManager.ChangePassword(UserManager.Users.ToList().Where(c => c.Id == User.Identity.GetUserId()).FirstOrDefault(), hashedNewPassword);
                    if (result)
                    {
                        return Json(new { Succedeed = true, message = "تمت عملية التعديل بنجاح" });
                    }
                    else
                    {
                        return Json(new { Succedeed = false, message = "لم يتم تغيير كلمة المرور" });
                    }
                }
                return View(admin);
            }
            return View(admin);
        }
        [HttpPost]
        public ActionResult LockAccount(UserDetailsVm user)
        {

            //var result = await UserManager.SetLockoutEnabledAsync(user.Id, true);
            //if (result.Succeeded)
            //    return Json(new { Succedeed = true, message = "تم قفل الحساب " });

             OperationDetails op =  _userService.LockUser(user.Id, true);

             return Json(new { Succedeed = op.Succedeed, message = op.Message,op.Property});

        }

        [HttpPost]
        public ActionResult UnLockAccount(UserDetailsVm user)
        {

            //var result = await UserManager.SetLockoutEnabledAsync(user.Id, false);

            //if (result.Succeeded)
            //    return Json(new { Succedeed = true, message = "تم إالغاء القفل عن الحساب " });

            //return Json(new { Succedeed = false, message = "لم يتم إلغاء القفل عن الحساب " });

            OperationDetails op = _userService.LockUser(user.Id, false);

            return Json(new { Succedeed = op.Succedeed, message = op.Message, op.Property });

        }

        [HttpPost]
        public async Task<ActionResult> ResendConfirmationEmail(UserDetailsVm user)
        {
            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

            var callbackUrl = Url.Action("ConfirmEmail", "Account",
   new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

            string email = Utils.buildUserConfirmEmail(callbackUrl);

            await UserManager.SendEmailAsync(user.Id, "Confirmation", email);

            return Json(new { Succedeed = true, message = "تمت عملية الارسال بنجاح" });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string Id)
        {
            var user = await UserManager.FindByIdAsync(Id);
            var logins = user.Logins;
            var rolesForUser = await UserManager.GetRolesAsync(Id);

            foreach (var login in logins.ToList())
            {
                await UserManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
            }

            if (rolesForUser.Count() > 0)
            {
                foreach (var item in rolesForUser.ToList())
                {
                    // item should be the name of the role
                    var result = await UserManager.RemoveFromRoleAsync(user.Id, item);
                }
            }
            OperationDetails op = _userService.DeleteUserData(user.Id);
            if (op.Succedeed)
                await UserManager.DeleteAsync(user);
            else
                return Json(new { Succedeed = false, message = "حدث خطأ بعملية الحذف" });
               

            return Json(new { Succedeed = true, message = "تمت عملية الحذف بنجاح" });
        }
	}
}