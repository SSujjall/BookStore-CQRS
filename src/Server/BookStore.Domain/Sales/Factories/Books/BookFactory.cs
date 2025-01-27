﻿namespace BookStore.Domain.Sales.Factories.Books;

using Exceptions;
using Models.Books;

internal class BookFactory : IBookFactory
{
    private string bookTitle = default!;
    private decimal bookPrice = default!;
    private int bookQuantity = default!;
    private string bookImageUrl = default!;

    private bool isTitleSet = false;
    private bool isPriceSet = false;
    private bool isQuantitySet = false;
    private bool isImageUrlSet = false;

    public IBookFactory WithTitle(string title)
    {
        this.bookTitle = title;
        this.isTitleSet = true;

        return this;
    }

    public IBookFactory WithPrice(decimal price)
    {
        this.bookPrice = price;
        this.isPriceSet = true;

        return this;
    }

    public IBookFactory WithQuantity(int quantity)
    {
        this.bookQuantity = quantity;
        this.isQuantitySet = true;

        return this;
    }

    public IBookFactory WithImageUrl(string imageUrl)
    {
        this.bookImageUrl = imageUrl;
        this.isImageUrlSet = true;

        return this;
    }

    public Book Build()
    {
        if (!this.isTitleSet ||
            !this.isPriceSet ||
            !this.isQuantitySet ||
            !this.isImageUrlSet)
        {
            throw new InvalidBookException("Title, price, quantity and image must have a value.");
        }

        return new Book(
            this.bookTitle,
            this.bookPrice,
            this.bookQuantity,
            this.bookImageUrl);
    }
}