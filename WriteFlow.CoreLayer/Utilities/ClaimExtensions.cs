using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WriteFlow.CoreLayer.Utilities
{
    public static class ClaimExtensions
    {
        public static string GetClaimValue(this ClaimsPrincipal user, string claimType) => user.FindFirst(claimType)?.Value ?? string.Empty;
    }
}
