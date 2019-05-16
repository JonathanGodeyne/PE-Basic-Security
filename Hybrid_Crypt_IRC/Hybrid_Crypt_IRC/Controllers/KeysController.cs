using System;
using System.IO;

using Microsoft.AspNetCore.Mvc;
using Hybrid_Crypt_IRC.Data;
using Microsoft.AspNetCore.Identity;
using Hybrid_Crypt_IRC.Models.ViewModels;
using Hybrid_Crypt_IRC.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Crypt_Lib;
using Microsoft.AspNetCore.Http;

using System.Text;


namespace Hybrid_Crypt_IRC.Controllers
{
    [Authorize]
    public class KeysController : Controller
    {
        private readonly UserManager<ChatCryptUser> _userManager;
        private readonly ChatCryptContext _chatCryptDbContext;
        public KeysController(ChatCryptContext chatCryptDbContext, UserManager<ChatCryptUser> UserManager)
        {
            this._chatCryptDbContext = chatCryptDbContext;
            this._userManager = UserManager;
        }

        public IActionResult Index()
        {
            PublicKeyViewModel publicKeyViewModel = new PublicKeyViewModel();
            UserInfo info = getUserInfo();
            if (info == null)
            {

                publicKeyViewModel.PublicKey = "No key found";
            }
            else
            {
                publicKeyViewModel.PublicKey = getUserInfo().PublicKey;

            }
            return View(publicKeyViewModel);
        }

        [HttpPost]
        public IActionResult GenerateKeys()
        {


            PublicKeyViewModel publicKeyViewModel = new PublicKeyViewModel();
            RsaUtil rsaUtil = new RsaUtil();
            UserInfo info = getUserInfo();
            HttpContext.Session.SetString("privateKey", rsaUtil.getPrivatekeyString());


            if (info == null)
            {
                var newUserInfo = new UserInfo()
                {
                    UserId = _userManager.GetUserId(User),
                    PublicKey = rsaUtil.getPublicKeyString()

                };


                _chatCryptDbContext.UserInfo.Add(newUserInfo);
            }
            else
            {

                getUserInfo().PublicKey = rsaUtil.getPublicKeyString();
                _chatCryptDbContext.UserInfo.Update(getUserInfo());

            }
            _chatCryptDbContext.SaveChanges();
            UnicodeEncoding encoding = new UnicodeEncoding();
            var privKeyInBytes = encoding.GetBytes(rsaUtil.getPrivatekeyString());


            return File(privKeyInBytes, System.Net.Mime.MediaTypeNames.Text.Plain, User.Identity.Name.Split('@')[0] + "_privateKey.txt");
        }


        [HttpPost]
        public async Task<IActionResult> UploadPrivateKey(IFormFile uploadedPrivKey)
        {
            if (uploadedPrivKey == null || uploadedPrivKey.Length == 0)
                return Content("The file in not selected");
            string resultPrivKey = String.Empty;

            using (var reader = new StreamReader(uploadedPrivKey.OpenReadStream()))
            {   
                resultPrivKey = await reader.ReadToEndAsync();
            }

            HttpContext.Session.SetString("privateKey", resultPrivKey);
            return View(new PrivateKeyViewModel{PrivateKey = resultPrivKey});


        }

        public UserInfo getUserInfo()
        {
            var userId = _userManager.GetUserId(User);
            return _chatCryptDbContext.UserInfo.FirstOrDefault(info => info.UserId == userId);
        }
    }
}