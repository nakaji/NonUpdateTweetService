using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CoreTweet;

namespace Nuts.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account/Login
        public async Task<ActionResult> Login()
        {
            var domain = Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped);
            var oAuthSession = await OAuth.AuthorizeAsync(
                ConfigurationManager.AppSettings["consumerKey"],
                ConfigurationManager.AppSettings["consumerSecret"],
                $"{domain}/Account/Callback");
            TempData["OAuthSession"] = oAuthSession;

            return Redirect(oAuthSession.AuthorizeUri.OriginalString);
        }

        public async Task<ActionResult> Callback(string oauth_token, string oauth_verifier)
        {
            var oAuthSession = TempData["OAuthSession"] as OAuth.OAuthSession;
            var token = await oAuthSession.GetTokensAsync(oauth_verifier);

            TempData["IsAuthenticated"] = true;

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> LogOff()
        {
            TempData["IsAuthenticated"] = false;

            return RedirectToAction("Index", "Home");
        }
    }
}