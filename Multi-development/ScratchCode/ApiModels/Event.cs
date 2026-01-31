namespace ScratchCode.ApiModels;

public class Event 
{
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

    public DateTime? CancelledDateTime { get; set; }
    public string CancelledNote { get; set; }

    
    public DateTime EndTime { get; set; }

    
    public DateTime WithDrawalDeadlineTime { get; set; }

    
    public int? MinParticipating { get; set; }

    
    public int? MaxParticipating { get; set; }

    
    public int? MaxWaiting { get; set; }

    
    public string ContractId { get; set; }

    
    public bool PaymentRequired { get; set; } = false;

    
    public bool AddressAndIBANRequired { get; set; }

    public string AvatarImageUrl { get; set; }
    public string HeaderImageUrl { get; set; }

    
    public bool AddressRequired { get; set; }

    
    public bool IBANRequired { get; set; }

    
    public string ClubRequired { get; set; }

    
    public DateTime CreatedAt { get; set; }

    
    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; }

    public string SeminarId { get; set; }

    
    public bool Disabled { get; set; }

    
    public bool Deleted { get; set; }

    public string Id { get; set; }


    
    public List<TagElement> TagElements { get; set; } = new();

    
    public List<string> Participants { get; set; } = new();

    
    public List<string> Waiting { get; set; } = new();

    
    public List<string> ContactPersons { get; set; } = new();

    
    
    public string PublicationCriteria { get; set; }

    
    public bool IsCompetitiveSport { get; set; }

    /// <summary>
    ///     Breitensport
    /// </summary>
    
    public bool IsPopularSport { get; set; }

    
    public bool HasReferee { get; set; }

    
    public DateTime PublishStartTime { get; set; }

    
    public DateTime PublishEndTime { get; set; }
    
    
    public string SeasonId { get; set; }
    
    public bool ChatEnabled { get; set; }

    public string ChatId { get; set; }
}