﻿using System;
using System.Linq;
using NoFuture.Law.Attributes;
using NoFuture.Law.US;

namespace NoFuture.Law.Criminal.US.Elements.AttendantCircumstances
{
    /// <summary>
    /// the inability or firm denial of willingness to engage
    /// </summary>
    [Note("this is an oxymoron")]
    public class VictimConsent : AttendantCircumstanceBase, IConsent
    {
        public Predicate<ILegalPerson> IsCapableThereof { get; set; } = lp => false;

        /// <summary>
        /// This is the typical place where prosecution\defense battle
        /// </summary>
        public Predicate<ILegalPerson> IsApprovalExpressed { get; set; } = lp => true;

        public override bool IsValid(params ILegalPerson[] persons)
        {
            if (persons == null || !persons.Any())
            {
                AddReasonEntry($"{nameof(persons)} is null or empty");
                return false;
            }

            foreach (var victim in persons.Victims())
            {
                var title = victim.GetLegalPersonTypeName();
                if (!IsCapableThereof(victim))
                {
                    AddReasonEntry($"{title} {victim.Name}, {nameof(IsCapableThereof)} is false");
                    return false;
                }

                if (!IsApprovalExpressed(victim))
                {
                    AddReasonEntry($"{title} {victim.Name}, {nameof(IsApprovalExpressed)} is false");
                    return false;
                }
            }

            return true;
        }
    }
}
