using Domain.Users;

namespace UnitTests.Fixtures;
public static class UserFixtures
{
    public static List<User> GetUsers()
    {
        return new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@microsoft.com",
            },
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "James",
                LastName = "Carter",
                Email = "jamescarter@microsoft.com",
            },
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Sophia",
                LastName = "Mitchell",
                Email = "sophiamitchell@microsoft.com",
            },
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Noah",
                LastName = "Wiliams",
                Email = "noahwiliams@microsoft.com",
            },
        };
    }
}
