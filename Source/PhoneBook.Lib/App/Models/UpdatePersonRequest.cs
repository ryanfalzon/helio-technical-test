﻿namespace PhoneBook.Lib.App.Models;

public class UpdatePersonRequest
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
}