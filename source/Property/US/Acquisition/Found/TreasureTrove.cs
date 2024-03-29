﻿using System;
using System.Collections.Generic;
using NoFuture.Law.Property.US.FormsOf;

namespace NoFuture.Law.Property.US.Acquisition.Found
{
    /// <summary>
    /// Currency of some kind hidden in something, typically walls or the earth.
    /// Often the owner is long dead.
    /// </summary>
    public class TreasureTrove : PropertyBase
    {
        public TreasureTrove(Func<IEnumerable<ILegalPerson>, ILegalPerson> getSubjectPerson) : base(getSubjectPerson) { }

        public Predicate<ILegalProperty> IsConcealed { get; set; } = p => false;

        public Predicate<ILegalProperty> IsGoldSilverOrCurrency { get; set; } = p => false;

        public override bool IsValid(params ILegalPerson[] persons)
        {
            if (base.PropertyOwnerIsInPossession(persons))
                return false;

            if (!IsConcealed(SubjectProperty))
            {
                AddReasonEntry($"{nameof(SubjectProperty)} named '{SubjectProperty.Name}', {nameof(IsConcealed)} is false");
                return false;
            }

            if (!IsGoldSilverOrCurrency(SubjectProperty))
            {
                AddReasonEntry($"{nameof(SubjectProperty)} named '{SubjectProperty.Name}', {nameof(IsGoldSilverOrCurrency)} is false");
                return false;
            }

            SubjectProperty = new ResDerelictae(SubjectProperty) { IsEntitledTo = lp => false, IsInPossessionOf = lp => false };

            return true;
        }
    }
}
