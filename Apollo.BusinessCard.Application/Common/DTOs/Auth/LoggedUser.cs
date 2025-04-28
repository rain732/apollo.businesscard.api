using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Apollo.BusinessCard.Application.Common.DTOs;

public class LoggedUser
{
    private readonly IHttpContextAccessor _httpContext;

    public LoggedUser(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }


    public Guid Id
    {
        get
        {
            var uuid = _httpContext.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "UserId")?.Value;
            return Guid.Parse(uuid);
        }
    }

    public Guid? OriginalId
    {
        get
        {
            Guid? uuid = Guid.Parse(_httpContext.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "OriginalId")?.Value);
            return uuid;
        }
    }

    public List<string> Roles
    {
        get
        {
            return _httpContext.HttpContext.User.Claims.Where(p => p.Type.Equals(ClaimTypes.Role)).Select(p => p.Value).ToList();
        }
    }
    public List<int> Departments
    {
        get
        {
            return _httpContext.HttpContext.User.Claims.Where(p => p.Type.Equals("Department")).Select(p => int.Parse(p.Value)).ToList();
        }
    }
    public List<int> DepartmentTypes
    {
        get
        {
            return _httpContext.HttpContext.User.Claims.Where(p => p.Type.Equals("DepartmentTypes")).Select(p => int.Parse(p.Value)).ToList();
        }
    }

    public string FullName
    {
        get
        {
            return _httpContext.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "Full Name")?.Value;
        }
    }

    public List<int> CommitteeRoles
    {
        get
        {
            return _httpContext.HttpContext.User.Claims.Where(p => p.Type == "CommitteeRoles").Select(p => int.Parse(p.Value)).ToList();
        }
    }


    public Guid? CommitteeId
    {
        get
        {
            return _httpContext?.HttpContext?.User.Claims.Where(p => p.Type == "CommitteeId").Select(p => Guid.Parse(p.Value)).FirstOrDefault();
        }
    }

    public bool IsDelegation
    {
        get
        {
            var claim = _httpContext?.HttpContext?.User.Claims
                .FirstOrDefault(p => p.Type == "IsDelegation");

            return claim != null && bool.TryParse(claim.Value, out bool isDelegation) && isDelegation;
        }
    }
}
