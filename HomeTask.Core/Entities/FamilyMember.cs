using System;
using System.Collections.Generic;
using System.Text;

namespace HomeTask.HomeTask.Core.Entities
{
    public class FamilyMember
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public FamilyMember()
        { }

        public FamilyMember(string name, string? email)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
    }
}
