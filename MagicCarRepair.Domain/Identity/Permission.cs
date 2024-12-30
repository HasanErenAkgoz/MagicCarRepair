using Core.Packages.Domain.Common;
using Core.Packages.Domain.Security;

namespace MagicCarRepair.Domain.Identity;

public class Permission : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Group { get; set; }
    public virtual ICollection<RolePermission> RolePermissions { get; set; }

    public Permission()
    {
        RolePermissions = new HashSet<RolePermission>();
    }
} 