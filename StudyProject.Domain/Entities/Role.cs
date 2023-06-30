﻿using StudyProject.Domain.Common;
using StudyProject.Infrastructure.Interfaces;

namespace StudyProject.Domain.Entities
{
    public class Role : BaseEntity, ISoftDelete
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid PermissionId { get; set; }
        public List<Permission> Permission { get; set; }
    }
}
