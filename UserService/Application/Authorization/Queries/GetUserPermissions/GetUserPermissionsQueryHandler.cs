﻿using Dapper;
using UserService.Application.Contracts;
using UserService.Application.Contracts.Queries;

namespace UserService.Application.Authorization.GetUserPermissions;

internal class GetUserPermissionsQueryHandler : IQueryHandler<GetUserPermissionsQuery, List<UserPermissionDto>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetUserPermissionsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<List<UserPermissionDto>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql = "SELECT " +
                           "[UserPermission].[PermissionCode] AS [Code] " +
                           "FROM [users].[UserPermissions] AS [UserPermission] " +
                           "WHERE [UserPermission].UserId = @UserId";
        var permissions = await connection.QueryAsync<UserPermissionDto>(sql, new { request.UserId });

        return permissions.AsList();
    }
}