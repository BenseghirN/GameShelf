using System.Runtime.Serialization;

namespace GameShelf.Domain.Enums
{
    public enum ProposalStatus
    {
        [EnumMember(Value = "En Attente")]
        EnAttente,
        [EnumMember(Value = "Validée")]
        Validee,
        [EnumMember(Value = "Refusée")]
        Refusee
    }
}