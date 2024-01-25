var Viewmodal = function () {
    self = this;

    self.array = ko.observableArray();
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
        self.array([]);
        $.ajax({
            type: "get",
            url: backendUrl + "/api/Test/Admin",
            //url: "https://backend-btl-duong.mooo.com/api/Test/Admin",
            contentType: "application/Json",
            success: function (data) {
                $.each(data, function (ex, item) {
                    self.array.push(item);
                });
            },
            error: function (err) {
                console.log(err);
            }

        })
    }
    self.Create = function () {
        $("#modal").modal('show');
        $('#name').val("");
        $('#desciption').val("");
        $('#datecreate').val("");
        self.save = function () {
            
            var crdata = {
                Name: $("#name").val(),
                Desciption: $("#desciption").val()
                
            }
            $.ajax({
                type: "POST",
                url: backendUrl + "/api/Test",
                // url: "https://backend-btl-duong.mooo.com/api/Test",
                data: JSON.stringify(crdata),
                contentType: "application/json",
                success: function () {
                    console.log(crdata);
                    self.click();
                    $('#modal').modal('hide');
                    self.showtoastState("Thêm thành công");
                },
                error: function (err) {
                    console.log(crdata);
                    self.showtoastError("Lỗi");
                }
            });
        }
    }
    self.Update = function (item) {
        $('#modal').modal("show");
        $('#name').val(item.name);
        $('#desciption').val(item.desciption);
        $('#datecreate').val(moment(item.datecreate).format('YYYY-MM-DD'));
        $('#idTest').val(item.idTest).attr('checked', true);
       

        //nhan nut save
        self.save = function () {
            

            var crdata = {
                Id: item.id,
                Name: $('#name').val(),
                Datecreate: $('#datecreate').val(),
                Desciption: $('#desciption').val(),
                
            }
            $.ajax({
                type: "PUT",
                url: backendUrl + "/api/Test/" + item.id,
                // url: "https://backend-btl-duong.mooo.com/api/Test/" +,
                data: JSON.stringify(crdata),
                contentType: "application/json",

                success: function (data) {
                    console.log(data);
                    self.click();
                    $('#modal').modal('hide');
                    self.showtoastState("Sửa thành công");
                },
                error: function (err) {
                    self.showtoastError("Lỗi");
                    console.log(JSON.stringify(crdata));
                    self.click();
                }
            });
        }
    }
    self.Delete = function (item) {
        if (confirm("ban muon xoa " + item.name + " khong")) {
            $.ajax({
                type: "DELETE",
                url: backendUrl + "/api/Test/" + item.id,
                // url: "https://backend-btl-duong.mooo.com/api/Test/" + item.id,
                contentType: "application/json",
                success: function (data) {

                    self.click();
                    self.showtoastState("Xóa thành công");
                }, error: function (err) {
                    self.showtoastError("Lỗi");
                    console.log(item);
                    self.click();
                }
            });
        }

    }
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