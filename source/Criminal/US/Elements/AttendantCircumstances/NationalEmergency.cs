﻿using NoFuture.Law.Criminal.US.Elements;

namespace NoFuture.Law.Criminal.US.Elements.AttendantCircumstances
{
    /// <summary>
    /// A declaration by the President per  National Emergencies Act (NEA)
    /// </summary>
    public class NationalEmergency : AttendantCircumstanceBase
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            return true;
        }
    }
}
