using Nancy;

namespace PetInfo.Web.modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/", true] = async (x, ct) =>
            {
                return View["index.html"];
            };
        }
    }
}