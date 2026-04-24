# Supported Webhooks

Parser soportado actualmente mediante WhatsAppWebhookParser:

- IncomingTextMessage via ExtractIncomingTextMessages
- OutgoingStatusMessage via ExtractStatusMessages
- IncomingLocationMessage via ExtractIncomingLocationMessages

## Ejemplo rapido

```csharp
using System.Text.Json;
using WhatsAppBussinesApi.Dotnet.Structure.Webhook;

var webhook = JsonSerializer.Deserialize<WhatsAppWebhook>(jsonPayload);
if (webhook is null)
{
    return;
}

var incomingText = WhatsAppWebhookParser.ExtractIncomingTextMessages(webhook);
var incomingLocation = WhatsAppWebhookParser.ExtractIncomingLocationMessages(webhook);
var outgoingStatuses = WhatsAppWebhookParser.ExtractStatusMessages(webhook);
```

## Navegacion

- Mensajes soportados: [supported-messages.md](supported-messages.md)
- Ejemplos: [../Readme_Example.md](../Readme_Example.md)
- Indice docs: [README.md](README.md)
