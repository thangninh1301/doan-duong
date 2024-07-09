var viewmodal = function () {
    self = this;
    self.arrays = ko.observableArray();
    self.roles1 = ko.observableArray();
    self.selectedRoles = ko.observableArray();
    self.click = function () {
        self.selectedRoles([]);
        $.ajax({
            type: "get",
            url: "https://backend-btl.mooo.com/api/Admin/AllUserRole",
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
            url: "https://backend-btl.mooo.com/api/Role",
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
                    url: "https://backend-btl.mooo.com/api/Admin/AddRoleForUser",
                    contentType: "application/json",
                    data: JSON.stringify(data1),
                    success: function (data) {
                        console.log(data);
                        self.click();
                        $('#modal').modal('hide');
                    },
                    error: function () {
                        $('#modal').modal('hidden');
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