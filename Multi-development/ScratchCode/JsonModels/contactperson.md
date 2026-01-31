

## Single Contact

A GET request to `api/contacts/{contactId}` will return a single contact person object in the following JSON format:

```json
{
  "id": 1,
  "name": "John Doe",
  "phoneNumber": "+1234567890",
  "emailAddress": "john@doe.com",
  "avatarImageUrl": "https://example.com/images/johndoe.png"
},
{
  "id": 2,
  "name": "John Doe",
  "phoneNumber": "+1234567890",
  "emailAddress": "john@doe.com",
  "avatarImageUrl": "https://example.com/images/johndoe.png"
}
```


```markdown
# Contact Person Data Transfer Object (DTO)

The following C# model represents a contact person object received via the API:

```csharp
public class ContactPerson
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
    public Uri AvatarImageUrl { get; set; }
}
```