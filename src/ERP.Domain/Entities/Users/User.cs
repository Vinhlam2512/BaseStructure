using ERP.Domain.Abstractions.Aggregates;
using ERP.Domain.Abstractions.Entities;
using ERP.Domain.Entities.Identity;

namespace ERP.Domain.Entities.Users;

public sealed class User : AggregateRoot<Guid>, ISoftDelete
{

    private User()
    {

    }

    private User(Name name, Email email, string passWordHashed, int accessFailed = 0, bool isLock = false)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        PassWordHashed = passWordHashed;
        AccessFailed = accessFailed;
        IsLocked = isLock;
    }


    public Name Name { get; private set; }

    public Email Email { get; private set; }

    public string PassWordHashed { get; private set; }

    public int AccessFailed { get; private set; }

    public bool IsLocked { get; private set; }

    public bool IsDelete { get; set; }

    public DateTime? DeletedAt { get; set; }

    public ICollection<RoleUser> RoleUsers { get; set; } = new List<RoleUser>();

    public static User Create(Name name, Email email, string passwordHashed)
    {
        var user = new User(name, email, passwordHashed);

        user.RoleUsers.Add(new RoleUser(user.Id, Role.User.Id));
        return user;
    }

    public void CountAccessFailed()
    {
        AccessFailed++;
        if (AccessFailed > 10)
        {
            LockAccount();
        }
    }

    private void LockAccount()
    {
        IsLocked = true;
    }

    public void ResetAccessFailed()
    {
        AccessFailed = 0;
        IsLocked = false;
    }

    public void Remove()
    {
        throw new NotImplementedException();
    }
}
