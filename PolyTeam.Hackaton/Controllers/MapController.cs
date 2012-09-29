using System;
using System.Globalization;
using System.Net;
using System.Web.Mvc;
using System.Xml;

namespace PolyTeam.Hackaton.Controllers
{
    public class MapController : Controller
    {
        public struct Coordinates
        {
            public double Lat;
            public double Lon;
        }

        //
        // GET: /Map/

        public JsonResult GetCoordinates(string street, string number)
        {
            var reguestGET =
                WebRequest.Create("http://nominatim.openstreetmap.org/search?q=" + number + "+" + street +
                                  ",+Донецк, Украина&format=xml&addressdetails=1");

            reguestGET.Proxy = null;

            var webResponse = reguestGET.GetResponse();

            var stream = webResponse.GetResponseStream();

            if (stream == null)
            {
                throw new Exception("Can't get response from server.");
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(stream);

            var tagPlace = (XmlElement) xmlDoc.GetElementsByTagName("place")[0];

            if (tagPlace == null)
            {
                throw new Exception("Can't find this address: " + street + ", " + number + ".");
            }

            var lat = Convert.ToSingle(tagPlace.GetAttribute("lat"), new CultureInfo("en-US"));
            var lon = Convert.ToSingle(tagPlace.GetAttribute("lon"), new CultureInfo("en-US"));

            var coord = new Coordinates {Lat = lat, Lon = lon};

            return Json(coord);
        }

    }
}
