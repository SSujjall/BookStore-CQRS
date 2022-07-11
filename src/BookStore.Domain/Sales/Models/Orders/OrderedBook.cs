﻿namespace BookStore.Domain.Sales.Models.Orders;

using Common.Models;
using Exceptions;

using static ModelConstants.OrderedBook;

public class OrderedBook : Entity<int>
{
    internal OrderedBook(Book book, int quantity)
    {
        this.Validate(book, quantity);

        this.Book = book;
        this.Quantity = quantity;
    }

    private OrderedBook(int quantity)
    {
        this.Quantity = quantity;

        this.Book = default!;
    }

    public Book Book { get; }

    public int Quantity { get; }

    private void Validate(Book book, int quantity)
    {
        if (!book.IsAvailable)
        {
            throw new InvalidOrderException("Book is out of stock.");
        }

        Guard.AgainstOutOfRange<InvalidOrderException>(
            quantity,
            MinQuantityValue,
            MaxQuantityValue,
            nameof(this.Quantity));
    }
}