using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
 

namespace LP_Resume
{
    class AutofacBootstrap
    {
        internal static void Init(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<Repos.ResumeRepo>()
             .As<Repos.IResumeRepo>()
             .InstancePerLifetimeScope();
        }
    }
}
