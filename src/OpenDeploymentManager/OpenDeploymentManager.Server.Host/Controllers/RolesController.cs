using System.Web.Http;
using OpenDeploymentManager.Server.Host.DataAccess;

namespace OpenDeploymentManager.Server.Host.Controllers
{
    [Authorize]
    [RoutePrefix("api/Roles")]
    [SaveChanges]
    public class RolesController : ControllerBase
    {
    }
}
