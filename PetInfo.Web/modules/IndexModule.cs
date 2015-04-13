using Nancy;

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
}