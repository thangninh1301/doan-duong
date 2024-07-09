var Viewmodal = function () {
    self = this;

    self.arrays = ko.observableArray();
    self.arrayTest = ko.observableArray();

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
    self.loadTest = function (item1) {
        self.arrayTest([]);
        $.ajax({
            type: "get",
            url: backendUrl + "/api/Test/Admin",
            // url: "https://backend-btl.mooo.com/api/Test/Admin",
            contentType: "application/Json",
            success: function (data) {
                $.each(data, function (ex, item) {
                    self.arrayTest.push(item);
                });
                if (item1 != null)
                    $('#idTest').val(item1.idTest)
            },
            error: function (err) {
                console.log(err);
            }

        })

    }

    self.click = function () {
        self.loadTest(null);

        $.ajax({
            type: "get",
            url: backendUrl + "/api/Admin/AllDoctorTest",
            // url: "https://backend-btl.mooo.com/api/Admin/AllDoctor",
            contentType: "application/json",
            success: function (data) {
                self.arrays([]);
                $.each(data, function (ex, item) {

                    self.arrays.push(item);

                })

            },
            error: function (err) {
                alert(err);
            }

        });
    }
    self.update = function (item) {
        self.loadTest(item);
        $('#id').val(item.id);
        $('#firstName').val(item.firstName);
        $('#lastName').val(item.lastName);
        $('#userName').val(item.userName);
        $('#phoneNumber').val(item.phoneNumber);
        if (!item.deleted) {
            ($("#deleted").val("False"));
        } else ($("#deleted").val("True"));
        $('#modal').modal("show");
        //nhan nut save
        /*console.log(item);*/
        self.save = function () {

            var abc = true;
            if ($("#deleted").val() == "False") {
                abc = false;
            }
            var id1 = item.idTest;

            var Obj = {
                Id: item.id,
                FirstName: $('#firstName').val(),
                LastName: $('#lastName').val(),
                IdTest: parseInt($('#idTest').val()),
                UserName: $('#userName').val(),
                PhoneNumber: $('#phoneNumber').val(),
                Deleted: abc,
            }
            if ($('#idTest').val()==null) {
                self.showtoastError("Chưa chọn Xét nghiệm cho bác sỹ")
            } else
            $.ajax({
                type: "PUT",
                url: backendUrl + "/api/User/" + item.id,
                //url: "https://backend-btl.mooo.com/api/User/" + item.id,
                data: JSON.stringify(Obj),
                contentType: "application/json",

                success: function () {
                    console.log(JSON.stringify(Obj));
                    self.click();
                    $('#modal').modal('hide');
                    self.showtoastState("Sửa thành công");
                },
                error: function (err) {
                    self.showtoastError("Lỗi");
                    /* console.log(JSON.stringify(Obj));
                     self.click();*/
                }
            });

        }
    }
    self.del = function (item) {
        if (confirm("ban muon xoa " + item.id + " khong")) {
            $.ajax({
                type: "put",
                url: backendUrl + "/api/User/delete/" + item.id,
                // url: "https://backend-btl.mooo.com/api/User/delete/" + item.id,
                success: function (data) {
                    self.click();
                    self.showtoastState("Xóa thành công");
                },
                error: function (error) {
                    self.showtoastError("Lỗi");
                    console.log(item);
                    self.click()
                }
            });
        }

    };
}
$(function () {
    var aaa = new Viewmodal();
    aaa.click();
    ko.applyBindings(aaa);
});
$(document).ready(function () {
    $("#myInput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#myTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});