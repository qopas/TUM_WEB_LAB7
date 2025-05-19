﻿using Application.DTOs.Book;

namespace Application.Mapping;

public static class BookMappingExtension
{
     public static BookDto ToDto(this BookRental.Domain.Entities.Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublicationDate = book.PublicationDate,
                GenreId = book.GenreId,
                AvailableQuantity = book.AvailableQuantity,
                RentalPrice = book.RentalPrice
            };
        }
     
        public static BookRental.Domain.Entities.Book ToEntity(this CreateBookDto dto)
        {
            
            return new BookRental.Domain.Entities.Book
            {
                Title = dto.Title,
                Author = dto.Author,
                PublicationDate = dto.PublicationDate,
                GenreId = dto.GenreId,
                AvailableQuantity = dto.AvailableQuantity,
                RentalPrice = dto.RentalPrice
            };
        }
        
        public static BookRental.Domain.Entities.Book ToEntity(this UpdateBookDto dto, BookRental.Domain.Entities.Book existingBook)
        {
            if (dto.Title != null)
                existingBook.Title = dto.Title;
                
            if (dto.Author != null)
                existingBook.Author = dto.Author;
                
            if (dto.PublicationDate.HasValue)
                existingBook.PublicationDate = dto.PublicationDate.Value;
                
            if (dto.GenreId != null)
                existingBook.GenreId = dto.GenreId;
                
            if (dto.AvailableQuantity.HasValue)
                existingBook.AvailableQuantity = dto.AvailableQuantity.Value;
                
            if (dto.RentalPrice.HasValue)
                existingBook.RentalPrice = dto.RentalPrice.Value;
                
            return existingBook;
        }
        
        public static IEnumerable<BookDto> ToDtoList(this IEnumerable<BookRental.Domain.Entities.Book> books)
        {
            return books.Select(b => b.ToDto());
        }
}
