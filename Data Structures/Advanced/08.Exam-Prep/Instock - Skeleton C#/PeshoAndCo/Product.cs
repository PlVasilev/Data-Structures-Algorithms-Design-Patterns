using System;
using System.Collections.Generic;
using System.Security.Permissions;

/// <summary>
/// <para>Product is the entity which your stock data structure
/// will consist of. Please, do not make any modifications as
/// it might lead to unexpected results</para>
/// </summary>
public class Product : IComparable<Product>, IComparer<Product>
{

    public Product(string label, double price, int quantity)
    {
        this.Label = label;
        this.Price = price;
        this.Quantity = quantity;

    }

    public string Label { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }

    public int Time { get; set; }

    public int CompareTo(Product other)
    {
        return Price.CompareTo(other.Price);
    }

    public int Compare(Product x, Product y)
    {
        return y.Price.CompareTo(x.Price);
    }

    public override bool Equals(object obj)
    {
        Product other = obj as Product;
        ;
        if (other == null)
        {
            return false;
        }

        return other.Label == this.Label;


    }

    public override int GetHashCode()
    {
        return Label.GetHashCode();
    }

}