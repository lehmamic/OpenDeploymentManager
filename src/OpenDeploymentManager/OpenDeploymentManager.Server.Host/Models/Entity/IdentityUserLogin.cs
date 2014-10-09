namespace OpenDeploymentManager.Server.Host.Models.Entity
{
    public sealed class IdentityUserLogin<TKey>
    {
        public string Id { get; set; }

        public TKey UserId { get; set; }

        public string Provider { get; set; }

        public string ProviderKey { get; set; }
    }
}