# WhatsAppBussinesApi.Dotnet

Libreria .NET 8+ para trabajar con WhatsApp Business API con enfoque builder y modelos tipados para webhooks.

Version objetivo actual: 2.1.0-beta.

## Navegacion

- Guia de ejemplos: [Readme_Example.md](Readme_Example.md)
- Documentacion de soporte actual: [docs/README.md](docs/README.md)
- Mensajes soportados: [docs/supported-messages.md](docs/supported-messages.md)
- Webhooks soportados: [docs/webhooks.md](docs/webhooks.md)

## Instalacion

```bash
dotnet add package WhatsAppBussinesApi.Dotnet
```

## Registro de servicios

```csharp
using WhatsAppBussinesApi.Dotnet;

builder.Services.AddWhatsAppBussinesApi();
```

## Configuracion

Configura credenciales en appsettings.json:

```json
{
  "WhatsAppBusiness": {
    "PhoneNumber": "<PHONE_NUMBER_ID>",
    "BearerToken": "<BEARER_TOKEN>",
    "Version": "v23.0"
  }
}
```

Tambien se conservan claves legacy por compatibilidad:

- WhatsAppBussines:NroWhatsApp
- WhatsAppBussines:BRT
- WhatsAppBussines:Version

## Envio de mensajes

### Texto simple

```csharp
using WhatsAppBussinesApi.Dotnet.Builders;

var message = WhatsAppMessageBuilder
    .Text
    .Text()
    .To("+1111111111")
    .WithBody("Hola desde .NET", previewUrl: false)
    .Build();

var result = await businessClient.SendMessage(message);
```

### Subida de media + envio de imagen por id

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

### Template

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

## Media API disponible

- UploadMedia(Stream, fileName, mimeType)
- GetMediaUrl(mediaId, phoneNumberId?)
- DownloadMedia(mediaUrl)
- DeleteMedia(mediaId, phoneNumberId?)

## Webhooks

Parser soportado actualmente:

- IncomingTextMessage
- OutgoingStatusMessage
- IncomingLocationMessage

Consulta [docs/webhooks.md](docs/webhooks.md) para ejemplos de uso.
