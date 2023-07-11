using iTextSharp.text;
using iTextSharp.text.pdf;
using ProyectoFinal.Data;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.Services
{
    public class PdfService : IPdfService
    {
        private readonly IQRService qrService;

        public PdfService(IQRService qRService)
        {
            this.qrService = qRService;
        }
        public byte[] MakeReceiptPdf(PdfReceipt data)
        {
            Document document = new();
            using (MemoryStream memoryStream = new())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // Agregar el logo al PDF
                string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Imagenes", "Logocinenet-07.png");
                if (File.Exists(logoPath))
                {
                    Image logo = Image.GetInstance(logoPath);
                    logo.ScaleAbsolute(180f, 112); // Establece el tamaño del logo (ancho x alto) en unidades absolutas
                    logo.Alignment = Element.ALIGN_CENTER; // Ajusta la alineación del logo según tus necesidades
                    document.Add(logo);
                }

                // Agregar texto "¡GRACIAS POR ELEGIRNOS!"
                Font graciasFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, Font.BOLD, BaseColor.BLACK);
                Paragraph graciasParagraph = new("¡GRACIAS POR ELEGIRNOS!", graciasFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(graciasParagraph);

                // Agregar texto "Tu compra se realizó con éxito"
                Font exitoFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);
                Paragraph exitoParagraph = new("Tu compra se realizó con éxito", exitoFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(exitoParagraph);

                // Agregar texto "CÓDIGO DE RETIRO" con el código de la compra
                Font codigoFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD, BaseColor.BLACK);
                Paragraph codigoParagraph = new("CÓDIGO DE RETIRO", codigoFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(codigoParagraph);

                var qr = Image.GetInstance(qrService.MakeQr(data));
                qr.ScaleAbsolute(180f, 180f);
                qr.Alignment = Element.ALIGN_CENTER;
                document.Add(qr);
                // Agregar el código de la compra
                Font codigoValueFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK);
                Paragraph codigoValueParagraph = new(data.Code, codigoValueFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(codigoValueParagraph);

                document.Add(Chunk.NEWLINE);
                // Crear una tabla para la información de la compra
                PdfPTable table = new(2)
                {
                    SpacingAfter = 10f
                }; // 2 columnas

                // Estilos de tipografía y color
                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, Font.BOLD, BaseColor.BLACK);
                Font regularFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD, BaseColor.DARK_GRAY);
                BaseColor titleBackgroundColor = new(135, 206, 250); // Color de fondo para el título
                BaseColor headerBackgroundColor = new(245, 245, 245); // Color de fondo para las celdas de título

                // Agregar título a la tabla
                PdfPCell titleCell = new(new Phrase("Información de tu compra", titleFont))
                {
                    Colspan = 2,
                    Padding = 10,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = titleBackgroundColor,
                    Border = PdfPCell.NO_BORDER
                };
                table.AddCell(titleCell);

                // Agregar contenido a la tabla
                AddTableCell(table, "Película:", data.Movie, headerBackgroundColor, regularFont);
                AddTableCell(table, "Tipo de función:", data.ShowType, headerBackgroundColor, regularFont);
                AddTableCell(table, "Sucursal:", data.BranchCinemaName, headerBackgroundColor, regularFont);
                AddTableCell(table, "Dirección:", data.Address, headerBackgroundColor, regularFont);
                AddTableCell(table, "Día:", data.Date, headerBackgroundColor, regularFont);
                AddTableCell(table, "Hora:", data.Time, headerBackgroundColor, regularFont);
                AddTableCell(table, "Sala:", data.Room.ToString(), headerBackgroundColor, regularFont);
                AddTableCell(table, "Cantidad de entradas:", data.AmountOfTickets.ToString(), headerBackgroundColor, regularFont);
                AddTableCell(table, "Total:", "$" + data.Total.ToString(), headerBackgroundColor, regularFont);
                AddTableCell(table, "Código de la compra:", data.Code, headerBackgroundColor, regularFont);

                document.Add(table);
                // Cerrar el documento
                document.Close();
                // Convertir el documento a un arreglo de bytes
                return memoryStream.ToArray();
            }
        }
        private void AddTableCell(PdfPTable table, string title, string value, BaseColor backgroundColor, Font font)
        {
            PdfPCell titleCell = new(new Phrase(title, font));
            PdfPCell valueCell = new(new Phrase(value, font));

            titleCell.BackgroundColor = backgroundColor;
            titleCell.HorizontalAlignment = Element.ALIGN_CENTER;
            titleCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            valueCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            valueCell.HorizontalAlignment = Element.ALIGN_CENTER;
            titleCell.BorderWidth = 0;
            valueCell.BorderWidth = 0;
            table.AddCell(titleCell);
            table.AddCell(valueCell);
        }
    }
}
