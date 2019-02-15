﻿using System;

namespace NoFuture.Rand.Law.US.Criminal.Elements.Intent.ComLaw
{
    /// <summary>
    /// without the additional desire to cause a result
    /// </summary>
    public class GeneralIntent : MensRea
    {
        public Predicate<ILegalPerson> IsIntentOnWrongdoing { get; set; } = lp => false;
        public Predicate<ILegalPerson> IsKnowledgeOfWrongdoing { get; set; } = lp => false;

        public override bool IsValid(ILegalPerson offeror = null, ILegalPerson offeree = null)
        {
            var defendant = Government.GetDefendant(offeror, offeree, this);
            if (defendant == null)
                return false;
            var intent = IsIntentOnWrongdoing(defendant);
            var knowledge = IsKnowledgeOfWrongdoing(defendant);

            if (!intent && !knowledge)
            {
                AddReasonEntry($"defendant {defendant.Name} did not " +
                               "have intent of wrong doing nor knowledge of wrong doing");
                return false;
            }

            return true;
        }

        public override int CompareTo(object obj)
        {
            if (obj is MaliceAforethought || obj is SpecificIntent)
                return -1;
            return 0;
        }
    }
}