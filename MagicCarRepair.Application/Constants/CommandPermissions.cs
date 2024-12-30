public static class CommandPermissions
{
    private static readonly Dictionary<Type, string> _permissions = new()
    {
        // User Commands
        // { typeof(UpdateUserCommand), "UpdateUser" },
    
    };

    public static string GetPermissionName(Type commandType)
    {
        if (!_permissions.ContainsKey(commandType))
            throw new ArgumentException($"No permission defined for command type: {commandType.Name}");
            
        return _permissions[commandType];
    }
} 