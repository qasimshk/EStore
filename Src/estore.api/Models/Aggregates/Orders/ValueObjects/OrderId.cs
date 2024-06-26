namespace estore.api.Models.Aggregates.Orders.ValueObjects;

using System.Collections.Generic;
using estore.api.Common;
using estore.api.Extensions;

public sealed class OrderId : ValueObject
{
    public int Value { get; }

    protected OrderId() { }

    public OrderId(int value) => Value = value;

    public static OrderId CreateUnique() => new(Guid.NewGuid().GuidToInteger());

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
