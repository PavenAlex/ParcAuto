using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParcAuto.Models;
using Web.Data;

namespace Web.Pages.Clients
{
    public class IndexModel : PageModel
    {
        private readonly ClientService _clientService;

        public IndexModel(ClientService clientService)
        {
            _clientService = clientService;
        }

        public List<Client> Clients { get; set; } = new();

        public async Task OnGetAsync()
        {
            Clients = await _clientService.GetClientsAsync();
        }
    }
}
