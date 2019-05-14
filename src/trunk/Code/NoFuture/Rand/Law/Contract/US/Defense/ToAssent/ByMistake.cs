﻿using System;
using NoFuture.Rand.Law.US;

namespace NoFuture.Rand.Law.Contract.US.Defense.ToAssent
{
    /// <summary>
    /// <![CDATA[
    /// a mistake concerning something fundamental to the nature of the contract
    /// ]]>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ByMistake<T> : DefenseBase<T> where T : ILegalConcept
    {
        public ByMistake(IContract<T> contract) : base(contract)
        {
        }

        public override bool IsValid(params ILegalPerson[] persons)
        {
            var offeror = persons.Offeror();
            var offeree = persons.Offeree();

            if (!base.IsValid(offeror, offeree))
                return false;


            throw new NotImplementedException();
        }
    }
}
