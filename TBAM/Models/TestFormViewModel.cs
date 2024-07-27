// Models/TestFormViewModel.cs
using System.Collections.Generic;

namespace TBAM.Models
{
public class TestFormViewModel
{
    public List<LineItem> LineItems { get; set; }
    public List<string>? ProductCodes { get; set; }
    public List<string>? Workcentres { get; set; }
    public List<string> PurposesOfTesting { get; set; }
    public List<string> Plants { get; set; }

    public string TestDetails { get; set; }

}

public class LineItem
{
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public string Workcentre { get; set; }
    public string Quantity { get; set; }
    public string BatchNumber { get; set; }
    public string Remarks { get; set; }
}

}
