﻿using NoFuture.Law;

namespace NoFuture.Law.Procedure.Civil.US
{
    public interface ILinkedLegalConcept : ILegalConcept
    {
        ILegalConcept LinkedTo { get; set; }

        bool NamesEqual(IVoca voca1, IVoca voca2);
    }
}
