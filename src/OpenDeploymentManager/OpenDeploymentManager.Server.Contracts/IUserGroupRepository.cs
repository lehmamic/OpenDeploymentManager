using System;
using System.Collections.Generic;
using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Server.Contracts
{
    [HttpRequestUriPrefix("api/UserGroups")]
    public interface IUserGroupRepository
    {
        [HttpRequestUri("")]
        PagingResult<UserGroup> Query(PagingQuery<UserGroup> query);

        [HttpRequestUri("{id}")]
        UserGroup GetById(Guid id);

        [HttpRequestUri("")]
        UserGroup Create([HttpBodyParameter] UserGroup user);

        [HttpRequestUri("{id}")]
        void Update(Guid id, [HttpBodyParameter] UserGroup user);

        [HttpRequestUri("{id}")]
        void Delete(Guid id);

        [HttpRequestUri("resources")]
        IEnumerable<string> GetResources();

        [HttpRequestUri("resources/{resource}/operations")]
        IEnumerable<string> GetResourceOperations(string resource);
    }
}
