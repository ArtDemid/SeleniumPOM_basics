using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SileniumTestIntro_2
{
    class BaseTest
    {
        private string name;
        private string id;
        private List<object> data;

        public string ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public List<object> Data { get => data; set => data = value; }

        public BaseTest(string id, string name, List<object> data)
        {
            this.ID = id;
            this.Name = name;
            this.Data = data;
        }
    }
}
