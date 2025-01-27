﻿namespace BookStore.Domain.Catalog.Models.Books;

using System;
using Authors;
using Common;
using Common.Events.Catalog;
using Common.Models;
using Exceptions;

using static Common.Models.ModelConstants.Book;
using static Common.Models.ModelConstants.Common;

public class Book : Entity<int>, IAggregateRoot
{
    internal Book(
        string title,
        decimal price,
        int quantity,
        string imageUrl,
        string description,
        Genre genre,
        Author author)
    {
        this.Validate(
            title,
            price,
            quantity,
            imageUrl,
            description);

        this.Title = title;
        this.Price = price;
        this.Quantity = quantity;
        this.ImageUrl = imageUrl;
        this.Description = description;
        this.Genre = genre;
        this.Author = author;

        this.RaiseEvent(new BookCreatedEvent(
            this.Title,
            this.Price,
            this.Quantity,
            this.ImageUrl));
    }

    private Book(
        string title,
        decimal price,
        int quantity,
        string imageUrl,
        string description)
    {
        this.Title = title;
        this.Price = price;
        this.Quantity = quantity;
        this.ImageUrl = imageUrl;
        this.Description = description;

        this.Genre = default!;
        this.Author = default!;
    }

    public string Title { get; private set; }

    public decimal Price { get; private set; }

    public int Quantity { get; private set; }

    public string ImageUrl { get; private set; }

    public string Description { get; private set; }

    public Genre Genre { get; private set; }

    public Author Author { get; private set; }

    public void Update(
        string title,
        decimal price,
        int quantity,
        string imageUrl,
        string description,
        Genre genre,
        Author author)
    {
        this.UpdateTitle(title)
            .UpdatePrice(price)
            .UpdateQuantity(quantity)
            .UpdateImageUrl(imageUrl)
            .UpdateDescription(description)
            .UpdateGenre(genre)
            .UpdateAuthor(author);

        this.RaiseEvent(new BookUpdatedEvent(
            this.Id,
            this.Title,
            this.Price,
            this.Quantity,
            this.ImageUrl));
    }

    public Book UpdateTitle(string title)
    {
        this.ValidateTitle(title);

        this.Title = title;

        return this;
    }

    public Book UpdatePrice(decimal price)
    {
        this.ValidatePrice(price);

        this.Price = price;

        return this;
    }

    public Book UpdateQuantity(int quantity)
    {
        this.ValidateQuantity(quantity);

        this.Quantity = quantity;

        return this;
    }

    public Book UpdateImageUrl(string imageUrl)
    {
        this.ValidateImageUrl(imageUrl);

        this.ImageUrl = imageUrl;

        return this;
    }

    public Book UpdateDescription(string description)
    {
        this.ValidateDescription(description);

        this.Description = description;

        return this;
    }

    public Book UpdateGenre(Genre genre)
    {
        this.Genre = genre;

        return this;
    }

    public Book UpdateAuthor(Author author)
    {
        this.Author = author;

        return this;
    }

    public override void Delete(DateTime now)
    {
        this.RaiseEvent(new BookDeletedEvent(this.Id));

        base.Delete(now);
    }

    private void Validate(
        string title,
        decimal price,
        int quantity,
        string imageUrl,
        string description)
    {
        this.ValidateTitle(title);
        this.ValidatePrice(price);
        this.ValidateQuantity(quantity);
        this.ValidateImageUrl(imageUrl);
        this.ValidateDescription(description);
    }

    private void ValidateTitle(string title)
        => Guard.ForStringLength<InvalidBookException>(
            title,
            MinNameLength,
            MaxNameLength,
            nameof(this.Title));

    private void ValidatePrice(decimal price)
        => Guard.AgainstOutOfRange<InvalidBookException>(
            price,
            MinPriceValue,
            MaxPriceValue,
            nameof(this.Price));

    private void ValidateQuantity(int quantity)
        => Guard.AgainstOutOfRange<InvalidBookException>(
            quantity,
            MinQuantityValue,
            MaxQuantityValue,
            nameof(this.Quantity));

    private void ValidateImageUrl(string imageUrl)
        => Guard.ForValidUrl<InvalidBookException>(
            imageUrl,
            nameof(this.ImageUrl));

    private void ValidateDescription(string description)
        => Guard.ForStringLength<InvalidBookException>(
            description,
            MinDescriptionLength,
            MaxDescriptionLength,
            nameof(this.Description));
}