using System.IO;
using System.Xml.Serialization;

namespace WindowsFormsApp1
{
    public class Config
    {
        public bool IsNullProp()
        {
            return string.IsNullOrEmpty(Num1) && string.IsNullOrEmpty(Num2) && string.IsNullOrEmpty(Operation) && N1 == null;
        }
        public static Config GetConfig()
        {
            Config config = null;
            string filename = Global.Config;
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(Config));
                    config = (Config)xml.Deserialize(fs);
                    fs.Close();
                }
            }
            else config = new Config();

            return config;
        }
        public void Save()
        {
            string filename = Global.Config;
            if (File.Exists(filename)) File.Delete(filename);
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Config));
                xml.Serialize(fs, this);
                fs.Close();
            }
        }
        public double? N1 { get; set; }
        public string Num1 { get; set; }
        public string Num2 { get; set; }
        public string Operation { get; set; }
    }
}
