﻿using System;
using NoFuture.Law.Property.US;
using NoFuture.Law.US;

namespace NoFuture.Law.Criminal.US.Elements.AgainstProperty.Theft
{
    /// <summary>
    /// Explanatory Note for Sections 223.1-223.9 of Model Penal Code
    /// </summary>
    public abstract class ConsolidatedTheft : PropertyConsent, IActusReus
    {
        public ConsolidatedTheft() : base(ExtensionMethods.Defendant) { }

        /// <summary>
        /// The typical idea of theft as grab and run stealing, or, more 
        /// generally as the idea that control of the property has been taken unlawfully
        /// </summary>
        public Predicate<ILegalPerson> IsTakenPossession { get; set; } = lp => false;

        /// <summary>
        /// When the thief has acquired some kind of legal entitlement over the property
        /// </summary>
        public Predicate<ILegalPerson> IsAcquiredTitle { get; set; } = lp => false;

        public override bool IsValid(params ILegalPerson[] persons)
        {
            var defendant = this.Defendant(persons);
            if (defendant == null)
                return false;
            var title = defendant.GetLegalPersonTypeName();
            if (SubjectProperty == null)
            {
                AddReasonEntry($"{title} {defendant.Name}, {nameof(SubjectProperty)} is null");
                return false;
            }

            if (SubjectProperty.IsEntitledTo(defendant))
            {
                AddReasonEntry($"{title} {defendant.Name}, is the owner of property {SubjectProperty}");
                return false;
            }

            if (!WithoutConsent(persons))
                return false;

            if (!PossessOrEntitle(persons))
                return false;

            return true;
        }

        public virtual bool CompareTo(IMensRea criminalIntent, params ILegalPerson[] persons)
        {
            return true;
        }

        protected internal bool PossessOrEntitle(ILegalPerson[] persons)
        {
            var defendant = this.Defendant(persons);
            if (defendant == null)
                return false;
            var title = defendant.GetLegalPersonTypeName();
            var isPossess = IsTakenPossession(defendant);
            if (isPossess)
                SubjectProperty.IsInPossessionOf = lp => lp.IsSamePerson(defendant);

            var isTitled = IsAcquiredTitle(defendant);
            if (isTitled)
                SubjectProperty.IsEntitledTo = lp => lp.IsSamePerson(defendant);

            if (!isPossess && !isTitled)
            {
                AddReasonEntry($"{title} {defendant.Name}, {nameof(IsTakenPossession)} is false");
                AddReasonEntry($"{title} {defendant.Name}, {nameof(IsAcquiredTitle)} is false");
                return false;
            }

            return true;
        }
    }
}
