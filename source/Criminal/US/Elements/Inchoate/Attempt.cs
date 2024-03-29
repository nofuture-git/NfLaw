﻿using System;
using NoFuture.Law.Attributes;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US;

namespace NoFuture.Law.Criminal.US.Elements.Inchoate
{
    /// <summary>
    /// a crime that has only just begun which may be defined by statute or common law
    /// </summary>
    public class Attempt : LegalConcept, IActusReus
    {
        /// <summary>
        /// A measure of criminal effort left to be done, not a measure of what is already done.
        /// </summary>
        public Predicate<ILegalPerson> IsProximity { get; set; } = lp => false;

        /// <summary>
        /// <![CDATA[ "thing itself speaks," meaning its clear that there is not other purpose but criminal ]]>
        /// </summary>
        [Aka("unequivocal test")]
        [EtymologyNote("Latin", "res ipsa loquitur", "thing itself speaks")]
        public Predicate<ILegalPerson> IsResIpsaLoquitur { get; set; } = lp => false;

        /// <summary>
        /// a point of progress where its probable that the crime will happen unless some exogenous force intervenes
        /// </summary>
        public Predicate<ILegalPerson> IsProbableDesistance { get; set; } = lp => false;

        /// <summary>
        /// Some kind of action which corroborates a proposition of intent - evidence of a suspicion
        /// </summary>
        public SubstantialSteps SubstantialSteps { get; set; }

        /// <summary>
        /// Attempt is not applicable to reckless or negligent intent
        /// </summary>
        public bool CompareTo(IMensRea criminalIntent, params ILegalPerson[] persons)
        {
            var intent = (criminalIntent as DeadlyWeapon)?.UtilizedWith ?? criminalIntent;
            var isInvalid2Attempt = intent is Recklessly || intent is Negligently;
            if (isInvalid2Attempt)
            {
                AddReasonEntry("generally, no such thing exists as reckless or negligent attempt");
                return false;
            }

            return true;
        }

        public override bool IsValid(params ILegalPerson[] persons)
        {
            var defendant = this.Defendant(persons);
            if (defendant == null)
                return false;
            var title = defendant.GetLegalPersonTypeName();
            var ispx = IsProximity(defendant);
            var ispd = IsProbableDesistance(defendant);
            var isril = IsResIpsaLoquitur(defendant);

            var isssub = SubstantialSteps?.IsValid(persons) ?? false;
            AddReasonEntryRange(SubstantialSteps?.GetReasonEntries());

            var isAttempt = ispx || ispd || isril || isssub;
            if (!isAttempt)
            {
                AddReasonEntry($"{title} {defendant.Name}, is attempt false");
                return false;
            }

            return true;
        }
    }
}
