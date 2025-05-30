using BookRental.Domain.Interfaces;
using BookRental.Domain.Common;
using MediatR;

namespace Application.Authentication.Commands.Logout;

public class LogoutCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<LogoutCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.RefreshToken))
        {
            await unitOfWork.RefreshTokens.UpdateAsync(request.RefreshToken, setters => setters
                .SetProperty(rt => rt.Invalidated, true));
        }
        return Result<bool>.Success(true);
    }
}
