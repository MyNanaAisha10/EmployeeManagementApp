﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Models;

public class EditProfileViewModel
{
    public string Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string UserName { get; set; }
}