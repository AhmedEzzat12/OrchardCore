using System.Collections.Generic;
using Microsoft.Extensions.Localization;
using OrchardCore.AuditTrail.Extensions;
using OrchardCore.AuditTrail.Models;
using OrchardCore.AuditTrail.Providers;
using OrchardCore.AuditTrail.Services.Models;
using OrchardCore.Entities;
using OrchardCore.Modules;
using OrchardCore.Users.AuditTrail.Models;

namespace OrchardCore.Users.AuditTrail.Providers
{
    [RequireFeatures("OrchardCore.AuditTrail")]
    public class UserAuditTrailEventProvider : AuditTrailEventProviderBase
    {
        public const string SignedUp = nameof(SignedUp);
        public const string LoggedIn = nameof(LoggedIn);
        public const string LogInFailed = nameof(LogInFailed);
        public const string PasswordReset = nameof(PasswordReset);
        public const string PasswordRecovered = nameof(PasswordRecovered);
        public const string Enabled = nameof(Enabled);
        public const string Disabled = nameof(Disabled);
        public const string Created = nameof(Created);
        public const string Deleted = nameof(Deleted);

        public UserAuditTrailEventProvider(IStringLocalizer<UserAuditTrailEventProvider> stringLocalizer)
        {
            T = stringLocalizer;
        }

        public override void Describe(DescribeContext context) =>
            context.For<UserAuditTrailEventProvider>("User", T["User"])
                .Event(SignedUp, T["Signed up"], T["A user was successfully signed up."], BuildAuditTrailEvent, true)
                .Event(LoggedIn, T["Logged in"], T["A user was successfully logged in."], BuildAuditTrailEvent, true)
                .Event(LogInFailed, T["Login failed"], T["An attempt to login failed due to incorrect credentials."], BuildAuditTrailEvent, true)
                .Event(PasswordReset, T["Password reset"], T["A user successfully reset the password."], BuildAuditTrailEvent, true)
                .Event(PasswordRecovered, T["Password recovered"], T["A user successfully recovered the password."], BuildAuditTrailEvent, true)
                .Event(Enabled, T["Enabled"], T["A user was enabled."], BuildAuditTrailEvent, true)
                .Event(Disabled, T["Disabled"], T["A user was disabled."], BuildAuditTrailEvent, true)
                .Event(Created, T["Created"], T["A user was created."], BuildAuditTrailEvent, true)
                .Event(Deleted, T["Deleted"], T["A user was deleted."], BuildAuditTrailEvent, true);

        private void BuildAuditTrailEvent(AuditTrailEvent auditTrailEvent, Dictionary<string, object> eventData)
        {
            auditTrailEvent.Put(new AuditTrailUserEvent
            {
                EventName = auditTrailEvent.EventName,
                UserName = eventData.Get<string>("UserName"),
                UserId = eventData.Get<string>("UserId")
            });
        }
    }
}