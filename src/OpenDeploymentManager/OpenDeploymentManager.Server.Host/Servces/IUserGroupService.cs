using System;
using System.Linq;
using OpenDeploymentManager.Common.Annotations;
using OpenDeploymentManager.Server.Host.Models.Entity;

namespace OpenDeploymentManager.Server.Host.Servces
{
    public interface IUserGroupService
    {
        IQueryable<ApplicationUserGroup> Query();

        ApplicationUserGroup GetById(Guid id);

        void Create([NotNull] ApplicationUserGroup userGroup);

        void Update([NotNull] ApplicationUserGroup userGroup);

        void Delete([NotNull] ApplicationUserGroup userGroup);
    }
}
