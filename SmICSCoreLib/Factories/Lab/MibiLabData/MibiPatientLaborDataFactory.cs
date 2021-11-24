﻿using SmICSCoreLib.Factories.General;
using SmICSCoreLib.Factories.MiBi;
using SmICSCoreLib.REST;
using System.Collections.Generic;
using SmICSCoreLib.Factories.Lab.MibiLabdata.ReceiveModel;

namespace SmICSCoreLib.Factories.Lab.MibiLabData
{
    public class MibiPatientLaborDataFactory : IMibiPatientLaborDataFactory
    {
        private IRestDataAccess _restData;
        private IAntibiogramFactory _antibiogram;
        List<MibiLabDataModel> mibiLabDatas;
        public MibiPatientLaborDataFactory(IRestDataAccess restData, IAntibiogramFactory antibiogram)
        {
            _restData = restData;
            _antibiogram = antibiogram;
        }

        public List<MibiLabDataModel> Process(PatientListParameter parameter)
        {
            mibiLabDatas = new List<MibiLabDataModel>();

            List<MetaDataReceiveModel> metaDatas = _restData.AQLQuery<MetaDataReceiveModel>(AQLCatalog.CasesWithResults(parameter));

            if (metaDatas != null)
            {
                getSampleData(metaDatas);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("metaDatas is null");
            }

            return mibiLabDatas;
        }

        /*private void getAllUIDs(List<CaseIDReceiveModel> cases)
        {
            foreach (CaseIDReceiveModel caseID in cases)
            {
                List<MetaDataReceiveModel> metaDatas = _restData.AQLQuery<MetaDataReceiveModel>(AQLCatalog.ReportMeta(caseID);

                getSampleData(metaDatas);
            }
        }*/

        private void getCaseRequirement(List<MetaDataReceiveModel> metaDatas)
        {
            foreach (MetaDataReceiveModel metaData in metaDatas)
            {
                List<RequirementReceiveModel> requirements = _restData.AQLQuery<RequirementReceiveModel>(AQLCatalog.Requirements(metaData));
            }
        }

        private void getSampleData(List<MetaDataReceiveModel> metaDatas)
        {
            foreach (MetaDataReceiveModel metaData in metaDatas)
            {
                List<SampleReceiveModel> sampleDatas = _restData.AQLQuery<SampleReceiveModel>(AQLCatalog.SamplesFromResult(metaData));

                if (sampleDatas != null)
                {
                    getPathogenData(sampleDatas, metaData);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("sampleDatas is null");
                }
            }
        }

        private void getPathogenData(List<SampleReceiveModel> sampleDatas, MetaDataReceiveModel metaData)
        {
            foreach (SampleReceiveModel sampleData in sampleDatas)
            {
                List<PathogenReceiveModel> pathogenDatas = _restData.AQLQuery<PathogenReceiveModel>(AQLCatalog.PathogensFromResult(metaData, sampleData));

                if (pathogenDatas != null)
                {
                    createMibiLabData(pathogenDatas, sampleData, metaData);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("pathogenDatas is null");
                }

            }
        }

        private void createMibiLabData(List<PathogenReceiveModel> pathogenDatas, SampleReceiveModel sampleData, MetaDataReceiveModel metaData)
        {
            foreach (PathogenReceiveModel pathogenData in pathogenDatas)
            {
                MibiLabDataModel mibiLabData = new MibiLabDataModel(metaData, sampleData, pathogenData);
                AntibiogramParameter antibiogramParameter = new AntibiogramParameter()
                {
                    CaseID = metaData.FallID,
                    Pathogen = pathogenData.KeimID,
                    LabID = sampleData.LabordatenID,
                    UID = metaData.UID,
                    EhrID = metaData.PatientID,
                    IsolatNo = pathogenData.IsolatNo
                };
                mibiLabData.Antibiogram = _antibiogram.Process(antibiogramParameter);
                mibiLabDatas.Add(mibiLabData);
            }
        }
    }
}