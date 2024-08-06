// Models/TestFormViewModel.cs
using System.Collections.Generic;

namespace TBAM.Models
{
public class TestFormViewModel
{
    public required List<LineItem> LineItems { get; set; }
    public List<Product>? Products { get; set; }
    public  List<string>? Workcentres { get; set; }
    public required List<string> PurposesOfTesting { get; set; }
    public required List<int> Plants { get; set; }

    public required string TestDetails { get; set; }
    public string RefNo {get; set;}
     

}

public class Product{
    public required string ProductCode { get; set; }
    public required string ProductName { get; set; }
}
public class LineItem
{
    public required string ProductCode { get; set; }
    public required string ProductName { get; set; }
    public required string Workcentre { get; set; }
    public required int Quantity { get; set; }
    public string? BatchNumber { get; set; }
    public required string Remarks { get; set; }
}

}
