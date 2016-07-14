using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarmicEnergy.Web.App_Start
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            //SetAutofacContainer();

            //Configure AutoMapper
            AutoMapperConfig.RegisterMaps();
        }
    }
}