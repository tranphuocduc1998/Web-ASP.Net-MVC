using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComicWeb.Areas.Admin.Models
{
    public class Administrators
    {
        [Required(ErrorMessage = "Hãy nhập tên đăng nhập")]
        [MaxLength(64, ErrorMessage = "nhập tối đa 64 ký tự")]
        [MinLength(3, ErrorMessage = "nhập ít nhất 3 ký tự")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Hãy nhập mật khẩu")]
        [StringLength(256, ErrorMessage = "mật khẩu phải nhập từ 5-256 ký tự", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Passwords { get; set; }

        [Required(ErrorMessage = "Hãy nhập họ và tên")]
        [MaxLength(64, ErrorMessage = "nhập tối đa 64 ký tự")]
        [MinLength(3, ErrorMessage = "nhập ít nhất 3 ký tự")]
        [StringLength(64)]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Hãy nhập Email")]
        [EmailAddress(ErrorMessage = "Email có định dạng không đúng")]
        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(256)]
        public string Avatar { get; set; }

        public int? isAdmin { get; set; }

        public int? Allowed { get; set; }
    }
}