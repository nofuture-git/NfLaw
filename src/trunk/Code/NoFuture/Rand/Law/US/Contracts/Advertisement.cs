﻿using System.Collections.Generic;

namespace NoFuture.Rand.Law.US.Contracts
{
    /// <summary>
    /// is merely an invitation to negotiate for purchase of commercial goods
    /// </summary>
    public class Advertisement : LegalDuty
    {
        private readonly List<string> _audit = new List<string>();
        public override bool IsValid(ILegalPerson promisor, ILegalPerson promisee)
        {
            return IsEnforceableInCourt;
        }

        public bool IsClear { get; set; }

        public bool IsDefinite { get; set; }

        public bool IsExplicit { get; set; }

        public bool IsNothingLeftToOpenNegotiation { get; set; }

        public override List<string> Audit => _audit;

        /// <summary>
        /// Only when all predictes are true is this true.
        /// Lefkowitz v. Great Minneapolis Surplus Store, 86 N.W.2d 689, 691 (Minn. 1957)
        /// </summary>
        public override bool IsEnforceableInCourt
        {
            get
            {
                var rslt = IsClear && IsDefinite && IsExplicit && IsNothingLeftToOpenNegotiation;
                if (!rslt)
                {
                    _audit.Add($"This {nameof(Advertisement)} is not enforceable: " +
                               $"{nameof(IsClear)} = {IsClear} " +
                               $"{nameof(IsDefinite)} = {IsDefinite} " +
                               $"{nameof(IsExplicit)} = {IsExplicit} " +
                               $"{nameof(IsNothingLeftToOpenNegotiation)} = {IsNothingLeftToOpenNegotiation}");
                }
                return rslt;
            }
        }
    }
}
