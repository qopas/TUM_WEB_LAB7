﻿using BookRental.Domain.Entities.Base;
using BookRental.Domain.Enums;

namespace BookRental.Domain.Entities;

public class Rent : BaseEntity
{
    public string BookId { get; set; }
    public string CustomerId { get; set; }
    public string DestinationId { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public RentStatus Status { get; set; }
    
    public virtual Book Book { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual Destination Destination { get; set; }
}