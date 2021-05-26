using System.IO;

namespace FinancasAPI.Infra.Base
{
    public class CommunicationJson
    {
        public string NameFile { get; set; }

        public string ReadFile(string path)
        {
            return File.ReadAllText(path + "/" + NameFile);
        }
    }
}
