
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokus
{
    class Loading
    {
        

        public List<char> sex = new List<char>();
        public List<double> weight = new List<double>();
        public List<double> height = new List<double>();
        public List<double> age = new List<double>();
        public Loading()
        {
            loadInfo();
        }
        private void loadInfo()
        {
            //id,sex,weight.kg,height.cm,age.yr,group.idx,sample,valid

            try
            {
                StreamReader infoReader = new StreamReader(@"C:\Users\Káťa\Desktop\Diplomka\300meshes-old\specs.csv");
                string line= infoReader.ReadLine();
                while ((line = infoReader.ReadLine()) != null)
                {
                    string[] tokens = line.Split(',');

                    sex.Add(char.Parse(tokens[1]));
                    weight.Add(double.Parse(tokens[2], CultureInfo.InvariantCulture));
                    height.Add(double.Parse(tokens[3], CultureInfo.InvariantCulture));
                    age.Add(double.Parse(tokens[4], CultureInfo.InvariantCulture));


                }
                infoReader.Close();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
