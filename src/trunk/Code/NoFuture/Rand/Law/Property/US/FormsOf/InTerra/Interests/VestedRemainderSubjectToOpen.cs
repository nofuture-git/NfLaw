﻿using System;
using NoFuture.Rand.Law.Attributes;

namespace NoFuture.Rand.Law.Property.US.FormsOf.InTerra.Interests
{
    /// <summary>
    /// The Rule of Convenience is the idea the a future interest becomes actual upon any one qualified to make a claim and does so.
    /// </summary>
    [Aka("vested remainder subject to partial defeasance")]
    public class VestedRemainderSubjectToOpen : Remainder
    {
        public VestedRemainderSubjectToOpen(Func<ILegalPerson[], ILegalPerson> getSubjectPerson) : base(getSubjectPerson)
        {
        }

        public VestedRemainderSubjectToOpen() : base(null) { }

        public override bool IsValid(params ILegalPerson[] persons)
        {
            throw new NotImplementedException();
        }
    }
}