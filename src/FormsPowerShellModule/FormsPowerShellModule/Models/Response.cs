using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsPowerShellModule.Models
{
    public class Response
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime SubmitDate { get; set; }
        public string Responder { get; set; }
        public string ResponderName { get; set; }
        public string Answers { get; set; }
        public DateTime ReleaseDate { get; set; }
        public object QuizResult { get; set; }
        public object EmailReceiptConsent { get; set; }
        public object SubmitLanguage { get; set; }
        public object MsRewardsData { get; set; }
        public object FormsProData { get; set; }
    }
}
