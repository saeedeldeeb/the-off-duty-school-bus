using BusManagement.Core.Common.Enums;

namespace BusManagement.Core.Common.Constants;

public static class Permissions
{
    public static IEnumerable<string> GeneratePermissionsList(string module)
    {
        return
        [
            $"Permissions.{module}.View",
            $"Permissions.{module}.Create",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete"
        ];
    }

    public static List<string> GenerateAllPermissions()
    {
        var allPermissions = new List<string>();

        var modules = Enum.GetValues(typeof(ModuleEnum));

        foreach (var module in modules)
            allPermissions.AddRange(GeneratePermissionsList(module.ToString()));

        return allPermissions;
    }

    public static object[,] GeneratePermissionsDataForSchoolTransportationManager()
    {
        // List of modules to generate permissions for
        var modules = new List<string>
        {
            ModuleEnum.VehicleBrand.ToString(),
            ModuleEnum.Vehicle.ToString(),
            ModuleEnum.Profile.ToString(),
            ModuleEnum.Rent.ToString(),
        };

        var permissions = new List<string>();

        foreach (var module in modules)
            permissions.AddRange(GeneratePermissionsList(module));
        var permissionsData = new object[permissions.Count, 4];
        for (var i = 0; i < permissions.Count; i++)
        {
            permissionsData[i, 0] = i;
            permissionsData[i, 1] = "fcdb4a01-a1be-4d5b-92e4-08b1163f47c7";
            permissionsData[i, 2] = "Permission";
            permissionsData[i, 3] = permissions[i];
        }

        return permissionsData;
    }

    public static object[,] GeneratePermissionsDataForCompanyTransportationManager()
    {
        // List of modules to generate permissions for
        var modules = new List<string>
        {
            ModuleEnum.Rent.ToString(),
            ModuleEnum.Profile.ToString(),
            ModuleEnum.Trip.ToString(),
            ModuleEnum.OffDuty.ToString()
        };

        var permissions = new List<string>();

        foreach (var module in modules)
            permissions.AddRange(GeneratePermissionsList(module));
        var permissionsData = new object[permissions.Count, 4];
        for (var i = 0; i < permissions.Count; i++)
        {
            permissionsData[i, 0] = i + permissions.Count;
            permissionsData[i, 1] = "0de8240e-0bfc-492d-9758-d041c1314812";
            permissionsData[i, 2] = "Permission";
            permissionsData[i, 3] = permissions[i];
        }

        return permissionsData;
    }

    public static class VehicleBrand
    {
        public const string View = "Permissions.VehicleBrand.View";
        public const string Create = "Permissions.VehicleBrand.Create";
        public const string Edit = "Permissions.VehicleBrand.Edit";
        public const string Delete = "Permissions.VehicleBrand.Delete";
    }

    public static class Vehicle
    {
        public const string View = "Permissions.Vehicle.View";
        public const string Create = "Permissions.Vehicle.Create";
        public const string Edit = "Permissions.Vehicle.Edit";
        public const string Delete = "Permissions.Vehicle.Delete";
    }

    public static class Profile
    {
        public const string View = "Permissions.Profile.View";
        public const string Create = "Permissions.Profile.Create";
        public const string Edit = "Permissions.Profile.Edit";
        public const string Delete = "Permissions.Profile.Delete";
    }

    public static class Trip
    {
        public const string View = "Permissions.Trip.View";
        public const string Create = "Permissions.Trip.Create";
        public const string Edit = "Permissions.Trip.Edit";
        public const string Delete = "Permissions.Trip.Delete";
    }

    public static class Rent
    {
        public const string View = "Permissions.Rent.View";
        public const string Create = "Permissions.Rent.Create";
        public const string Edit = "Permissions.Rent.Edit";
        public const string Delete = "Permissions.Rent.Delete";
    }

    public static class OffDuty
    {
        public const string View = "Permissions.OffDuty.View";
        public const string Create = "Permissions.OffDuty.Create";
        public const string Edit = "Permissions.OffDuty.Edit";
        public const string Delete = "Permissions.OffDuty.Delete";
    }
}
