using System.Net;
using System.Web.Http;
using System.Web.Http.OData.Extensions;
using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Common.Projection;
using OpenDeploymentManager.Server.Contracts;
using OpenDeploymentManager.Server.Host.DataAccess;
using OpenDeploymentManager.Server.Host.Models.Entity;
using OpenDeploymentManager.Server.Host.Servces;

namespace OpenDeploymentManager.Server.Host.Controllers
{
    [Authorize]
    [RoutePrefix("api/Roles")]
    [SaveChanges]
    public class RolesController : ControllerBase
    {
        private readonly IUserRoleService roleService;

        public RolesController(IUserRoleService roleService)
        {
            this.roleService = roleService.ArgumentNotNull("roleService");
        }

        // GET api/roles?$top=10&$skip=10
        [Route("")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public PagingResult<Role> GetRoles(ODataQueryOptions<Role, ApplicationRole> options)
        {
            var roles = options.ApplyTo<ApplicationRole>(this.roleService.Query())
                .ProjectedAsCollection<Role>();

            return new PagingResult<Role>(
                roles,
                Request.ODataProperties().TotalCount);
        }

        // GET api/roles/5
        [Route("{id}")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public Role GetRole(string id)
        {
            ApplicationRole role = this.roleService.GetById(id);
            if (role == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return role.ProjectedAs<Role>();
        }
    }
}