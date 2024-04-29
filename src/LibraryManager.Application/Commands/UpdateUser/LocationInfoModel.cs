using LibraryManager.Core.ValueObjects;

namespace LibraryManager.Application.Commands.UpdateUser
{
    public class LocationInfoModel
    {
        public string Cep { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public LocationInfo ToValueObject() => new LocationInfo(Cep, Address, District, City, State);
    }
}