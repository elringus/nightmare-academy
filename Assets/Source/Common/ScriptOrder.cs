using System;

/// <summary>
/// Sets Unity script execution order.
/// </summary>
public class ScriptOrder : Attribute
{
    public readonly int Order;

    public ScriptOrder (int order)
    {
        Order = order;
    }
}
