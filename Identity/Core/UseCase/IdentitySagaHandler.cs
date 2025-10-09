using Api.Events;
using Core.Domain.RepositoryInterface;
using Microsoft.Extensions.DependencyInjection;
using NATS.Client;
using System.Text;
using System.Text.Json;

namespace Core.UseCase
{
    public class IdentitySagaHandler
    {
        private readonly IConnection nats;
        private readonly IServiceScopeFactory scopeFactory;

        public IdentitySagaHandler(IConnection nats, IServiceScopeFactory scopeFactory)
        {
            this.nats = nats;
            this.scopeFactory = scopeFactory;
        }

        public void Subscribe()
        {
            // Identity sluša purchase request event
            nats.SubscribeAsync("tours.purchase.requested", async (s, e) =>
            {
                var json = Encoding.UTF8.GetString(e.Message.Data);
                var evt = JsonSerializer.Deserialize<TourPurchaseRequested>(json);
                if (evt == null) return;

                using var scope = scopeFactory.CreateScope();
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                var user = await userRepository.GetById((int)evt.UserId);
                if (user == null)
                {
                    PublishBlocked(evt, "User not found");
                    return;
                }

                if (user.IsBlocked)
                {
                    PublishBlocked(evt, "User is blocked");
                }
                else
                {
                    PublishActive(evt);
                }
            });
        }

        private void PublishActive(TourPurchaseRequested request)
        {
            var evt = new UserActive(request.PurchaseId, request.UserId);
            var data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(evt));
            nats.Publish("identity.user.active", data);
        }

        private void PublishBlocked(TourPurchaseRequested request, string reason)
        {
            var evt = new UserBlocked(request.PurchaseId, request.UserId);
            var data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(evt));
            nats.Publish("identity.user.blocked", data);
        }
    }
}
