using ProyectoFinal.Data;

namespace ProyectoFinal.ServicesContracts
{
    public interface IPdfService
    {
        byte[] MakeReceiptPdf(PdfReceipt data);
    }
}
