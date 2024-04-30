using LibraryManager.Core.ValueObjects;

namespace LibraryManager.Application.Commands.UpdateUser
{
    public class LocationInfoModel
    {
        public LocationInfoModel(LocationInfo locationInfo)
        {
            Cep = locationInfo.Cep;
            Address = locationInfo.Address;
            District = locationInfo.District;
            City = locationInfo.City;
            State = locationInfo.State;
        }

        public string Cep { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public LocationInfo ToValueObject() => new LocationInfo(Cep, Address, District, City, State);
    }
}