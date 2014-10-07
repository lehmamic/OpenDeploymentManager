using AutoMapper;
using OpenDeploymentManager.Server.Contracts;
using OpenDeploymentManager.Server.Host.Models.Entity;

namespace OpenDeploymentManager.Server.Host.Models.Mapping
{
    public class ApplicationUserGroupProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<ApplicationUserGroup, UserGroup>();
            this.CreateMap<UserGroup, ApplicationUserGroup>()
                .ForMember(dest => dest.GlobalPermissions, src => src.Ignore());
        }
    }
}