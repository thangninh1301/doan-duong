var viewmodel = function () {
    var self = this;
    self.arrays = ko.observableArray();
    self.arrayDepartment = ko.observableArray();
    self.arrayTimeSlot = ko.observableArray();
    self.checkTimeSlot = function (item1) {
        self.arrayTimeSlot([]);
        $.ajax({
            type: "GET",
            url: "https://localhost:44310/api/TimeSlot",
            contentType: "application/json",
            success: function (data) {
                $.each(data, function (ex, item) {
                    self.arrayTimeSlot.push(item)
                });
                console.log(item1.idTimeMeet)
                if (idTimeMeet != null) {
                    $('#idTimeMeet').val(item1.idTimeMeet)
                }
            },
            error: function (err) {
                alert(err);
            },
        });
    }
    self.arrayDoctor = ko.observableArray();
    self.loadDoctor = function (idDepart,idDoctor) {
        self.arrayDoctor([]);
        $.ajax({
            type: "get",
            url: "https://localhost:44310/api/Admin/Doctor/" + idDepart ,
            contentType: "application/json",
            success: function (data) {
                self.arrayDoctor([]);
                $.each(data, function (ex, item) {
                    self.arrayDoctor.push(item);
                });
                $('#idDoctor').val(idDoctor);

            },
            error: function () {
            },
        });
    }
    self.loadDepartment = function (item1) {
        self.arrayDepartment([]);
        $.ajax({
            type: "get",
            url: "https://localhost:44310/api/Department",
            contentType: "application/json",
            success: function (data) {
                self.arrayDepartment([]);
                $.each(data, function (ex, item) {
                    
                    self.arrayDepartment.push(item);
                });
                if (item1 != null) {
                    $('#idDepartment').val(item1.idDepartment);
                    
                }

            },
            error: function () { },
        });
    }
   
   
    self.loadApoinMent_ticket = function (regis) {
        $.ajax({
            type: "GET",
            url: "https://localhost:44310/api/ApointmentTickets/" + regis.idApointment,
            contentType: "application/json",
            success: function (data) {
                self.checkTimeSlot(data);
                self.loadDepartment(data);
                self.loadDoctor(data.idDepartment, data.idDoctor);
                $('#idApointment').val(data.id);
                $('#dateMeet').val(data.dateMeet)
                $('#decription').val(data.decription)
                if (data.status == 1)
                    $('#status').attr('checked', true);
                else $('#status').attr('checked', false);


               
            },
            error: function () {                
                $('#dateMeet').val(regis.dateMeet);
                $('#decription').val("")
                self.checkTimeSlot(regis);
                self.loadDepartment(null);
                $('#status').attr('checked', true);
            },
        });
    }
    
    self.click = function () {
        self.arrays([]);
        $.ajax({
            type: "GET",
            url: "https://localhost:44310/api/Admin",
            contentType: "application/json",
            success: function (data) {
                $.each(data, function (ex, item) {
                    self.arrays.push(item);
                })
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
    self.search = function () {
        
        $.ajax({
            type: "get",
            url: "https://localhost:44310/api/Admin/Date/" + $('#inputDate').val(),
            contentType: "application/json",
            
            success: function (data) {
                self.arrays([]);
                $.each(data, function (ex, item) {
                    self.arrays.push(item);
                })
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
    self.insert = function (item) {
        self.loadApoinMent_ticket(item);
        $('#modal').modal('show');
        $('#idDepartment').change(function () {
            self.loadDoctor($('#idDepartment').val(),0);
        })
        self.save = function () {
            var check = 0;
            if ($('#status').attr('checked', true)) {
                check = 1;
            }
            var ob = {
                    Id: item.idApointment,
                    IdTimeMeet: parseInt($('#idTimeMeet').val()),
                    IdDepartment: parseInt($('#idDepartment').val()),
                    IdRegisterTicket: item.idRegis,
                    DateMeet: ($('#dateMeet').val()),
                    IdDoctor: $('#idDoctor').val(),
                    Status: check,
                    Decription: $('#decription').val(),
                    Deleted : false,
            }
            $.ajax({
                type: "POST",
                url: "https://localhost:44310/api/Admin/Add",
                data: JSON.stringify(ob),
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
    var abc = new viewmodel();
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