﻿using System;
using System.Collections.Generic;
using NoFuture.Rand.Law.US.Criminal.Elements;

namespace NoFuture.Rand.Law.US.Criminal
{
    public abstract class CrimeBase : ObjectiveLegalConcept, ICrime
    {
        public override bool IsValid(ILegalPerson offeror = null, ILegalPerson offeree = null)
        {
            ClearReasons();
            var defendant = Government.GetDefendant(offeror, offeree, this);
            if (defendant == null)
                return false;

            if (!IsChargedWith(defendant))
            {
                AddReasonEntry($"there are no charges against {defendant.Name}");
                return false;
            }

            if (Concurrence == null)
            {
                AddReasonEntry($"{nameof(Concurrence)} is missing");
                return false;
            }

            if (!Concurrence.IsValid(offeror, offeree))
            {
                AddReasonEntry($"{nameof(Concurrence)} is invalid");
                AddReasonEntryRange(Concurrence.GetReasonEntries());
                return false;
            }

            foreach (var elem in AdditionalElements)
            {
                if (!elem.IsValid(offeror, offeree))
                {
                    AddReasonEntryRange(elem.GetReasonEntries());
                    return false;
                }
            }

            return true;
        }

        public abstract int CompareTo(object obj);

        public Concurrence Concurrence { get; set; } = new Concurrence();

        /// <summary>
        /// A enclosure to describe the specific charges
        /// </summary>
        public virtual Predicate<ILegalPerson> IsChargedWith { get; set; } = lp => true;

        /// <summary>
        /// A short hand property to the same stack-residing variable in <see cref="Concurrence"/>
        /// </summary>
        public ActusReus ActusReus
        {
            get => Concurrence.ActusReus;
            set => Concurrence.ActusReus = value;
        }

        /// <summary>
        /// A short hand property to the same stack-residing variable in <see cref="Concurrence"/>
        /// </summary>
        public MensRea MensRea
        {
            get => Concurrence.MensRea;
            set => Concurrence.MensRea = value;
        }

        public IList<IElement> AdditionalElements { get; } = new List<IElement>();
    }
}