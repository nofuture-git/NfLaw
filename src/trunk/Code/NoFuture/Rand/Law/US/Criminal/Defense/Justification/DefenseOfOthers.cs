﻿namespace NoFuture.Rand.Law.US.Criminal.Defense.Justification
{
    /// <inheritdoc />
    /// <summary>
    /// <![CDATA[
    /// under the circumstances as the actor believes them to be, the 
    /// person whom he seeks to protect would be justified in using 
    /// such protective force (Model Penal Code § 3.05(1) (b))
    /// ]]>
    /// </summary>
    public class DefenseOfOthers : DefenseOfBase
    {
        public DefenseOfOthers(ICrime crime) : base(crime)
        {
        }

        /// <summary>
        /// <![CDATA[
        /// The subjective test where it reasonably appeared that the victim had a right to self-defense 
        /// and, thereby, that right is conveyed to the defendant
        /// ]]>
        /// </summary>
        public SubjectivePredicate<ILegalPerson> IsReasonablyAppearedInjuryOrDeath { get; set; } = lp => false;

        public override bool IsValid(ILegalPerson offeror = null, ILegalPerson offeree = null)
        {
            var defendant = Government.GetDefendant(offeror, offeree, this);
            if (defendant == null)
                return false;
            if (!base.IsValid(offeror, offeree))
                return false;

            if (!IsReasonablyAppearedInjuryOrDeath(defendant))
            {
                AddReasonEntry($"defendant, {defendant.Name}, {nameof(IsReasonablyAppearedInjuryOrDeath)} is false");
                return false;
            }

            return true;
        }
    }
}
