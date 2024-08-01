using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TBAM.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "character varying(20)")]
        public required string FirstName { get; set; }

        [Column(TypeName = "character varying(20)")]
        public required string LastName { get; set; }

        [Column(TypeName = "character varying(40)")]
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Boolean? IsDeleted { get; set; }
    }

    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "character varying(20)")]
        public required string RoleName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Boolean? IsDeleted { get; set; }
    }

    public class UserRole
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int LoginId { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [ForeignKey("User")]
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

        [ForeignKey("User")]
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }

        [ForeignKey("User")]
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Boolean? IsDeleted { get; set; }
    }

    public class Plants
    {
        [Key]
        public int Id { get; set; }

        public required int PlantId { get; set; }

        public required string Description { get; set; }
    }

    public class ProductCodes
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Plants")]
        public int PlantId { get; set; }

        public required string ProductCode { get; set; }

        public required string ProductName { get; set; }


    }
    public class Workcentres
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Plants")]
        public int PlantId { get; set; }

        public required string Description { get; set; }
    }
    public class PurposesOfTesting
    {
        [Key]
        public int Id { get; set; }

        public required string Description { get; set; }

    }

    public class TestBatch
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("PurposesOfTesting")]
        public required int PurposesOfTestingId { get; set; }

        [ForeignKey("Plants")]
        public required int PlantId { get; set; }

        public required string TestDetails { get; set; }

        [ForeignKey("User")]
        public required int CreatedBy { get; set; }
        public required DateTime CreatedAt { get; set; }

        [ForeignKey("User")]
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }

        [ForeignKey("User")]
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Boolean? IsDeleted { get; set; }

        [ForeignKey("User")]
        public int ApprovedBy { get; set; }

        [ForeignKey("Role")]
        public int ApproveLevel { get; set; }

        public required string Status { get; set; }
    }
    public class TestBatchItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TestBatch")]
        public required int TestBatchId { get; set; }

        public required string ProductCode { get; set; }

        public required string ProductName { get; set; }

        public required string Workcentre { get; set; }

        public required int Quantity { get; set; }

        public string? BatchNumber { get; set; }

        public required string Remarks { get; set; }

        public int? Cost { get; set; }

        [ForeignKey("User")]
        public required int CreatedBy { get; set; }
        public required DateTime CreatedAt { get; set; }

        [ForeignKey("User")]
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }

        [ForeignKey("User")]
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Boolean? IsDeleted { get; set; }
    }
}
