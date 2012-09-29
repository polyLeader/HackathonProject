using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Injection;
using BusinessLogic.Domain;
using BusinessLogic.Core;

namespace PolyTeam.Hackaton.NInjection
{
    public class MainModule:NinjectModule
    {
        public override void Load()
        {
            this.Bind<DatabaseContext>().ToMethod(x => new DatabaseContext()).InRequestScope();
            this.Bind<ICryptoProvider>().To<CryptoProvider>().InRequestScope();
            this.Bind<IUserProcessor>().To<UserProcessor>().InRequestScope();
            this.Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            this.Bind<IProblemRepository>().To<ProblemRepository>().InRequestScope();
        }
    }
}