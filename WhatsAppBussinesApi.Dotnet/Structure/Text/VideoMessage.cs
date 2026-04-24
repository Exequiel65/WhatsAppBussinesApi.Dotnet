using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    public class VideoMessage : BaseMessage
    {
        public override TypeMessage type => TypeMessage.video;
        public required BaseVideo video { get; init; }

        public VideoMessage()
        {
        }

        [SetsRequiredMembers]
        public VideoMessage(string to, string id, string? caption = null)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            video = new VideoComponentWithId(id)
            {
                caption = caption
            };
        }

        [SetsRequiredMembers]
        public VideoMessage(string to, Uri link, string? caption = null)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            video = new VideoComponentWithLink(link?.ToString() ?? throw new ArgumentNullException(nameof(link)))
            {
                caption = caption
            };
        }

        [SetsRequiredMembers]
        public VideoMessage(string to, BaseVideo video)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.video = video ?? throw new ArgumentNullException(nameof(video));
        }
    }

    public class BaseVideo : BaseMedia
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? id { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? link { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? caption { get; set; }
    }

    public class VideoComponentWithId : BaseVideo
    {
        public VideoComponentWithId()
        {
        }

        public VideoComponentWithId(string id)
        {
            this.id = string.IsNullOrWhiteSpace(id)
                ? throw new ArgumentException("Video id cannot be empty", nameof(id))
                : id;
        }
    }

    public class VideoComponentWithLink : BaseVideo
    {
        public VideoComponentWithLink()
        {
        }

        public VideoComponentWithLink(string link)
        {
            this.link = string.IsNullOrWhiteSpace(link)
                ? throw new ArgumentException("Video link cannot be empty", nameof(link))
                : link;
        }
    }
}
