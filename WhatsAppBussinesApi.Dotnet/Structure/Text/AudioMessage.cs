using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

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
        public AudioMessage(string to, string id, bool? voice = null)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            audio = new AudioComponent(id)
            {
                voice = voice
            };
        }

        [SetsRequiredMembers]
        public AudioMessage(string to, Uri link, bool? voice = null)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            audio = new AudioComponent(link)
            {
                voice = voice
            };
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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? id { get; init; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? link { get; init; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? voice { get; init; }

        public AudioComponent()
        {
        }

        public AudioComponent(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Audio id cannot be empty", nameof(id));
            }

            this.id = id;
        }

        public AudioComponent(Uri link)
        {
            this.link = link?.ToString() ?? throw new ArgumentNullException(nameof(link));
        }
    }
}
