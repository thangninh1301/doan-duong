var Viewmodel = function () {
    self = this;

    self.arrays = ko.observableArray();
    self.arrayDepartment = ko.observableArray();
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
    self.loadDepartment = function (item1) {
        self.arrayDepartment([]);
        $.ajax({
            type: "get",
            url: backendUrl + "/api/Departments",
           // url: "https://backend-btl-duong.mooo.com/api/Departments",
            contentType: "application/Json",
            success: function (data) {
                $.each(data, function (ex, item) {
                    self.arrayDepartment.push(item);
                });
                console.log(item1.arrayDepartment)
                if (idDepartment != null) {
                    $('idDepartment').val(item1.idDepartment)
                }
            },
            error: function (err) {
                console.log(err);
            }

        })

    }
    self.click = function () {
        $.ajax({
            type: "get",
            url: backendUrl + "/api/Admin/Doctor",
            //url: "https://backend-btl-duong.mooo.com/api/Admin/Doctor",
            contentType: "application/json",
            success: function (data) {
                $.each(data, function (ex, item) {
                    item.nameDepartment = "";
                    $.ajax({
                        type: "get",
                        url: backendUrl + "/api/Departments/" + item.idDepartment,
                       // url: "https://backend-btl-duong.mooo.com/api/Departments/" + item.idDepartment,
                        contentType: "application/json",
                        success: function (data1) {
                                item.nameDepartment = data1.name;
                                console.log("ngoai :", item.nameDepartment);
                                self.arrays.push(item);
                        },
                        error: function (err) {
                            item.nameDepartment = "Benhnhan";
                            self.arrays.push(item);
                        }
                    });
                })

            },
            error: function (err) {
                alert(err);
            }

        });
    }

    self.update = function (item) {
        self.loadDepartment(item);
        $('#id').val(item.id);
        $('#firstName').val(item.firstName);
        $('#lastName').val(item.lastName);
       /* $('#idDepartment').val(item.idDepartment);*/
        $('#userName').val(item.userName);
        $('#email').val(item.email);
        $('#securityStamp').val(item.securityStamp);
        $('#concurrencyStamp').val(item.concurrencyStamp);
        $('#phoneNumber').val(item.phoneNumber);
        if (!item.deleted) {
            ($("#deleted").val() == "False");
        } ($("#deleted").val() == "True");
        $('#modal').modal("show");
        //nhan nut save
        /*console.log(item);*/
        self.save = function () {

            var abc = true;
            if ($("#deleted").val() == "False") {
                abc = false;
            }
            var id1 = item.idDepartment;
            var Obj = {
                Id: item.id,
                FirstName: $('#firstName').val(),
                LastName: $('#lastName').val(),
                IdDepartment: parseInt($('#idDepartment').val()),
                UserName: $('#userName').val(),
                Email: item.email,
                SecurityStamp: item.securityStamp,
                ConcurrencyStamp: item.concurrencyStamp,
                PhoneNumber: $('#phoneNumber').val(), 
                Deleted:abc,
            }
            $.ajax({
                type: "PUT",
                url: backendUrl + "/api/User/" + item.id,
                //url: "https://backend-btl-duong.mooo.com/api/User/" + item.id,
                data: JSON.stringify(Obj),
                contentType: "application/json",

                success: function () {
                    console.log(JSON.stringify(Obj));
                    self.click();
                    self.showtoastState("Sửa thành công");
                    document.location = 'Users';
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
               // url: "https://backend-btl-duong.mooo.com/api/User/delete/" + item.id,
                success: function (data) {
                    self.click();
                    self.showtoastState("Xóa thành công");
                    document.location = 'Users';

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



$(function(){
    var aaa = new Viewmodel();
     aaa.click();
   ko.applyBindings(aaa);
});
