using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.Delete;

internal sealed class DeleteUserCommandHandler(IApplicationDbContext context) : ICommandHandler<DeleteUserCommand, string>
{
    public async Task<Result<string>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        context.Users.Remove(user);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success<string>($"User with {user.Id} deleted successfully.");
    }
}
