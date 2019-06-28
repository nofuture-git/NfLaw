﻿using System;
using System.Collections.Generic;

namespace NoFuture.Rand.Law.Property.US.Found
{
    /// <summary>
    /// Currency of some kind hidden in something, typically walls or the earth.
    /// Often the owner is long dead.
    /// </summary>
    public class TreasureTrove : PropertyBase
    {
        public TreasureTrove(Func<IEnumerable<ILegalPerson>, ILegalPerson> getSubjectPerson) : base(getSubjectPerson) { }

        public Predicate<ILegalProperty> IsConcealedLocation { get; set; } = p => false;

        public Predicate<ILegalProperty> IsGoldSilverOrCurrency { get; set; } = p => false;

        public override bool IsValid(params ILegalPerson[] persons)
        {
            if (base.PropertyOwnerIsInPossession(persons))
                return false;

            if (!IsConcealedLocation(SubjectProperty))
            {
                AddReasonEntry($"{nameof(SubjectProperty)} named '{SubjectProperty.Name}', {nameof(IsConcealedLocation)} is false");
                return false;
            }

            if (!IsGoldSilverOrCurrency(SubjectProperty))
            {
                AddReasonEntry($"{nameof(SubjectProperty)} named '{SubjectProperty.Name}', {nameof(IsGoldSilverOrCurrency)} is false");
                return false;
            }

            return true;
        }
    }
}
