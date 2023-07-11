using ProyectoFinal.ServicesContracts;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.Json;

namespace ProyectoFinal.Services
{
    public class QRService : IQRService
    {
        public byte[] MakeQr(object result)
        {
            var ticketJson = JsonSerializer.Serialize(result);
            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(ticketJson, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            using MemoryStream stream = new();
            qrCodeImage.Save(stream, ImageFormat.Png);
            return stream.ToArray();
        }
    }
}
