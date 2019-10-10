using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewIdentityCore.Models
{
    public class LoginUser: IdentityUser
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }

        [Display(Name = "First Name")]
        //[Required(ErrorMessage = "First Name should not be empty.")]
        [StringLength(50, ErrorMessage = "First Name should be 50 characters only.")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Middle Name should be 50 characters only.")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        //[Required(ErrorMessage = "Last Name should not be empty.")]
        [StringLength(50, ErrorMessage = "Last Name should be 50 characters only.")]
        public string LastName { get; set; }

        [Display(Name = "Employee Id")]
       // [Required(ErrorMessage = "Employee Id Shouldn't be empty.", AllowEmptyStrings = false)]
        [MinLength(6, ErrorMessage = "Employee Id Should be minimum 6 characters and maximum 10 characters.")]
        [MaxLength(10, ErrorMessage = "Employee Id Should be minimum 6 characters and maximum 10 characters.")]
        public string EmployeeId { get; set; }

        [Display(Name = "Email Id")]
       // [Required(ErrorMessage = "Email Id should not be empty.")]
        [StringLength(150, ErrorMessage = "Email Id should be 150 characters only.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string EmailId { get; set; }

        [DataType(DataType.Password)] public string Password { get; set; }

        [Display(Name = "Designation")]
        public int? DesignationId { get; set; }

        [Display(Name = "Department")]
       // [Required(ErrorMessage = "Please select Department.")]
        public int? DepartmentId { get; set; }

        [Display(Name = "Reporting To")]
        public int? ReportingPersonId { get; set; }

        //public virtual LoginUser ReportingPerson { get; set; }

        [Display(Name = "Date of Joining")]
        public DateTime? DateOfJoining { get; set; }

        [Display(Name = "Mobile Number")]
        [MinLength(10, ErrorMessage = "Mobile number Should be minimum 10 characters and maximum 12 characters.")]
        [MaxLength(12, ErrorMessage = "Mobile number Should be minimum 10 characters and maximum 12 characters.")]
        public string MobileNo { get; set; }

        //[Required(ErrorMessage = "Please select Salutation.")]
        public int? SalutationId { get; set; }
        public int? LoginAttempts { get; set; }
        public int? Status { get; set; }
        public int? Createdby { get; set; }
        //public virtual LoginUser CreatedLoginUser { get; set; }
        public int? Modifyby { get; set; }
        //public virtual LoginUser ModifiedLoginUser { get; set; }
        [StringLength(20)] public string IpAddress { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? LastPasswordChange { get; set; }
        public string FullName => FirstName + " " + MiddleName + " " + LastName;
        public string FullNameWithId => FirstName + " " + MiddleName + " " + LastName + " ( " + EmployeeId + ")";
        public string LoggedInSessionToken { get; set; }
        public int? ReferenceId { get; set; }
        //public virtual LoginUser Reference { get; set; }
        public bool? IsLoginPasswords { get; set; }
        public bool? PasswordFlag { set; get; }
        //public byte[] PasswordHash { get; set; }
        //public byte[] PasswordSalt { get; set; }
    }
}
