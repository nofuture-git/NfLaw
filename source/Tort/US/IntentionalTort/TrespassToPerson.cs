﻿using System;
using NoFuture.Law.US;

namespace NoFuture.Law.Tort.US.IntentionalTort
{
    /// <summary>
    /// Either of defamation or physical assault, battery or imprisonment.
    /// </summary>
    public class TrespassToPerson : TortTrespass
    {
        public TrespassToPerson(Func<ILegalPerson[], ILegalPerson> getSubjectPerson) : base(getSubjectPerson) { }

        public TrespassToPerson() : base(ExtensionMethods.Tortfeasor) { }

    }
}
