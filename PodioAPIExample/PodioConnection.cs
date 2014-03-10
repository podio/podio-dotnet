using PodioAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PodioAPIExample
{
    public class PodioConnection
    {

        public static Podio GetCilent()
        {
            string clientId = "podiocms";
            string clientSecret = "qeAmDY2oDAzI8pmNsY1jbg8c9v0ZQYoeRqTnQ9z2u6CigEPIJhlskDZ558MT2Hrp";
            var podio = new Podio(clientId, clientSecret);
            return podio; 
        }
    }
}