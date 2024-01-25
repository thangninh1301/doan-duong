

var viewmodel = function () {
    self = this;
    self.array = ko.observableArray();
    self.click = function () {
        self.array([]);
        $.ajax({
            type: "get",
            url: "https://backend-btl-duong.mooo.com/api/Department",
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
            $.ajax({
                type: "POST",
                url: "https://backend-btl-duong.mooo.com/api/Department",
                data: JSON.stringify(crdata),
                contentType: "application/json",
                success: function () {
                    console.log(crdata);
                    self.click();
                },
                error: function (err) {
                    console.log(crdata);
                }
            });
        }
    }
        self.Update = function (item) {
            $('#modal').modal("show");
            $('#name').val(item.name);
            $('#decription').val(item.decription);
            $('#datecreate').val(item.datecreate);
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
                $.ajax({
                    type: "PUT",
                    url: "https://backend-btl-duong.mooo.com/api/Department",
                    data: JSON.stringify(crdata),
                    contentType: "application/json",

                    success: function (data) {
                        console.log(data);
                        self.click();
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
            if (confirm("ban muon xoa " + item.name + " khong")) {
                $.ajax({
                    type: "PUT",
                    url: "https://backend-btl-duong.mooo.com/api/Department/" + item.id,
                    contentType: "application/json",
                    success: function (data) {

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
        var abc = new viewmodel();
        abc.click();
        ko.applyBindings(abc);

    })