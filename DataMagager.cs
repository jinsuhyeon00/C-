using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GUS20S_Project2_1_진수현_201921093
{
    internal class DataMagager
    {
        public static List<Vegetable> VegetableList = new List<Vegetable>(); //채소
        public static List<Seafood> SeafoodList = new List<Seafood>(); //수산물
        public static List<Industrial> IndustrialfoodList = new List<Industrial>(); //공산품

        static DataMagager()
        {
            Load();
        }
        public static void Load()
        {
            try
            {
                    string vegetableOutput = File.ReadAllText(@"./VegetableList.xml"); //채소
                    XElement vegetableXElement = XElement.Parse(vegetableOutput);
                    VegetableList = (from item in vegetableXElement.Descendants("vegetable")
                                     select new Vegetable()
                                     {
                                         Id = item.Element("id").Value,
                                         HousingDate = item.Element("housingDate").Value,
                                         Name = item.Element("name").Value,
                                         NumStock = int.Parse(item.Element("numStock").Value),
                                         Price = int.Parse(item.Element("price").Value),
                                         isSoldOut = item.Element("isSoldOut").Value != "0" ? true : false
                                     }).ToList<Vegetable>();

                string seafoodOutput = File.ReadAllText(@"./SeafoodList.xml"); //수산물
                XElement seafoodXElement = XElement.Parse(seafoodOutput);
                SeafoodList = (from item in seafoodXElement.Descendants("seafood")
                                    select new Seafood()
                                    {
                                        Id = item.Element("id").Value,
                                        Storage = item.Element("storage").Value,
                                        Name = item.Element("name").Value,
                                        NumStock = int.Parse(item.Element("numStock").Value),
                                        Price = int.Parse(item.Element("price").Value),
                                        isSoldOut = item.Element("isSoldOut").Value != "0" ? true : false
                                    }).ToList<Seafood>();

                string industrialOutput = File.ReadAllText(@"./IndustrialfoodList.xml"); //공산품
                XElement industrialXElement = XElement.Parse(industrialOutput);
                IndustrialfoodList = (from item in industrialXElement.Descendants("industrialfood")
                                     select new Industrial()
                               {
                                   Id = item.Element("id").Value,
                                   ExpireDate = item.Element("expireDate").Value,
                                   Name = item.Element("name").Value,
                                   NumStock = int.Parse(item.Element("numStock").Value),
                                   Price = int.Parse(item.Element("price").Value),
                                   isSoldOut = item.Element("isSoldOut").Value != "0" ? true : false
                               }).ToList<Industrial>();
            }
            catch (FileNotFoundException)
            {
                Save();
            }
        }
        public static void Save()
        {
            string vegetableOutput = "";  //채소
            vegetableOutput += "<vegetableList>\n";
            foreach (var item in VegetableList)
            {
                vegetableOutput += "<vegetable>\n";
                vegetableOutput += "<id>" + item.Id + "</id>\n";
                vegetableOutput += "<housingDate>" + item.HousingDate + "</housingDate>\n";
                vegetableOutput += "<name>" + item.Name + "</name>\n";
                vegetableOutput += "<numStock>" + item.NumStock + "</numStock>\n";
                vegetableOutput += "<price>" + item.Price + "</price>\n";
                vegetableOutput += "<isSoldOut>" + (item.isSoldOut ? 1 : 0) + "</isSoldOut>\n";
                vegetableOutput += "</vegetable>\n";
            }
            vegetableOutput += "</vegetableList>\n";

            string seafoodOutput = ""; //수산물
            seafoodOutput += "<seafoodList>\n";
            foreach (var item in SeafoodList)
            {
                seafoodOutput += "<seafood>\n";
                seafoodOutput += "<id>" + item.Id + "</id>\n";
                seafoodOutput += "<storage>" + item.Storage + "</storage>\n";
                seafoodOutput += "<name>" + item.Name + "</name>\n";
                seafoodOutput += "<numStock>" + item.NumStock + "</numStock>\n";
                seafoodOutput += "<price>" + item.Price + "</price>\n";
                seafoodOutput += "<isSoldOut>" + (item.isSoldOut ? 1 : 0) + "</isSoldOut>\n";
                seafoodOutput += "</seafood>\n";
            }
            seafoodOutput += "</seafoodList>\n";

            string industrialOutput = ""; //공산품
            industrialOutput += "<industrialfoodList>\n";
            foreach (var item in IndustrialfoodList)
            {
                industrialOutput += "<industrialfood>\n";
                industrialOutput += "<id>" + item.Id + "</id>\n";
                industrialOutput += "<expireDate>" + item.ExpireDate + "</expireDate>\n";
                industrialOutput += "<name>" + item.Name + "</name>\n";
                industrialOutput += "<numStock>" + item.NumStock + "</numStock>\n";
                industrialOutput += "<price>" + item.Price + "</price>\n";
                industrialOutput += "<isSoldOut>" + (item.isSoldOut ? 1 : 0) + "</isSoldOut>\n";
                industrialOutput += "</industrialfood>\n";
            }
            industrialOutput += "</industrialfoodList>\n";
            
            File.WriteAllText(@"./VegetableList.xml", vegetableOutput);
            File.WriteAllText(@"./SeafoodList.xml", seafoodOutput);
            File.WriteAllText(@"./IndustrialfoodList.xml", industrialOutput);
        }        
    }

}