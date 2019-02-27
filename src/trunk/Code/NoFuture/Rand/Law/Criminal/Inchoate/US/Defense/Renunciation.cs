﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoFuture.Rand.Law.Criminal.US;
using NoFuture.Rand.Law.Criminal.US.Defense;
using NoFuture.Rand.Law.Criminal.Inchoate.US.Elements;

namespace NoFuture.Rand.Law.Criminal.Inchoate.US.Defense
{
    /// <summary>
    /// an affirmative defense for <see cref="Conspiracy"/>
    /// </summary>
    public class Renunciation : DefenseBase
    {
        public Renunciation(ICrime crime) : base(crime)
        {
        }

        public Predicate<ILegalPerson> IsVoluntarily { get; set; } = lp => false;
        public Predicate<ILegalPerson> IsCompletely { get; set; } = lp => false;

        /// <summary>
        /// The conspiracy is a plan to commit some crime which 
        /// is called the object of the conspiracy.  It is this 
        /// crime that must have been thwarted.
        /// </summary>
        public Predicate<ILegalPerson> IsResultCrimeThwarted { get; set; } = lp => false;

        public override bool IsValid(params ILegalPerson[] persons)
        {
            var defendant = Crime.GetDefendant(persons);
            if (defendant == null)
                return false;

            if (!TestIsActusReusOfType(typeof(Conspiracy), typeof(Solicitation)))
                return false;

            if (!IsCompletely(defendant))
            {
                AddReasonEntry($"defendant, {defendant.Name}, {nameof(IsCompletely)} is false");
                return false;
            }
            if (!IsVoluntarily(defendant))
            {
                AddReasonEntry($"defendant, {defendant.Name}, {nameof(IsVoluntarily)} is false");
                return false;
            }
            if (!IsResultCrimeThwarted(defendant))
            {
                AddReasonEntry($"defendant, {defendant.Name}, {nameof(IsResultCrimeThwarted)} is false");
                return false;
            }

            return true;
        }
    }
}