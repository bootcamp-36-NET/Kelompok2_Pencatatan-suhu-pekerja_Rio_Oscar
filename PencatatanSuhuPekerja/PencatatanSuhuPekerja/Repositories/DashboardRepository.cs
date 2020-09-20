using Microsoft.EntityFrameworkCore;
using PencatatanSuhuPekerjaAPI.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PencatatanSuhuPekerjaAPI.Repositories
{
    public class DashboardRepository
    {
        private readonly MyContext _myContext;

        public DashboardRepository(MyContext myContext)
        {
            this._myContext = myContext;
        }

        public async Task<List<int>> GetTemperatureChartData()
        {
            List<int> chartData = new List<int>();

            for (int i = 6; i > 1; i--)
            {
                DateTime lastweekday = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - i);

                var indicatedDataOnIDay = await _myContext.Temperatures.Where(q => q.Date.ToString("d") == lastweekday.ToString("d")).Select(q => q.EmployeeTemperature).ToListAsync();

                if (indicatedDataOnIDay.Count == 0 || indicatedDataOnIDay == null)
                {
                    chartData.Add(0);
                }
                else
                {
                    var totalIndicatedDataOnIDay = indicatedDataOnIDay.Where(q => double.Parse(q) >= 37.3).Count();
                    chartData.Add(totalIndicatedDataOnIDay);
                }

            }

            return chartData;
        }
    }
}