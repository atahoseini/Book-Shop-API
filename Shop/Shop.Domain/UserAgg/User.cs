using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.UserAgg.Enum;
using Shop.Domain.UserAgg.Services;
using System.Diagnostics.Tracing;
using System.Xml.Serialization;

namespace Shop.Domain.UserAgg;

public class User : AggregateRoot
{


    public string Name { get; private set; }
    public string Family { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public Gender Gender { get; private set; }
    public List<UserRole> Roles { get; private set; }
    public List<Wallet> Wallets { get; private set; }
    public List<UserAddress> Addresses { get; private set; }

    public User(string name, string family, string phoneNumber, string email, string password, Gender gender, IDomainUserServices domainUserServices)
    {
        Guard(phoneNumber, email, domainUserServices);
        Name=name;
        Family=family;
        PhoneNumber=phoneNumber;
        Email=email;
        Password=password;
        Gender=gender;
    }

    public static User RegisterUser(string email,string phoneNumber, string password, IDomainUserServices domainUserServices)
    {
        return new User("","", phoneNumber, email,password,Gender.None,domainUserServices);
    }

    public void Edit(string name, string family, string phoneNumber, string email, Gender gender , IDomainUserServices domainUserServices)
    {
        Guard(phoneNumber, email, domainUserServices);
        Name=name;
        Family=family;
        PhoneNumber=phoneNumber;
        Email=email;
        Gender = gender;

    }

    public void AddAddress(UserAddress address)
    {
        address.UserId = Id;
        Addresses.Add(address);
    }

    public void EditAddress(UserAddress address)
    {
        var oldAddress = Addresses.FirstOrDefault(a => a.Id == address.Id);
        if (oldAddress == null)
            throw new InvalidDomainDataException("Address not found");
        Addresses.Remove(oldAddress);
        address.UserId = Id;
        Addresses.Add(address);
    }

    public void DeleteAddress(UserAddress address)
    {
        var oldAddress = Addresses.FirstOrDefault(a => a.Id == address.Id);
        if (oldAddress == null)
            throw new InvalidDomainDataException("Address not found");
        Addresses.Remove(oldAddress);
    }

    public void ChargeWallet(Wallet wallet)
    {
        wallet.UserId = Id;
        Wallets.Add(wallet);
    }

    public void SetRoles(List<UserRole> roles)
    {
        roles.ForEach(r => r.UserId = Id);
        Roles.Clear();
        Roles.AddRange(roles);
    }

    public void Guard(string phoneNumber, string email,IDomainUserServices  domainUserServices)
    {
        NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));
        NullOrEmptyDomainDataException.CheckString(email, nameof(email));
        if(phoneNumber.Length==11)
            throw new InvalidDomainDataException("Phone number is not valid");
        if(EmailValidation.IsValidEmail(email)==false)
            throw new InvalidDomainDataException("Email is not valid");

        if(phoneNumber != PhoneNumber)
        {
            if (domainUserServices.PhoneNumberIsExist(phoneNumber))
                throw new InvalidDomainDataException("Phone number is duplicate");
        }

        if (email != Email)
        {
            if (domainUserServices.isEmailExisats(phoneNumber))
                throw new InvalidDomainDataException("Email Address is duplicate");
        }


    }
 


}