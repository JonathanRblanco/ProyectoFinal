using Ardalis.Result;
using MediatR;
using ProyectoFinal.Data;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.CheckOuts
{
    public class ReceiptPdfHandler : IRequestHandler<ReceiptPdfRequest, Result<byte[]>>
    {
        private readonly IPdfService pdfService;

        public ReceiptPdfHandler(IPdfService pdfService)
        {
            this.pdfService = pdfService;
        }
        public async Task<Result<byte[]>> Handle(ReceiptPdfRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return Result.Success(pdfService.MakeReceiptPdf(new PdfReceipt
                {
                    Address = request.Address,
                    AmountOfTickets = request.AmountOfTickets,
                    BranchCinemaName = request.BranchName,
                    Code = request.Code,
                    Date = request.Date,
                    Time = request.Time,
                    Movie = request.Movie,
                    Room = request.Room,
                    ShowType = request.ShowType,
                    Total = request.Total.ToString()
                }));
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
