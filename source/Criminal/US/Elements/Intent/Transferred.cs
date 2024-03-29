﻿using NoFuture.Law.Criminal.US.Elements.Act;

namespace NoFuture.Law.Criminal.US.Elements.Intent
{
    /// <summary>
    /// A way to allow there to still be intent whenever the original 
    /// target is missed and lands upon some other victim
    /// </summary>
    public class Transferred : MensRea
    {
        private readonly IMensRea _intent;

        public Transferred(IMensRea intent)
        {
            _intent = intent;
        }

        public override bool IsValid(params ILegalPerson[] persons)
        {
            return _intent.IsValid(persons);
        }

        public override int CompareTo(object obj)
        {
            return _intent.CompareTo(obj);
        }

        public override bool CompareTo(IActusReus criminalAct, params ILegalPerson[] persons)
        {
            return _intent.CompareTo(criminalAct);
        }
    }
}
