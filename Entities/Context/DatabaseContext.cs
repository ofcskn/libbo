using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Entities.Models;

namespace Entities.Context
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<About> About { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Announcement> Announcement { get; set; }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BookView> BookView { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<MailMessage> MailMessage { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<MessageView> MessageView { get; set; }
        public virtual DbSet<Mulct> Mulct { get; set; }
        public virtual DbSet<MulctView> MulctView { get; set; }
        public virtual DbSet<Process> Process { get; set; }
        public virtual DbSet<ProcessView> ProcessView { get; set; }
        public virtual DbSet<Safe> Safe { get; set; }
        public virtual DbSet<Setting> Setting { get; set; }
        public virtual DbSet<Sitemap> Sitemap { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Thread> Thread { get; set; }
        public virtual DbSet<ThreadView> ThreadView { get; set; }
        public virtual DbSet<ToDoList> ToDoList { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAPTOP-AATLPISC;Database=Libbo;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<About>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.Adress)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Role).HasMaxLength(50);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.Property(e => e.Announcer).HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Link).HasMaxLength(100);

                entity.Property(e => e.SubTitle).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Biography).IsRequired();

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HomeTown)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Image).HasMaxLength(60);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Score)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(800);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Printery)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PrintingYear)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Score)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<BookView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("BookView");

                entity.Property(e => e.AuthorName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AuthorSurname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(800);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Printery)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PrintingYear)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Score)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.ShortTitle)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<MailMessage>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Detail).HasMaxLength(50);

                entity.Property(e => e.ReceiverMail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SenderMail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Subject).HasMaxLength(50);
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.Property(e => e.FileNames)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.SubTitle).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.EducationStatus).HasMaxLength(50);

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.ImageUrl).HasMaxLength(250);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.School)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IpAdress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<MessageView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("MessageView");

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.IpAdress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Mulct>(entity =>
            {
                entity.Property(e => e.Detail).HasMaxLength(250);

                entity.Property(e => e.Money).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<MulctView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("MulctView");

                entity.Property(e => e.Detail).HasMaxLength(250);

                entity.Property(e => e.Money).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<ProcessView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ProcessView");

                entity.Property(e => e.BookName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MemberName).HasMaxLength(20);

                entity.Property(e => e.MemberSurname)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.StaffName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.StaffSurname)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Safe>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Month)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.Property(e => e.JsonData).IsRequired();

                entity.Property(e => e.Lang)
                    .IsRequired()
                    .HasMaxLength(8);
            });

            modelBuilder.Entity<Sitemap>(entity =>
            {
                entity.HasKey(e => e.Permalink);

                entity.Property(e => e.Permalink).HasMaxLength(256);

                entity.Property(e => e.ChangeFrequency)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Priority).HasColumnType("decimal(2, 1)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.Mail).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Phone)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Score)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<ThreadView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ThreadView");

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.ImageUrl).HasMaxLength(250);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.School)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<ToDoList>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Goal)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Role).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Role).HasMaxLength(50);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
