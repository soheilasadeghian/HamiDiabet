using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HamiDiabet.Models
{
    public class SignUp
    {
        [Required(ErrorMessage = "لطفا نام را وارد نمایید")]
        [Display(Name ="نام: ")]
        public string name { get; set; }

        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد نمایید")]
        [Display(Name = "نام خانوادگی: ")]
        public string family { get; set; }

        [Required(ErrorMessage = "لطفا شهر را وارد نمایید")]
        [Display(Name = "شهر: ")]
        public long cityId { get; set; }

        [Required(ErrorMessage = "لطفا شماره موبایل را وارد نمایید")]
        [Display(Name = "شماره موبایل: ")]
        public string mobile { get; set; }

        [Required(ErrorMessage = "لطفا گذرواژه را وارد نمایید")]
        [Display(Name = "گذرواژه: ")]
        [DataType(DataType.Password, ErrorMessage = "گذرواژه معتبر نمی باشد")]
        [StringLength(150, ErrorMessage = "طول گذرواژه کمتر از 6 کاراکتر است", MinimumLength = 6)]
        public string password { get; set; }

        [Compare("password", ErrorMessage = "گذرواژه با تکرار آن مغایرت دارد")]
        [Required(ErrorMessage = "لطفا گذرواژه را تکرار نمایید")]
        [Display(Name = "تکرار گذرواژه: ")]
        [DataType(DataType.Password,ErrorMessage = "گذرواژه معتبر نمی باشد")]
        [StringLength(150, ErrorMessage = "طول گذرواژه کمتر از 6 کاراکتر است", MinimumLength = 6)]
        public string confirmPassword { get; set; }

        [Display(Name = "اشتراک در خبرنامه")]
        public bool subscribeNewsletter { get; set; }

        public string err { get; set; }
    }
}