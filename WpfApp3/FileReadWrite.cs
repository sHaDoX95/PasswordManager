using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WpfApp2
{
    internal class FileReadWrite : IReaderWriter
    {
        public string Reader(string fileName)
        {
            if (File.Exists(fileName))
            {
                return File.ReadAllText(fileName);
            }
            else
            {
                return null;
            }
        }

        public bool Writer(string data, string fileName)
        {
            try
            {
                File.WriteAllText(fileName, data);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}
