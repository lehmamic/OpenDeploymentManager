using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Extensions;
using System.Web.Http.OData.Query;
using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Common.Projection;
using OpenDeploymentManager.Server.Contracts;
using OpenDeploymentManager.Server.Host.Models.Entity;
using OpenDeploymentManager.Server.Host.Properties;
using OpenDeploymentManager.Server.Host.Servces;
using Raven.Client;

namespace OpenDeploymentManager.Server.Host.Controllers
{
    [Authorize]
    [RoutePrefix("api/Users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService.ArgumentNotNull("userService");
        }

        // GET api/users
        [Route("")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an async method.")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public PageResult<User> GetUsers(ODataQueryOptions<User> options)
        {
            var users = options.ApplyTo(this.userService.Query())
                .As<ApplicationUser>()
                .ProjectedAsCollection<User>();

            return new PageResult<User>(
                users,
                Request.ODataProperties().NextLink,
                Request.ODataProperties().TotalCount);
        }

        // GET api/users/5
        [Route("{id}", Name = "GetUserById")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public User GetUser(string id)
        {
            ApplicationUser user = this.userService.GetById(id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return user.ProjectedAs<User>();
        }

        // POST api/users/
        [Route("")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [HttpPost]
        public IHttpActionResult CreateUser([FromBody]CreateUser model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            ApplicationUser existingUser = this.userService.GetById(model.UserName);
            if (existingUser != null)
            {
                this.ModelState.AddModelError(string.Empty, "A user with this user name already exist.");
                return this.BadRequest(this.ModelState);
            }

            var user = model.ProjectedAs<ApplicationUser>();

            IdentityResult result = this.userService.Create(user, model.Password);
            IHttpActionResult errorResult = this.GetErrorResult(result);
            if (errorResult != null)
            {
                return errorResult;
            }

            string location = this.Url.Link("GetUserById", new { id = user.UserName });
            var content = user.ProjectedAs<User>();

            return this.Created(location, content);
        }

        // PUT api/users/5
        [Route("{id}")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [HttpPut]
        public IHttpActionResult UpdateUser(string id, [FromBody]User model)
        {
            ApplicationUser user = this.userService.GetById(id);
            if (user == null)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            model.ProjectedTo(user);

            IdentityResult result = this.userService.Update(user);
            IHttpActionResult errorResult = this.GetErrorResult(result);
            if (errorResult != null)
            {
                return errorResult;
            }

            return this.Ok();
        }

        // PUT api/users/5/SetPassword
        [Route("{id}/SetPassword")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [HttpPut]
        public IHttpActionResult SetPassword(string id, [FromBody]SetPassword model)
        {
            ApplicationUser user = this.userService.GetById(id);
            if (user == null)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            model.ProjectedTo(user);

            IdentityResult result = this.userService.SetPassword(user, model.NewPassword);
            IHttpActionResult errorResult = this.GetErrorResult(result);
            if (errorResult != null)
            {
                return errorResult;
            }

            return this.Ok();
        }

        // DELETE api/users/5
        [Route("{id}")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [HttpDelete]
        public IHttpActionResult DeleteUser(string id)
        {
            if (string.Equals(id, this.User.Identity.Name, StringComparison.OrdinalIgnoreCase))
            {
                this.ModelState.AddModelError(string.Empty, Resources.UsersController_CanNotDeleteCurrentUser);
                return this.BadRequest(this.ModelState);
            }

            ApplicationUser user = this.userService.GetById(id);
            if (user != null)
            {
                IdentityResult result = this.userService.Delete(user);
                IHttpActionResult errorResult = this.GetErrorResult(result);
                if (errorResult != null)
                {
                    return errorResult;
                }
            }

            return this.Ok();
        }
    }
}