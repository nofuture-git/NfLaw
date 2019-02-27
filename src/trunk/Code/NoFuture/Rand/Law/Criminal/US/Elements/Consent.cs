﻿using System;
using System.Linq;

namespace NoFuture.Rand.Law.Criminal.US.Elements
{
    /// <summary>
    /// the inability or firm denial of willingness to engage in intercourse
    /// </summary>
    public class Consent : AttendantCircumstances, IConsent
    {
        public Predicate<ILegalPerson> IsCapableThereof { get; set; } = lp => false;

        /// <summary>
        /// This is the typical place where prosecution\defense battle
        /// </summary>
        public Predicate<ILegalPerson> IsDenialExpressed { get; set; } = lp => true;

        public override bool IsValid(params ILegalPerson[] persons)
        {
            if (persons == null || !persons.Any())
            {
                AddReasonEntry($"{nameof(persons)} is null or empty");
                return false;
            }

            foreach (var person in persons)
            {
                var victim = person as IVictim;
                if(victim == null)
                    continue;

                if (!IsCapableThereof(victim))
                {
                    AddReasonEntry($"victim, {victim.Name}, {nameof(IsCapableThereof)} is false");
                    return false;
                }

                if (IsDenialExpressed(victim))
                {
                    AddReasonEntry($"victim, {victim.Name}, {nameof(IsDenialExpressed)} is true");
                    return false;
                }
                AddReasonEntry($"victim, {victim.Name}, {nameof(IsDenialExpressed)} is false");
            }

            return true;
        }
    }
}
