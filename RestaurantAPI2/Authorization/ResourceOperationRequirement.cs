using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public enum ResourceOperation
    {
        Create,
        Read,
        Update,
        Delete,
    }
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperationRequirement(ResourceOperation resourceOpertion)
        {
            ResourceOpertion = resourceOpertion;
        }
        public ResourceOperation ResourceOpertion { get; }
    }
}
