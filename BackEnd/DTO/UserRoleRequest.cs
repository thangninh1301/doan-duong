using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTO
{
    public class UserRoleRequest
    {
        [StringLength(450)]
       public string UserId { get; set; }
        public string UserName { get; set; }
        public string CurrentRoleNames { get; set; }
        public string NewRoleNames { get; set; }
    }
}
