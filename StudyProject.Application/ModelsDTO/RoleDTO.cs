using StudyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyProject.Application.ModelsDTO
{
    internal class RoleDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Permissions { get; set; }
    }
}
