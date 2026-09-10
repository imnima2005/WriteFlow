using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteFlow.CoreLayer.DTOs.MainPageDto;

namespace WriteFlow.CoreLayer.Services.MainPage
{
    public interface IMainPageServices
    {
        MainPageDto GetData();
        SiteStatsDto GetSiteStats();
    }

}
