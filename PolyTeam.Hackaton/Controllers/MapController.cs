using System;
using System.Globalization;
using System.Net;
using System.Web.Mvc;
using System.Xml;
using BusinessLogic.Core;
using BusinessLogic.Domain;

namespace PolyTeam.Hackaton.Controllers
{
    public class MapController : Controller
    {
        public struct Coordinates
        {
            public double Lat;
            public double Lon;
        }

        public struct OsmObject
        {
            public string Road;
            public string HouseNumber;

        }

        private readonly IStreetRepository streetRepository;
        //
        // GET: /Map/

        public MapController(IStreetRepository streetRepository)
        {
            this.streetRepository = streetRepository;
        }
        [HttpGet]
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
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            
            var lat = Convert.ToSingle(tagPlace.GetAttribute("lat"), new CultureInfo("en-US"));
            var lon = Convert.ToSingle(tagPlace.GetAttribute("lon"), new CultureInfo("en-US"));

            var coord = new Coordinates { Lat = lat, Lon = lon };

            return Json(coord, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetObject(float lat, float lon)
        {
            var reguestGET =
                WebRequest.Create("http://nominatim.openstreetmap.org/reverse?format=xml&lat=" +
                lat.ToString(new CultureInfo("en-US")) + "&lon=" + lon.ToString(new CultureInfo("en-US")) +
                "&addressdetails=1&accept-language=ru");

            reguestGET.Proxy = null;

            var webResponse = reguestGET.GetResponse();

            var stream = webResponse.GetResponseStream();

            if (stream == null)
            {
                throw new Exception("Can't get response from server.");
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(stream);

            var tagHouseNumber = (XmlElement)xmlDoc.GetElementsByTagName("house_number")[0];
            var tagRoad = (XmlElement)xmlDoc.GetElementsByTagName("road")[0];

            var osmObject = new OsmObject
            {
                Road = tagRoad == null ? null : tagRoad.InnerText,
                HouseNumber = tagHouseNumber == null ? null : tagHouseNumber.InnerText
            };

            return Json(osmObject);
        }

        [HttpGet]
        public JsonResult GetAllStreets()
        {
            return Json(streetRepository.GetAll(), JsonRequestBehavior.AllowGet);
        }
    }
}
