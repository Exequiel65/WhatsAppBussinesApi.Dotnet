using WhatsAppBussinesApi.Dotnet.Structure.Text;

namespace WhatsAppBussinesApi.Dotnet.Builders.Text
{
    public sealed class AudioMessageBuilder : IMessageBuilder<AudioMessage>
    {
        private string? _recipient;
        private AudioComponent? _audio;
        private bool? _voice;

        public AudioMessageBuilder To(string phoneNumber)
        {
            _recipient = phoneNumber;
            return this;
        }

        public AudioMessageBuilder WithAudioId(string id)
        {
            _audio = new AudioComponent(id);
            return this;
        }

        public AudioMessageBuilder WithAudioLink(string link)
        {
            _audio = new AudioComponent(new Uri(link));
            return this;
        }

        public AudioMessageBuilder AsVoice(bool voice = true)
        {
            _voice = voice;
            return this;
        }

        public AudioMessageBuilder WithAudio(AudioComponent audio)
        {
            _audio = audio;
            return this;
        }

        public AudioMessage Build()
        {
            if (string.IsNullOrWhiteSpace(_recipient))
            {
                throw new InvalidOperationException("Recipient phone number is required.");
            }

            if (_audio is null)
            {
                throw new InvalidOperationException("Audio payload is required.");
            }

            return new AudioMessage
            {
                to = _recipient,
                audio = new AudioComponent
                {
                    id = _audio.id,
                    link = _audio.link,
                    voice = _voice
                }
            };
        }
    }
}
