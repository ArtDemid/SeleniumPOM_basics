using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
using System.IO;

namespace SileniumTestIntro_2
{
    class TestSuiteGetter
    {

        private string status;
        private string name;
        private string id;
        private List<object> testDataset;
        private string model;
        private string testUri;

        
        private BaseTest testCase;
        private List<BaseTest> testset = new List<BaseTest>();

        internal BaseTest TestCase { get => testCase; set => testCase = value; }
        internal List<BaseTest> Testset { get => testset; set => testset = value; }
        public string TestUri { get => testUri; set => testUri = value; }

        private void GetTestsFromXml()
        {
            XmlDocument testSuiteDoc = new XmlDocument();
            testSuiteDoc.Load("TestSuite_XML.xml");
            XmlElement root = testSuiteDoc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("test");

            model = root.Attributes["model"].InnerText;

            var cdata = root.FirstChild as XmlCDataSection;
            var uri = cdata.Value;
            testUri = uri;

            //XmlNodeList cdataFinder = root.SelectNodes("testsuite");
            //foreach (var item in cdataFinder)
            //{
            //    if (item is XmlCDataSection)
            //    {
            //        XmlCDataSection cdataSection = item as XmlCDataSection;
            //        var uri = cdataSection.Value;
            //        break;
            //    }
            //}
            //var uri = testSuiteDoc.SelectSingleNode("![CDATA").InnerText;

            foreach (XmlNode node in nodes)
            {
                //status = node["status"].InnerText;
                //name = node["name"].InnerText;
                //id = node["id"].InnerText;

                status = node.Attributes["status"].InnerText;
                name = node.Attributes["name"].InnerText;
                id = node.Attributes["id"].InnerText;

                testDataset = new List<object>();
                XmlNode dataRoot = node.SelectSingleNode("dataset");
                XmlNodeList dataNodes = dataRoot.SelectNodes("data");

                if (status == "Ready for test")
                {
                    foreach (XmlNode dataNode in dataNodes)
                    {
                        object data = dataNode.InnerText;
                        testDataset.Add(data);
                    }

                    //int testId = Convert.ToInt32(id);
                    TestCase = new BaseTest(id, name, testDataset);
                    //tests.Add(testId);
                    Testset.Add(TestCase);
                }
            }
        }

        private void GetTestsFromJson()
        {
            var jsonStr = "";
            using (StreamReader reader = new StreamReader("TestSuite_JSON.json"))
            {
                jsonStr = reader.ReadToEnd();
                var jObj = JObject.Parse(jsonStr);

                model = (string) jObj["testsuite"]["-model"];
                var uri = jObj["testsuite"]["#cdata-section"];

                testUri = (string) uri;

                //testUri = new Uri((string)uri);

                //var testsName = from item in jObj["testsuite"]["test"]
                //                select (string)item["-name"];
                //var testsId = from item in jObj["testsuite"]["test"]
                //              select (string)item["-id"];
                //var testsStatus = from item in jObj["testsuite"]["test"]
                //                  select (string)item["-status"];
                //var dataSet = from item in jObj["testsuite"]["test"]["dataset"]
                //                  select (string)item["data"];

                foreach (var test in jObj["testsuite"]["test"])
                {
                    name = (string) test["-name"];
                    id = (string) test["-id"];
                    status = (string) test["-status"];
                    testDataset = new List<object>();

                    foreach (var data in test["dataset"])
                    {
                        foreach (var item in test["dataset"]["data"])
                        {
                            object value = item;
                            testDataset.Add(value);
                        }
                    }

                    if (status == "Ready for test")
                    {
                        TestCase = new BaseTest(id, name, testDataset);
                        Testset.Add(TestCase);
                    }      
                }
            }
        }

        private static IEnumerable<JToken> NewMethod(JObject jObj)
        {
            return jObj["testsuite"]["test"]["dataset"].SelectMany(i => i["data"]);
        }

        public List<BaseTest> DefineTestSet(string format)
        {
            switch (format)
            {
                case "json":
                    GetTestsFromJson();
                    break;
                case "xml":
                    GetTestsFromXml();
                    break;
                default:
                    break;
            }
            
            return Testset;
        }
    }
}
