﻿using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Book.Commands.DeleteBook;

public class DeleteBookCommandHandler(IBookRepository bookRepository) : IRequestHandler<DeleteBookCommand, bool>
{
    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetByIdAsync(request.Id);
        if (book == null)
        {
            return false;
        }

        await bookRepository.DeleteAsync(request.Id);
        return true;
    }
}