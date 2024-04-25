using Domain.Entities.Base;

namespace Domain.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; }
    public List<Article> Articles { get; set; }
}
