using WhatsAppBussinesApi.Dotnet.Structure;

namespace WhatsAppBussinesApi.Dotnet.Builders.Text
{
    public interface IMessageBuilder<out TMessage> where TMessage : BaseMessage
    {
        TMessage Build();
    }
}
