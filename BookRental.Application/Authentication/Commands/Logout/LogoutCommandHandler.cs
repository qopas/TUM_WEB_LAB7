using BookRental.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Commands.Logout;

public class LogoutCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<LogoutCommand, bool>
{
    public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!string.IsNullOrEmpty(request.RefreshToken))
            {
                var refreshToken = await unitOfWork.RefreshTokens
                    .Find(rt => rt.Token == request.RefreshToken && rt.UserId == request.UserId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (refreshToken != null)
                {
                    refreshToken.Invalidated = true;
                    await unitOfWork.RefreshTokens.UpdateAsync(refreshToken);
                }
            }
            else
            {
                var userRefreshTokens = unitOfWork.RefreshTokens
                    .Find(rt => rt.UserId == request.UserId && !rt.Invalidated);

                foreach (var token in userRefreshTokens)
                {
                    token.Invalidated = true;
                    await unitOfWork.RefreshTokens.UpdateAsync(token);
                }
            }

            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}