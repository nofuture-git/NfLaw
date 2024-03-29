﻿using System;
using NoFuture.Law.US;

namespace NoFuture.Law.Tort.US.ReasonableCare
{
    public class OfMentalCapacity : ReasonableCareBase
    {
        public OfMentalCapacity(Func<ILegalPerson[], ILegalPerson> getSubjectPerson) : base(getSubjectPerson)
        {
        }

        public Predicate<ILegalPerson> IsMentallyIncapacitated { get; set; } = lp => false;

        /// <summary>
        /// Implied is that the incapacity is sudden - if its gradual then that would mean its foreseeable
        /// </summary>
        public Predicate<ILegalPerson> IsIncapacityForeseeable { get; set; } = lp => false;

        public Predicate<ILegalPerson> IsExercisedMentalCare { get; set; } = lp => false;

        public override bool IsValid(params ILegalPerson[] persons)
        {
            var subj = GetSubjectPerson(persons);
            if (subj == null)
            {
                AddReasonEntry($"{nameof(GetSubjectPerson)} returned nothing");
                return false;
            }

            var title = subj.GetLegalPersonTypeName();

            if (!IsMentallyIncapacitated(subj))
            {
                AddReasonEntry($"{title} {subj.Name}, {nameof(IsMentallyIncapacitated)} is false");
                return false;
            }

            if (!IsIncapacityForeseeable(subj))
            {
                AddReasonEntry($"{title} {subj.Name}, {nameof(IsIncapacityForeseeable)} is false");
                return true;
            }

            if (!IsExercisedMentalCare(subj))
            {
                AddReasonEntry($"{title} {subj.Name}, {nameof(IsExercisedMentalCare)} is false");
                return false;
            }

            return true;
        }
    }
}
