using System.ComponentModel.DataAnnotations;
namespace TBAM.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class TestBatchListModel
    {
        public string Filter { get; set; }
        public List<TestBatchModel> ListOfBatch { get; set; }
        public int Count { get; set; }

        public int CompletedCount {get; set;}
        public int PendingCount {get; set;}

    }
    public class TestBatchModel
    {
        public int Id { get; set; }
        public string RefNo { get; set; }
        public string PurposesOfTesting { get; set; }
        public int Plant { get; set; }
        public string TestDetails { get; set; }
        public string Status { get; set; }


    }

    public class ConfirmSendForApprovalViewModel
    {
        public string RefNo { get; set; }
    }
}
