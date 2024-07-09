var viewmodal = function () {
    self = this;
    self.arrays = ko.observableArray();
    self.roles1 = ko.observableArray();
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
    self.selectedRoles = ko.observableArray();
    self.click = function () {
        self.selectedRoles([]);
        $.ajax({
            type: "get",
            url: backendUrl + "/api/Admin/AllUserRole" ,
           // url: "https://backend-btl.mooo.com/api/Admin/AllUserRole",
            contentType: "application/json",
            success: function (data) {
                self.arrays([]);
                $.each(data, function (ex, item) {
                    self.arrays.push(item);
                });      
            },
            error: function () {
            },
        });
    }
    self.Update = function (item1) {
        $('#modal').modal('show');
        
        $.ajax({
            type: "get",
            url: backendUrl + "/api/Role",
           // url: "https://backend-btl.mooo.com/api/Role",
            contentType: "application/json",
            success: function (data) {
                self.selectedRoles([]);
                self.roles1([]);
                var roles = item1.roles;
                roles+= "";
                $.each(data, function (ex, item) {
                    console.log(item);
                    self.roles1.push(item);
                    var name = item.name;
                    name+= "";
                    if (roles.indexOf(name) >= 0) self.selectedRoles.push(name);
                    console.log(self.selectedRoles());
                    /*var bool = item1.indexOf(item);
                    if (bool) self.selectedRoles.push(item);*/
                });
               
            },
            error: function () {
            },
        })
        var newRoles = "";

        self.save = function () {
            console.log(self.selectedRoles());
            for (let i = 0; i < self.selectedRoles().length; i++) {
                newRoles += "," + self.selectedRoles()[i];
            }
            /*$.each(self.selectedRoles, function (x) {
                newRoles += "," + x.name;

            });*/
                 var data1 = {
                     UserId: item1.userId,
                     UserName: item1.userName,
                     CurrentRoleNames: item1.roles,
                     NewRoleNames: newRoles,
            }
                $.ajax({
                    type: "post",
                    url: backendUrl + "/api/Admin/AddRoleForUser",
                   // url: "https://backend-btl.mooo.com/api/Admin/AddRoleForUser",
                    contentType: "application/json",
                    data: JSON.stringify(data1),
                    success: function (data) {
                        console.log(data);
                        self.click();
                        $('#modal').modal('hide');
                        self.showtoastState("Cập nhật quyền thành công");
                    },
                    error: function () {
                        $('#modal').modal('hide');
                        self.showtoastError("Lỗi");

                    },
                })
           
        }
    }
}
$(function(){
    var abc = new viewmodal();
    abc.click();
    ko.applyBindings(abc);
});
$(document).ready(function () {
    $("#myInput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#myTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});