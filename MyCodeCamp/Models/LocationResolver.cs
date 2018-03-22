using AutoMapper;
using MyCodeCamp.Data.Entities;

namespace MyCodeCamp.Models
{
    public class LocationResolver : IValueResolver<CampModel, Camp , Location>
    {
        public Location Resolve(CampModel source, Camp destination, Location destMember, ResolutionContext context)
        {
            Location location = destination.Location ?? new Location();

            location.Address1 = source.LocationAddress1;
            location.Address2 = source.LocationAddress2;
            location.Address3 = source.LocationAddress3;
            location.CityTown = source.LocationCityTown;
            location.StateProvince = source.LocationStateProvince;
            location.PostalCode = source.LocationPostalCode;
            location.Country = source.LocationCountry;

            return location;
        }
    }
}