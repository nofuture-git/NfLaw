﻿using System;
using NoFuture.Rand.Law.Attributes;
using NoFuture.Rand.Law.Criminal.US.Elements.Intent;
using NoFuture.Rand.Law.Criminal.US.Elements.Intent.ComLaw;

namespace NoFuture.Rand.Law.Criminal.Homicide.US.Elements
{
    /// <summary>
    /// A kind of specific intent to murder that is explosion of 
    /// sudden indignation.
    /// </summary>
    /// <remarks>
    /// words alone are not enought to constitute adequate provocation 
    /// </remarks>
    [Aka("heat of passion")]
    public class AdequateProvocation : MensRea, IHomicideConcurrance
    {
        public DateTime Inception { get; set; }
        public DateTime? Terminus { get; set; }
        public DateTime? TimeOfTheDeath { get; set; }

        /// <summary>
        /// an objective person would be incited into killing 
        /// </summary>
        public ObjectivePredicate<ILegalPerson> IsReasonableToInciteKilling { get; set; } = lp => false;

        /// <summary>
        /// The defendant must actually be provoked 
        /// </summary>
        public SubjectivePredicate<ILegalPerson> IsDefendantActuallyProvoked { get; set; } = lp => false;

        public override bool IsValid(params ILegalPerson[] persons)
        {
            var defendant = GetDefendant(persons);
            if (defendant == null)
                return false;

            if (!IsReasonableToInciteKilling(defendant))
            {
                AddReasonEntry($"defendant, {defendant.Name}, {nameof(IsReasonableToInciteKilling)} is false");
                return false;
            }

            if (!IsDefendantActuallyProvoked(defendant))
            {
                AddReasonEntry($"defendant, {defendant.Name}, {nameof(IsDefendantActuallyProvoked)} is false");
                return false;
            }

            if (!Murder.IsHomicideConcurrance(this, this, defendant.Name))
                return false;

            return true;
        }

        public override int CompareTo(object obj)
        {
            return new SpecificIntent().CompareTo(obj);
        }

        public bool IsInRange(DateTime dt)
        {
            var afterOrOnFromDt = Inception <= dt;
            var beforeOrOnToDt = Terminus == null || Terminus.Value >= dt;
            return afterOrOnFromDt && beforeOrOnToDt;
        }
    }
}
