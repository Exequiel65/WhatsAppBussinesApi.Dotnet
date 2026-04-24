# Examples

Guia rapida para beta 2.1.0.

Tambien puedes ver:

- Soporte actual de mensajes: [docs/supported-messages.md](docs/supported-messages.md)
- Webhooks soportados: [docs/webhooks.md](docs/webhooks.md)

## Dependency Injection

```csharp
private readonly IWhatsAppBusinessClient _businessClient;

public MessagesController(IWhatsAppBusinessClient businessClient)
{
    _businessClient = businessClient;
}
```

## Outbound Messages (Builder API)

### Text

```csharp
using WhatsAppBussinesApi.Dotnet.Builders;

var message = WhatsAppMessageBuilder
    .Text
    .Text()
    .To("+1111111111")
    .WithBody("Test body example", previewUrl: false)
    .Build();

var result = await _businessClient.SendMessage(message);
```

### Upload media + send image by media id

```csharp
using WhatsAppBussinesApi.Dotnet.Builders;

using var file = File.OpenRead("./assets/image.png");
var mediaId = await _businessClient.UploadMedia(file, "image.png", "image/png");

var imageMessage = WhatsAppMessageBuilder
    .Text
    .Image()
    .To("+1111111111")
    .WithImageId(mediaId!)
    .WithCaption("Imagen subida via media endpoint")
    .Build();

var result = await _businessClient.SendMessage(imageMessage);
```

### Document

```csharp
using WhatsAppBussinesApi.Dotnet.Builders;

var message = WhatsAppMessageBuilder
    .Text
    .Document()
    .To("+44455666666")
    .WithDocumentLink("https://example.com/file.pdf", "invoice.pdf")
    .Build();

var result = await _businessClient.SendMessage(message);
```

### Location

```csharp
using WhatsAppBussinesApi.Dotnet.Builders;

var message = WhatsAppMessageBuilder
    .Text
    .Location()
    .To("+1111111111")
    .WithLocation(-64.188579m, -31.420807m, "Stele", "Av. Hipolito Yrigoyen 12-66")
    .Build();

var result = await _businessClient.SendMessage(message);
```

### Interactive buttons (reply)

```csharp
using WhatsAppBussinesApi.Dotnet.Builders;

var message = WhatsAppMessageBuilder
    .Text
    .InteractiveButtons()
    .To("+669546669")
    .WithBody("Selecciona una opcion")
    .AddReplyButton("user_123asd", "Ver usuario")
    .AddReplyButton("re_send_21f12", "Reenviar")
    .Build();

var result = await _businessClient.SendMessage(message);
```

### Interactive list

```csharp
using WhatsAppBussinesApi.Dotnet.Builders;
using WhatsAppBussinesApi.Dotnet.Structure.Text;

var rows = new List<Row>
{
    new Row { id = "12", title = "Title", description = "Description" },
    new Row { id = "123", title = "Title 2", description = "Description 2" }
};

var message = WhatsAppMessageBuilder
    .Text
    .InteractiveList()
    .To("+56666544")
    .WithBody("Example interactive list message")
    .WithButtonLabel("View options")
    .AddSection("Main options", rows)
    .Build();

var result = await _businessClient.SendMessage(message);
```

### Interactive CTA URL

```csharp
using WhatsAppBussinesApi.Dotnet.Builders;

var message = WhatsAppMessageBuilder
    .Text
    .InteractiveCtaUrl()
    .To("+541111111111")
    .WithBody("Conoce la beta 2.1.0")
    .WithDisplayText("Ver release")
    .WithUrl("https://github.com/Exequiel65/WhatsAppBussinesApi.Dotnet")
    .Build();

var result = await _businessClient.SendMessage(message);
```

### Request location

```csharp
using WhatsAppBussinesApi.Dotnet.Builders;

var message = WhatsAppMessageBuilder
    .Text
    .InteractiveLocation()
    .To("+541111111111")
    .WithBody("Comparte tu ubicacion actual")
    .Build();

var result = await _businessClient.SendMessage(message);
```

## Template Messages

### Body parameters only

```csharp
using WhatsAppBussinesApi.Dotnet.Builders;

var message = WhatsAppMessageBuilder
    .Template
    .To("+541111111111")
    .WithTemplateName("name_template")
    .WithLanguage("en")
    .AddBodyCurrencyParameter("$100.99", "USD", 100990)
    .AddBodyDateTimeParameter(DateTime.UtcNow, "en")
    .AddBodyTextParameter("Customer")
    .Build();

var result = await _businessClient.SendMessage(message);
```

### Header + body

```csharp
using WhatsAppBussinesApi.Dotnet.Builders;

var message = WhatsAppMessageBuilder
    .Template
    .To("+54365444555")
    .WithTemplateName("test_example")
    .WithLanguage("en")
    .AddHeaderText("Summer")
    .AddBodyTextParameter("Example")
    .Build();

var result = await _businessClient.SendMessage(message);
```

### Header image + URL button

```csharp
using WhatsAppBussinesApi.Dotnet.Builders;
using WhatsAppBussinesApi.Dotnet.Structure.Templates;

var message = WhatsAppMessageBuilder
    .Template
    .To("+54365444555")
    .WithTemplateName("template_with_button")
    .WithLanguage("en")
    .AddHeaderImage(new Uri("https://example.com/img.png"))
    .AddBodyTextParameter("Example")
    .AddInteraction(new UrlButtonComponent(new Uri("https://google.com")))
    .Build();

var result = await _businessClient.SendMessage(message);
```

## Webhook: JSON to class + parser

```csharp
using System.Text.Json;
using WhatsAppBussinesApi.Dotnet.Structure.Webhook;

[HttpPost("webhook")]
public IActionResult ReceiveWebhook([FromBody] JsonElement payload)
{
    var webhook = JsonSerializer.Deserialize<WhatsAppWebhook>(payload.GetRawText());
    if (webhook is null)
    {
        return BadRequest("Invalid webhook payload");
    }

    var incomingText = WhatsAppWebhookParser.ExtractIncomingTextMessages(webhook);
    var incomingLocation = WhatsAppWebhookParser.ExtractIncomingLocationMessages(webhook);
    var statuses = WhatsAppWebhookParser.ExtractStatusMessages(webhook);

    return Ok(new
    {
        IncomingText = incomingText.Count,
        IncomingLocation = incomingLocation.Count,
        OutgoingStatuses = statuses.Count
    });
}
```
