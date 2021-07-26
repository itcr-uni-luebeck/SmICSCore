﻿using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Quartz;
using System;


namespace SmICSCoreLib.StatistikServices.CronJob
{
    [DisallowConcurrentExecution]
    public class JobGetReport: IJob
    {
        private readonly ILogger<JobGetReport> _logger;

        public JobGetReport(ILogger<JobGetReport> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {         
            RkiService rkiRestApi = new();
            bool status = rkiRestApi.SerializeRkiData();
            if (status == true)
            {
                _logger.LogInformation($"**DailyReport wurde am " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss") + " erfolgreich durchgeführt!");
            }
            else
            {
                _logger.LogInformation($"**DailyReport könnte am " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss") + " leider nicht durchgeführt werden!");
            }
            return Task.CompletedTask;
        }
    }
}
