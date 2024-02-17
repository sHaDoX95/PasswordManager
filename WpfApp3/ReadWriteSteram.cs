using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WpfApp2
{
    internal class ReadWriteSteram : IReaderWriter
    {
        public string Reader(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                return sr.ReadToEnd();
            }
        }

        public bool Writer(string data, string fileName)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.Write(data);
                }
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}
