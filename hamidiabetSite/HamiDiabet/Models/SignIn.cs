using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HamiDiabet.Models
{
    public class SignIn
    {
        [Required(ErrorMessage = "لطفا شماره موبایل را وارد نمایید")]
        [Display(Name = "شماره موبایل: ")]
        public string mobile { get; set; }

        [Required(ErrorMessage = "لطفا گذرواژه را وارد نمایید")]
        [Display(Name = "گذرواژه: ")]
        public string password { get; set; }
        
        public string err { get; set; }
    }
}