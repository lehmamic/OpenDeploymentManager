using AutoMapper;
using OpenDeploymentManager.Server.Contracts;
using OpenDeploymentManager.Server.Host.Models.Entity;

namespace OpenDeploymentManager.Server.Host.Models.Mapping
{
    public class ApplicationRoleProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<ApplicationRole, Role>();
            this.CreateMap<Role, ApplicationRole>();
        }    }
}
