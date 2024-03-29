﻿using System;
using System.Linq;
using NoFuture.Law.Attributes;
using NoFuture.Law.Contract.US.Terms;
using NoFuture.Law.US;

namespace NoFuture.Law.Contract.US.Semiosis
{
    /// <summary>
    /// <![CDATA[
    /// how much credit to give the writing in a contract over oral 
    /// ]]>
    /// </summary>
    [Aka("oral evidence rule")]
    [Note("not the same criminal term applied to convicts - 'parole'")]
    [EtymologyNote("Old French", "parol", "for 'oral'")]
    public class ParolEvidenceRule<T> : DilemmaBase<T> where T : ILegalConcept
    {
        public ParolEvidenceRule(IContract<T> contract) : base(contract)
        {
        }

        public Predicate<Term<object>> IsCollateralInForm { get; set; } = r => false;

        public Predicate<Term<object>> IsNotContradictWritten { get; set; } = r => false;

        public Predicate<Term<object>> IsNotExpectedWritten { get; set; } = r => false;

        public override bool IsValid(params ILegalPerson[] persons)
        {
            var offeror = this.Offeror(persons);
            var offeree = this.Offeree(persons);
            if (offeree == null || offeror == null)
                return false;

            if (!TryGetTerms(offeror, offeree))
            {
                AddReasonEntry("parol evidence rule requires a contract with assent");
                return false;
            }

            var agreedTerms = AgreedTerms;
            if (agreedTerms != null && agreedTerms.Any(t => t is ExpresslyConditionalTerm))
            {
                AddReasonEntryRange(Contract.Assent.GetReasonEntries());
                AddReasonEntry("additional terms cannot be included since one " +
                               "of the written terms expressly states, " +
                               "additional terms cannot be included.");
                return false;
            }

            var contractTerms = Contract.Assent as IAssentTerms;
            if (contractTerms == null)
            {
                AddReasonEntry($"{nameof(Contract)} {nameof(Contract.Assent)} does " +
                               $"not cast as {nameof(IAssentTerms)}");
                return false;
            }

            var additionalTerms = contractTerms.GetAdditionalTerms(offeror, offeree);
            if (additionalTerms == null || !additionalTerms.Any())
            {
                AddReasonEntryRange(Contract.Assent.GetReasonEntries());
                return false;
            }

            var oralAdditionalTerms = additionalTerms.Where(t => t.IsCategory(new OralTerm()))
                .ToList();

            if (!oralAdditionalTerms.Any())
            {
                AddReasonEntry("none of the additional terms are oral in source");
                return false;
            }

            foreach (var ot in oralAdditionalTerms)
            {
                if(ot == null)
                    continue;
                if (!IsCollateralInForm(ot))
                {
                    AddReasonEntry($"the term '{ot.Name}' is not collateral in form");
                    return false;
                }

                if (!IsNotContradictWritten(ot))
                {
                    AddReasonEntry($"the term '{ot.Name}' is a contradiction of what is in writing");
                    return false;
                }

                if (!IsNotExpectedWritten(ot))
                {
                    AddReasonEntry($"the term '{ot.Name}' is expected to have been written");
                    return false;
                }
            }

            return true;
        }
    }
}
