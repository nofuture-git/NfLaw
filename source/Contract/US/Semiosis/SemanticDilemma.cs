﻿using System;
using System.Collections.Generic;
using System.Linq;
using NoFuture.Law.US;

namespace NoFuture.Law.Contract.US.Semiosis
{
    /// <summary>
    /// A type to handle the problem of a court having to pick one of two
    /// term meanings as the intended one.
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// The ordinary standard, or "plain meaning," is simply the meaning of the people who did 
    /// not write the document. The fallacy consists in assuming that there is or ever can be 
    /// some one real or absolute meaning. In truth, there can be only some person's meaning; 
    /// and that person, whose meaning the law is seeking, is the writer of the document.
    /// ]]>
    /// </remarks>
    public class SemanticDilemma<T> : DilemmaBase<T> where T : ILegalConcept
    {
        public SemanticDilemma(IContract<T> contract) : base(contract)
        {
        }

        /// <summary>
        /// An option condition on any of the agreed terms which must be met.
        /// </summary>
        public Predicate<Term<object>> IsPrerequisite { get; set; } = t => true;

        /// <summary>
        /// The method in which one and only one of the dilemma terms is the one 
        /// the original parties intended.
        /// </summary>
        public Predicate<Term<object>> IsIntendedMeaningAtTheTime { get; set; } = t => false;

        public override bool IsValid(params ILegalPerson[] persons)
        {
            var offeror = this.Offeror(persons);
            var offeree = this.Offeree(persons);
            if (offeree == null || offeror == null)
                return false;

            if (!TryGetTerms(offeror, offeree))
            {
                return false;
            }

            var preque = IsPrerequisite ?? (t => true);
            if (AgreedTerms.All(agreedTerm => !preque(agreedTerm)))
            {
                AddReasonEntry($"none of the terms between {offeror.Name} and {offeree.Name} " +
                               "satisfied the prerequisite condition.");
                return false;
            }

            var resolved = new List<bool>();

            foreach (var term in AgreedTerms)
            {
                var offerorTerm = OfferorTerms.First(v => v.Name == term.Name);
                var offereeTerm = OffereeTerms.First(v => v.Name == term.Name);

                var isSemanticConflict = !offereeTerm.EqualRefersTo(offerorTerm);

                //try it both ways 
                if (!isSemanticConflict)
                {
                    isSemanticConflict = !offerorTerm.EqualRefersTo(offereeTerm);
                }
                if(!isSemanticConflict)
                    continue;

                var isOfferorPreferred = IsIntendedMeaningAtTheTime(offerorTerm);
                var isOffereePreferred = IsIntendedMeaningAtTheTime(offereeTerm);
                var isOneOnlyOneValid = isOfferorPreferred ^ isOffereePreferred;
                if (!isOneOnlyOneValid)
                {
                    AddReasonEntry($"the term '{offereeTerm.Name}' has two different " +
                                   $"meanings and neither {offeror.Name}'s " +
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
