namespace Model.ViewModels.Role
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EditRoleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Role name is required!")]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; } = new List<string>();
    }
}
