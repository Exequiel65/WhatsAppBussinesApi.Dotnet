# Examples of message objects
It is necessary to inject the Business Client service
```c#
private readonly IWhatsAppBusinessClient 
public TextController(IWhatsAppBusinessClient businessClient)
{
    _businessClient = businessClient;
}
```

## Type Text

#### _Simple Text:_
```c#
var text = new BodyText()
{
    body = "Test body example", // Texto to message
    preview_url = false, //true or false for view Url
};

var message = new TextMessage()
{
    to = "+111111111", // Phone number to sender
    text = text
};

// send message
var result = await _businessClient.SendMessage(message);
```

#### _Location:_
```c#
// Phone Number, longitude, latitude, Name, address
var message = new LocationMessage("+1111111", -31.420807m, -64.188579m, "Stele", "Av. Hipólito Yrigoyen 12-66");
var result = await _businessClient.SendMessage(message);
```

#### _File:_
To send files, check the allowed file types
```c#
// Phone Number, link for download file
var message = new DocumentMessage("+44455666666", new Uri("Link"));
var result = await _businessClient.SendMessage(message);
```

#### _Interactive Reply:_

```c#
var body = new TextBody("Test interactive message");
var actions = new ActionButtonsReply()
{
    buttons = new List<ButtonAction>
    {
    //payload, button name
        new ButtonAction("user_123asd", "View User"),
        new ButtonAction("re_send_21f12", "Re send message to confirmation"),
    }

};
var component = new InteractiveComponent(body, actions);
//Phone number
var message = new InteractiveButtonsMessage("+669546669", component);

var result = await _businessClient.SendMessage(message);
```

#### _Interactive List:_

```c#

// Options to list
var rows = new List<Row>()
{
    new Row()
    {
        id = "12",
        title = "Title",
        description = "Description",
    },
    new Row()
    {
        id = "123",
        title = "Titl3e",
        description = "Descriptiona",
    },
};

// Section contains rows
var sections = new List<SectionList>()
    {
        new SectionList()
        {
            title = "Test",
            rows = rows

        }
};
// Phone Number, string for text in messagge
var message = new InteractiveListMessage("+56666544", "Example message interactive list", "View Options", sections);
var result = await _businessClient.SendMessage(message);
```
## Type Template

### _Only Text_
```c#
// this template have three enviroment
List<BaseParameters> texts = new()
{
     new ParameterCurrency("$100.99", "USD", 100990),
     new ParameterDateTime(DateTime.Now, "es"),
     new ParameterText("ster")
};
// Phone Number, Template Name, language template, data enviroment
var message = new TemplateTextMessage("+541111111111", "name_template", "es", texts);
var result = await _businessClient.SendMessage(message);
return Ok(result);
```

### _With Header Text_
```c#
 var headerText = "Summer";

List<BaseParameters> texts = new()
{
     new ParameterCurrency("$100.99", "USD", 100990),
     new ParameterDateTime(DateTime.Now, "es"),
     new ParameterText("ster")
};
// Phone Number, Template Name, language, data header, data text
var message = new TemplateHeaderMessage("+54365444555", "test_example", "es", headerText, texts);
var result = await _businessClient.SendMessage(message);

return Ok(result);
```

### _With Header Image_
```c#
List<BaseParameters> texts = new()
{
     new ParameterCurrency("$100.99", "USD", 100990),
     new ParameterDateTime(DateTime.Now, "es"),
     new ParameterText("ster")
};

// template with header type image
var message = new TemplateHeaderMessage("phone_number", "template_name", "es", new Uri("https://example.com/img.png"), texts);

var result = await _businessClient.SendMessage(message);

return Ok(result);
```
### _With Header Location_
```c#
LocationParameter headerLocation = new LocationParameter()
{
    latitude = "56.56",
    longitude = "56.56",
    address = "calle 123",
    name = "Test Example Location"
};

List<BaseParameters> texts = new()
{
     new ParameterCurrency("$100.99", "USD", 100990),
     new ParameterDateTime(DateTime.Now, "es"),
     new ParameterText("ster")
};

var message = new TemplateHeaderMessage("phone_number", "template_name", "es", headerLocation, texts);

var result = await _businessClient.SendMessage(message);

return Ok(result);
```

### _Interactive url_
```c#
var headerText = "BPNX";

List<BaseParameters> texts = new()
{
     new ParameterText("Example"),
     new ParameterDateTime(DateTime.Now, "es"),
     new ParameterDateTime(DateTime.Now, "es"),
     new ParameterText("Test"),
     new ParameterText("User"),
     new ParameterText("BPNX"),
};

List<BaseButtonComponent> buttons = new()
{
    new UrlButtonComponent(new Uri("https://google.com"))
};


var message = new TemplateInteractiveMessage("phone_number", "template_name", "es", texts, headerText: headerText, buttonComponents: buttons);

var result = await _businessClient.SendMessage(message);

return Ok(result);
```
### _Interactive quick reply_
```c#
List<BaseParameters> body = new()
{
     new ParameterText("Example"),
     new ParameterText("BPNX"),
};

List<BaseButtonComponent> buttons = new()
{
    new QuickReplyButton("text_1"),
    new QuickReplyButton("text_2", 1)
};


var message = new TemplateInteractiveMessage("phone_number", "template_name", "es", body, buttonComponents: buttons);

var result = await _businessClient.SendMessage(message);

return Ok(result);
```
### _Interactive copy code_
```c#
 List<BaseParameters> body = new()
{
     new ParameterText("Example"),
};

List<BaseButtonComponent> buttons = new()
{
    new CopyCodeButton("code_example")
};


var message = new TemplateInteractiveMessage("phone_number", "tempalte_name", "es", body, buttonComponents: buttons);

var result = await _businessClient.SendMessage(message);

return Ok(result);
```
