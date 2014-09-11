using System;

namespace OpenDeploymentManager.Server.Contracts
{
    public interface IResourceService<T> where T : ResourceBase
    {
        PagingResult<T> Query();

        T GetById(Guid id);

        void Create(T item);

        void Update(T item);

        void Delete(T item);
    }
}