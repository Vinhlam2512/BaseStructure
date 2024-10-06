namespace LETOS.Domain.Entities.Users;
public sealed record Name
{
    public Name()
    {
    }

    public string UserName { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public static Name Create(string firstName, string lastName, string userName)
    {
        if (firstName.Length == 0 || lastName.Length == 0)
        {
            throw new ApplicationException("Vui lòng điền đầy đủ thông tin họ và tên!");
        }

        return new Name
        {
            FirstName = firstName,
            LastName = lastName,
            UserName = userName
        };
    }


}
