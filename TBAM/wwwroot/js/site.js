// //var lineItemCounter = @Model.LineItems.Count;

// $(document).ready(function (lineItemCounter, allProductCodes, availableWorkcentres) {
//     $('#addLineItem').click(function () {
//         var selectedCodes = [];
//         $('select[name^="LineItems"][name$="ProductCode"]').each(function () {
//             selectedCodes.push($(this).val());
//         });

//         // var allProductCodes = @Html.Raw(Json.Serialize(Model.ProductCodes));
//         var availableProductCodes = allProductCodes.filter(code => !selectedCodes.includes(code));

//         if (availableProductCodes.length === 0) {
//             alert("All product codes are already selected.");
//             return false;
//         }

//         var productOptions = availableProductCodes.map(code => `<option value="${code}">${code}</option>`).join('');

//         var availableWorkcentres = @Html.Raw(Json.Serialize(Model.Workcentres));
//         var workcentreOptions = '';
//         availableWorkcentres.forEach(function (workcentre) {
//             workcentreOptions += '<option value="' + workcentre + '">' + workcentre + '</option>';
//         });

//         $('<tr id="lineItemRow' + lineItemCounter + '"><td>' +
//             '<select name="LineItems[' + lineItemCounter + '].ProductCode" class="form-control">' + productOptions + '</select>' +
//             '</td><td>' +
//             '<input type="text" name="LineItems[' + lineItemCounter + '].ProductName" class="form-control" />' +
//             '</td><td>' +
//             '<select name="LineItems[' + lineItemCounter + '].Workcentre" class="form-control">' + workcentreOptions + '</select>' +
//             '</td><td>' +
//             '<input type="text" name="LineItems[' + lineItemCounter + '].Quantity" class="form-control" />' +
//             '</td><td>' +
//             '<input type="text" name="LineItems[' + lineItemCounter + '].BatchNumber" class="form-control" />' +
//             '</td><td>' +
//             '<input type="text" name="LineItems[' + lineItemCounter + '].Remarks" class="form-control" />' +
//             '</td><td>' +
//             '<button type="button" class="btn btn-danger" onclick="removeLineItem(' + lineItemCounter + ');">Delete</button>' +
//             '</td></tr>').appendTo('#lineItemsTable tbody');
//         lineItemCounter++;
//         return false;
//     });

// });

// function removeLineItem(index) {
//     $('#lineItemRow' + index).remove();
// }