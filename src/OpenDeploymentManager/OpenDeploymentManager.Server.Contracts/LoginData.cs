namespace OpenDeploymentManager.Server.Contracts
{
    public class TokenRequest
    {
        public string grant_type { get; set; }

        public string username { get; set; }

        public string password { get; set; }
    }
}
