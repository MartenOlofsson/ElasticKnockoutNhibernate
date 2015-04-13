using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Nancy;
using Nancy.Responses;
using PetInfo.Web.models;

namespace PetInfo.Web.modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                return View["index.html"];
            };
        }
    }

    public class OwnerModule : NancyModule
    {
        public OwnerModule()
        {
            Get["/owners"] = parameters =>
            {
                var owners = new List<Owner>
                {
                    new Owner
                    {
                        Name = "Cesar Milan",
                        Age = 33
                    }
                };

                return new JsonResponse(owners, new DefaultJsonSerializer());
            };
            Post["/owners/save"] = parameters =>
            {


                return HttpStatusCode.OK;
            };
        }
    }
}