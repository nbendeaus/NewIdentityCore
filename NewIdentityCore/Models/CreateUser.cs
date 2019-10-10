using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewIdentityCore.Models
{
    #region oldmodel
    public class CreateUser
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public string UserId { get; set; }

        [Display(Name = "First Name")]
        public string FName { get; set; }
        [Display(Name = "Middle Name")]
        public string MName { get; set; }
        [Display(Name = "Last Name")]
        public string LName { get; set; }

        [Display(Name = "Mobile Number")]
        public string PhoneNo2 { get; set; }
        public string Address { get; set; }
        [Display(Name = "Salutation")]
        public int SalutationId { get; set; }

        [Display(Name = "Role")]
        public int RoldId { get; set; }

        [Display(Name = "State")]
        public int StateId { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }
    #endregion
}
