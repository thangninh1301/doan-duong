var viewmodal = function () {
    var self = this;
    self.arrays = ko.observableArray();
    self.click = function () {
        $.ajax({
            type: "GET",
            url: backendUrl + "/api/Admin/Statistic/doctorInDepart",
           // url: "https://backend-btl.mooo.com/api/Admin/Statistic/doctorInDepart" ,
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                self.arrays([]);
                $.each(data, function (ex, item) {
                    self.arrays.push(item);
                });
            },
            error: function (error) {
                alert("error");
            }
        });
    }
}
$(function () {
    var abc = new viewmodal();
    abc.click();
    ko.applyBindings(abc);
})