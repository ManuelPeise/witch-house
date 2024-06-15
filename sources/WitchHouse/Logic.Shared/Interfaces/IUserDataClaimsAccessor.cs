

namespace Logic.Shared.Interfaces
{
    public interface IUserDataClaimsAccessor
    {
        public Dictionary<string, string> Claims { get; }

        T? GetClaimsValue<T>(string key);
    }
}
