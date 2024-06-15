using Data.Shared.Enums;
using Logic.Shared.Interfaces;
using System.Security.Claims;

namespace Logic.Shared
{
    // TODO buid factory IUserDataClaimsAccessor
    public class UserDataClaimsAccessor : IUserDataClaimsAccessor
    {

        private Dictionary<string, string> _claimsDictionary;

        public Dictionary<string, string> Claims => _claimsDictionary;

        public UserDataClaimsAccessor()
        {
            _claimsDictionary = new Dictionary<string, string>();
            LoadClaimsData();
        }

        /// <summary>
        /// gets the value of claim
        /// </summary>
        /// <param name="key">type of UserIdentityClaims </param>
        public T? GetClaimsValue<T>(string key)
        {
            if (_claimsDictionary.ContainsKey(key))
            {
                var selectedClaimField = _claimsDictionary[key];
                var type = typeof(T);

                if (type == typeof(string))
                {
                    return (T)Convert.ChangeType(selectedClaimField, type);
                }

                if (type == typeof(Guid))
                {
                    return (T)Convert.ChangeType(new Guid(selectedClaimField), type);
                }

                if (type == typeof(DateTime))
                {
                    return (T)Convert.ChangeType(DateTime.Parse(selectedClaimField), type);
                }

                if (type == typeof(UserRoleEnum))
                {
                    return (T)Convert.ChangeType(Enum.Parse(typeof(UserRoleEnum), selectedClaimField), type);
                }
            }

            return default;
        }

        private void LoadClaimsData()
        {
            var claims = ClaimsPrincipal.Current?.Claims.ToList() ?? new List<Claim>();

            foreach (var claim in claims)
            {
                if (!_claimsDictionary.ContainsKey(claim.Type))
                {
                    _claimsDictionary.Add(claim.Type, claim.Value);
                }
            }
        }

    }
}
