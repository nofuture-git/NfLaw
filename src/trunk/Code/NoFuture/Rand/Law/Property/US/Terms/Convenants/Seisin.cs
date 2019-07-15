﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoFuture.Rand.Law.Attributes;

namespace NoFuture.Rand.Law.Property.US.Terms
{
    /// <summary>
    /// a promise that the offeree has legal possession of the property
    /// </summary>
    [Aka("warranty of possession")]
    [EtymologyNote("old french", "seisine", "completion of the ceremony of feudal investiture")]
    public class CovenantOfSeisin : Term<ILegalPerson>
    {
        public CovenantOfSeisin(string name, ILegalPerson reference) : base(name, reference)
        {
        }

        public CovenantOfSeisin(string name, ILegalPerson reference, params ITermCategory[] categories) : base(name, reference, categories)
        {
        }
    }
}
