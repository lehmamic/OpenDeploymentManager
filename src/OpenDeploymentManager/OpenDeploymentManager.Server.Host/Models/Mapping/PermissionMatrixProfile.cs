using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OpenDeploymentManager.Common.Reflection;
using OpenDeploymentManager.Server.Contracts;
using OpenDeploymentManager.Server.Host.Models.Entity;
using OpenDeploymentManager.Server.Host.Security;

namespace OpenDeploymentManager.Server.Host.Models.Mapping
{
    public class PermissionSetProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<Collection<ResourcePermissionEntry>, PermissionSet>()
                .ConstructUsing(ConvertTo);
        }

        private static PermissionSet ConvertTo(Collection<ResourcePermissionEntry> permissions)
        {
            var permissionSet = new PermissionSet
                                    {
                                        ResourceTypes = permissions.ToDictionary(k => k.Resource, ExtractAllowedOperations)
                                    };

            return permissionSet;
        }

        private static Permissions ExtractAllowedOperations(ResourcePermissionEntry entry)
        {
            var resourcePermission = new Permissions();

            ResourceOperations permitted = entry != null ? entry.PermittedOperations : ResourceOperations.None;
            foreach (ResourceOperations operation in GetAvailableOperations().Where(o => permitted.HasFlag(o)))
            {
                resourcePermission.AllowedOperations.Add(operation.ToOperationName());
            }

            return resourcePermission;
        }

        private static IEnumerable<ResourceOperations> GetAvailableOperations()
        {
            return EnumHelper.GetValues<ResourceOperations>().Where(o => o != ResourceOperations.None && o != ResourceOperations.All);
        }
    }
}
