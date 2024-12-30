using System.Reflection;
using System.Linq;
using MagicCarRepair.Domain.Entities;

namespace MagicCarRepair.Application.Security.Constants;

public static class PermissionConstants
{
    public static class Roles
    {
        public const string Manage = "roles.manage";
        public const string AssignToUser = "roles.assign";
        public const string ManagePermissions = "roles.manage.permissions";
    }

    public static class Users
    {
        public const string Create = "users.create";
        public const string Update = "users.update";
        public const string Delete = "users.delete";
        public const string View = "users.view";
    }

    // Diğer modüller için de benzer şekilde...
} 