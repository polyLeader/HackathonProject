using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Injection;
using BusinessLogic.Domain;

namespace PolyTeam.Hackaton.NInjection
{
    public class MainModule:NinjectModule
    {
        public override void Load()
        {
            this.Bind<DatabaseContext>().ToMethod(x => new DatabaseContext()).InRequestScope();
        }
    }
}