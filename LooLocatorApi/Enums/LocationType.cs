using System.Runtime.Serialization;

namespace LooLocatorApi.Enums;

public enum LocationType
{
    [EnumMember(Value = "Business")] Business,

    [EnumMember(Value = "Public")] Public
}