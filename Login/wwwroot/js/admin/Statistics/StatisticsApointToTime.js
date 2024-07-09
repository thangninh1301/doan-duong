var viewmodel = function () {
    var self = this;
    self.arrays = ko.observableArray();

    /*self.UserArrays = ko.observableArray();
    self.resultArrays = ko.observableArray();*/

    self.click = function () {
        var id = $('#admin').val();
        $.ajax({
            type: "GET",
            url: backendUrl + "/api/Adminthongke/admin/" + id,
           // url: "https://backend-btl.mooo.com/api/Adminthongke/admin/" + id,
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
    var ap = new viewmodel();
    ap.click();
    ko.applyBindings(ap);
});