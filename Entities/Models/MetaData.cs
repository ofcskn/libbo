using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Entities.Models
{
    public class AdminMetaData
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage ="Lütfen kullanıcı adı giriniz.")]
        public string UserName { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Lütfen isim giriniz.")]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Lütfen soyisim giriniz.")]
        public string Surname { get; set; }
        public string Phone { get; set; }
        [MaxLength(60)]
        [Required(ErrorMessage = "Lütfen mail adresinizi giriniz.")]
        public string Email { get; set; }
        [MaxLength(300)]
        [Required(ErrorMessage = "Lütfen adresinizi giriniz.")]
        public string Adress { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Lütfen fotoğrafınızı ekleyiniz.")]
        public string Image { get; set; }
        
        public DateTime LastLoginDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsAdmin { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Lütfen rol ekleyiniz.")]
        public string Role { get; set; }
    }
    public class AuthorMetaData
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Lütfen ad ekleyiniz.")]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Lütfen soyad ekleyiniz.")]
        public string Surname { get; set; }
        [MinLength(10, ErrorMessage = "Lütfen doğru girdiğinizden emin olunuz.")]
        [MaxLength(10, ErrorMessage = "Lütfen 10 karakter giriniz")]
        public string Phone { get; set; }
        [DataType(DataType.DateTime,ErrorMessage ="Lütfen geçerli bir tarih giriniz")]
        [Required(ErrorMessage = "Lütfen doğum günü ekleyiniz.")]
        public DateTime Birthday { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DeathDay { get; set; }
        [Required(ErrorMessage = "Lütfen biyografi ekleyiniz.")]
        public string Biography { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Lütfen memleket ekleyiniz.")]
        public string HomeTown { get; set; }
        [Required(ErrorMessage = "Lütfen ülke ekleyiniz.")]
        public string Country { get; set; }
        public string Score { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
    public class BookMetaData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen isim ekleyiniz.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen yayınlanma yılını ekleyiniz.")]
        public string PrintingYear { get; set; }
        [Required(ErrorMessage = "Lütfen sayfa no ekleyiniz.")]
        public int PageNo { get; set; }
        [Required(ErrorMessage = "Lütfen basımevi ekleyiniz.")]
        public string Printery { get; set; }
        [Required(ErrorMessage = "Lütfen durum ekleyiniz.")]
        public bool Status { get; set; }
        [Required(ErrorMessage = "Lütfen kategori seçiniz.")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Lütfen yazar ekleyiniz.")]
        public int AuthorId { get; set; }
        public int? ReadCount { get; set; }
        public string Score { get; set; }

        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Process> Process { get; set; }
    }
    public class CategoryMetaData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen isim giriniz.")]
        public string Name { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
    public class MemberMetaData
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Adınız 50 karakteri geçemez.")]
        [Required(ErrorMessage = "Lütfen isim ekleyiniz.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen soyisim ekleyiniz.")]
        [MaxLength(50, ErrorMessage = "Soyadınız 50 karakteri geçemez.")]
        public string Surname { get; set; }
        [MinLength(10, ErrorMessage = "Telefon numaranızı doğru girdiğinizden emin olun.")]
        [MaxLength(15, ErrorMessage = "Telefon numaranızın başına 0 koymadan tekrar deneyiniz.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Lütfen mail ekleyiniz.")]
        public string Mail { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Lütfen kullanıcı adı ekleyiniz.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Lütfen şifre ekleyiniz.")]
        public string Password { get; set; }
       
        [Required(ErrorMessage = "Lütfen şifrenizi tekrar gririniz.")]
        public string PasswordAgain { get; set; }
        [Required(ErrorMessage = "Lütfen okul ekleyiniz.")]
        public string School { get; set; }
        public bool TermConfirm { get; set; }
        [Required(ErrorMessage = "Lütfen eğitim durumu belirtiniz.")]
        public string EducationStatus { get; set; }


        public virtual ICollection<Mulct> Mulct { get; set; }
        public virtual ICollection<Process> Process { get; set; }
    }
    public class MulctMetaData
    {
        public int Id { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public decimal? Money { get; set; }
        public string Detail { get; set; }
        public int MemberId { get; set; }
        public int? ProcessId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Process Process { get; set; }
    }
    public class ProcessMetaData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen kitap seçiniz.")]
        public int BookId { get; set; }
        [Required(ErrorMessage = "Lütfen üye seçiniz.")]
        public int MemberId { get; set; }
        [Required(ErrorMessage = "Lütfen personel seçiniz.")]
        public int StaffId { get; set; }
        public DateTime GettingDate { get; set; }
        public DateTime ReturningDate { get; set; }

        public virtual Book Book { get; set; }
        public virtual Member Member { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual ICollection<Mulct> Mulct { get; set; }
    }
    public class StaffMetaData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen isim ekleyiniz.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen soyisim ekleyiniz.")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Lütfen telefon ekleyiniz.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Lütfen mail ekleyiniz.")]
        public string Mail { get; set; }
        [Required(ErrorMessage = "Lütfen doğum günü tarihi ekleyiniz.")]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Lütfen işe başlama tarihi seçiniz.")]
        public DateTime DateOfStart { get; set; }
        public string Score { get; set; }
    }
    public class UserMetaData
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string Image { get; set; }
        public string Role { get; set; }
        public bool Confirmation { get; set; }
    }
}
