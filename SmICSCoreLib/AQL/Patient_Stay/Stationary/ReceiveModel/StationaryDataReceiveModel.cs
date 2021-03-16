﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmICSCoreLib.AQL.Patient_Stay.Stationary.ReceiveModel
{
    public class StationaryDataReceiveModel
    {
        [JsonProperty(PropertyName = "FallID")]
        public string FallID { get; set; }

        [JsonProperty(PropertyName = "PatientID")]
        public string PatientID { get; set; }

        [JsonProperty(PropertyName = "Versorgungsfallgrund")]
        public string Versorgungsfallgrund { get; set; }

        [JsonProperty(PropertyName = "Art_der_Aufnahme")]
        public string Art_der_Aufnahme { get; set; }

        [JsonProperty(PropertyName = "Datum_Uhrzeit_der_Aufnahme")]
        public DateTime Datum_Uhrzeit_der_Aufnahme { get; set; }

        [JsonProperty(PropertyName = "Art_der_Entlassung")]
        public string Art_der_Entlassung { get; set; }

        [JsonProperty(PropertyName = "Datum_Uhrzeit_der_Entlassung")]
        public DateTime Datum_Uhrzeit_der_Entlassung { get; set; }
    }
}
