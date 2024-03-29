﻿using System;
using NoFuture.Law.Attributes;
using NoFuture.Law.US;

namespace NoFuture.Law.Procedure.Civil.US.Judgment
{
    /// <summary>
    /// A claim is extinguished and replaced by a judgment - it cannot be brought again.
    /// </summary>
    [Aka("claim preclusion")]
    public class ResJudicata : CivilProcedureBase, IJudgment
    {
        /// <summary>
        /// (1) there must be a final judgment
        /// </summary>
        public Predicate<ILegalConcept> IsFinalJudgment { get; set; } = lc => false;

        /// <summary>
        /// (2) the judgment must be &quot;on the merits&quot;
        /// </summary>
        public Predicate<ILegalConcept> IsJudgmentBasedOnMerits { get; set; } = lc => false;

        /// <summary>
        /// (3) the claims must be the same in the first and second suits
        /// </summary>
        /// <remarks> same rules used in pleading&apos;s joiner logic </remarks>
        [Aka("same claim requirement")]
        public Predicate<ILegalConcept> IsSameQuestionOfLawOrFact { get; set; } = lc => false;

        /// <summary>
        /// (4) the parties in the second action must be the same as those in the first
        /// </summary>
        public Predicate<ILegalPerson> IsSamePartyAsPrior { get; set; } = lc => false;

        public override bool IsValid(params ILegalPerson[] persons)
        {
            if (!IsCourtAssigned())
                return false;

            var plaintiff = this.Plaintiff(persons);
            var defendant = this.Defendant(persons); 

            if (plaintiff == null || defendant == null)
            {
                return false;
            }

            var plaintiffTitle = plaintiff.GetLegalPersonTypeName();
            var defendantTitle = defendant.GetLegalPersonTypeName();

            if (!TryGetCauseOfAction(plaintiff, out var subject))
                return false;

            var prefixReason = $"{plaintiffTitle} {plaintiff.Name} v. " +
                                $"{defendantTitle} {defendant.Name}, ";

            if (!IsFinalJudgment(subject))
            {
                AddReasonEntry($"{prefixReason} {nameof(IsFinalJudgment)} is false");
                return false;
            }

            if (!IsJudgmentBasedOnMerits(subject))
            {
                AddReasonEntry($"{prefixReason} {nameof(IsJudgmentBasedOnMerits)} is false");
                return false;
            }

            if (!IsSameQuestionOfLawOrFact(subject))
            {
                AddReasonEntry($"{prefixReason} {nameof(IsSameQuestionOfLawOrFact)} is false");
                return false;
            }

            if (!IsSamePartyAsPrior(plaintiff))
            {
                AddReasonEntry($"{plaintiffTitle} {plaintiff}, {nameof(IsSamePartyAsPrior)} is false");
                return false;
            }


            if (!IsSamePartyAsPrior(defendant))
            {
                AddReasonEntry($"{defendantTitle} {defendant}, {nameof(IsSamePartyAsPrior)} is false");
                return false;
            }

            return true;
        }

    }
}
