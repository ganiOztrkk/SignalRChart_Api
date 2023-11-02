using CovidChart.Api.Models;
using Microsoft.AspNetCore.SignalR;

namespace CovidChart.Api.Hubs;

public class CovidHub : Hub
{
    private readonly CovidService _service;

    public CovidHub(CovidService service)
    {
        _service = service;
    }

    public async Task GetCovidList()
    {
        await Clients.All.SendAsync("ReceiveCovidList", _service.GetCovidChartList());
    }
}