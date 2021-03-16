﻿using SmICSCoreLib.AQL.PatientInformation.PatientMovement.ReceiveModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmICSCoreLib.AQL.PatientInformation.PatientMovement
{
    public class EpsiodeOfCareParameter
    {
        public string PatientID { get; set; }
        public string CaseID { get; set; }

        public EpsiodeOfCareParameter() { }
        public EpsiodeOfCareParameter(PatientStayModel patientStay)
        {
            PatientID = patientStay.PatientID;
            CaseID = patientStay.FallID;
        }
    }
}
