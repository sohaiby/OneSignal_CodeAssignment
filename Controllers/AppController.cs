using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneSignal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Mvc;

namespace OneSignal.Controllers
{
    public class AppController : Controller
    {
        // GET: App
        [HttpGet]
        public async Task<ActionResult> ViewApps()
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", string.Format("Basic {0}", WebConfigurationManager.AppSettings["SignalAuthKey"]));

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://onesignal.com/api/v1/apps");
                //requestMessage.Headers.Add("Authorization", string.Format("Basic {0}", WebConfigurationManager.AppSettings["SignalAuthKey"]));
                //requestMessage.Content = new StringContent(jsonAsStringContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.GetAsync(requestMessage.RequestUri).Result;

                string content = await response.Content.ReadAsStringAsync();
                List<ViewApps> lstApps = JsonConvert.DeserializeObject<List<ViewApps>>(content);

                return View(lstApps);
            }
            return View("ViewsApps");
        }

        public ActionResult CreateApp()
        {
            return View();
        }

        public ActionResult UpdateApp()
        {
            UpdateAppModel uModel = new UpdateAppModel();
            if (this.RouteData.Values.ContainsKey("id"))
            {
                string param = this.RouteData.Values["id"].ToString();
                uModel.id = param.Split('$')[0];
                uModel.name = param.Split('$')[1];
                return View(uModel);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateRequest(OneSignal.Models.CreateAppModel cModel)
        {
            if (ModelState.IsValid)
            {
                string siteName = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")); ;
                cModel.site_name = siteName;
                cModel.chrome_web_origin = siteName;
                cModel.chrome_web_sub_domain = siteName;
                string jsonAsStringContent = JsonConvert.SerializeObject(cModel);

                HttpClient client = new HttpClient();

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://onesignal.com/api/v1/apps");
                requestMessage.Headers.Add("Authorization", string.Format("Basic {0}", WebConfigurationManager.AppSettings["SignalAuthKey"]));
                requestMessage.Content = new StringContent(jsonAsStringContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.SendAsync(requestMessage).GetAwaiter().GetResult();

                TempData["Success"] = "App created Successfully!";
                return RedirectToAction("CreateApp");

            }
            return View("CreateApp");
        }

        [HttpPost]
        public ActionResult UpdateRequest(OneSignal.Models.UpdateAppModel uModel)
        {
            if (ModelState.IsValid)
            {
                string jsonAsStringContent = JsonConvert.SerializeObject(uModel);

                HttpClient client = new HttpClient();

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, string.Format("https://onesignal.com/api/v1/apps/{0}", uModel.id));
                requestMessage.Headers.Add("Authorization", string.Format("Basic {0}", WebConfigurationManager.AppSettings["SignalAuthKey"]));
                requestMessage.Content = new StringContent(jsonAsStringContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.SendAsync(requestMessage).GetAwaiter().GetResult();

                TempData["Success"] = "App updated Successfully!";
                return RedirectToAction("ViewApps");

            }
            return View("UpdateApp");
        }
    }

}