using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TBAM.Models;
using System.Security.Claims;
using System.Text.Json;
using TBAM.Data;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;


public class DataService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DataService> _logger;
    private readonly IWebHostEnvironment _environment;



    public DataService(ILogger<DataService> logger, ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _logger = logger;
        _context = context;
        _environment = environment;
    }


    public async Task<string> CreateTestBatch(TestFormViewModel model, int? userId)
    {
        var isCreatedSuccessfully = false;

        var newRefNo = "TBAM/";

        bool isModified = false;

        //RefNo is null for the first time save.
        if (model.RefNo == null)
        {
            var maxrefno = _context.TestBatch.Where(v => v.Refno != null).Select(v => v.Refno);

            var maxNumberAfterSlash = maxrefno
                                    .AsEnumerable()
                                    .Select(refno => refno.Split('/').LastOrDefault())
                                    .Where(part => !string.IsNullOrEmpty(part))
                                    .Select(part => int.Parse(part))
                                    .DefaultIfEmpty(0)
                                    .Max();

            var nextNumber = maxNumberAfterSlash + 1;
            var nextRefNoValue = nextNumber.ToString("D6");

            newRefNo += nextRefNoValue;
        }
        else
        {
            isModified = await UpdateTestBatch(model.RefNo, userId);
        }

        var PurposesOfTestingId = _context.PurposesOfTesting.Select(p => p).Where(p => p.Description == model.PurposesOfTesting[0]);

        var PlantId = _context.Plants.Select(p => p).Where(p => p.PlantId == model.Plants[0]);

        var userRoleId = _context.UserRole.Where(ur => ur.LoginId == userId).FirstOrDefault().RoleId;
        //fill TestBatch model
        var testBatchDbModel = new TestBatch
        {
            Refno = model.RefNo == null ? newRefNo : model.RefNo, //if first time save then newRefNo; To update use the older RefNo.
            PurposesOfTestingId = PurposesOfTestingId.FirstOrDefault().Id,
            PlantId = PlantId.FirstOrDefault().Id,
            TestDetails = model.TestDetails,
            CreatedBy = (int)userId,
            Status = "Pending",
            ApproveLevel = userRoleId

        };

        var utcDateTimeValue = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

        //if modified
        if (isModified)
        {
            testBatchDbModel.ModifiedAt = utcDateTimeValue;
            testBatchDbModel.ModifiedBy = userId;
        }
        _context.TestBatch.Add(testBatchDbModel);

        await _context.SaveChangesAsync();
        var lastInsertedId = testBatchDbModel.Id;

        foreach (var item in model.LineItems)
        {
            var testBatchItemDbModel = new TestBatchItem
            {
                TestBatchId = lastInsertedId,
                Refno = model.RefNo == null ? newRefNo : model.RefNo, //if first time save then newRefNo; To update use the older RefNo.
                ProductCode = item.ProductCode,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                Workcentre = item.Workcentre,
                Remarks = item.Remarks,
                CreatedBy = (int)userId,
                Cost = item.Cost,
                BatchNumber = item.BatchNumber
            };



            //if modified
            if (isModified)
            {
                testBatchItemDbModel.ModifiedAt = utcDateTimeValue;
                testBatchItemDbModel.ModifiedBy = userId;
            }
            _context.TestBatchItem.Add(testBatchItemDbModel);
        }

        await _context.SaveChangesAsync();

        isCreatedSuccessfully = true;

        return testBatchDbModel.Refno;
    }

    public async Task<TestBatchListModel> GetTestBatchList(int? userId, string? Filter = null)
{
    var userRole = _context.UserRole.Where(ur => ur.LoginId == userId).FirstOrDefault();
    var role = _context.Role.Where(r => r.Id == userRole.RoleId).FirstOrDefault();
    var dataList = new List<TestBatch>();

    //var dataList = Filter == null ? await _context.TestBatch.Select(p => p).Where(p => p.IsDeleted == false && p.ApproveLevel == role.Id).ToListAsync() :
      //                 Filter == "Pending" ? await _context.TestBatch.Select(p => p).Where(p => p.IsDeleted == false && p.ApproveLevel == role.Id && p.Status.Equals("Pending")).ToListAsync() :
        //                                     await _context.TestBatch.Select(p => p).Where(p => p.IsDeleted == false && p.ApproveLevel != role.Id  && p.Status.Equals("Pending") || p.Status.Equals("Completed")).ToListAsync();

    switch (role.RoleName)
    {
        case "Initiator":
            dataList = Filter == null ? await _context.TestBatch.Select(p => p).Where(p => p.IsDeleted == false && p.ApproveLevel == role.Id).ToListAsync() :
                                        Filter == "Pending" ? await _context.TestBatch.Select(p => p).Where(p => p.IsDeleted == false && p.ApproveLevel == role.Id && p.Status.Equals("Pending")).ToListAsync() :
                                                              await _context.TestBatch.Select(p => p).Where(p => p.IsDeleted == false && p.ApproveLevel != role.Id  && p.Status.Equals("Pending") || p.Status.Equals("Completed")).ToListAsync();
            break;
        case "QC":
            dataList = Filter == null ? await _context.TestBatch.Select(p => p).Where(p => p.IsDeleted == false && p.ApproveLevel == role.Id).ToListAsync() :
                                        Filter == "Pending" ? await _context.TestBatch.Where(p =>  p.IsDeleted == false && p.ApproveLevel == role.Id && (p.isQAApproved == null || p.isQAApproved == false)).ToListAsync():
                                                              await _context.TestBatch.Where(p =>p.IsDeleted == false && p.isQAApproved == true ).ToListAsync();            
            break;
        case "Costing":
            dataList = Filter == null ? await _context.TestBatch.Select(p => p).Where(p => p.IsDeleted == false && p.ApproveLevel == role.Id).ToListAsync() :
                                        Filter == "Pending" ? await _context.TestBatch.Where(p =>p.IsDeleted == false && p.ApproveLevel == role.Id && ( p.isCostingApproved == null || p.isCostingApproved == false)).ToListAsync():
                                                              await _context.TestBatch.Where(p => p.IsDeleted == false && p.isCostingApproved == true).ToListAsync();            
            break;
        case "Manufacturing Head":
            dataList = Filter == null ? await _context.TestBatch.Select(p => p).Where(p => p.IsDeleted == false && p.ApproveLevel == role.Id).ToListAsync() :
                                        Filter == "Pending" ? await _context.TestBatch.Where(p => p.IsDeleted == false && p.ApproveLevel == role.Id && (p.isManufacturingHeadApproved == null || p.isManufacturingHeadApproved == false)).ToListAsync():
                                                              await _context.TestBatch.Where(p =>p.IsDeleted == false && p.isManufacturingHeadApproved == true).ToListAsync();            
            break;
        case "SAP":
            dataList = Filter == null ? await _context.TestBatch.Select(p => p).Where(p => p.IsDeleted == false && p.ApproveLevel == role.Id).ToListAsync() :
                                        Filter == "Pending" ? await _context.TestBatch.Where(p =>p.IsDeleted == false && p.ApproveLevel == role.Id && ( p.isSAPApproved == null || p.isSAPApproved == false)).ToListAsync():
                                                              await _context.TestBatch.Where(p => p.IsDeleted == false && p.isSAPApproved == true).ToListAsync();            
            break;
    }

    var listOfBatch = new List<TestBatchModel>();

    foreach (var testbatch in dataList)
    {
        var purposeOfTesting = _context.PurposesOfTesting.Select(p => p).Where(p => p.Id == testbatch.PurposesOfTestingId);
        var plant = _context.Plants.Select(p => p).Where(p => p.Id == testbatch.PlantId);

        var Batch = new TestBatchModel
        {
            RefNo = testbatch.Refno,
            PurposesOfTesting = purposeOfTesting.FirstOrDefault().Description,
            Plant = plant.FirstOrDefault().PlantId,
            TestDetails = testbatch.TestDetails,
            Status = testbatch.Status
        };

        listOfBatch.Add(Batch);
    }

    var model = new TestBatchListModel
    {
        Filter = Filter,
        ListOfBatch = listOfBatch
    };

    return model;
}

    public async Task<TestBatchModel> GetTestBatch(int? userId, string RefNo)
    {
        var data = await _context.TestBatch.Select(p => p).Where(p => p.IsDeleted == false && p.Refno == RefNo).ToListAsync();

        var purposeOfTesting = _context.PurposesOfTesting.Select(p => p).Where(p => p.Id == data.FirstOrDefault().PurposesOfTestingId);
        var plant = _context.Plants.Select(p => p).Where(p => p.Id == data.FirstOrDefault().PlantId);

        var testBatch = new TestBatchModel
        {
            RefNo = RefNo,
            PurposesOfTesting = purposeOfTesting.FirstOrDefault().Description,
            Plant = plant.FirstOrDefault().PlantId,
            TestDetails = data.FirstOrDefault().TestDetails,
            Status = data.FirstOrDefault().Status
        };

        return testBatch;
    }

    public async Task<List<LineItem>> GetTestBatchItemList(int? userId, string RefNo)
    {

        var dataList = await _context.TestBatchItem.Select(p => p).Where(p => p.Refno == RefNo && p.IsDeleted == false).ToListAsync();

        var listOfLineItem = new List<LineItem>();

        foreach (var testBatchItem in dataList)
        {
            var lineItem = new LineItem
            {
                ProductCode = testBatchItem.ProductCode,
                ProductName = testBatchItem.ProductName,
                Workcentre = testBatchItem.Workcentre,
                Quantity = testBatchItem.Quantity,
                Remarks = testBatchItem.Remarks,
                Cost = testBatchItem.Cost,
                BatchNumber = testBatchItem.BatchNumber ?? ""
            };

            listOfLineItem.Add(lineItem);
        }
        return listOfLineItem;
    }

    public async Task<bool> UpdateTestBatch(string RefNo, int? userId)
    {

        var isTestBatchItemUpdated = await UpdateTestBatchItem(RefNo, userId);


        var dataList = _context.TestBatch.Select(p => p).Where(p => p.Refno == RefNo && p.IsDeleted == false && p.Status == "Pending").ToList();
        var utcDateTimeValue = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

        foreach (var testbatch in dataList)
        {
            testbatch.IsDeleted = true;
            testbatch.DeletedBy = userId;
            testbatch.DeletedAt = utcDateTimeValue;


            _context.TestBatch.Update(testbatch);
        }

        //await _context.SaveChangesAsync();



        return true;
    }

    public async Task<bool> UpdateTestBatchItem(string RefNo, int? userId)
    {

        var dataList = _context.TestBatchItem.Select(p => p).Where(p => p.Refno == RefNo && p.IsDeleted == false).ToList();

        var listOfLineItem = new List<LineItem>();

        var utcDateTimeValue = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);


        foreach (var testBatchItem in dataList)
        {
            testBatchItem.DeletedAt = utcDateTimeValue;
            testBatchItem.DeletedBy = userId;
            testBatchItem.IsDeleted = true;

            _context.TestBatchItem.Update(testBatchItem);
        }

        //await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<TestBatchListModel>> GetDashboardCounts(int userId, string userRole)
    {

        var listOfTestBatchListCount = new List<TestBatchListModel>();

        if(userRole == "Admin")
        {

        var roles = await _context.Role.Where(r => r.IsDeleted == false).ToListAsync();

        foreach (var role in roles)
        {
            
            var count = await _context.TestBatch.CountAsync(p => p.IsDeleted == false && p.ApproveLevel == role.Id);
            var completed = await _context.TestBatch.CountAsync(p => p.IsDeleted == false && p.ApproveLevel == role.Id && p.Status.Equals("Completed"));
            var pending = await _context.TestBatch.CountAsync(p => p.IsDeleted == false && p.ApproveLevel == role.Id && p.Status.Equals("Pending"));

            var testBatchList = new TestBatchListModel
            {
                Filter = role.RoleName,
                Count = count,
                CompletedCount = completed,
                PendingCount = pending
            };
            listOfTestBatchListCount.Add(testBatchList);

        }
        }
        else{

            var currentRole = _context.Role.Where(r => r.RoleName == userRole && r.IsDeleted == false).FirstOrDefault();



            var completed = 0;
            var pending =0;

            switch (currentRole.RoleName)
    {
        case "Initiator":                            
            pending = await _context.TestBatch.CountAsync(p => p.IsDeleted == false && p.ApproveLevel == currentRole.Id && p.Status.Equals("Pending")) ;
            completed = await _context.TestBatch.CountAsync(p => p.IsDeleted == false && p.ApproveLevel != currentRole.Id  && p.Status.Equals("Pending") || p.Status.Equals("Completed"));
            break;
        case "QC":
            pending = await _context.TestBatch.CountAsync(p =>  p.IsDeleted == false && p.ApproveLevel == currentRole.Id && (p.isQAApproved == null || p.isQAApproved == false)) ;
            completed = await _context.TestBatch.CountAsync(p =>p.IsDeleted == false && p.isQAApproved == true );
            break;
        case "Costing":
            pending = await _context.TestBatch.CountAsync(p =>  p.IsDeleted == false && p.ApproveLevel == currentRole.Id && (p.isCostingApproved == null || p.isCostingApproved == false)) ;
            completed = await _context.TestBatch.CountAsync(p =>p.IsDeleted == false && p.isCostingApproved == true );

           
            break;
        case "Manufacturing Head":
            pending = await _context.TestBatch.CountAsync(p =>  p.IsDeleted == false && p.ApproveLevel == currentRole.Id && (p.isManufacturingHeadApproved == null || p.isManufacturingHeadApproved == false)) ;
            completed = await _context.TestBatch.CountAsync(p =>p.IsDeleted == false && p.isManufacturingHeadApproved == true );

            break;
        case "SAP":
            pending = await _context.TestBatch.CountAsync(p =>  p.IsDeleted == false && p.ApproveLevel == currentRole.Id && (p.isSAPApproved == null || p.isSAPApproved == false)) ;
            completed = await _context.TestBatch.CountAsync(p =>p.IsDeleted == false && p.isSAPApproved == true );
            break;
    }

            var testBatchList = new TestBatchListModel
            {
                Filter = userRole,
                CompletedCount = completed,
                PendingCount = pending
            };
            listOfTestBatchListCount.Add(testBatchList);
        }

        return listOfTestBatchListCount;
    }

    public async Task<bool> GetEditPermission(string? userRole, string RefNo)
    {
        var role = await _context.Role.Where(r => r.IsDeleted == false && r.RoleName == userRole).ToListAsync();

        var status = await _context.TestBatch.Select(p => p).Where(p => p.IsDeleted == false && p.Refno == RefNo && p.ApproveLevel == role.FirstOrDefault().Id && p.Status.Equals("Pending")).ToListAsync();

        if (status.Count != 0)
        {
            return true;
        }

        return false;
    }

   public async Task<bool> SendAction(string RefNo, string? userRole, int? userId, int action)
{
    var nextApproval = getNextApproval(userRole, action);

    var utcDateTimeValue = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

    var pendingApproverTestBatch = _context.TestBatch.Select(p => p).Where(p => p.IsDeleted == false && p.Refno == RefNo && p.Status.Equals("Pending")).FirstOrDefault();

    //To maintain a history of completed
    if (pendingApproverTestBatch != null)
    {
        if (action == 1) // Send for approval
        {
            var nextRole = _context.Role.Where(r => r.RoleName == nextApproval && r.IsDeleted == false).FirstOrDefault();
            var currentRole = _context.Role.Where(r => r.RoleName == userRole && r.IsDeleted == false).FirstOrDefault();

            pendingApproverTestBatch.ApproveLevel = nextRole==null ? currentRole.Id : nextRole.Id;
            pendingApproverTestBatch.ModifiedAt = utcDateTimeValue;
            pendingApproverTestBatch.ModifiedBy = userId;

            switch (userRole)
            {
                case "QC":
                    pendingApproverTestBatch.isQAApproved = true;
                    pendingApproverTestBatch.QAApprovedAt = utcDateTimeValue;
                    pendingApproverTestBatch.QAApprovedBy = userId;
                    break;
                case "Costing":
                    pendingApproverTestBatch.isCostingApproved = true;
                    pendingApproverTestBatch.CostingApprovedAt = utcDateTimeValue;
                    pendingApproverTestBatch.CostingApprovedBy = userId;
                    break;
                case "Manufacturing Head":
                    pendingApproverTestBatch.isManufacturingHeadApproved = true;
                    pendingApproverTestBatch.ManufacturingHeadApprovedAt = utcDateTimeValue;
                    pendingApproverTestBatch.ManufacturingHeadApprovedBy = userId;
                    break;
                case "SAP":
                    pendingApproverTestBatch.Status = "Completed";
                    pendingApproverTestBatch.isSAPApproved = true;
                    pendingApproverTestBatch.SAPApprovedAt = utcDateTimeValue;
                    pendingApproverTestBatch.SAPApprovedBy = userId;
                    break;
            }

            _context.TestBatch.Update(pendingApproverTestBatch);

            await _context.SaveChangesAsync();

            // Send mail for approval
        }
        else // Send back to previous approval
        {
            var previousRole = _context.Role.Where(r => r.RoleName == nextApproval && r.IsDeleted == false).FirstOrDefault();

            pendingApproverTestBatch.ApproveLevel = previousRole.Id;
            pendingApproverTestBatch.ModifiedAt = utcDateTimeValue;
            pendingApproverTestBatch.ModifiedBy = userId;

            switch (userRole)
            {
                case "QC":
                    pendingApproverTestBatch.isQAApproved = false;
                    pendingApproverTestBatch.QAApprovedAt = null;
                    pendingApproverTestBatch.QAApprovedBy = null;
                    break;
                case "Costing":
                    pendingApproverTestBatch.isQAApproved = false;

                    // pendingApproverTestBatch.isCostingApproved = false;
                    pendingApproverTestBatch.CostingApprovedAt = null;
                    pendingApproverTestBatch.CostingApprovedBy = null;
                    break;
                case "Manufacturing Head":
                    pendingApproverTestBatch.isCostingApproved = false;

                    // pendingApproverTestBatch.isManufacturingHeadApproved = false;
                    pendingApproverTestBatch.ManufacturingHeadApprovedAt = null;
                    pendingApproverTestBatch.ManufacturingHeadApprovedBy = null;
                    break;
                case "SAP":
                    pendingApproverTestBatch.isManufacturingHeadApproved = false;

                    pendingApproverTestBatch.isSAPApproved = false;
                    pendingApproverTestBatch.SAPApprovedAt = null;
                    pendingApproverTestBatch.SAPApprovedBy = null;
                    break;
            }

            _context.TestBatch.Update(pendingApproverTestBatch);

            await _context.SaveChangesAsync();

            // Send mail for rejection
        }

        if (nextApproval == "")
        {
            pendingApproverTestBatch.Status = "Completed";
            pendingApproverTestBatch.ModifiedAt = utcDateTimeValue;
            pendingApproverTestBatch.ModifiedBy = userId;

            _context.TestBatch.Update(pendingApproverTestBatch);

            await _context.SaveChangesAsync();
        }

        return true;
    }

    return false;
}

    private string getNextApproval(string userRole, int action=1)
    {
        var nextApproval = "";
        if (action == 1)

        {
            //Get approver name
            switch (userRole)
            {
                case "Initiator":
                    nextApproval = "QC";
                    break;
                case "QC":
                    nextApproval = "Costing";
                    break;
                case "Costing":
                    nextApproval = "Manufacturing Head";
                    break;
                case "Manufacturing Head":
                    nextApproval = "SAP";
                    break;
                case "SAP":
                    Console.WriteLine("High level");
                    break;
            }

        }
        else
        {
            switch (userRole)
            {
                case "Initiator":
                    nextApproval = "";
                    break;
                case "QC":
                    nextApproval = "Initiator";
                    break;
                case "Costing":
                    nextApproval = "QC";
                    break;
                case "Manufacturing Head":
                    nextApproval = "Costing";
                    break;
                case "SAP":
                    nextApproval = "Manufacturing Head";
                    break;
            }
        }

        return nextApproval;
    }

    public List<Product> GetProductCodesForPlant(string plant)
    {
        var productCodes = new List<Product>();

        productCodes = _context.ProductMaster
            .Where(pc => pc.PlantId.ToString() == plant)
            .Select(pc => new Product
            {
                ProductCode = pc.ProductCode,
                ProductName = pc.ProductName
            })
            .ToList();


        return productCodes;
    }

    public async Task<string> UpdateTestBatchForCostingAndSAP(TestFormViewModel model,int? userId, string? userRole){
        var dataList = _context.TestBatchItem.Select(p => p).Where(p => p.Refno == model.RefNo && p.IsDeleted == false).ToList();
        var utcDateTimeValue = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        
        foreach (var item in model.LineItems)
        {
            var testBatchItem = dataList.FirstOrDefault(tbi => tbi.ProductCode == item.ProductCode);
            if (testBatchItem != null)
            {
                if(userRole == "Costing")
                    testBatchItem.Cost = item.Cost;
                else
                    testBatchItem.BatchNumber = item.BatchNumber;

                testBatchItem.ModifiedAt = utcDateTimeValue;
                testBatchItem.ModifiedBy = userId;
                _context.TestBatchItem.Update(testBatchItem);
            }
        }

        await _context.SaveChangesAsync();

        return model.RefNo;
    }

}