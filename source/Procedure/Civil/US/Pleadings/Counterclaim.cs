﻿using System;

namespace NoFuture.Law.Procedure.Civil.US.Pleadings
{
    /// <summary>
    /// Complaint in opposition to some other plaintiff&apos;s
    /// complaint both of which concern the same set of facts\circumstances.
    /// </summary>
    /// <remarks>
    /// Fed Civil Procedure Rule 13
    /// </remarks>
    public class Counterclaim : Crossclaim
    {
        /// <summary>
        /// Counter claim must concern the same facts and\or questions of law
        /// </summary>
        public Func<ILegalConcept, ILegalConcept, bool> IsSameQuestionOfLawOrFact { get; set; } = (lc0, lc1) => false;

        public override bool IsValid(params ILegalPerson[] persons)
        {
            return TestFuncEnclosure(IsSameQuestionOfLawOrFact, nameof(IsSameQuestionOfLawOrFact), persons)
                   && base.IsValid(persons);
        }
    }
}
