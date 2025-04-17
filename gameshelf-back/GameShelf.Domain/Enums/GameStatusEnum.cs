using System.Runtime.Serialization;

namespace GameShelf.Domain.Enums
{
    public enum GameStatus
    {
        [EnumMember(Value = "Possédé")]
        Possede,
        [EnumMember(Value = "En Cours")]
        EnCours,
        [EnumMember(Value = "Terminé")]
        Termine
    }
}