namespace Model.ViewModels.Role
{
    using System.ComponentModel.DataAnnotations;

    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }
    }
}
