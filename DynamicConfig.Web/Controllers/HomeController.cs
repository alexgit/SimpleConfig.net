using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicConfig.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Effortless configuration for your .NET app";

            ConfigExample();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }

        public ActionResult ShowConfig()
        {
            Easy();

            ClientAccess accessSettings = Config.ClientAccess.Value;

            ViewBag.ClientAccessSettings = accessSettings;

            double breakingBalanceFront = Config.Car.Braking.Balance.front.Value;
            double breakingBalanceRear = Config.Car.Braking.Balance.rear.Value;
            string breakingPressureLevel = Config.Car.Braking.Pressure.level.Value;

            ViewBag.BreakingBalanceFront = breakingBalanceFront;
            ViewBag.BreakingBalanceRear = breakingBalanceRear;
            
            return View();
        }

        private void Easy() 
        {                       
            ViewBag.config = Config;            
        }

        private void ConfigExample() 
        {
            ViewBag.example = @"
            <configSections>                
                <section name=""mySection"" type=""DynamicConfig.DynamicConfigSectionHandler"" />
                ...
            </configSections>
            ...

            <mySection>
                <car make=""BMW"" team=""Sauber"">
                    <raceType name=""F1 Gran Prix"" />

                    <brakes>
                        <balance front=""0.5"" rear=""0.5"" />
                        <pressure level=""high"" />
                        <brakeSize value=""Standard"" />
                    </brakes>

                    <balance frontAntiRollBar=""3"" rearAntiRollbBar=""9"" />

                    <suspension>
                        <rideHeight front=""2"" rear=""2"" />
                        <springStiffness front=""5"" rear=""7"" />
                    </suspension>                  
                    ...
                    
                </car>
            </mySection>";
        }
    }
}
