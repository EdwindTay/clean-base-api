using Clean.Core.Utils.Crypto;

namespace Clean.Application.Configurations
{
    public class FirebaseConfiguration
    {
        public string ProjectId { get; set; }
        public string ApiKey { get; set; }
        public string TokenUri { get; set; }

        public void Decrypt(string key)
        {
            ProjectId = AES.Decrypt(key, ProjectId);
            ApiKey = AES.Decrypt(key, ApiKey);
        }
    }
}
