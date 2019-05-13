﻿using NoFuture.Rand.Law.US.Elements;
using NoFuture.Rand.Law.US;

namespace NoFuture.Rand.Law.Tort.US.Elements
{
    public abstract class TortTrespass : TrespassBase
    {
        public Damage PropertyDamage { get; set; }

        public Causation Causation { get; set; }

        public override void ClearReasons()
        {
            PropertyDamage?.ClearReasons();
            base.ClearReasons();
        }

        protected virtual bool IsPhysicalDamage(ILegalPerson[] persons)
        {
            if (PropertyDamage == null)
            {
                AddReasonEntry($"{nameof(PropertyDamage)} is unassigned");
                return false;
            }

            PropertyDamage.GetSubjectPerson = PropertyDamage.GetSubjectPerson ?? ExtensionMethods.Tortfeasor;
            PropertyDamage.SubjectProperty = PropertyDamage.SubjectProperty ?? SubjectProperty;
            var rslt = PropertyDamage.IsValid(persons);
            AddReasonEntryRange(PropertyDamage.GetReasonEntries());

            Causation.GetSubjectPerson = Causation.GetSubjectPerson ?? ExtensionMethods.Tortfeasor;
            rslt = rslt && Causation.IsValid(persons);
            AddReasonEntryRange(Causation.GetReasonEntries());

            return rslt;
        }
    }
}
