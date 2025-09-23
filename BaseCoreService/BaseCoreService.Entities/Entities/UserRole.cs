using BaseCoreService.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCoreService.Entities
{
    [TableConfig("user_role")]
    public class UserRole: BaseEntity
    {
        [Key]
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid RoleID { get; set; }
    }
}
