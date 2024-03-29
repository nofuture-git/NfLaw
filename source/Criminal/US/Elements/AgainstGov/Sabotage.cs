﻿using System;
using System.Collections.Generic;
using System.Linq;
using NoFuture.Law;
using NoFuture.Law.Enums;
using NoFuture.Law.Attributes;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.Property.US.FormsOf;
using NoFuture.Law.US;

namespace NoFuture.Law.Criminal.US.Elements.AgainstGov
{
    /// <summary>
    /// <![CDATA[ 18 U.S.C. § 2151 et seq., ]]> 
    /// destroy, damaging or defective harbor/national defense property, war material, premises, utilities
    /// </summary>
    [Note("listen all'yll itz'a")]
    public class Sabotage : LegalConcept, IActusReus, ILegalProperty
    {
        private readonly ILegalProperty _govProperty;

        public Sabotage(string propertyName)
        {
            _govProperty = new GovernmentProperty(propertyName);
        }

        /// <summary>
        /// Is producing defective material
        /// </summary>
        public Predicate<ILegalPerson> IsDefectiveProducerOf { get; set; } = lp => false;

        /// <summary>
        /// Is destroyer of this property
        /// </summary>
        public Predicate<ILegalPerson> IsDestroyerOf { get; set; } = lp => false;

        /// <summary>
        /// Is source of damage to this property
        /// </summary>
        public Predicate<ILegalPerson> IsDamagerOf { get; set; } = lp => false;

        /// <summary>
        /// harbor or national defense property premises or utilities
        /// </summary>
        public bool IsDefenseProperty { get; set; }

        /// <summary>
        /// war materials property premises or utilities
        /// </summary>
        public bool IsWarProperty { get; set; }

        public override bool IsValid(params ILegalPerson[] persons)
        {
            var defendant = this.Defendant(persons);
            if (defendant == null)
                return false;
            var title = defendant.GetLegalPersonTypeName();
            if (!IsDefenseProperty && !IsWarProperty)
            {
                AddReasonEntry($"{title} {defendant.Name}, {nameof(IsDefenseProperty)} " +
                               $"and {nameof(IsWarProperty)} are false for property '{Name}'");
                return false;
            }

            var defect = IsDefectiveProducerOf(defendant);
            var destroy = IsDestroyerOf(defendant);
            var dmg = IsDamagerOf(defendant);

            if (new[] {defect, destroy, dmg}.All(p => p == false))
            {
                AddReasonEntry($"{title} {defendant.Name}, {nameof(IsDefectiveProducerOf)}, " +
                               $"{nameof(IsDestroyerOf)} and {nameof(IsDamagerOf)} are false " +
                               $"for property '{Name}'");
                return false;
            }

            return true;
        }

        public bool CompareTo(IMensRea criminalIntent, params ILegalPerson[] persons)
        {
            var validIntent = new[]
            {
                typeof(SpecificIntent), typeof(Purposely), typeof(GeneralIntent), typeof(Knowingly), typeof(Negligently)
            };
            if (validIntent.All(t => t != criminalIntent.GetType()))
            {
                var nms = string.Join(", ", validIntent.Select(t => t.Name));
                AddReasonEntry($"{nameof(Sabotage)} requires intent of {nms}");
                return false;
            }

            return true;
        }

        public Predicate<ILegalPerson> IsEntitledTo
        {
            get => _govProperty.IsEntitledTo;
            set => _govProperty.IsEntitledTo = value;
        }

        public Predicate<ILegalPerson> IsInPossessionOf
        {
            get => _govProperty.IsInPossessionOf;
            set => _govProperty.IsInPossessionOf = value;
        }

        public decimal? CurrentPropertyValue { get; }
        public Func<DateTime?, decimal?> PropertyValue { get; set; } = dt => null;

        public int GetRank()
        {
            var val = CurrentPropertyValue.GetValueOrDefault(0m);
            return Convert.ToInt32(Math.Round(val, 0));
        }

        #region IVoca HAS-A IS-A
        public IDictionary<string, object> ToData(KindsOfTextCase txtCase)
        {
            return _govProperty.ToData(txtCase);
        }

        public string Name
        {
            get => _govProperty.Name;
            set => _govProperty.Name = value;
        }

        public int NamesCount => _govProperty.NamesCount;
        public void AddName(KindsOfNames k, string name)
        {
            _govProperty.AddName(k, name);
        }

        public string GetName(KindsOfNames k)
        {
            return _govProperty.GetName(k);
        }

        public bool AnyNames(Predicate<KindsOfNames> filter)
        {
            return _govProperty.AnyNames(filter);
        }

        public bool AnyNames(Predicate<string> filter)
        {
            return _govProperty.AnyNames(filter);
        }

        public bool AnyNames(Func<KindsOfNames, string, bool> filter)
        {
            return _govProperty.AnyNames(filter);
        }

        public bool AnyNames()
        {
            return _govProperty.AnyNames();
        }

        public int RemoveName(Predicate<KindsOfNames> filter)
        {
            return _govProperty.RemoveName(filter);
        }

        public int RemoveName(Predicate<string> filter)
        {
            return _govProperty.RemoveName(filter);
        }

        public int RemoveName(Func<KindsOfNames, string, bool> filter)
        {
            return _govProperty.RemoveName(filter);
        }

        public KindsOfNames[] GetAllKindsOfNames()
        {
            return _govProperty.GetAllKindsOfNames();
        }

        public void CopyNamesFrom(IVoca voca)
        {
            _govProperty.CopyNamesFrom(voca);
        }
        #endregion
    }
}
