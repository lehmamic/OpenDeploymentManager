using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenDeploymentManager.Server.Host.Models.Entity;

namespace OpenDeploymentManager.Server.Host.Servces
{
    public interface IUserRoleService
    {
        IQueryable<ApplicationRole> Query();

        ApplicationRole GetById(string id);

        ApplicationRole GetByName(string roleName);
    }
}
