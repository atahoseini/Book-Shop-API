using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.UserAgg;

public class UserAddress : BaseEntity
{
    public long UserId { get; internal set; }
    public string Shire { get; private set; }
    public string City { get; private set; }
    public string PostCode { get; private set; }
    public string PostalAddress { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string NationalCode { get; private set; }
    public bool ActiveAddress { get; private set; }


    public UserAddress(long userId, string shire, string city, string postCode, string postalAddress, string phoneNumber, string name, string family, string nationalCode)
    {
        Gurad(shire, city, postCode, postalAddress, phoneNumber, name, family, nationalCode);
        UserId=userId;
        Shire=shire;
        City=city;
        PostCode=postCode;
        PostalAddress=postalAddress;
        PhoneNumber=phoneNumber;
        Name=name;
        Family=family;
        NationalCode=nationalCode;
        ActiveAddress = false;
    }

    public void Edit(string shire, string city, string postCode, string postalAddress, string phoneNumber, string name, string family, string nationalCode)
    {
        Gurad(shire, city, postCode, postalAddress, phoneNumber, name, family, nationalCode);
        Shire=shire;
        City=city;
        PostCode=postCode;
        PostalAddress=postalAddress;
        PhoneNumber=phoneNumber;
        Name=name;
        Family=family;
        NationalCode=nationalCode;
    }


    public void SetActiveAddress()
    {
        ActiveAddress=true;
    }

    public void Gurad(string shire, string city, string postCode, string postalAddress, string phoneNumber, string name, string family, string nationalCode)
    {
        NullOrEmptyDomainDataException.CheckString(shire, nameof(shire));
        NullOrEmptyDomainDataException.CheckString(city, nameof(city));
        NullOrEmptyDomainDataException.CheckString(postCode, nameof(postCode));
        NullOrEmptyDomainDataException.CheckString(postalAddress, nameof(postalAddress));
        NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));
        NullOrEmptyDomainDataException.CheckString(name, nameof(name));
        NullOrEmptyDomainDataException.CheckString(family, nameof(family));
        NullOrEmptyDomainDataException.CheckString(nationalCode, nameof(nationalCode));

        if(IranianNationalIdChecker.IsValid(nationalCode) == false)
            throw new InvalidDomainDataException("National code is not valid");




    }

}

