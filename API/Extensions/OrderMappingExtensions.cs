using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using Core.Entities.OrderAggregate;

namespace API.Extensions
{
   public static class OrderMappingExtensions
{
 public static OrderDto ToDto(this Order order)
{
    try
    {
        if (order == null) throw new ArgumentNullException(nameof(order));

        // Check each property individually with a descriptive error message
        if (string.IsNullOrEmpty(order.BuyerEmail))
            throw new InvalidOperationException($"Order {order.Id}: BuyerEmail is null or empty");

        if (order.ShippingAddress == null)
            throw new InvalidOperationException($"Order {order.Id}: ShippingAddress is null");

        if (order.DeliveryMethod == null)
            throw new InvalidOperationException($"Order {order.Id}: DeliveryMethod is null");

        if (order.PaymentSummary == null)
            throw new InvalidOperationException($"Order {order.Id}: PaymentSummary is null");

        if (order.OrderItems == null)
            throw new InvalidOperationException($"Order {order.Id}: OrderItems is null");

        // Check each OrderItem and its ItemOrdered
        var orderItems = order.OrderItems.Select(item =>
        {
            if (item == null)
                throw new InvalidOperationException($"Order {order.Id}: Contains a null OrderItem");
            
            if (item.ItemOrdered == null)
                throw new InvalidOperationException($"Order {order.Id}: OrderItem contains null ItemOrdered");
            
            return item.ToDto();
        }).ToList();

        if (string.IsNullOrEmpty(order.PaymentIntentId))
            throw new InvalidOperationException($"Order {order.Id}: PaymentIntentId is null or empty");

        // If all checks pass, create the DTO
        return new OrderDto
        {
            Id = order.Id,
            BuyerEmail = order.BuyerEmail,
            OrderDate = order.OrderDate,
            ShippingAddress = order.ShippingAddress,
            PaymentSummary = order.PaymentSummary,
            DeliveryMethod = order.DeliveryMethod.Description ?? "Not specified",
            ShippingPrice = order.DeliveryMethod.Price,
            OrderItems = orderItems,
            Subtotal = order.Subtotal,
            Total = order.GetTotal(),
            Status = order.Status.ToString(),
            PaymentIntentId = order.PaymentIntentId
        };
    }
    catch (Exception ex)
    {
        // Wrap any exception with order context
        throw new Exception($"Error mapping order {order?.Id}: {ex.Message}", ex);
    }
}

   public static OrderItemDto ToDto(this OrderItem orderItem)
{
    if (orderItem == null) throw new ArgumentNullException(nameof(orderItem));
    if (orderItem.ItemOrdered == null) throw new InvalidOperationException("ItemOrdered is null");

    return new OrderItemDto
    {
        ProductId = orderItem.ItemOrdered.ProductId,
        ProductName = orderItem.ItemOrdered.ProductName,
        PictureUrl = orderItem.ItemOrdered.PictureUrl,
        Price = orderItem.Price,
        Quantity = orderItem.Quantity
    };
}
}
}