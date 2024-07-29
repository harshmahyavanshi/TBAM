using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TBAM.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "character varying(50)")]
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Boolean? IsDeleted { get; set; }
    }

    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "character varying(50)")]
        public required string RoleName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Boolean? IsDeleted { get; set; }
    }

    public class UserRole
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "character varying(50)")]
        public int LoginId{ get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Boolean? IsDeleted { get; set; }
    }
    
    public class Plants{
        [Key]
        public int Id { get; set; }
        
        [Column(TypeName = "character varying(50)")]
        public required string Description {get; set;}
    }

    public class ProductCodes{
        [Key]
        public int Id { get; set; }

        [ForeignKey("Plants")]
        public int PlantId {get; set;}

        [Column(TypeName = "character varying(20)")]
        public required string ProductCode {get; set;}
        
        [Column(TypeName = "character varying(20)")]
        public required string ProductName {get; set;}


    }
    public class Workcentres{
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Plants")]
        public int PlantId {get; set;}

        [Column(TypeName = "character varying(20)")]
        public required string Description {get; set;}
    }
    public class PurposesOfTesting{
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "character varying(20)")]
        public required string Description {get; set;}

    }

    public class TestBatch{
        [Key]
        public int Id {get; set;}
        
        [Column(TypeName = "character varying(20)")]
        public required string PurposesOfTesting { get; set; }

        [Column(TypeName = "character varying(20)")]
        public required string Plant { get; set; }

        [Column(TypeName = "character varying(20)")]
        public required string TestDetails { get; set; }

        [ForeignKey("User")]
        public required int CreatedBy { get; set; }
        public required DateTime CreatedAt { get; set; }

        [ForeignKey("User")]
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        [ForeignKey("User")]
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Boolean? IsDeleted { get; set; }

        [ForeignKey("User")]
        public int ApprovedBy {get; set;}

        [ForeignKey("Role")]
        public int ApproveLevel {get; set;}

        public required string Status {get; set;}
    }
    public class TestBatchItem{
        [Key]
        public int Id {get; set;}

        [ForeignKey("TestBatch")]
        public int TestBatchId {get; set;}

        [Column(TypeName = "character varying(20)")]

        public required string ProductCode { get; set; }
        
        [Column(TypeName = "character varying(20)")]
        public required string ProductName { get; set; }

        [Column(TypeName = "character varying(20)")]
        public required string Workcentre { get; set; }

        public required int Quantity { get; set; }

        [Column(TypeName = "character varying(20)")]
        public string? BatchNumber { get; set; }

        [Column(TypeName = "character varying(20)")]
        public required string Remarks { get; set; }

        public int? Cost { get; set; }

        [ForeignKey("User")]
        public required int CreatedBy { get; set; }
        public required DateTime CreatedAt { get; set; }

        [ForeignKey("User")]
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("User")]
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Boolean? IsDeleted { get; set; }
    }
}
