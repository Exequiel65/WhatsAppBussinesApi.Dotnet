using System.Diagnostics.CodeAnalysis;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    public class ReactionMessage : BaseMessage
    {
        public override TypeMessage type => TypeMessage.reaction;
        public required ReactionComponent reaction { get; init; }

        public ReactionMessage()
        {
        }

        [SetsRequiredMembers]
        public ReactionMessage(string to, string messageId, string emoji)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            reaction = new ReactionComponent(messageId, emoji);
        }

        [SetsRequiredMembers]
        public ReactionMessage(string to, ReactionComponent reaction)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.reaction = reaction ?? throw new ArgumentNullException(nameof(reaction));
        }
    }

    public class ReactionComponent
    {
        public string message_id { get; set; }
        public string emoji { get; set; }

        public ReactionComponent()
        {
            message_id = string.Empty;
            emoji = string.Empty;
        }

        public ReactionComponent(string messageId, string emoji)
        {
            if (string.IsNullOrWhiteSpace(messageId))
            {
                throw new ArgumentException("Message id is required.", nameof(messageId));
            }

            if (string.IsNullOrWhiteSpace(emoji))
            {
                throw new ArgumentException("Emoji is required.", nameof(emoji));
            }

            message_id = messageId;
            this.emoji = emoji;
        }
    }
}
