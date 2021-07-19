using Clean.Core.Utils.Crypto;

namespace Clean.Web.Configuration
{
    public class ConnectionStringsConfiguration
    {
        public string CleanDb { get; set; }

        public void Decrypt(string key)
        {
            CleanDb = AES.Decrypt(key, CleanDb);
        }
    }
}
