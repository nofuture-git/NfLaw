﻿using NoFuture.Rand.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Rand.Law.Criminal.US.Elements.Intent.PenalCode;

namespace NoFuture.Rand.Law.Criminal.Homicide.US.Elements
{
    /// <summary>
    /// this is homicide which lacks intent
    /// </summary>
    public class ManslaughterInvoluntary : Manslaughter
    {
        public override bool CompareTo(IMensRea criminalIntent, params ILegalPerson[] persons)
        {
            var isRequiredIntent = criminalIntent is Negligently 
                                 || criminalIntent is Recklessly 
                                 || criminalIntent is GeneralIntent;
            if (!isRequiredIntent)
            {
                AddReasonEntry($"{nameof(ManslaughterInvoluntary)} is expected {nameof(Recklessly)} " +
                               $"or {nameof(Negligently)} intent, not {criminalIntent?.GetType().Name}");
                return false;
            }

            return true;
        }
    }
}
