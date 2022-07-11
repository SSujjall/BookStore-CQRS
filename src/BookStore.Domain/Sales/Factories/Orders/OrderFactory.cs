﻿namespace BookStore.Domain.Sales.Factories.Orders;

using System;
using Exceptions;
using Models.Customers;
using Models.Orders;

internal class OrderFactory : IOrderFactory
{
    private Customer orderCustomer = default!;
    private readonly State defaultOrderState = State.Pending;
    private readonly DateTime defaultOrderDate = DateTime.UtcNow;

    private bool isCustomerSet = false;

    public IOrderFactory ForCustomer(Customer customer)
    {
        this.orderCustomer = customer;
        this.isCustomerSet = true;

        return this;
    }

    public Order Build()
    {
        if (!this.isCustomerSet)
        {
            throw new InvalidOrderException("Customer must have a value.");
        }

        return new Order(
            this.defaultOrderDate,
            this.defaultOrderState,
            this.orderCustomer);
    }
}