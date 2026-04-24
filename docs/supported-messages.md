# Supported Messages

Estado actual de mensajes soportados en la beta 2.1.0.

## Text Messages

- Audio
- Document
- Image
- Interactive CTA URL
- Interactive List
- Interactive Sequence Multimedia
- Interactive Reply Buttons
- Location
- Request Location
- Reaction
- Sticker
- Text
- Video

## Template Messages

El builder actual soporta:

- Definicion de template name y language.
- Parametros de body: text, currency, date_time.
- Parametros de header: text, image, location.
- Interacciones de botones: quick_reply, url, copy_code.

## Media Methods

Metodos disponibles en IWhatsAppBusinessClient:

- UploadMedia(Stream fileStream, string fileName, string mimeType)
- GetMediaUrl(string mediaId, string? phoneNumberId = null)
- DownloadMedia(string mediaUrl)
- DeleteMedia(string mediaId, string? phoneNumberId = null)

## Nota de roadmap

La proxima release se enfocara en plantillas.

## Navegacion

- Webhooks: [webhooks.md](webhooks.md)
- Ejemplos: [../Readme_Example.md](../Readme_Example.md)
- Indice docs: [README.md](README.md)
