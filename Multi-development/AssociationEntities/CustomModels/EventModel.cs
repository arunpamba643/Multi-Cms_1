using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationEntities.CustomModels
{


    public class CreatorClubPoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class Creator
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string AvatarImageUrl { get; set; }
        public CreatorClubPoint CreatorClubPoint { get; set; }
    }

    public class StartingPoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class EventModel
    {
        public string Id { get; set; }
        public bool RequestRequired { get; set; }
        public Creator Creator { get; set; }
        public string Pkey { get; set; }
        public bool Closed { get; set; }
        public int FilterRestrictionCount { get; set; }
        public int InvitedCount { get; set; }
        public bool DisableParticipants { get; set; }
        public string DetailPageId { get; set; }
        public int ParticipantCount { get; set; }
        public int WaitingCount { get; set; }
        public string Description { get; set; }
        public string EventType { get; set; }
        public DateTime StartTime { get; set; }
        public string CancelledNote { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime WithDrawalDeadlineTime { get; set; }
        public int MaxParticipating { get; set; }
        public int MaxWaiting { get; set; }
        public string ContractId { get; set; }
        public bool PaymentRequired { get; set; }
        public bool AddressAndIBANRequired { get; set; }
        public string AvatarImageUrl { get; set; }
        public string HeaderImageUrl { get; set; }
        public bool AddressRequired { get; set; }
        public bool IbanRequired { get; set; }
        public string ClubRequired { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Name { get; set; }
        public string SeminarId { get; set; }
        public bool Disabled { get; set; }
        public bool Deleted { get; set; }
        public StartingPoint StartingPoint { get; set; }
        public List<string> TagElements { get; set; }
        public List<string> Divisions { get; set; }
        public List<string> Disciplines { get; set; }
        public List<string> ContactPersons { get; set; }
        public string PublicationCriteria { get; set; }
        public bool IsCompetitiveSport { get; set; }
        public bool IsPopularSport { get; set; }
        public bool HasReferee { get; set; }
        public DateTime PublishStartTime { get; set; }
        public DateTime PublishEndTime { get; set; }
        public bool ChatEnabled { get; set; }
        public string ChatId { get; set; }
    }

    public class EventFilters
    {
        public DateTime? fromDate { get; set; }
        public ScopeType? ScopeType { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string? OrderBy { get; set; }
        public List<string>? SearchFields { get; set; }
        public string? Keyword { get; set; }
        public List<string>? DivisionsIds { get; set; } = new List<string>();
        public List<string>? DisciplinesIds { get; set; } = new List<string>();
        public List<string>? TagIds { get; set; } = new List<string>();
        public List<string>? CreatorIds { get; set; } = new List<string>();
    }

  

}
