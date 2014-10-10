using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData.Extensions;
using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Common.Annotations;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Common.Projection;
using OpenDeploymentManager.Server.Contracts;
using OpenDeploymentManager.Server.Host.DataAccess;
using OpenDeploymentManager.Server.Host.Models.Entity;
using OpenDeploymentManager.Server.Host.Security;
using OpenDeploymentManager.Server.Host.Servces;

namespace OpenDeploymentManager.Server.Host.Controllers
{
    [Authorize]
    [RoutePrefix("api/UserGroups")]
    [SaveChanges]
    public class UserGroupsController : ControllerBase
    {
        private const string UserGroupById = "UserGroupById";

        private readonly IUserGroupService userGroupService;
        private readonly ISecurityService securityService;

        public UserGroupsController([NotNull] IUserGroupService userGroupService, [NotNull] ISecurityService securityService)
        {
            this.userGroupService = userGroupService.ArgumentNotNull("userGroupService");
            this.securityService = securityService.ArgumentNotNull("securityService");
        }

        // GET api/usergroups?$top=10&$skip=10
        [Route("")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public PagingResult<UserGroup> GetUserGroups(ODataQueryOptions<UserGroup, ApplicationUserGroup> options)
        {
            var userGroups = options.ApplyTo<ApplicationUserGroup>(this.userGroupService.Query())
                .ProjectedAsCollection<UserGroup>();

            return new PagingResult<UserGroup>(
                userGroups,
                Request.ODataProperties().TotalCount);
        }

        // GET api/usergroups/5
        [Route("{id}", Name = UserGroupById)]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public UserGroup GetUserGroup(Guid id)
        {
            ApplicationUserGroup userGroup = this.userGroupService.GetById(id);
            if (userGroup == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return userGroup.ProjectedAs<UserGroup>();
        }

        // POST api/usergroups/
        [Route("")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [HttpPost]
        public IHttpActionResult CreateUserGroup([FromBody]UserGroup model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var userGroup = model.ProjectedAs<ApplicationUserGroup>();
            this.userGroupService.Create(userGroup);

            string location = this.Url.Link(UserGroupById, new { id = userGroup.Id });
            var content = userGroup.ProjectedAs<UserGroup>();

            return this.Created(location, content);
        }

        // PUT api/usergroups/5
        [Route("{id}")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [HttpPut]
        public IHttpActionResult UpdateUserGroup(Guid id, [FromBody]UserGroup model)
        {
            ApplicationUserGroup userGroup = this.userGroupService.GetById(id);
            if (userGroup == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            model.ProjectedTo(userGroup);
            this.userGroupService.Update(userGroup);

            return this.Ok();
        }

        // DELETE api/usergroups/5
        [Route("{id}")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [HttpDelete]
        public IHttpActionResult DeleteUserGroup(Guid id)
        {
            ApplicationUserGroup userGroup = this.userGroupService.GetById(id);
            if (userGroup != null)
            {
                this.userGroupService.Delete(userGroup);
            }

            return this.Ok();
        }

        // GET api/usergroups/resources
        [Route("resources")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public IEnumerable<string> GetProtectedResources()
        {
            IEnumerable<GlobalResources> resources = this.securityService.GetGlobalResources();
            return resources.Select(r => r.ToResourceName());
        }

        // GET api/usergroups/resources/5/operations
        [Route("resources/{resource}/operations")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public IEnumerable<string> GetResourceOperations(string resource)
        {
            if (resource.IsValidResource())
            {
                
            }
            GlobalResources globalResource = resource.FromResourceName();

            IEnumerable<ResourceOperations> operationses = this.securityService.GetResourceOperations(globalResource);
            return operationses.Select(r => r.ToOperationName());
        }

        // GET api/usergroups/5/permissions
        [Route("{id}/permissions")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public PermissionSet GetPermissions(Guid id)
        {
            ApplicationUserGroup userGroup = this.userGroupService.GetById(id);
            if (userGroup == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return userGroup.GlobalPermissions.ProjectedAs<PermissionSet>();
        }

        // GET api/usergroups/5/permissions
        [Route("{id}/permissions")]
        [HttpPut]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public IHttpActionResult UpdatePermissions(Guid id, PermissionSet permissionSet)
        {
            ApplicationUserGroup userGroup = this.userGroupService.GetById(id);
            if (userGroup == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            userGroup.GlobalPermissions = permissionSet.ProjectedAs<PermissionMatrix>();
            this.userGroupService.Update(userGroup);

            return this.Ok();
        }
    }
}
