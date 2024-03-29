﻿using System.Linq;
using NoFuture.Law.US;

namespace NoFuture.Law.Contract.US.Ucc
{
    /// <inheritdoc cref="LegalConcept"/>
    /// <summary>
    /// <![CDATA[
    /// all things which are movable at the time of identification
    /// ]]>
    /// </summary>
    public class Goods : LegalConcept, IUccItem
    {
        public Goods()
        {
            Merchantability = new Merchantable(this);
        }

        public override bool IsValid(params ILegalPerson[] persons)
        {
            var offeror = this.Offeror(persons);
            var offeree = this.Offeree(persons);
            if (offeree == null || offeror == null)
                return false;

            if (!IsEnforceableInCourt)
            {
                AddReasonEntry($"{GetType().Name} is not enforceable in court");
                return false;
            }

            if (!IsMovable)
            {
                AddReasonEntry($"{GetType().Name} is not movable");
                return false;
            }

            if (!IsIdentified)
            {
                AddReasonEntry($"{GetType().Name} has no identification");
                return false;
            }

            if (!Merchantability.IsValid(offeror, offeree))
            {
                AddReasonEntry($"{GetType().Name} is not merchantable");
                AddReasonEntryRange(Merchantability.GetReasonEntries());
                return false;
            }

            return true;
        }

        public virtual bool IsMovable { get; set; } = true;
        public virtual bool IsIdentified { get; set; } = true;
        public override bool IsEnforceableInCourt => true;
        public Merchantable Merchantability { get; }
    }
}
