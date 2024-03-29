﻿using System;
using NoFuture.Law.Tort.US.Terms;
using NoFuture.Law.US;

namespace NoFuture.Law.Tort.US.UnintentionalTort
{
    /// <inheritdoc cref="INegligence"/>
    public class Negligence : UnoHomine, INegligence
    {
        public Negligence(Func<ILegalPerson[], ILegalPerson> getSubjectPerson) : base(getSubjectPerson)
        {
        }

        /// <summary>
        /// defendant&apos;s conduct was morally wrong according to prevailing community norms
        /// </summary>
        public Predicate<ILegalPerson> IsConductMorallyWrong { get; set; } = lp => false;

        /// <summary>
        /// whether the defendant breached a safety convention commonly understood
        /// in the community to protect the kinds of people like the plaintiff
        /// </summary>
        public CustomsTerm SafetyConvention { get; set; }

        /// <summary>
        /// The connection of a person to the cause in both fact and law.
        /// </summary>
        public Causation Causation { get; set; }

        public override bool IsValid(params ILegalPerson[] persons)
        {
            var subj = GetSubjectPerson(persons);
            if (subj == null)
            {
                AddReasonEntry($"{nameof(GetSubjectPerson)} returned nothing");
                return false;
            }
            var title = subj.GetLegalPersonTypeName();

            if (IsConductMorallyWrong(subj))
            {
                AddReasonEntry($"{title} {subj.Name}, {nameof(IsConductMorallyWrong)} is true");
                return true;
            }

            if (Causation != null)
            {
                Causation.GetSubjectPerson = GetSubjectPerson;

                if (!Causation.IsValid(persons))
                {
                    AddReasonEntryRange(Causation.GetReasonEntries());
                    return false;
                }

                //it must not be due to any voluntary action on the part of plaintiff.
                var plaintiff = this.Plaintiff(persons);
                if (plaintiff == null)
                    return false;

                if (Causation.IsValid(plaintiff))
                {
                    AddReasonEntry($"{plaintiff.GetLegalPersonTypeName()} {plaintiff.Name}, " +
                                   $"{nameof(Causation)} {nameof(IsValid)} is true");
                    return false;
                }

                return true;
            }

            if (SafetyConvention != null && SafetyConvention.IsValid(persons) == false)
            {
                AddReasonEntryRange(SafetyConvention.GetReasonEntries());
                AddReasonEntry($"{title} {subj.Name}, {nameof(SafetyConvention)} {nameof(IsValid)} is false");
                return true;
            }

            return false;
        }
    }
}
