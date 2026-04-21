# Examples

## Dependency Injection

```csharp
private readonly IWhatsAppBusinessClient _businessClient;

public MessagesController(IWhatsAppBusinessClient businessClient)
{
    _businessClient = businessClient;
}
```

## Outbound Messages (Builder API)

### Simple text message

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

### Location message

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

### Document message

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

### Interactive buttons message

```csharp
using WhatsAppBussinesApi.Dotnet.Builders;

var message = WhatsAppMessageBuilder
    .Text
    .InteractiveButtons()
    .To("+669546669")
    .WithBody("Test interactive message")
    .AddReplyButton("user_123asd", "View User")
    .AddReplyButton("re_send_21f12", "Re-send confirmation")
    .Build();

var result = await _businessClient.SendMessage(message);
```

### Interactive list message

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

### Header image + interactions

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

## Webhook: JSON to Class + Interpretation

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

    var incomingTextMessages = WhatsAppWebhookParser.ExtractIncomingTextMessages(webhook);
    var statusUpdates = WhatsAppWebhookParser.ExtractStatusMessages(webhook);

    foreach (var message in incomingTextMessages)
    {
        // message.Body, message.From, message.Timestamp, etc.
    }

    foreach (var status in statusUpdates)
    {
        // status.MessageId, status.Status, status.Timestamp, etc.
    }

    return Ok();
}
```
