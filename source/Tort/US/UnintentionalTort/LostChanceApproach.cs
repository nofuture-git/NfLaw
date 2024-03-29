﻿using System;
using NoFuture.Law.Attributes;
using NoFuture.Law.US;

namespace NoFuture.Law.Tort.US.UnintentionalTort
{
    [Aka("loss-of-chance")]
    public class LostChanceApproach : UnoHomine, INegligence
    {
        public LostChanceApproach(Func<ILegalPerson[], ILegalPerson> getSubjectPerson) : base(getSubjectPerson)
        {
        }

        public Func<ILegalPerson, double> GetLostProbability { get; set; } = lp => 1D;

        public Func<ILegalPerson, double> GetActualProbability { get; set; } = lp => 1D;

        public IFactualCause<ILegalPerson> FactualCause { get; set; }

        public override bool IsValid(params ILegalPerson[] persons)
        {
            var subj = GetSubjectPerson(persons);
            if (subj == null)
            {
                AddReasonEntry($"{nameof(GetSubjectPerson)} returned nothing");
                return false;
            }
            var title = subj.GetLegalPersonTypeName();

            if (FactualCause == null)
            {
                AddReasonEntry($"{title} {subj.Name}, {nameof(FactualCause)} is unassigned");
                return false;
            }

            if (!FactualCause.IsValid(persons))
            {
                AddReasonEntryRange(FactualCause.GetReasonEntries());
                return false;
            }

            var plaintiff = this.Plaintiff(persons);
            if (plaintiff == null)
                return false;

            var expected = GetLostProbability(plaintiff);
            var actual = GetActualProbability(plaintiff);

            if (expected <= actual)
            {
                AddReasonEntry($"{plaintiff.GetLegalPersonTypeName()} {plaintiff.Name}, {nameof(GetLostProbability)} " +
                               $"of {expected} is less-than-equal-to {nameof(GetActualProbability)} of {actual}");
                return false;
            }

            return true;
        }
    }
}
