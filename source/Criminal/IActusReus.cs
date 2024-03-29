﻿using NoFuture.Law.Attributes;

namespace NoFuture.Law.Criminal
{
    /// <summary>
    /// Action or conduct which is constituent element of a crime
    /// </summary>
    [Aka("criminal act", "volition")]
    [EtymologyNote("Latin", "actus reus", "guilty act")]
    public interface IActusReus : ILegalConcept
    {
        /// <summary>
        /// Determines if this criminal act is valid 
        /// when combined with the particular <see cref="criminalIntent"/>
        /// </summary>
        bool CompareTo(IMensRea criminalIntent, params ILegalPerson[] persons);
    }
}
