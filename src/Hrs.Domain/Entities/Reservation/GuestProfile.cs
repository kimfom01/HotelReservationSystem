using Hrs.Domain.Entities.Common;

namespace Hrs.Domain.Entities.Reservation;

public class GuestProfile : BaseEntity
{
    public GuestProfile(string firstName, string lastName, string contactEmail, string sex, int age)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        ContactEmail = contactEmail;
        Sex = sex;
        Age = age;
    }

    public string FirstName { get; private set; }
    public string LastName { get; set; }
    public string ContactEmail { get; set; }
    public string Sex { get; set; }
    public int Age { get; set; }

    public IReadOnlyCollection<Reservation>? Reservations { get; private set; }

    public static GuestProfile CreateGuestProfile(
        string firstName,
        string lastName,
        string contactEmail,
        string sex,
        int age)
    {
        return new GuestProfile(firstName, lastName, contactEmail, sex, age);
    }
}