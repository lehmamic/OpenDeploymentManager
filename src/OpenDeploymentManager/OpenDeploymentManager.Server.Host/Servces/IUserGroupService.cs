using System;
using System.Linq;
using OpenDeploymentManager.Server.Host.Models.Entity;

namespace OpenDeploymentManager.Server.Host.Servces
{
    public interface IUserGroupService
    {
        IQueryable<ApplicationUserGroup> Query();

        ApplicationUserGroup GetById(Guid id);

        void Create(ApplicationUserGroup userGroup);
    }
}
