using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParcAuto.Models;
using Web.Data;

namespace Web.Vehicles
{
    public class IndexModel : PageModel
    {
        private readonly VehicleService _vehicleService;

        public IndexModel(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public List<Vehicle> Vehicles { get; set; } = new();

        public async Task OnGetAsync()
        {
            Vehicles = await _vehicleService.GetVehiclesAsync();
        }
    }
}
