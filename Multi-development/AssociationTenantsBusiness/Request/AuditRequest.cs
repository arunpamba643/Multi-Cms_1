using MediatR;

namespace AssociationBusiness.Request
{
    public class AuditRequest : INotification
    {
        public string? Message { get; set; }
    }
}
