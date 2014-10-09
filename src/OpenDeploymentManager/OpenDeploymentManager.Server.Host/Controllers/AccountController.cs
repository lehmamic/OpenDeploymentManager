﻿using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Common.Projection;
using OpenDeploymentManager.Server.Contracts;
using OpenDeploymentManager.Server.Host.DataAccess;
using OpenDeploymentManager.Server.Host.Models.Entity;
using OpenDeploymentManager.Server.Host.Servces;

namespace OpenDeploymentManager.Server.Host.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    [SaveChanges]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService.ArgumentNotNull("userService");
        }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public User GetUser()
        {
            ApplicationUser user = this.userService.GetByName(this.User.Identity.Name);

            return user.ProjectedAs<User>();
        }

        // PUT api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        [HttpPut]
        public IHttpActionResult UpdateUser([FromBody]User model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            ApplicationUser user = this.userService.GetByName(this.User.Identity.Name);
            if (user == null)
            {
                return this.BadRequest();
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

        // POST api/Account/Logout
        [Route("Logout")]
        [HttpPost]
        public IHttpActionResult LogOff()
        {
            this.Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return this.Ok();
        }

        // POST api/Account/ChangePassword
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("ChangePassword")]
        [HttpPost]
        public IHttpActionResult ChangePassword(ChangePassword model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            ApplicationUser user = this.userService.GetByName(this.User.Identity.Name);
            if (user == null)
            {
                return this.BadRequest();
            }

            IdentityResult result = this.userService.ChangePassword(user.Id, model.OldPassword, model.NewPassword);
            IHttpActionResult errorResult = this.GetErrorResult(result);
            if (errorResult != null)
            {
                return errorResult;
            }

            return this.Ok();
        }
    }
}
