using AutoMapper;
using OpenDeploymentManager.Server.Contracts;
using OpenDeploymentManager.Server.Host.Models.Entity;

namespace OpenDeploymentManager.Server.Host.Models.Mapping
{
    public class ApplicationUserProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<ApplicationUser, User>();
            this.CreateMap<User, ApplicationUser>();

            this.CreateMap<ApplicationUser, CreateUser>();
        }
    }
}
