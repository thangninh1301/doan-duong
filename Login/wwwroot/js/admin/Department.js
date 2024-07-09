

var viewmodel = function () {
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
            url: backendUrl + "/api/Departments",
            //url: "https://backend-btl.mooo.com/api/Department",
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
        $('#decription').val("");
        $('#datecreate').val("");
        self.save = function () {
            var a = true;
            if ($('#no').prop("checked")) {
                a = false;
            }
            var crdata = {
                Name: $("#name").val(),
                Decription: $("#decription").val(),
                Deleted: a
            }
            if ($('#name').val() == "" ) {
                self.showtoastError("Không để trống trường Tên")
            }
            else
            $.ajax({
                type: "POST",
                url: backendUrl + "/api/Department",
               // url: "https://backend-btl.mooo.com/api/Department",
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
            $('#decription').val(item.decription);
            $('#datecreate').val(moment(item.datecreate).format('YYYY-MM-DD'));
            $('#idDepartment').val(item.idDepartment).attr('checked', true);
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
                    Id: item.id,
                    Name: $('#name').val(),
                    Datecreate: $('#datecreate').val(),
                    Decription: $('#decription').val(),
                    Deleted: check
                }
                if ($('#name').val() == "") {
                    self.showtoastError("Không để trống trường Tên")
                }
                else
                $.ajax({
                    type: "PUT",
                    url: backendUrl + "/api/Department",
                   // url: "https://backend-btl.mooo.com/api/Department",
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
                    type: "PUT",
                    url: backendUrl + "/api/Department/" + item.id,
                   // url: "https://backend-btl.mooo.com/api/Department/" + item.id,
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
        var abc = new viewmodel();
        abc.click();
        ko.applyBindings(abc);

    })