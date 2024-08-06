using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TBAM.Models;
using System.Security.Claims;
using System.Text.Json;
using TBAM.Data;
using Microsoft.AspNetCore.Http.Features;


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


    public async Task<bool> CreateTestBatch(TestFormViewModel model, int? userId)
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

        //fill TestBatch model
        var testBatchDbModel = new TestBatch
        {
            Refno = model.RefNo == null ? newRefNo : model.RefNo, //if first time save then newRefNo; To update use the older RefNo.
            PurposesOfTestingId = PurposesOfTestingId.FirstOrDefault().Id,
            PlantId = PlantId.FirstOrDefault().Id,
            TestDetails = model.TestDetails,
            CreatedBy = (int)userId,
            Status = "Initiator"

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

        return isCreatedSuccessfully;
    }

    public async Task<TestBatchListModel> GetTestBatchList(int? userId)
    {
        var dataList = _context.TestBatch.Select(p => p).Where(p => p.CreatedBy == userId && p.IsDeleted == false).ToList();

        var listOfBatch = new List<TestBatchModel>();
        // var purposeOfTesting = "Purpose of testing 1";
        // var plant = 2100;
        // var TestDetails = "Test details";
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
            ListOfBatch = listOfBatch
        };
        return model;
    }

    public async Task<TestBatchModel> GetTestBatch(int? userId, string RefNo)
    {
        var data = _context.TestBatch.Select(p => p).Where(p => p.CreatedBy == userId && p.IsDeleted == false && p.Refno == RefNo).ToList();

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

        var dataList = _context.TestBatchItem.Select(p => p).Where(p => p.Refno == RefNo && p.IsDeleted == false && p.CreatedBy == userId).ToList();

        var listOfLineItem = new List<LineItem>();

        foreach (var testBatchItem in dataList)
        {
            var lineItem = new LineItem
            {
                ProductCode = testBatchItem.ProductCode,
                ProductName = testBatchItem.ProductName,
                Workcentre = testBatchItem.Workcentre,
                Quantity = testBatchItem.Quantity,
                Remarks = testBatchItem.Remarks
            };

            listOfLineItem.Add(lineItem);
        }
        return listOfLineItem;
    }

    public async Task<bool> UpdateTestBatch(string RefNo, int? userId)
    {

        var isTestBatchItemUpdated = await UpdateTestBatchItem(RefNo, userId);

        if (isTestBatchItemUpdated)
        {

            var dataList = _context.TestBatch.Select(p => p).Where(p => p.Refno == RefNo && p.IsDeleted == false).ToList();
            var utcDateTimeValue = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

            foreach (var testbatch in dataList)
            {
                testbatch.IsDeleted = true;
                testbatch.DeletedBy = userId;
                testbatch.DeletedAt = utcDateTimeValue;


                _context.TestBatch.Update(testbatch);
            }

            await _context.SaveChangesAsync();

        }

        return false;
    }

    public async Task<bool> UpdateTestBatchItem(string RefNo, int? userId)
    {

        var dataList = _context.TestBatchItem.Select(p => p).Where(p => p.Refno == RefNo).ToList();

        var listOfLineItem = new List<LineItem>();

        var utcDateTimeValue = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);


        foreach (var testBatchItem in dataList)
        {
            testBatchItem.DeletedAt = utcDateTimeValue;
            testBatchItem.DeletedBy = userId;
            testBatchItem.IsDeleted = true;

            _context.TestBatchItem.Update(testBatchItem);
        }

        await _context.SaveChangesAsync();

        return true;
    }
}