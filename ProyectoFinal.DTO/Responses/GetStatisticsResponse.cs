using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.DTO.Responses
{
    public class GetStatisticsResponse
    {
        public decimal Total { get; set; }
        public string Movie { get; set; }
        public string Month { get; set; }
        public int ImageId { get; set; }
    }
}
