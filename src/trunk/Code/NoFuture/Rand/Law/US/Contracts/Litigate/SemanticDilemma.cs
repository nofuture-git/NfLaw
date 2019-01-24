﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NoFuture.Rand.Law.US.Contracts.Litigate
{
    /// <summary>
    /// A type to handle the problem of a court having to pick one of two
    /// term meanings as the valid one.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SemanticDilemma<T> : LitigateBase<T>
    {
        public SemanticDilemma(IContract<T> contract) : base(contract)
        {
        }

        /// <summary>
        /// An option condition on any of the agreed terms which must be met.
        /// </summary>
        public Predicate<Term<object>> IsPrerequisite { get; set; } = t => true;

        /// <summary>
        /// The method in which one and only one of the dilemma terms is decieded.
        /// </summary>
        public Predicate<Term<object>> IsPreferred { get; set; } = t => false;

        public override bool IsValid(ILegalPerson offeror, ILegalPerson offeree)
        {
            if (Contract?.Assent == null)
            {
                AddReasonEntry("resolving ambiguous terms requires a contract with assent");
                return false;
            }

            var agreedInNameTerms = Contract.Assent.GetInNameAgreedTerms(offeror, offeree);
            AddReasonEntryRange(Contract.Assent.GetReasonEntries());
            if (!agreedInNameTerms.Any())
            {
                return false;
            }

            var preque = IsPrerequisite ?? (t => true);
            if (!agreedInNameTerms.Any(agreedTerm => preque(agreedTerm)))
            {
                AddReasonEntry($"none of the terms between {offeror?.Name} and {offeree?.Name} " +
                               "satisfied the prerequisite condition.");
                return false;
            }

            var sorTerms = Contract.Assent.TermsOfAgreement(offeror);
            var seeTerms = Contract.Assent.TermsOfAgreement(offeree);

            var resolved = new List<bool>();

            foreach (var term in agreedInNameTerms)
            {
                var offerorTerm = sorTerms.First(v => v.Name == term.Name);
                var offereeTerm = seeTerms.First(v => v.Name == term.Name);

                var isSemanticConflict = !offereeTerm?.EqualRefersTo(offerorTerm) ?? false;

                if (!isSemanticConflict)
                    continue;

                var isOfferorPreferred = IsPreferred(offerorTerm);
                var isOffereePreferred = IsPreferred(offereeTerm);
                var isOneOnlyOneValid = isOfferorPreferred ^ isOffereePreferred;
                if (!isOneOnlyOneValid)
                {
                    AddReasonEntry($"the term '{offereeTerm.Name}' has two different " +
                                   $"meanings andneither {offeror.Name}'s " +
                                   $"nor {offeree.Name}'s is preferred ");
                }

                if (isOfferorPreferred)
                {
                    AddReasonEntry($"the preferred meaning of '{offereeTerm.Name}' is " +
                                   $"the one given by {offeror.Name} and NOT " +
                                   $"the one given by {offeree.Name}");
                }
                if (isOffereePreferred)
                {
                    AddReasonEntry($"the preferred meaning of '{offereeTerm.Name}' is " +
                                   $"the one given by {offeree.Name} and NOT " +
                                   $"the one given by {offeror.Name}");
                }

                resolved.Add(isOneOnlyOneValid);
            }

            return resolved.Any() && resolved.All(v => v);
        }
    }
}
