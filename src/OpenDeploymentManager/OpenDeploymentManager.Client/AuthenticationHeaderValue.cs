namespace OpenDeploymentManager.Client
{
    public class AuthenticationHeaderValue
    {
        private readonly string header;
        private readonly string value;

        public AuthenticationHeaderValue(string header, string value)
        {
            this.header = header;
            this.value = value;
        }

        public string Header
        {
            get
            {
                return this.header;
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }
        }
    }
}