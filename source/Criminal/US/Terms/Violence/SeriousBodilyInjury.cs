﻿namespace NoFuture.Law.Criminal.US.Terms.Violence
{
    public class SeriousBodilyInjury : NondeadlyForce
    {
        protected override string CategoryName { get; } = "serious bodily injury";
        public override int GetRank()
        {
            return base.GetRank() + 1;
        }
    }
}
