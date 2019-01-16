﻿using System;

namespace NoFuture.Rand.Law.US.Contracts.Defense
{
    /// <summary>
    /// <![CDATA[
    /// A contracts must be "put into sufficient writing" to be enforceable.
    /// ]]>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StatuteOfFrauds<T> : DefenseBase<T>
    {
        public StatuteOfFrauds(IContract<T> contract) : base(contract)
        {
            Scope = new StatuteOfFraudsScope<T>(contract);
        }

        public StatuteOfFraudsScope<T> Scope { get; set; }

        public Predicate<IContract<T>> IsSufficientWriting { get; set; }

        public Predicate<IContract<T>> IsSigned { get; set; }

        public override bool IsValid(ILegalPerson offeror, ILegalPerson offeree)
        {
            if (!base.IsValid(offeror, offeree))
                return false;

            Predicate<IContract<T>> dfx = c => false;
            if (!Scope.IsValid(offeror, offeree))
            {
                AddReasonEntry("Is not within Statute of Frauds");
                //it is a valid defense
                return true;
            }

            var isSuffWrit = IsSufficientWriting ?? dfx;
            if (!isSuffWrit(Contract))
            {
                AddReasonEntry("Statute of Frauds, without sufficient writing");
                //it is a valid defense
                return true;
            }

            var isSigned = IsSigned ?? dfx;
            if (!isSigned(Contract))
            {
                AddReasonEntry("Statute of Frauds, without sufficient writing");
                //is a valid defense
                return true;
            }

            return false;
        }

        /// <summary>
        /// English Parliament passed this in 1677 and replealed it in 1954.  
        /// It is still active in the United States.
        /// </summary>
        public override bool IsEnforceableInCourt => true;
    }
}