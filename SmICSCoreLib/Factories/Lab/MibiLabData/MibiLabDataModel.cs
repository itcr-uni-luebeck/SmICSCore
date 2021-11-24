﻿using Newtonsoft.Json;
using SmICSCoreLib.Factories.General;
using SmICSCoreLib.Factories.Lab.ViroLabData.ReceiveModel;
using System;
using SmICSCoreLib.Factories.Lab.MibiLabdata.ReceiveModel;
using SmICSCoreLib.Factories.MiBi;
using System.Collections.Generic;

namespace SmICSCoreLib.Factories.Lab.MibiLabData
{
    public class MibiLabDataModel : LabData
    {
        [JsonProperty(PropertyName = "MREKlasse")]
        public string MREKlasse { get; set; }
        [JsonProperty(PropertyName = "Antibiogram")]
        public List<Antibiogram> Antibiogram { get; set; }

        public MibiLabDataModel() { }
        public MibiLabDataModel(MetaDataReceiveModel metaData, SampleReceiveModel sampleData, PathogenReceiveModel pathogenData)
        {
            ZeitpunktProbenentnahme = sampleData.ZeitpunktProbeentnahme;
            ZeitpunktProbeneingang = sampleData.ZeitpunktProbeneingang;
            MaterialID = sampleData.MaterialID;
            Material_l = sampleData.MaterialID;
            Befund = (pathogenData.Befund == "Nachweis") ? true : false;
            Befundkommentar = pathogenData.Befundkommentar;
            KeimID = pathogenData.KeimID;
            LabordatenID = sampleData.LabordatenID + pathogenData.IsolatNo;
            PatientID = metaData.PatientID;
            CaseID = metaData.FallID;
            ProbeID = sampleData.LabordatenID;
            MREKlasse = pathogenData.MREKlasse;
        }
    }
}