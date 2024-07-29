// Models/TestFormViewModel.cs
using System.Collections.Generic;

namespace TBAM.Models
{
public class TestFormViewModel
{
    public required List<LineItem> LineItems { get; set; }
    public required List<string>? ProductCodes { get; set; }
    public required List<string>? Workcentres { get; set; }
    public required List<string> PurposesOfTesting { get; set; }
    public required List<string> Plants { get; set; }

    public required string TestDetails { get; set; }

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
