using AssociationEntities.CustomModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationRepository.Data
{
    public class EventData
    {
        public static string jsonData = @"[
  {
    ""$id"": ""22537"",
    ""requestRequired"": false,
    ""creator"": {
      ""$id"": ""22538"",
      ""id"": ""bbeee06d-8cae-447a-a511-01b5b089843c"",
      ""name"": ""Skiverband Schwarzwald Nord e.V."",
      ""label"": ""association"",
      ""avatarImageUrl"": """",
      ""creatorClubPoint"": {
        ""latitude"": 48.123456,
        ""longitude"": 8.765432
      }
    },
    ""pkey"": ""de_1"",
    ""closed"": false,
    ""filterRestrictionCount"": 10,
    ""invitedCount"": 0,
    ""disableParticipants"": false,
    ""detailPageId"": """",
    ""participantCount"": 10,
    ""waitingCount"": 0,
    ""description"": ""<p>25.04. - 29.04.2024</p>\r\n"",
    ""eventType"": ""ausbildung"",
    ""startTime"": ""2024-04-25T20:00:00+00:00"",
    ""cancelledNote"": """",
    ""endTime"": ""2024-04-29T13:00:00+00:00"",
    ""withDrawalDeadlineTime"": ""2024-03-08T09:27:00+00:00"",
    ""maxParticipating"": 18,
    ""maxWaiting"": 4,
    ""contractId"": ""e602e7dd-60e5-4aaa-afda-08dbb9ed6f6b"",
    ""paymentRequired"": true,
    ""addressAndIBANRequired"": false,
    ""avatarImageUrl"": """",
    ""headerImageUrl"": """",
    ""addressRequired"": true,
    ""ibanRequired"": true,
    ""clubRequired"": ""required"",
    ""createdAt"": ""2023-09-20T15:31:36+00:00"",
    ""updatedAt"": ""1901-12-13T20:45:52+00:00"",
    ""name"": ""2024 Modul 3 Ü30 Trainer C & B"",
    ""seminarId"": ""ÜMod3-24"",
    ""disabled"": false,
    ""deleted"": false,
    ""id"": ""ca22dd7c-ff78-462e-b3a9-23f64a00ddd7"",
    ""startingPoint"": {
      ""latitude"": 47.876543,
      ""longitude"": 9.345678
    },
    ""tagElements"": [ ""tag1"", ""tag2"", ""tag3"", ""tag4"" ],
    ""divisions"": [ ""division1"", ""division2"", ""division3"", ""division4"" ],
    ""disciplines"": [ ""discipline1"", ""discipline2"", ""discipline3"", ""discipline4"" ],
    ""contactPersons"": [ ""person1"", ""person2"", ""person3"", ""person4"" ],
    ""publicationCriteria"": ""Association"",
    ""isCompetitiveSport"": false,
    ""isPopularSport"": false,
    ""hasReferee"": false,
    ""publishStartTime"": ""1901-12-13T20:45:52+00:00"",
    ""publishEndTime"": ""1901-12-13T20:45:52+00:00"",
    ""chatEnabled"": false,
    ""chatId"": ""a9222ca2735ad1aebef43d6a7cd2408c7820ddae828caac7e108976b5698bd6d""
  },


  {
    ""$id"": ""12345"",
    ""requestRequired"": true,
    ""creator"": {
      ""$id"": ""54321"",
      ""id"": ""abcdef12-3456-7890-cdef-abcdef123456"",
      ""name"": ""Community Sports Club"",
      ""label"": ""club"",
      ""avatarImageUrl"": ""https://example.com/avatar.png"",
      ""creatorClubPoint"": {
        ""latitude"": 40.7128,
        ""longitude"": -74.0060
      }
    },
    ""pkey"": ""us_2"",
    ""closed"": false,
    ""filterRestrictionCount"": 5,
    ""invitedCount"": 20,
    ""disableParticipants"": false,
    ""detailPageId"": ""event123"",
    ""participantCount"": 15,
    ""waitingCount"": 5,
    ""description"": ""<p>Join us for a fun day of sports and games!</p>"",
    ""eventType"": ""sports"",
    ""startTime"": ""2024-06-15T09:00:00+00:00"",
    ""cancelledNote"": """",
    ""endTime"": ""2024-06-15T17:00:00+00:00"",
    ""withDrawalDeadlineTime"": ""2024-05-30T00:00:00+00:00"",
    ""maxParticipating"": 20,
    ""maxWaiting"": 5,
    ""contractId"": ""xyz123"",
    ""paymentRequired"": false,
    ""addressAndIBANRequired"": false,
    ""avatarImageUrl"": ""https://example.com/event_avatar.png"",
    ""headerImageUrl"": ""https://example.com/event_header.png"",
    ""addressRequired"": true,
    ""ibanRequired"": false,
    ""clubRequired"": ""not_required"",
    ""createdAt"": ""2024-03-01T12:00:00+00:00"",
    ""updatedAt"": ""2024-03-10T08:30:00+00:00"",
    ""name"": ""Community Sports Day"",
    ""seminarId"": ""CS-2024"",
    ""disabled"": false,
    ""deleted"": false,
    ""id"": ""7890ab12-cdef-3456-7890-abcdef123456"",
    ""startingPoint"": {
      ""latitude"": 40.7128,
      ""longitude"": -74.0060
    },
    ""tagElements"": [ ""tag2"", ""tag3"" ],
    ""divisions"": [ ""division1"", ""division3"" ],
    ""disciplines"": [ ""discipline1"", ""discipline4"" ],
    ""contactPersons"": [ ""person1"", ""persion2"" ],
    ""publicationCriteria"": ""Public"",
    ""isCompetitiveSport"": false,
    ""isPopularSport"": true,
    ""hasReferee"": false,
    ""publishStartTime"": ""2024-04-01T00:00:00+00:00"",
    ""publishEndTime"": ""2024-05-01T00:00:00+00:00"",
    ""chatEnabled"": true,
    ""chatId"": ""event123_chat""
  },
 
  // Event 2
  {
    ""$id"": ""23456"",
    ""requestRequired"": true,
    ""creator"": {
      ""$id"": ""65432"",
      ""id"": ""12345678-9012-3456-7890-123456789012"",
      ""name"": ""Adventure Club"",
      ""label"": ""club"",
      ""avatarImageUrl"": ""https://example.com/avatar2.png"",
      ""creatorClubPoint"": {
        ""latitude"": 35.6895,
        ""longitude"": 139.6917
      }
    },
    ""pkey"": ""jp_1"",
    ""closed"": false,
    ""filterRestrictionCount"": 10,
    ""invitedCount"": 30,
    ""disableParticipants"": false,
    ""detailPageId"": ""event456"",
    ""participantCount"": 20,
    ""waitingCount"": 10,
    ""description"": ""<p>Explore the great outdoors with us!</p>"",
    ""eventType"": ""adventure"",
    ""startTime"": ""2024-07-10T08:00:00+00:00"",
    ""cancelledNote"": """",
    ""endTime"": ""2024-07-12T18:00:00+00:00"",
    ""withDrawalDeadlineTime"": ""2024-06-20T00:00:00+00:00"",
    ""maxParticipating"": 30,
    ""maxWaiting"": 10,
    ""contractId"": ""abc456"",
    ""paymentRequired"": true,
    ""addressAndIBANRequired"": true,
    ""avatarImageUrl"": ""https://example.com/event_avatar2.png"",
    ""headerImageUrl"": ""https://example.com/event_header2.png"",
    ""addressRequired"": true,
    ""ibanRequired"": true,
    ""clubRequired"": ""required"",
    ""createdAt"": ""2024-04-01T09:00:00+00:00"",
    ""updatedAt"": ""2024-04-10T15:30:00+00:00"",
    ""name"": ""Outdoor Adventure Weekend"",
    ""seminarId"": ""ADVENT-2024"",
    ""disabled"": false,
    ""deleted"": false,
    ""id"": ""23456789-0123-4567-8901-234567890123"",
    ""startingPoint"": {
      ""latitude"": 35.6895,
      ""longitude"": 139.6917
    },
    ""tagElements"": [ ""tag2"", ""tag3"" ],
    ""divisions"": [ ""division1"", ""division3"" ],
    ""disciplines"": [ ""discipline1"", ""discipline4"" ],
    ""contactPersons"": [ ""person1"", ""persion2"" ],
    ""publicationCriteria"": ""Public"",
    ""isCompetitiveSport"": false,
    ""isPopularSport"": false,
    ""hasReferee"": false,
    ""publishStartTime"": ""2024-05-01T00:00:00+00:00"",
    ""publishEndTime"": ""2024-06-01T00:00:00+00:00"",
    ""chatEnabled"": true,
    ""chatId"": ""event456_chat""
  }

]";
        //// Deserialize JSON data to EventDetails object
        //public static EventModel eventDetails = JsonConvert.DeserializeObject<EventModel>(jsonData);

        // Create a list to hold EventDetails objects
        public static List<EventModel> eventList = JsonConvert.DeserializeObject<List<EventModel>>(jsonData);




        public EventData()
        {
            // Add the deserialized EventDetails object to the list
            //eventList.Add(eventDetails);
        }
    }


}
