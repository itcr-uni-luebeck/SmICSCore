﻿using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Quartz;
using System;
using Microsoft.Extensions.Logging.Abstractions;
using SmICSCoreLib.DB.MenuItems;
using SmICSCoreLib.DB.Models;
using SmICSCoreLib.Factories.MenuList;
using System.Collections.Generic;
using System.Linq;

namespace SmICSCoreLib.CronJobs
{
    [DisallowConcurrentExecution]
    public class MenuItemsJob : IJob
    {
        private IMenuItemDataAccess _dataAccess;
        private IMenuListFactory _menuListFac;
        public MenuItemsJob(IMenuItemDataAccess dataAccess, IMenuListFactory menuListFac)
        {
            _dataAccess = dataAccess;
            _menuListFac = menuListFac;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Task patho = Task.Run(UpdatePathogens);
            Task wards = Task.Run(UpdateWards);
            await Task.WhenAll(wards);
        }

        public void UpdateWards()
        {
            List<Ward> wards = _dataAccess.GetWards().GetAwaiter().GetResult();
            List<WardMenuEntry> wardMenuEntries = null;
            if (wards.Count == 0)
            {
                DateTime date = new DateTime((DateTime.Now.Year - 10), 1, 1);
                try {
                    string year = Environment.GetEnvironmentVariable("FIRST_DATA_ENTRY_YEAR");
                    date = new DateTime(Convert.ToInt32(year), 1, 1);
                }
                catch
                { 
                    Console.WriteLine(string.Format("ENV FIRST_DATA_ENRTY_YEAR couldn't be read. Instead {0} was set as FIRST_DATA_ENRTY_YEAR!", date.Year));
                }
                wardMenuEntries = _menuListFac.WardsAsync(JobType.FIRST_STARTUP, date).GetAwaiter().GetResult();
            }
            else
            {
                wardMenuEntries = _menuListFac.WardsAsync(JobType.DAILY, DateTime.Now.Date.AddDays(-1.0)).GetAwaiter().GetResult();
            }
            if (wardMenuEntries is not null)
            {
                foreach (WardMenuEntry ward in wardMenuEntries)
                {
                    if (!string.IsNullOrEmpty(ward.ID))
                    {
                        if (wards is not null && wards.Where(w => w.Name == ward.ID).Count() == 0)
                        {
                            _dataAccess.SetWard(new Ward() { Name = ward.ID });
                        }
                        else if (wards is null)
                        {
                            _dataAccess.SetWard(new Ward() { Name = ward.ID });
                        }
                    }
                }
                wardMenuEntries = null;
                wards = null;
            }
        }

        public void UpdatePathogens()
        {
            List<PathogenMenuEntry> pathoMenu = null;
            List<Pathogen> pathogens = _dataAccess.GetPathogens().GetAwaiter().GetResult();
            if(pathogens.Count == 0)
            {
                DateTime date = new DateTime((DateTime.Now.Year - 10), 1, 1);
                try
                {
                    string year = Environment.GetEnvironmentVariable("FIRST_DATA_ENTRY_YEAR");
                    date = new DateTime(Convert.ToInt32(year), 1, 1);
                }
                catch
                {
                    Console.WriteLine(string.Format("ENV FIRST_DATA_ENRTY_YEAR couldn't be read. Instead {0} was set as FIRST_DATA_ENRTY_YEAR!", date.Year));
                }
                pathoMenu = _menuListFac.PathogensAsync(JobType.FIRST_STARTUP, date).GetAwaiter().GetResult();
            }
            else
            {
                pathoMenu = _menuListFac.PathogensAsync(JobType.DAILY, DateTime.Now.Date.AddDays(-1.0)).GetAwaiter().GetResult();
            }
            if (pathoMenu is not null)
            {
                foreach (PathogenMenuEntry pat in pathoMenu)
                {
                    if (!string.IsNullOrEmpty(pat.ID))
                    {
                        if (pathogens.Where(p => p.Code == pat.ID).Count() == 0)
                        {
                            _dataAccess.SetPathogen(new Pathogen() { Code = pat.ID, Name = pat.Name });
                        }
                        else if (pathogens.Where(p => p.Code == pat.ID).Count() == 1)
                        {
                            _dataAccess.UpdatePathogen(new Pathogen() { Code = pat.ID, Name = pat.Name });
                        }
                    }
                }
                pathoMenu = null;
                pathogens = null;
            }
        }
    }
}
