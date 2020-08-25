using System;

namespace ACI.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string NormalizedEmail { get; private set; }
        public bool EmailConfirmed { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime RegistrationDate { get; private set; }

        public UserEntity(Guid id, string firstName, string lastName, string email,
            string normalizedEmail, bool emailConfirmed, string passwordHash, DateTime birthDate, DateTime registrationDate)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.NormalizedEmail = normalizedEmail;
            this.EmailConfirmed = emailConfirmed;
            this.PasswordHash = passwordHash;
            this.BirthDate = birthDate;
            this.RegistrationDate = registrationDate;
        }
        public UserEntity() { }


        public bool HasMinimunAge()
        {
            int age = -1;
            if (DateTime.Now.Year > BirthDate.Year)
                age = (DateTime.Now.Month >= BirthDate.Month && DateTime.Now.Day >= BirthDate.Day)
                    ? age = DateTime.Now.Year - BirthDate.Year
                    : age = (DateTime.Now.Year - BirthDate.Year) - 1;

            return (age >= 18);
        }

        public void SetEmail(string email)
        {
            this.Email = email;
        }

        public bool SetEmailConfirmed(bool confirmed)
        {
            return (confirmed) ? this.EmailConfirmed = true : this.EmailConfirmed = false;
        }

        public void SetPasswordHash(string passwordHash)
        {
            this.PasswordHash = passwordHash;
        }
    }
}
