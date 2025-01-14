﻿using System;
using System.Collections.Generic;

namespace SmICSCoreLib.Factories.MiBi.PatientView.Parameter
{
    public class PathogenParameter : SpecimenParameter
    {
        //private string _name;
        //public string Name { get
        //    {
        //        return _name;
        //    }
        //    set
        //    {
        //        _name = value;
        //        MedicalField = GetResultType(value);
        //    } 
        //}
        private List<string> _pathogenCodes;
        public List<string> PathogenCodes 
        {
            get
            {
                return _pathogenCodes;
            }
            set 
            {
                _pathogenCodes = value;
                MedicalField = GetResultType(value);
            }
        }
        public string LabID { get; internal set; }
        public PathogenParameter() { }
        public PathogenParameter(SpecimenParameter parameter) : base(parameter) {}
        public PathogenParameter(PathogenParameter parameter) : base(parameter) 
        {
            LabID = parameter.LabID;
            MedicalField = parameter.MedicalField;
        }
        public PathogenParameter(SpecimenParameter parameter, string medicalField) : base(parameter) 
        {
            MedicalField = medicalField;
        }

        private string GetResultType(List<string> _pathogenCodes)
        {
            if (_pathogenCodes is not null)
            {
                if (_pathogenCodes.Contains("94500-6"))
                {
                    return SmICSCoreLib.Factories.General.MedicalField.VIROLOGY;
                }
                return SmICSCoreLib.Factories.General.MedicalField.MICROBIOLOGY;
            }
            throw new ArgumentNullException("Missing PathogenParameter");
        }

        public string PathogenCodesToAqlMatchString()
        {
            string convertedList = string.Join("','", PathogenCodes);
            return "{'" + convertedList + "'}";
        }
    }
}