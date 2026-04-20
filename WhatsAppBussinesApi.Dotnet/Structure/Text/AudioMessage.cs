using System.Diagnostics.CodeAnalysis;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    public class AudioMessage : BaseMessage
    {
        public override TypeMessage type => TypeMessage.audio;
        public required AudioComponent audio { get; init; }

        public AudioMessage()
        {
        }

        [SetsRequiredMembers]
        public AudioMessage(string to, string id)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            audio = new AudioComponent(id);
        }

        [SetsRequiredMembers]
        public AudioMessage(string to, AudioComponent audio)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.audio = audio ?? throw new ArgumentNullException(nameof(audio));
        }
    }

    public class AudioComponent
    {
        public string id { get; init; }

        public AudioComponent(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Audio id cannot be empty", nameof(id));
            }

            this.id = id;
        }
    }
}
