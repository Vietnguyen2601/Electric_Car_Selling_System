using Microsoft.AspNetCore.SignalR;

namespace ElectricVehicleDealer.Presentation.Hubs
{
    public class VehicleHub : Hub
    {
        public const string VehicleCatalogChangedEvent = "VehicleCatalogChanged";
    }
}
