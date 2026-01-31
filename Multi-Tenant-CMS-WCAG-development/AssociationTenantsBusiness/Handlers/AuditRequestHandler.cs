using AssociationBusiness.Request;
using MediatR;

namespace AssociationBusiness.Handlers
{
    public class AuditRequestHandler : INotificationHandler<AuditRequest>
    {
        public Task Handle(AuditRequest notification, CancellationToken cancellationToken)
        {
            Console.WriteLine(notification.Message);
            return Task.CompletedTask;
        }
    }

    public class AuditRequestProcessor : INotificationHandler<AuditRequest>
    {
        public Task Handle(AuditRequest notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Checked " + notification.Message);
            return Task.CompletedTask;
        }
    }
}
