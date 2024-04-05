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
