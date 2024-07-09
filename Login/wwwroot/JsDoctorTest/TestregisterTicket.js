
var viewmodel = function () {
    self = this;
    self.arrays = ko.observableArray();
    self.convertToKoObject = function (data) {
        var newObj = ko.mapping.fromJS(data);
        newObj.Selected = ko.observable(false);
        return newObj;
    }
    self.showtoastError = function (msg, title) {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "showDuration": "3000",
            "hideDuration": "3000",
            "timeOut": "3000",
            "extendedTimeOut": "3000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr['error'](title, msg);
    };
    self.showtoastState = function (msg, title) {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "showDuration": "3000",
            "hideDuration": "3000",
            "timeOut": "3000",
            "extendedTimeOut": "3000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr['success'](title, msg);
    };
    self.click = function () {
        var doctorTestId = $('#doctorTestId').val();
        self.arrays([]);
        $.ajax({
            type: "get",
            url: backendUrl + "/api/DoctorTest/aa/" + doctorTestId,
            //url: "https://backend-btl.mooo.com/api/DoctorTest/aa/",
            contentType: "application/Json",
            success: function (data) {
                $.each(data, function (ex, item) {
                    self.arrays.push(item);
                    console.log(data);
                });
            },
            error: function (err) {
                console.log(err);
            }

        })
    }
    self.listResult = function (item) {
       
        $('#modal').modal('show');
        console.log("item", item)
        self.save = function () {
            var myfile = $('#url').prop("files");
            var formdata = new FormData()
            formdata.append('file', myfile[0]);
            formdata.append("idDoctorTest", item.idDoctorTest);
            formdata.append("idResult", item.idResult);
            formdata.append("diagnostic", $('#diagnostic').val());
            $.ajax({
                type: "post",
                url: "https://backend-btl.mooo.com/api/DoctorTest",
                cache: false,
                contentType: false,
                processData: false,
                data: formdata,
                success: function (data) {
                    self.click(formdata);
                    
                    console.log(data);
                    $('#modal').modal('hide');
                }, error: function (err) {

                }
            });
        }

    }
}

$(function () {
    var abc = new viewmodel();
    abc.click();
    ko.applyBindings(abc);

})