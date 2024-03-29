﻿using System;
using NoFuture.Law.US;

namespace NoFuture.Law.Contract.US.Defense
{
    /// <summary>
    /// <![CDATA[
    /// A contracts must be "put into sufficient writing" to be enforceable.
    /// ]]>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StatuteOfFrauds<T> : DefenseBase<T> where T : ILegalConcept
    {
        public StatuteOfFrauds(IContract<T> contract) : base(contract)
        {
            Scope = new StatuteOfFraudsScope<T>(contract);
        }

        public StatuteOfFraudsScope<T> Scope { get; set; }

        public Predicate<IContract<T>> IsSufficientWriting { get; set; }

        public Predicate<IContract<T>> IsSigned { get; set; }

        public override bool IsValid(params ILegalPerson[] persons)
        {
            var offeror = this.Offeror(persons);
            var offeree = this.Offeree(persons);
            if (offeree == null || offeror == null)
                return false;

            if (!base.IsValid(offeror, offeree))
                return false;

            Predicate<IContract<T>> dfx = c => false;
            var scope = Scope ?? new StatuteOfFraudsScope<T>(Contract);

            if (!scope.IsValid(offeror, offeree))
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
        /// English Parliament passed this in 1677 and repealed it in 1954.  
        /// It is still active in the United States.
        /// </summary>
        public override bool IsEnforceableInCourt => true;
    }
}
