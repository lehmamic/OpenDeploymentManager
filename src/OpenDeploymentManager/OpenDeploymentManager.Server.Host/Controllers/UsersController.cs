﻿using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData.Extensions;
using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Common.Projection;
using OpenDeploymentManager.Server.Contracts;
using OpenDeploymentManager.Server.Host.DataAccess;
using OpenDeploymentManager.Server.Host.Models.Entity;
using OpenDeploymentManager.Server.Host.Properties;
using OpenDeploymentManager.Server.Host.Security;
using OpenDeploymentManager.Server.Host.Servces;

namespace OpenDeploymentManager.Server.Host.Controllers
{
    [Authorize(Roles = RoleNames.Administrator)]
    [RoutePrefix("api/Users")]
    [SaveChanges]
    public class UsersController : ControllerBase
    {
        private const string UserById = "UserById";

        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService.ArgumentNotNull("userService");
        }

        // GET api/users?$top=10&$skip=10
        [Route("")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public PagingResult<User> GetUsers(ODataQueryOptions<User, ApplicationUser> options)
        {
            var users = options.ApplyTo<ApplicationUser>(this.userService.Query())
                .ProjectedAsCollection<User>();

            return new PagingResult<User>(
                users,
                Request.ODataProperties().TotalCount);
        }

        // GET api/users/5
        [Route("{id}", Name = UserById)]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public User GetUser(Guid id)
        {
            ApplicationUser user = this.userService.GetById(id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return user.ProjectedAs<User>();
        }

        // GET api/users/byname/admin
        [Route("byname/{userName}")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public User GetUser(string userName)
        {
            ApplicationUser user = this.userService.GetByName(userName);
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

            var user = model.ProjectedAs<ApplicationUser>();

            IdentityResult result = this.userService.Create(user, model.Password);
            IHttpActionResult errorResult = this.GetErrorResult(result);
            if (errorResult != null)
            {
                return errorResult;
            }

            string location = this.Url.Link(UserById, new { id = user.Id });
            var content = user.ProjectedAs<User>();

            return this.Created(location, content);
        }

        // PUT api/users/5
        [Route("{id}")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [HttpPut]
        public IHttpActionResult UpdateUser(Guid id, [FromBody]User model)
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
        public IHttpActionResult SetPassword(Guid id, [FromBody]SetPassword model)
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

            IdentityResult result = this.userService.SetPassword(id, model.NewPassword);
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
        public IHttpActionResult DeleteUser(Guid id)
        {
            ApplicationUser user = this.userService.GetById(id);
            if (user != null)
            {
                if (string.Equals(user.UserName, this.User.Identity.Name, StringComparison.OrdinalIgnoreCase))
                {
                    this.ModelState.AddModelError(string.Empty, Resources.UsersController_CanNotDeleteCurrentUser);
                    return this.BadRequest(this.ModelState);
                }

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