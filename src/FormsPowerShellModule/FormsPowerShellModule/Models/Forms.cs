using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FormsPowerShellModule.Models
{
    public class Forms
    {
        public class FormsBackground
        {
            public object AltText { get; set; }
            public object ContentType { get; set; }
            public object FileIdentifier { get; set; }
            public object OriginalFileName { get; set; }
            public object ResourceId { get; set; }
            public object ResourceUrl { get; set; }
            public object Height { get; set; }
            public object Width { get; set; }
            public object Size { get; set; }
        }

        public class FormsHeader
        {
            public object AltText { get; set; }
            public object ContentType { get; set; }
            public object FileIdentifier { get; set; }
            public object OriginalFileName { get; set; }
            public object ResourceId { get; set; }
            public object ResourceUrl { get; set; }
            public object Height { get; set; }
            public object Width { get; set; }
            public object Size { get; set; }
        }

        public class FormsLogo
        {
            public object AltText { get; set; }
            public object ContentType { get; set; }
            public object FileIdentifier { get; set; }
            public object OriginalFileName { get; set; }
            public object ResourceId { get; set; }
            public object ResourceUrl { get; set; }
            public object Height { get; set; }
            public object Width { get; set; }
            public object Size { get; set; }
        }

        public string CreatedBy { get; set; }
        public List<object> PermissionTokens { get; set; }
        public object FileUploadFormInfo { get; set; }
        public bool XlFileUnSynced { get; set; }
        public object Description { get; set; }
        public int OnlineSafetyLevel { get; set; }
        public int ReputationTier { get; set; }
        public FormsBackground Background { get; set; }
        public FormsHeader Header { get; set; }
        public FormsLogo Logo { get; set; }
        public string TableId { get; set; }
        public string OtherInfo { get; set; }
        public object RuntimeResponses { get; set; }
        public List<object> Permissions { get; set; }
        public List<object> ResponderPermissions { get; set; }
        public string Status { get; set; }
        public object Category { get; set; }
        public string LocaleInfo { get; set; }
        public List<object> DescriptiveQuestions { get; set; }
        public object PredefinedResponses { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Version { get; set; }
        public string OwnerId { get; set; }
        public string OwnerTenantId { get; set; }
        public string Settings { get; set; }
        public object DeserializedSettings { get; set; }
        public int SoftDeleted { get; set; }
        public object ThankYouMessage { get; set; }
        public int Flags { get; set; }
        public object EmailReceiptEnabled { get; set; }
        public string Type { get; set; }
        public object MeetingId { get; set; }
        public object FormsInsightsInfo { get; set; }
        public string FormsProRtTitle { get; set; }
        public object FormsProRtDescription { get; set; }
        public object DefaultLanguage { get; set; }
        public object DataClassificationLevel { get; set; }
        public List<object> LocaleList { get; set; }
        public object ResponseThresholdCount { get; set; }
        public object InviteExpiryDays { get; set; }
        public int TenantSwitches { get; set; }
        public object PrivacyUrl { get; set; }
        public string CollectionId { get; set; }
        public object MfpBranchingData { get; set; }
        public int RowCount { get; set; }
        public string ProgressBarEnabled { get; set; }
        public string TrackingId { get; set; }
    }
}
