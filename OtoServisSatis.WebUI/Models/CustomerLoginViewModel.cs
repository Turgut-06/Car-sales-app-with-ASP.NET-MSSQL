using System.ComponentModel.DataAnnotations;

namespace OtoServisSatis.WebUI.Models
{
    public class CustomerLoginViewModel
    {
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Sifre { get; set; }
    }
}
