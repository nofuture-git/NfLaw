﻿using System;
using System.Linq;
using NoFuture.Law.Attributes;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US;

namespace NoFuture.Law.Criminal.US.Elements.AgainstPublic
{
    /// <summary>
    /// criminal conduct that negatively impacts the quality of
    /// life for citizens in any given city, county or state.
    /// </summary>
    [Aka("disturbing the peace")]
    public class DisorderlyConduct: LegalConcept, IActusReus
    {
        /// <summary>
        /// a loud and unreasonable noise given the context-setting (e.g. library v. city street)
        /// </summary>
        public Predicate<ILegalPerson> IsUnreasonablyLoud { get; set; } = lp => false;

        /// <summary>
        /// obscene utterance or gesture
        /// </summary>
        public Predicate<ILegalPerson> IsObscene { get; set; } = lp => false;

        /// <summary>
        /// engage in fighting or threatening, or state fighting words
        /// </summary>
        public Predicate<ILegalPerson> IsCombative { get; set; } = lp => false;

        /// <summary>
        /// A situation that is dangerous and poses a risk to others in
        /// the vicinity of the defendant&apos;s conduct
        /// </summary>
        public Predicate<ILegalPerson> IsIllegitimateHazardous { get; set; } = lp => false;

        public override bool IsValid(params ILegalPerson[] persons)
        {
            var defendant = this.Defendant(persons);
            if (defendant == null)
                return false;
            var title = defendant.GetLegalPersonTypeName();
            var loud = IsUnreasonablyLoud(defendant);
            var obscene = IsObscene(defendant);
            var combative = IsCombative(defendant);
            var hazard = IsIllegitimateHazardous(defendant);

            if (new[] {loud, obscene, combative, hazard}.All(p => false))
            {
                AddReasonEntry($"{title} {defendant.Name}, {nameof(IsUnreasonablyLoud)}, " +
                               $"{nameof(IsObscene)}, {nameof(IsCombative)} " +
                               $"and {nameof(IsIllegitimateHazardous)} is false");
                return false;
            }

            return true;
        }

        public virtual bool CompareTo(IMensRea criminalIntent, params ILegalPerson[] persons)
        {
            var validIntent = criminalIntent is Recklessly || criminalIntent is Purposely ||
                              criminalIntent is SpecificIntent;
            if (!validIntent)
            {
                AddReasonEntry($"{nameof(DisorderlyConduct)} requires intent " +
                               $"of {nameof(Recklessly)}, {nameof(Purposely)}, " +
                               $"{nameof(SpecificIntent)}");
                return false;
            }

            return true;
        }
    }
}
