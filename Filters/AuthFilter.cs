using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthFilter : IAuthorizeData, IFilterMetadata
{
    public string Policy { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public string Roles { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public string AuthenticationSchemes { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}