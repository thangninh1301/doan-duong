var viewmodal = function () {
    self = this;
    self.array = ko.observableArray();
    self.click = function () {
        self.array([]);
        $.ajax({
            type: "get",
            url: "https://backend-btl.mooo.com/api/Role",
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
            $.ajax({
                type: "POST",
                url: "https://backend-btl.mooo.com/api/Role",
                data: JSON.stringify(crdata),
                contentType: "application/json",
                success: function () {
                    console.log(crdata);
                    document.location = "Role";
                    self.click();

                },
                error: function (err) {
                    console.log(" Not Failed");
                }
            });}
        
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
            $.ajax({
                type: "PUT",
                url: "https://backend-btl.mooo.com/api/Role/" + item.id,
                data: JSON.stringify(crdata),
                contentType: "application/json",

                success: function (data) {
                    console.log(data);
                    self.click();
                    document.location = 'Role';
                },
                error: function (err) {
                    alert("loi " + err.status + "<!----!>" + err.statusText);
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
                url: "https://backend-btl.mooo.com/api/Role/" + item.id,
                contentType: "application/json",
                success: function (data) {
                    console.log(item);
                    self.click();
                }, error: function (err) {
                    alert("loi " + err.status + "<!----!>" + err.statusText);
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