using WhatsAppBussinesApi.Dotnet.Structure;
using WhatsAppBussinesApi.Dotnet.Structure.Text.Interactives;

namespace WhatsAppBussinesApi.Dotnet.Builders.Text
{
    public sealed class InteractiveSequenceMediaBuilder : IMessageBuilder<InteractiveMultimediaSecuence>
    {
        private string? _recipient;
        private string _bodyText;
        private readonly List<CardAction> _cards = [];

        public InteractiveSequenceMediaBuilder To(string phoneNumber)
        {
            _recipient = phoneNumber;
            return this;
        }
        public InteractiveSequenceMediaBuilder WithBody(string text)
        {
            _bodyText = text;
            return this;
        }

        public InteractiveSequenceMediaBuilder AddSequence(CardAction card)
        {
            _cards.Add(card);
            return this;
        }

        public InteractiveSequenceMediaBuilder AddSequence(List<CardAction> cards)
        {
            _cards.AddRange(cards);
            return this;
        }


        public InteractiveMultimediaSecuence Build()
        {
            if (string.IsNullOrEmpty(_recipient))
                throw new InvalidOperationException("Recipient phone number must be provided.");
            if (string.IsNullOrEmpty(_bodyText))
                throw new InvalidOperationException("Body text must be provided.");
            if (_cards.Count == 0)
                throw new InvalidOperationException("At least one card action must be added.");
            var sequenceAction = new SequenceAction(_cards);
            var interactiveComponent = new SequenceInteractiveComponent(_bodyText, sequenceAction);
            return new InteractiveMultimediaSecuence(_recipient, interactiveComponent);
        }
    }


    public class SequenceBuilder
    {
        private readonly List<CardAction> _cards = new();
        private string _index;
        private string _text;
        private HeaderCard _header;
        private BaseAction _action;


        public SequenceBuilder Index(int index) { _index = index.ToString(); return this; }
        public SequenceBuilder WithBody (string body ) { _text = body; return this; }
        public SequenceBuilder WithImage(string urlImage) {
            if (_header != null)
                throw new InvalidOperationException("Header card already set. Cannot set both image and video.");
            _header = new HeaderCard(urlImage, HeaderCardType.image);
            return this;
        }
        public SequenceBuilder WithVideo (string urlVideo) 
        {
            if (_header != null)
                throw new InvalidOperationException("Header card already set. Cannot set both image and video.");

            _header = new HeaderCard(urlVideo, HeaderCardType.video);
            return this;
        }

        public SequenceBuilder WithButtonUrl(string urlButton, string text)
        {
            if (_action != null)
                throw new InvalidOperationException("Action already set. Cannot set multiple actions.");
            _action = new CtaUrlAction(text, urlButton);
            return this;
        }

        public SequenceBuilder AddReplyButton(string id, string title)
        {
            if (_action is not ActionButtonsReply && _action != null)
                throw new InvalidOperationException("Action already set. Cannot set multiple actions.");
            var action = _action as ActionButtonsReply;

            action.buttons.Add(new ButtonAction(id, title));

            _action = action;
            return this;
        }

        public SequenceBuilder AddButton(ButtonAction button)
        {
            if (_action is not ActionButtonsReply && _action != null)
                throw new InvalidOperationException("Action already set. Cannot set multiple actions.");
            var action = _action as ActionButtonsReply;
            action.buttons.Add(button);
            _action = action;
            return this;
        }

        public SequenceBuilder WithButtonsReply(List<ButtonAction> buttons)
        {
            if (_action != null)
                throw new InvalidOperationException("Action already set. Cannot set multiple actions.");
            _action = new ActionButtonsReply(buttons);
            return this;
        }

        public void Add()
        {
            if (string.IsNullOrEmpty(_index))
                throw new InvalidOperationException("Index must be provided.");
            if (string.IsNullOrEmpty(_text))
                throw new InvalidOperationException("Body text must be provided.");
            if (_header == null)
                throw new InvalidOperationException("Header card must be provided.");
            if (_action == null)
                throw new InvalidOperationException("Action must be provided.");
            _cards.Add(new CardAction(_index, _header, new TextBody(_text), _action));
        }


        public List<CardAction>Build()
        {
            return _cards;
        }
    }
}
