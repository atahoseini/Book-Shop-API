using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.UserAgg.Enum;
using System;
using System.Data;
using System.Xml.Serialization;

namespace Shop.Domain.UserAgg;

public class Wallet : BaseEntity
{
    public Wallet(int price, string description, bool isFinally, DateTime? finallyDate, WalletType walletType)
    {
        if (price <= 500)
            throw new InvalidDomainDataException("Price must be greater than 500");

        Price=price;
        Description=description;
        IsFinally=isFinally;
        FinallyDate=finallyDate;
        WalletType=walletType;
    }

    public long UserId { get; internal set; }
    public int Price { get; private set; }
    public string Description { get; private set; }
    public  bool IsFinally { get; private set; }
    public DateTime? FinallyDate { get; private set; }
    public WalletType WalletType { get; private set; }
    public void Finally(string refCode)
    {
        IsFinally = true;
        FinallyDate = DateTime.Now;
        Description += $"| RefId : {refCode}";
    }
    public void Finally()
    {
        IsFinally = true;
        FinallyDate = DateTime.Now;
    }
}

