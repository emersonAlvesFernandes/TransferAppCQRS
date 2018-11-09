using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TransferAppCQRS.WebApi.ViewModels
{
    public class AccountViewModel
    {
        [Required(ErrorMessage = "Agency is Required")]                
        [DisplayName("Agency")]
        public int Agency { get; set; }

        [Required(ErrorMessage = "Number is Required")]
        [DisplayName("Number")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "CustomerGuId is Required")]
        [DisplayName("CustomerGuId")]
        public Guid CustomerGuId { get; set; }
    }
}
