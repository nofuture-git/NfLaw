﻿using System;
using NoFuture.Rand.Law.Attributes;

namespace NoFuture.Rand.Law.US.Contracts
{
    /// <inheritdoc />
    [Note("Latin assimilation of 'com' (with, together) + 'trahere' (to pull, drag)")]
    public interface IContract<T> : IObjectiveLegalConcept
    {
        [Note("Is the manifestation of willingness to enter into a bargain")]
        IObjectiveLegalConcept Offer { get; set; }

        Func<IObjectiveLegalConcept, T> Acceptance { get; set; }
    }
}
