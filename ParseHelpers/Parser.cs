using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace ParseHelpers
{
    public static class Parser
    {
        public struct Street
        {
            public string Name;
            public string Lang;
        }

        public static List<Street> GetStreets(string inputFileName)
        {
            if (inputFileName == null)
            {
                throw new IOException("Incorrect file name!");
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(inputFileName);

            var xmlNodeList = xmlDoc.GetElementsByTagName("way");

            var currentStreet = new Street();
            var allStreets = new List<Street>();

            foreach (XmlNode node in xmlNodeList)
            {
                var isStreet = false;
                foreach (XmlElement tag in node.ChildNodes)
                {
                    if (tag.Name != "tag") continue;
                    
                    var attribK = tag.GetAttribute("k");
                    var attribV = tag.GetAttribute("v");
                    
                    isStreet = (attribK == "highway" &&
                                    (attribV == "residential" ||
                                     attribV == "tertiary" ||
                                     attribV == "secondary" ||
                                     attribV == "primary")) || isStreet;

                    if (!isStreet) continue;

                    switch (attribK)
                    {
                        case "name":
                        case "name:uk":
                            currentStreet.Lang = "uk";
                            break;
                        case "name:ru":
                            currentStreet.Lang = "ru";
                            break;
                        case "name:en":
                            currentStreet.Lang = "en";
                            break;
                        default:
                            currentStreet.Lang = null;
                            break;
                    }
                    currentStreet.Name = attribV;

                    if (!allStreets.Contains(currentStreet) && currentStreet.Lang != null)
                    {
                        allStreets.Add(currentStreet);
                    }
                }
            }

            for (var i = 0; i < allStreets.Count; i++)
            {
                if (allStreets[i].Lang != "uk") continue;

                var tmp = new Street { Lang = "ru", Name = allStreets[i].Name };
                if (allStreets.Contains(tmp)) allStreets.Remove(allStreets[i]);
            }

            return allStreets;
        }
    }
}
