﻿using NoFuture.Law.Attributes;

namespace NoFuture.Law.US.Persons
{
    /// <summary>
    /// The defendant against some <see cref="IImpleader"/> claim.
    /// Where the claim is derived from some original <see cref="IPlaintiff"/> claim.
    /// </summary>
    [Aka("third-party defendant")]
    public interface IImpleadee : IThirdParty, IDefendant
    {
    }
}
