namespace OpenDeploymentManager.Server.Contracts
{
    public interface IPagingQuery
    {
        int Top { get; set; }

        int Skip { get; set; }
    }
}