# WhatsAppBussinesApi.Dotnet

`WhatsAppBussinesApi.Dotnet` is a .NET 8+ library to send WhatsApp Business messages and parse incoming webhook payloads.

The library now includes:

- A builder-based API to construct messages in a fluent and validated way.
- Strongly typed webhook models.
- Webhook interpretation helpers that convert webhook objects into easy-to-consume domain results.

## Installation

```bash
dotnet add package WhatsAppBussinesApi.Dotnet
```

## Service Registration

```csharp
using WhatsAppBussinesApi.Dotnet;

builder.Services.AddWhatsAppBussinesApi();
```

## Configuration

Add your credentials in `appsettings.json`:

```json
{
  "WhatsAppBusiness": {
    "PhoneNumber": "<PHONE_NUMBER_ID>",
    "BearerToken": "<BEARER_TOKEN>",
    "Version": "v23.0"
  }
}
```

## Sending Messages (Builder API)

### Text message

```csharp
using WhatsAppBussinesApi.Dotnet.Builders;

var message = WhatsAppMessageBuilder
    .Text
    .Text()
    .To("+1111111111")
    .WithBody("Hello from .NET", previewUrl: false)
    .Build();

var result = await businessClient.SendMessage(message);
```

### Image message

```csharp
using WhatsAppBussinesApi.Dotnet.Builders;

var imageMessage = WhatsAppMessageBuilder
    .Text
    .Image()
    .To("+1111111111")
    .WithImageLink("https://example.com/image.png")
    .Build();

var result = await businessClient.SendMessage(imageMessage);
```

### Template message

```csharp
using WhatsAppBussinesApi.Dotnet.Builders;

var templateMessage = WhatsAppMessageBuilder
    .Template
    .To("+1111111111")
    .WithTemplateName("order_update")
    .WithLanguage("en")
    .AddBodyTextParameter("John")
    .AddBodyTextParameter("#A-1024")
    .Build();

var result = await businessClient.SendMessage(templateMessage);
```

## Webhook JSON Conversion and Interpretation

The webhook flow is:

1. Receive raw JSON from your webhook endpoint.
2. Deserialize into `WhatsAppWebhook`.
3. Interpret with `WhatsAppWebhookParser`.

```csharp
using System.Text.Json;
using WhatsAppBussinesApi.Dotnet.Structure.Webhook;

var webhook = JsonSerializer.Deserialize<WhatsAppWebhook>(jsonPayload);

if (webhook is null)
{
    return;
}

var incomingTextMessages = WhatsAppWebhookParser.ExtractIncomingTextMessages(webhook);
var outgoingStatuses = WhatsAppWebhookParser.ExtractStatusMessages(webhook);
```

`ExtractIncomingTextMessages` returns normalized incoming text messages.

`ExtractStatusMessages` returns delivery/status updates for outgoing messages.

## More Examples

See the full examples in `Readme_Example.md`.
