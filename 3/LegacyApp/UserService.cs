using System;

namespace LegacyApp
{
    public class UserService
    {
        public ClientRepository ClientRepository { get; set; }
        public UserCreditService UserCreditService { get; set; } 
        //public UserDataAccess UserDataAccess { get; set; }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!ValidateFirstNameLastName(firstName, lastName))
                return false;

            if (!ValidateEmail(email))
                return false;

            if (!ValidateAge(dateOfBirth))
                return false;

            var client = ClientRepository.GetById(clientId);

            var user = CreateUser(firstName, lastName, email, dateOfBirth, client);

            SetCreditLimit(client, user);

            if (user.HasCreditLimit && user.CreditLimit < 500)
                return false;

            UserDataAccess.AddUser(user);
            return true;
        }
        
        private User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, Client client)
        {
            return new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
        }

        private void SetCreditLimit(Client client, User user)
        {
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else
            {
                int creditLimit = UserCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                if (client.Type == "ImportantClient")
                    creditLimit *= 2;

                user.CreditLimit = creditLimit;
                user.HasCreditLimit = true;
            }
        }
        
        private bool ValidateFirstNameLastName(string firstName, string lastName)
        {
            return !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName);
        }

        private bool ValidateEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        private bool ValidateAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                age--;
            return age >= 21;
        }
    }
}
