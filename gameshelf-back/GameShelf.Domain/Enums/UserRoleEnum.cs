using System.Runtime.Serialization;

namespace GameShelf.Domain.Enums
{
    public enum UserRole
    {
        [EnumMember(Value = "Utilisateur")]
        User,
        [EnumMember(Value = "Administrateur")]
        Admin
    }
}