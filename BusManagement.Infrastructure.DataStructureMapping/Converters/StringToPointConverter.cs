using AutoMapper;
using NetTopologySuite.Geometries;

namespace BusManagement.Infrastructure.DataStructureMapping.Converters;

public class StringToPointConverter : IValueConverter<string, Point>
{
    public Point Convert(string sourceMember, ResolutionContext context)
    {
        if (string.IsNullOrWhiteSpace(sourceMember))
            return null;

        var parts = sourceMember.Split(',');
        if (parts.Length != 2)
            throw new ArgumentException("Invalid format. Expected 'lat,lng'.");

        if (!double.TryParse(parts[0], out var lat) || !double.TryParse(parts[1], out var lng))
            throw new ArgumentException(
                "Invalid format. Expected 'lat,lng' where lat and lng are numbers."
            );

        return new Point(lng, lat) { SRID = 4326 };
    }
}
