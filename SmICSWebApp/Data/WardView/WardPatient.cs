﻿using SmICSCoreLib.Factories.General;
using SmICSCoreLib.Factories.MiBi.Nosocomial;
using SmICSCoreLib.Factories.PatientInformation.PatientData;
using System;
using System.Collections.Generic;

namespace SmICSWebApp.Data.WardView
{
    public class WardPatient : Case
    {
        public DateTime? CurrentResult { get; internal set; }
        public DateTime Admission { get; internal set; }
        public DateTime? Discharge { get; internal set; }
        public DateTime? FirstWardPositiveResult { get; internal set; }
        public DateTime? LastWardResult { get; internal set; }
        public DateTime? FirstPositiveResult { get; internal set; }
        public Dictionary<string, InfectionStatus> InfectionStatus { get; internal set; }
        public PatientData PatientData { get; internal set; }
        public string Pathogen { get; internal set; }
        public List<string> PathogenCodes { get; internal set; }

        public DateTime? CaseAdmission { get; internal set; }
        public DateTime? CaseDischarge { get; internal set; }
    }
}