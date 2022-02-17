using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsPowerShellModule.Models
{
    public class Image
    {
        public object AltText { get; set; }
        public object ContentType { get; set; }
        public object FileIdentifier { get; set; }
        public object Height { get; set; }
        public object OriginalFileName { get; set; }
        public object ResourceId { get; set; }
        public object ResourceUrl { get; set; }
        public object Width { get; set; }
        public object Size { get; set; }
    }

    public class Question
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public double Order { get; set; }
        public string Type { get; set; }
        public bool Required { get; set; }
        public object QuestionInfo { get; set; }
        public bool IsQuiz { get; set; }
        public object GroupId { get; set; }
        public object DefaultValue { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Status { get; set; }
        public object Subtitle { get; set; }
        public object AllowMultipleValues { get; set; }
        public bool TitleHasPhishingKeywords { get; set; }
        public bool SubtitleHasPhishingKeywords { get; set; }
        public object QuestionTagForIntelligence { get; set; }
        public bool IsFromSuggestion { get; set; }
        public object InsightsInfo { get; set; }
        public object FormsProRTQuestionTitle { get; set; }
        public object FormsProRTSubtitle { get; set; }
        public string TrackingId { get; set; }
        public Image Image { get; set; }
        public object FileUploadSPOInfo { get; set; }
    }
}
