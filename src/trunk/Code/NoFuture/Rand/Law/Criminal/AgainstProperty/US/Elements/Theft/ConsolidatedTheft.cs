﻿using System;
using System.Linq;
using NoFuture.Rand.Core;
using NoFuture.Rand.Law.Criminal.US;
using NoFuture.Rand.Law.Criminal.US.Elements.Act;
using NoFuture.Rand.Law.Criminal.US.Elements.Intent;

namespace NoFuture.Rand.Law.Criminal.AgainstProperty.US.Elements.Theft
{
    /// <summary>
    /// Explanatory Note for Sections 223.1-223.9 of Model Penal Code
    /// </summary>
    public abstract class ConsolidatedTheft : CriminalBase, IActusReus
    {
        public virtual ILegalProperty SubjectOfTheft { get; set; }
        public virtual decimal? AmountOfTheft { get; set; }
        public virtual IConsent Consent { get; set; }

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
            var defendant = GetDefendant(persons);
            if (defendant == null)
                return false;

            if (SubjectOfTheft == null)
            {
                AddReasonEntry($"defendant, {defendant.Name}, {nameof(SubjectOfTheft)} is null");
                return false;
            }

            if (VocaBase.Equals(SubjectOfTheft.EntitledTo, defendant))
            {
                AddReasonEntry($"defendant, {defendant.Name}, is the owner of property {SubjectOfTheft}");
                return false;
            }

            if (!GetConsent(persons))
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
            var defendant = GetDefendant(persons);
            if (defendant == null)
                return false;
            var isPossess = IsTakenPossession(defendant);
            if (isPossess)
                SubjectOfTheft.InPossessionOf = defendant;

            var isTitled = IsAcquiredTitle(defendant);
            if (isTitled)
                SubjectOfTheft.EntitledTo = defendant;

            if (!isPossess && !isTitled)
            {
                AddReasonEntry($"defendant, {defendant.Name}, {nameof(IsTakenPossession)} is false");
                AddReasonEntry($"defendant, {defendant.Name}, {nameof(IsAcquiredTitle)} is false");
                return false;
            }

            return true;
        }

        /// <summary>
        /// The expected result of <see cref="Consent"/> - default
        /// is false (i.e. the thief did not have consent to take such-and-such).
        /// </summary>
        protected virtual bool ConsentExpectedAs { get; set; } = false;

        /// <summary>
        /// Tests that <see cref="Consent"/> was not given by <see cref="SubjectOfTheft"/> owner
        /// </summary>
        /// <param name="persons"></param>
        /// <returns></returns>
        protected virtual bool GetConsent(ILegalPerson[] persons)
        {
            //is all the dependencies present
            if (SubjectOfTheft?.EntitledTo == null || Consent == null 
                                                   || persons == null 
                                                   || !persons.Any())
                return true;

            //did the caller pass in any IVictim types
            var victims = persons.Where(lp => lp is IVictim).ToList();
            if (!victims.Any())
                return true;

            //is any of our victims also the owner of the property
            var ownerVictims = victims.Where(v => VocaBase.Equals(v, SubjectOfTheft.EntitledTo)).ToList();
            if (!ownerVictims.Any())
                return true;

            foreach (var ownerVictim in ownerVictims)
            {
                var validConsent = Consent.IsValid(ownerVictim);
                //did the owner victim in fact give consent 
                if (validConsent != ConsentExpectedAs)
                {
                    AddReasonEntry($"owner-victim {ownerVictim.Name}, {nameof(Consent)} {nameof(IsValid)} " +
                                   $"is {validConsent}, it was expected to be {ConsentExpectedAs} " +
                                   $"for property {SubjectOfTheft}");
                    return false;
                }
            }

            return true;
        }

        protected virtual bool TryGetPossesorOfProperty(out ILegalPerson possessor)
        {
            possessor = SubjectOfTheft?.InPossessionOf;
            if (possessor == null)
            {
                AddReasonEntry($"the {nameof(SubjectOfTheft)}, {nameof(SubjectOfTheft.InPossessionOf)} is null");
                return false;
            }
            return true;
        }
    }
}
