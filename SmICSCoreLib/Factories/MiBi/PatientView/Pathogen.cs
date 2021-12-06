﻿using System;
using System.Collections.Generic;

namespace SmICSCoreLib.Factories.MiBi.PatientView
{
    public class Pathogen
    {
        public string Name { get; set; }
        public bool Result { get; set; }
        public DateTime Timestamp { get; set; }
        public string Rate { get; set; }
        public string IsolatNr { get; set; }
        public List<Antibiogram> Antibiograms { get; set; }
    }
}