﻿using System;

namespace NoFuture.Rand.Law.US
{
    public abstract class UnoHomine : LegalConcept
    {
        protected Func<ILegalPerson[], ILegalPerson> GetSubjectPerson { get; set; }
        protected UnoHomine(Func<ILegalPerson[], ILegalPerson> getSubjectPerson)
        {
            GetSubjectPerson = getSubjectPerson;
        }
    }
}
