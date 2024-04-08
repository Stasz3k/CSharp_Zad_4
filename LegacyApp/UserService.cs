using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!IsValidName(firstName) || !IsValidName(lastName) || !IsValidEmail(email) || !IsOldEnough(dateOfBirth))
            {
                return false;
            }

            var client = GetClient(clientId);
            var user = CreateUser(client, firstName, lastName, email, dateOfBirth);
            CalculateCreditLimit(user, client);

            if (IsCreditLimitBelowThreshold(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }
        
        private bool IsOldEnough(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            {
                age--;
            }

            return age >= 21;
        }
        //Ta metoda nie jest kompletna jednak z uwagi na sama refaktoryzacje nie zmieniono dzialania
        private bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains(".");
        }
        
        private bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }
        
        private Client GetClient(int clientId)
        {
            var clientRepository = new ClientRepository();
            return clientRepository.GetById(clientId);
        }
        
        private User CreateUser(Client client, string firstName, string lastName, string email, DateTime dateOfBirth)
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
        
        private int CalculateCreditLimit(User user, Client client)
        {
            int creditLimit;

            using (var userCreditService = new UserCreditService())
            {
                creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);

                if (client.Type == "ImportantClient")
                {
                    creditLimit *= 2;
                }
            }

            return creditLimit;
        }
        
        private bool IsCreditLimitBelowThreshold(User user)
        {
            return user.HasCreditLimit && user.CreditLimit < 500;
        }

    }
}
