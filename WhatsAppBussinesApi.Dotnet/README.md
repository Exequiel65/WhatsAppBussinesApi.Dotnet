# WhatsAppBussinesApi.Dotnet

Libreria .NET 8+ para WhatsApp Business API con builders para mensajes y parser tipado para webhooks.

Version objetivo actual: 2.1.0-beta.

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

```json
{
  "WhatsAppBusiness": {
    "PhoneNumber": "<PHONE_NUMBER_ID>",
    "BearerToken": "<BEARER_TOKEN>",
    "Version": "v23.0"
  }
}
```

Tambien se soportan claves legacy:

- WhatsAppBussines:NroWhatsApp
- WhatsAppBussines:BRT
- WhatsAppBussines:Version

## Quick Send Example

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

## Media Upload Example

```csharp
using var stream = File.OpenRead("./assets/promo.png");
var mediaId = await businessClient.UploadMedia(stream, "promo.png", "image/png");

var imageMessage = WhatsAppMessageBuilder
    .Text
    .Image()
    .To("+1111111111")
    .WithImageId(mediaId!)
    .Build();

var result = await businessClient.SendMessage(imageMessage);
```

## Supported Today

- Text messages: audio, document, image, interactive (cta url, list, sequence multimedia, reply buttons), location, request location, reaction, sticker, text, video.
- Template messages: template builder with body parameters, header parameters and button interactions.
- Webhooks: IncomingTextMessage, OutgoingStatusMessage, IncomingLocationMessage.

Repository docs:

- [../README.md](../README.md)
- [../Readme_Example.md](../Readme_Example.md)
- [../docs/supported-messages.md](../docs/supported-messages.md)
