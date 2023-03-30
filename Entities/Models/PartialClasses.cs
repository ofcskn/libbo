using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    [ModelMetadataType(typeof(AdminMetaData))]
    public partial class Admin
    {
    }
    [ModelMetadataType(typeof(AuthorMetaData))]
    public partial class Author
    {
    }
    [ModelMetadataType(typeof(BookMetaData))]
    public partial class Book
    {
    }
    [ModelMetadataType(typeof(CategoryMetaData))]
    public partial class Category
    {
    }

    [ModelMetadataType(typeof(MemberMetaData))]
    public partial class Member
    {
    }
    [ModelMetadataType(typeof(MulctMetaData))]
    public partial class Mulct
    {
    }
    [ModelMetadataType(typeof(ProcessMetaData))]
    public partial class Process
    {
    }
    [ModelMetadataType(typeof(UserMetaData))]
    public partial class User
    {
    }
    [ModelMetadataType(typeof(StaffMetaData))]
    public partial class Staff
    {
    }
}
