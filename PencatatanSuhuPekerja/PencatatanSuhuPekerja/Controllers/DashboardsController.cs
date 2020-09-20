using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PencatatanSuhuPekerjaAPI.Repositories;

namespace PencatatanSuhuPekerjaAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly DashboardRepository _dashboardRepository;

        public DashboardsController(DashboardRepository dashboardRepository)
        {
            this._dashboardRepository = dashboardRepository;
        }

        // GET: Dashboards
        [HttpGet]
        public async Task<ActionResult> GetChartData()
        {
            var chartData = await _dashboardRepository.GetTemperatureChartData();
            return Ok(chartData);
        }

    }
}