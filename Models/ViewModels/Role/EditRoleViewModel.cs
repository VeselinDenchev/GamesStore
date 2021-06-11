namespace Model.ViewModels.Role
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Constants;

    public class EditRoleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = ErrorMessage.ROLE_NAME_REQUIRED)]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; } = new List<string>();
    }
}
