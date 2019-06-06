﻿using System;
using NoFuture.Rand.Law.US;

namespace NoFuture.Rand.Law
{
    public interface IAct : ILegalConcept
    {
        /// <summary>
        /// There must always be something willfully
        /// </summary>
        Predicate<ILegalPerson> IsVoluntary { get; set; }

        /// <summary>
        /// There must be some outward act or failure to act 
        /// (thoughts, plans, labels, status are not actions).
        /// </summary>
        Predicate<ILegalPerson> IsAction { get; set; }

        /// <summary>
        /// There must be some outward act or failure to act 
        /// (thoughts, plans, labels, status are not actions).
        /// </summary>
        Duty Duty { get; set; }
    }
}