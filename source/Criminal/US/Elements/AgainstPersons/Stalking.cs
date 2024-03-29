﻿using System;
using System.Collections.Generic;
using System.Linq;
using NoFuture.Law.Criminal.US.Elements.AgainstPersons.Credible;
using NoFuture.Law.US;

namespace NoFuture.Law.Criminal.US.Elements.AgainstPersons
{
    /// <summary>
    /// Intended to punish conduct that is a precursor to assault, battery, or other crimes
    /// </summary>
    /// <remarks>
    /// Examples include harassing, approaching, pursuing, making explicit or implicit threat
    /// </remarks>
    public class Stalking : LegalConcept, IActusReus, IDominionOfForce, IElement
    {
        /// <summary>
        /// is unique among criminal acts in that it must occur on more than one occasion or repeatedly
        /// </summary>
        public IEnumerable<IAgitate> Occasions { get; set; } = new List<IAgitate>();

        public Predicate<ILegalPerson> IsPresentAbility { get; set; } = lp => false;
        public Predicate<ILegalPerson> IsApparentAbility { get; set; } = lp => false;

        public override bool IsValid(params ILegalPerson[] persons)
        {
            var defendant = this.Defendant(persons);
            if (defendant == null || Occasions == null)
                return false;
            var title = defendant.GetLegalPersonTypeName();
            var ipa = IsPresentAbility(defendant);
            var iaa = IsApparentAbility(defendant);

            if (!ipa && !iaa)
            {
                AddReasonEntry($"{title} {defendant.Name}, {nameof(IsPresentAbility)} is false");
                AddReasonEntry($"{title} {defendant.Name}, {nameof(IsApparentAbility)} is false");
                return false;
            }

            var numOccurances = Occasions.Count();
            if (numOccurances <= 1)
            {
                AddReasonEntry($"{title} {defendant.Name}, {nameof(Occasions)} count is {numOccurances}");
                return false;
            }

            numOccurances = 0;
            foreach (var agitate in Occasions)
            {
                var isAgitateValid = agitate?.IsValid(persons) ?? false;
                if (isAgitateValid)
                    numOccurances += 1;
                AddReasonEntryRange(agitate?.GetReasonEntries());
            }

            return numOccurances > 1;
        }

        public bool CompareTo(IMensRea criminalIntent, params ILegalPerson[] persons)
        {
            return true;
        }

    }
}
