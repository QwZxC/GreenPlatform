using Domain.Entities.Base;

namespace Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; }
    public List<GreenPlatformUser> Users { get; set; }
}
