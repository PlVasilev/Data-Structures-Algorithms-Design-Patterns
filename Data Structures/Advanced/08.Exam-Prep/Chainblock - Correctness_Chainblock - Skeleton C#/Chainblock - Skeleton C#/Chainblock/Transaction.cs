using System;
using System.Collections.Generic;

public class Transaction : IComparable<Transaction>
{
    public int Id { get; set; }
    public TransactionStatus Status { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public double Amount { get; set; }

    public Transaction(int id, TransactionStatus st, string from, string to, double amount)
    {
        this.Id = id;
        this.Status = st;
        this.From = from;
        this.To = to;
        this.Amount = amount;
    }

    public override bool Equals(object obj)
    {
        Transaction other = obj as Transaction;
        ;
        if (other == null)
        {
            return false;
        }

        return other.Id == this.Id;


    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public int CompareTo(Transaction other)
    {
        var result = (int)(other.Amount * 100 - this.Amount * 100);
        if (result == 0)
        {
            result = this.Id - other.Id;
        }

        return result;
    }




}