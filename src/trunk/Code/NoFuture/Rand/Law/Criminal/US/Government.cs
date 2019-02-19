﻿using System;

namespace NoFuture.Rand.Law.Criminal.US
{
    /// <summary>
    /// criminal prosecution is always instituted by the government
    /// </summary>
    public class Government : LegalPerson
    {
        private static readonly Guid _uid = Guid.NewGuid();
        protected Government() { }

        private static Government _value;
        public static Government Value => _value ?? (_value = new Government());

        public override bool Equals(object obj)
        {
            return obj is Government;
        }

        public override int GetHashCode()
        {
            return _uid.GetHashCode();
        }
    }
}