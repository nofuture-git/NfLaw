﻿using System;
using NoFuture.Law.Criminal.US.Elements.Inchoate;
using NoFuture.Law.US;

namespace NoFuture.Law.Criminal.US.Defense.Excuse
{
    /// <summary>
    /// A defense to Attempt
    /// </summary>
    public class Impossibility : InchoateDefenseBase
    {
        public Impossibility(ICrime crime) : base(crime)
        {
        }

        /// <summary>
        /// the defendant believes what they are doing is illegal but its not
        /// </summary>
        public Predicate<ILegalPerson> IsLegalImpossibility { get; set; } = lp => false;

        /// <summary>
        /// the crime failed because the facts are not as he or she believes them to be
        /// </summary>
        public virtual Predicate<ILegalPerson> IsFactualImpossibility { get; set; } = lp => false;

        public override bool IsValid(params ILegalPerson[] persons)
        {
            var defendant = this.Defendant(persons);
            if (defendant == null)
                return false;

            if (!TestIsActusReusOfType(typeof(Attempt)))
                return false;

            if (IsFactualImpossibility(defendant))
            {
                AddReasonEntry($"defendant, {defendant.Name}, {nameof(IsFactualImpossibility)} is true");
                AddReasonEntry("factual impossibility as a defense does not work");
            }

            if (!IsLegalImpossibility(defendant))
            {
                AddReasonEntry($"defendant, {defendant.Name}, {nameof(IsLegalImpossibility)} is false");
                return false;
            }

            return true;
        }
    }
}
