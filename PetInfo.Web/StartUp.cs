using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Owin;
using Nancy.Owin;

namespace PetInfo.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}