var viewmodel = function () {
    var self = this;
    self.arrays = ko.observableArray();
    self.UserArrays = ko.observableArray();
    self.resultArrays = ko.observableArray();

    self.click = function () {
        var id = $('#Id').val();
        $.ajax({
            type: "GET",
            url: "https://backend-btl-duong.mooo.com/api/UserApointmentTicket/"+id,
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                self.arrays([]);
                $.each(data, function (ex, item) {
                    self.arrays.push(item);
                });
            },
            error: function (error) {
                alert("error");
            }
        });
    }

   /* self.click = function () {
        $.ajax({
            type: "get",
            url: "/api/UserId",
            contentType: "application/json",
            success: function (data1) {
                self.UserArrays([]);
                $.each(data1, function (ex, item1) {
                    self.UserArrays.push(item1);
                });

            },
            error: function (err) {
                alert(err);
            }

        });
    }*/

    self.createRegister = function (item) {
        $("#model").modal('show');
      
       /* $('#idPatient').val("");*/
        $('#symptom').val("");
        $('#dateMeet').val("");
        $('#idTimeMeet').val("");
        $('#dateCreate').val(item.dateCreate);
        $('#status').val(item.status);
        $('#deleted').val("");

        self.save = function () {
            var check = true;
            if ($('#no').attr('checked', true)) {
                check = false;
            }
            var DAdata = {
               /* IdPatient: $('#idPatient').val(),*/
                Symptom: $('#symptom').val(),
                DateMeet: $('#dateMeet').val(),
                IdTimeMeet: parseInt($('#idTimeMeet').val()),
                DateCreate: item.dateCreate,
                Status: item.status,
                Deleted: check
            }
            $.ajax({
                type: "POST",
                url: "https://backend-btl-duong.mooo.com/api/UserRegisterTicket",
                data: ko.toJSON(DAdata),
                contentType: "application/json",
                success: function (data) {
                    console.log(DAdata);
                    self.click();
                }, error: function (err) {
                    alert("loi " + err.status + "<!----!>" + err.statusText);
                    console.log(DAdata);
                    self.click();
                }
            });
        }


    }

    self.userResult = function () {
      
        self.resultArrays([]);
        $.ajax({
            type: "GET",
            url: "https://backend-btl-duong.mooo.com/api/UserResult",
            contentType: "application/json",
            success: function (data) {
                self.resultArrays([]);
                $.each(data, function (ex, item) {
                    self.resultArrays.push(item);
                })
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
}


$(function () {
    var ap = new viewmodel();
    ko.applyBindings(ap);
});
