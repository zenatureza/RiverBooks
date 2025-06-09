using System.ComponentModel.DataAnnotations.Schema;
using RiverBooks.SharedKernel;

namespace RiverBooks.OrderProcessing.Domain;

internal class Order : IHaveDomainEvents
{
  public Guid Id { get; init; } = Guid.NewGuid();
  public Guid UserId { get; private set; }
  public Address ShippingAddress { get; private set; } = default!;
  public Address BillingAddress { get; private set; } = default!;
  private readonly List<OrderItem> _orderItems = [];
  public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
  private List<DomainEventBase> _domainEvents = [];
  [NotMapped]
  public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

  protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
  void IHaveDomainEvents.ClearDomainEvents() => _domainEvents.Clear();
  public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.UtcNow;

  private void AddOrderItem(OrderItem item) => _orderItems.Add(item);

  internal class Factory
  {
    public static Order Create(
      Guid userId,
      Address shippingAddress,
      Address billingAddress,
      IEnumerable<OrderItem> items)
    {
      var order = new Order
      {
        UserId = userId,
        ShippingAddress = shippingAddress,
        BillingAddress = billingAddress
      };
      foreach (var item in items)
      {
        order.AddOrderItem(item);
      }

      var createdEvent = new OrderCreatedEvent(order);
      order.RegisterDomainEvent(createdEvent);
      return order;
    }
  }
}
