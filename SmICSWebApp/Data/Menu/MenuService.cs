﻿using SmICSCoreLib.DB.MenuItems;
using SmICSCoreLib.DB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmICSWebApp.Data.Menu
{
    public class MenuService
    {
        private IMenuItemDataAccess _menuDataAccess;

        public MenuService(IMenuItemDataAccess menuDataAccess)
        {
            _menuDataAccess = menuDataAccess;
        }

        public async Task<List<PathogenEntry>> GetPathogens()
        {
            try
            {
                List<Pathogen> pathogens = await _menuDataAccess.GetPathogens();
                List<PathogenEntry> pathogenEntries = new List<PathogenEntry>();
                foreach (Pathogen pat in pathogens)
                {
                    if (pat.Name.ToLower().Contains("sars-cov-2"))
                    {
                        IEnumerable<PathogenEntry> tmp = pathogenEntries.Where(p => p.Name == "SARS-CoV-2");
                        if (tmp.Count() == 0)
                        {
                            PathogenEntry patho = new PathogenEntry()
                            {
                                Name = "SARS-CoV-2",
                                Codes = new List<string>()
                            {
                                pat.Code
                            }
                            };
                            pathogenEntries.Add(patho);
                        }
                        else if (tmp.Count() == 1)
                        {
                            tmp.First().Codes.Add(pat.Code);
                        }
                    }
                    else
                    {
                        PathogenEntry patho = new PathogenEntry()
                        {
                            Name = pat.Name,
                            Codes = new List<string>()
                        {
                            pat.Code
                        }
                        };
                        pathogenEntries.Add(patho);
                    }
                }
                return pathogenEntries;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Ward>> GetWards()
        {
            try
            {
                return await _menuDataAccess.GetWards();
            }
            catch
            {
                throw;
            }
        }
    }
}
