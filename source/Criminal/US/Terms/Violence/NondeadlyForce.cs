﻿namespace NoFuture.Law.Criminal.US.Terms.Violence
{
    /// <inheritdoc cref="IForce" />
    /// <summary>
    /// Any force which does not have the potential to kill.
    /// </summary>
    public class NondeadlyForce : TermCategory, IForce
    {
        protected override string CategoryName { get; } = "non-deadly force";
    }
}