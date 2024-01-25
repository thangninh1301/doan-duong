var viewmodal = function () {
    self = this;
    self.array = ko.observableArray();
    self.click = function () {
        self.array([]);
        $.ajax({
            type: "get",
            url: "https://localhost:44310/api/TimeSlot/Admin",
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
        self.save = function () {
            var a = true;
            if ($('#no').prop("checked")) {
                a = false;
            }
            var crdata = {
                Decription: $("#decription").val(),
                Deleted: a
            }
            $.ajax({
                type: "POST",
                url: "https://localhost:44310/api/TimeSlot",
                data: JSON.stringify(crdata),
                contentType: "application/json",
                success: function (data) {
                    console.log(data);
                    self.click();
                    $('#modal').modal('hide');
                }, error: function (err) {
                alert("loi "+ err.status + "<!----!>" + err.statusText);
                console.log(JSON.stringify(crdata));
                self.click();
                }
              });
        }
    }
    self.Update = function (item) {
        $('#modal').modal("show");
        $('#decription').val(item.decription);
        $('#datecreate').val(item.dateCreate);
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
                Deleted: check
            }
            $.ajax({
                type: "PUT",
                url: "https://localhost:44310/api/TimeSlot" ,
                data: JSON.stringify(crdata),
                contentType: "application/json",

                success: function (data) {
                    console.log(data);
                    self.click();
                    $('#modal').modal('hide');
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
        if (confirm("ban muon xoa " + item.decription + " khong")) {
            $.ajax({
                type: "PUT",
                url: "https://localhost:44310/api/TimeSlot/delete/" + item.id,
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
    var db = new viewmodal();
    db.click();
    ko.applyBindings(db);
});