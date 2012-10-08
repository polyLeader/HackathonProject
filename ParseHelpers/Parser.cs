using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        public struct Deputy
        {
            public string FirstName;
            public string LastName;
            public string SecondName;
            public string Party;
        }

        /// <summary>
        /// Get all Donetsk's streets
        /// </summary>
        /// <param name="inputFileName">When null - request to server for streets list, otherwise locale file name</param>
        /// <returns>List of structures 'Street'</returns>
        public static List<Street> GetStreets(string inputFileName)
        {
            var xmlDoc = new XmlDocument();
            WebResponse webResponse = null;
            Stream stream = null;

            if (inputFileName == null)
            {

                var reguestGET = WebRequest.Create("https://dl.dropbox.com/u/33987496/HackatonProject/streets.xml");

                reguestGET.Proxy = null;

                webResponse = reguestGET.GetResponse();

                stream = webResponse.GetResponseStream();

                if (stream == null)
                {
                    throw new Exception("Can't get response from server.");
                }

                xmlDoc.Load(stream);
            }
            else
            {
                xmlDoc.Load(inputFileName);
            }

            var xmlNodeList = xmlDoc.GetElementsByTagName("street");

            var allStreets = new List<Street>();

            foreach (XmlElement element in xmlNodeList)
            {
                allStreets.Add(new Street
                { 
                    Lang = element.GetAttribute("lang"), 
                    Name = element.InnerText
                });
            }

            if (webResponse != null) webResponse.Close();
            if (stream != null) stream.Close();

            return allStreets;
        }

        /// <summary>
        /// Get all Donetsk's deputies
        /// </summary>
        /// <param name="inputFileName">When null - request to server for deputies list, otherwise locale file name</param>
        /// <returns>List of structures 'Deputy'</returns>
        public static List<Deputy> GetDeputies(string inputFileName)
        {
            StreamReader streamReader;
            WebResponse webResponse = null;
            Stream stream = null;

            if (inputFileName == null)
            {
                var reguestGET = WebRequest.Create("https://dl.dropbox.com/u/33987496/HackatonProject/deputies.list");

                reguestGET.Proxy = null;

                webResponse = reguestGET.GetResponse();

                stream = webResponse.GetResponseStream();

                if (stream == null)
                {
                    throw new Exception("Can't get response from server.");
                }

                streamReader = new StreamReader(stream, Encoding.UTF8);
            }
            else
            {
                streamReader = new StreamReader(inputFileName, Encoding.UTF8);
            }

            var deputies = new List<Deputy>();
            var deputy = new Deputy();

            while (true)
            {
                var currentLine = streamReader.ReadLine();

                if (currentLine == null) break;

                var tmp = currentLine.Split(',');

                // delete one space char
                deputy.Party = tmp[1].Remove(0, 1);

                var fullName = tmp[0].Split(' ');

                deputy.FirstName = fullName[1];
                deputy.LastName = fullName[0];
                deputy.SecondName = fullName[2];

                deputies.Add(deputy);
            }

            if (webResponse != null) webResponse.Close();
            if (stream != null) stream.Close();
            streamReader.Close();

            return deputies;
        }
    }
}
