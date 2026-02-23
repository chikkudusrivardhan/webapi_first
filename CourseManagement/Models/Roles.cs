using System.Text.Json.Serialization;

namespace CourseManagement.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Roles
    {
        Admin = 1,
        Student = 2,
        CourseOwner = 3
    }
}
