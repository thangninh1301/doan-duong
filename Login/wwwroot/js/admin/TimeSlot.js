var viewmodal = function () {
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
            url: backendUrl + "/api/TimeSlot/Admin",
           // url: "https://localhost:44310/api/TimeSlot/Admin",
            contentType: "application/json",
            success: function (data) {
                $.each(data, function (ex, item) { self.array.push(item) });
            },
            error: function (err) {
                alert(err);
            },
        });
    }
    self.Create = function () {
        $("#modal").modal('show');

        $('#decription').val("");
        $('#datecreate').val("");
        $('#max').val();
        self.save = function () {
            var a = true;
            if ($('#no').prop("checked")) {
                a = false;
            }
            var crdata = {
                Decription: $("#decription").val(),
                Max: parseInt($("#max").val()),
                Deleted: a
            }
            $.ajax({
                type: "POST",
                url: backendUrl + "/api/TimeSlot",
               // url: "https://localhost:44310/api/TimeSlot",
                data: JSON.stringify(crdata),
                contentType: "application/json",
                success: function (data) {
                    console.log(data);
                    self.click();
                    $('#modal').modal('hide');
                    self.showtoastState("Thêm thành công");
                }, error: function (err) {
                    self.showtoastError("Lỗi");
                console.log(JSON.stringify(crdata));
                self.click();
                }
              });
        }
    }
    self.Update = function (item) {
        $('#modal').modal("show");
        $('#decription').val(item.decription);
        $('#datecreate').val(moment(item.dateCreate).format('YYYY-MM-DD'));
        $('#max').val(item.max);
        if (!item.deleted) {
            $('#no').attr('checked', true);
        } else $('#yes').attr('checked', true);

        //nhan nut save
        self.save = function () {
            var check = true;
            if ($('#no').prop('checked')) {
                check = false;
            }

            var crdata = {
                Id:item.id,
                DateCreate: item.dateCreate,
                Decription: $('#decription').val(),
                Max: parseInt($('#max').val()),
                Deleted: check
            }
            $.ajax({
                type: "PUT",
                url: backendUrl + "/api/TimeSlot",
               // url: "https://localhost:44310/api/TimeSlot" ,
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
        if (confirm("ban muon xoa " + item.decription + " khong")) {
            $.ajax({
                type: "PUT",
                url: backendUrl + "/api/TimeSlot/delete/" + item.id,
                //url: "https://localhost:44310/api/TimeSlot/delete/" + item.id,
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
    var db = new viewmodal();
    db.click();
    ko.applyBindings(db);
});