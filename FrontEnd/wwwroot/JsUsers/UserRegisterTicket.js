var viewmodel = function () {
    var self = this;
    self.arrays = ko.observableArray();

    self.arrayTimeSlot = ko.observableArray();
    self.checkTimeSlot = function () {
        self.arrayTimeSlot([]);
        $.ajax({
            type: "GET",
            url: "https://localhost:44367/api/TimeSlot",
            contentType: "application/json",
            success: function (data) {
                $.each(data, function (ex, item) {
                    self.arrayTimeSlot.push(item)
                });             
            },
            error: function (err) {
                alert(err);
            },
        });
    }

    self.click = function () {
        var id = $('#Id').val();
        $.ajax({
            type: "GET",
            url: "https://localhost:44367/api/UserRegisterTicket/" + id,
            contentType: "application/json",
            success: function (data) {
                self.arrays([]);
                $.each(data, function (ex, item) {
                    self.arrays.push(item);
                });
         
            },
            error: function (err) {
                console.log(err);
            }
        });
    }

    self.showDetail = function (item) {    
        $('#timeMeetApoint').text(item.apointmentTicket1.idTimeMeet);
        $('#departmentApoint').text(item.departmentApoint);
        $('#datemeetApoint').text(item.apointmentTicket1.dateMeet);   
        $('#description').text(item.apointmentTicket1.decription);
        $('#Doctor1').text(item.doctor1);
        $('#statusApoint').text(item.statusApoint);

        $('#diagnostic').text(item.diagnostic);
        $('#therapyRegiment').text(item.therapyRegiment);
        $('#dateCreate').text(item.dateCreate);

        $('#modal').modal('show');
    }

    self.createRegister = function (item) {
        $('#myModel').modal('show');
        self.checkTimeSlot(null);
        $('#symptom').val("");
        $('#dateMeet').val("");
        $('#idTimeMeet').val("");
        $('#status').val("");
        $('#deleted').val("");      
        self.save = function () {
           
            var DAdata = {
                IdPatient: $('#Id').val(),
                Symptom: $('#symptom').val(),
                DateMeet: $('#dateMeet').val(),
                IdTimeMeet: parseInt($('#idTimeMeet').val()),
                Status: "0",
            }
            $.ajax({
                type: "POST",
                url: "https://localhost:44367/api/RegisterTicket",
                data: JSON.stringify(DAdata),
                contentType: "application/json",
                success: function (data) {
                    console.log(DAdata);
                    self.click();
                    $('#myModel').modal('toggle');

                }, error: function (err) {
                    alert("loi " + err.status + "<!----!>" + err.statusText);
                    console.log(DAdata);
                    self.click();
                }
            });
        }
    }

    self.delete = function (item) {
        if (confirm("ban muon xoa " + item.idRegis + " khong")) {
            $.ajax({
                type: "PUT",
                url: "https://localhost:44367/api/RegisterTicket/" + item.idRegis,
                contentType: "application/json",
                success: function (data) {
                    self.click();
                }, error: function (err) {
                    alert("loi " + err.status + "<!----!>" + err.statusText);
                   
                    self.click();
                }
            });
        }

    }
    
    
}
$(function () {
    var ap = new viewmodel();
    ap.click();
    ko.applyBindings(ap);
});
$(document).ready(function () {
    $("#search1").on("keyup", function () {
        var value = $("#search1").val().toLowerCase();
        $("#search_table tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});