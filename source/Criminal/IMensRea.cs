﻿using System;
using NoFuture.Law.Attributes;

namespace NoFuture.Law.Criminal
{
    /// <summary>
    /// the willful intent to do harm
    /// </summary>
    [Aka("criminal intent", "vicious will")]
    [EtymologyNote("Latin", "mens rea", "guilty mind")]
    public interface IMensRea : IIntent, IComparable
    {
        /// <summary>
        /// Determines if this criminal intent is valid 
        /// when combined with the particular <see cref="criminalAct"/>
        /// </summary>
        bool CompareTo(IActusReus criminalAct, params ILegalPerson[] persons);
    }
}
