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
            url: backendUrl + "/api/Role",
           // url: "https://backend-btl.mooo.com/api/Role",
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
        $('#modal').modal("show");
        $('#id').val(""),
        $('#name').val("");
        $('#normalizedName').val("");
        self.save = function () {
            var crdata = {
                Id: $('#id').val(),
                Name: $("#name").val(),
                NormalizedName: $("#normalizedName").val(),

            }
            if ($('#id').val() == "" || $('#name').val() == "") {
                self.showtoastError("Không để trống Mã hoặc Tên")
            }
            else
            $.ajax({
                type: "POST",
                url: backendUrl + "/api/Role",
                //url: "https://backend-btl.mooo.com/api/Role",
                data: JSON.stringify(crdata),
                contentType: "application/json",
                success: function () {
                    console.log(crdata);
                    document.location = "Role";
                    self.click();
                    self.showtoastState("Thêm thành công");

                },
                error: function (err) {
                    self.showtoastError("Lỗi");
                    
                }
            });

        }
        
        }
    self.Update = function (item) {
        $('#modal').modal("show");
        $('#id').val(item.id),
        $('#name').val(item.name);
        $('#normalizedName').val(item.normalizedName);
        
        //nhan nut save
        self.save = function () {
            

            var crdata = {
                Id: item.id,
                Name: $('#name').val(),
                NormalizedName: $('#normalizedName').val()  
            }
            if ($('#id').val() == "" || $('#name').val()=="") {
                self.showtoastError("Không để trống Mã hoặc Tên")
            }
            else
            $.ajax({
                type: "PUT",
                url: backendUrl + "/api/Role/" + item.id,
               // url: "https://backend-btl.mooo.com/api/Role/" + item.id,
                data: JSON.stringify(crdata),
                contentType: "application/json",

                success: function (data) {
                    console.log(data);
                    self.click();
                    self.showtoastState("Sửa thành công");
                    document.location = 'Role';
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
        if (confirm("ban muon xoa " + item.name+ " khong")) {
            $.ajax({
                type: "delete",
                url: backendUrl + "/api/Role/" + item.id,
               // url: "https://backend-btl.mooo.com/api/Role/" + item.id,
                contentType: "application/json",
                success: function (data) {
                    console.log(item);
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