using System.Runtime.Serialization;

namespace GameShelf.Domain.Enums
{
    public enum GameStatus
    {
        [EnumMember(Value = "Terminé")]
        Termine,
        [EnumMember(Value = "Possédé")]
        Possede,
        [EnumMember(Value = "En cours")]
        EnCours,
    }
}