////const { data } = require("jquery");
////const { toJSON } = require("knockout");

var viewmodel = function () {
    var self = this;
    self.arrays = ko.observableArray();
    self.arrayDepartment = ko.observableArray();
    self.loadDepartment = function (item1) {
        self.arrayDepartment([]);
        $.ajax({
            type: "get",
            url: backendUrl + "/api/Department",
            //url: "https://localhost:44310/api/Department",
            contentType: "application/json",
            success: function (data) {
                self.arrayDepartment([]);
                $.each(data, function (ex, item) {
                    self.arrayDepartment.push(item);
                });
                if (item1 != null) {
                    $('#idDepartment').val(item1.idDepartment)
                }

            },
            error: function () { },
        });
    }
    self.loadApoinMent_ticket = function (regis) {
        $.ajax({
            type: "GET",
            url: backendUrl + "/api/ApointmentTickets",
           // url: "https://localhost:44310/api/ApointmentTickets",
            contentType: "application/json",
            success: function (data) {
                $('#dateMeet').val(regis.dateMeet)
                $('#status').val("")   
                $('#decription').val("")
                self.loadDepartment(null);
                $.each(data, function (ex, item) {
                    if (regis.id == item.idRegisterTicket) {
                        $('#idApointment').val(item.id)
                        $('#dateMeet').val(item.dateMeet)
                        $('#status').val(item.status)
                        $('#decription').val(item.decription)
                        if (!item.deleted) {
                            $('#no').attr('checked', true);
                        } else $('#yes').attr('checked', true);
            
                        self.loadDepartment(item);
                   
                    } 

                    
                })
            },
            error: function () {
                console.log("abc", regis);
            },
        });
    }
    self.checkTimeSlot = function (item1) {
        self.arrayTimeSlot([]);
        $.ajax({
            type: "GET",
            url: backendUrl + "/api/TimeSlot",
           // url: "https://localhost:44310/api/TimeSlot",
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
    self.click = function () {
        self.arrays([]);
        //load Regis
        $.ajax({
            type: "GET",
            url: backendUrl + "/api/RegisterTicket",
           // url: "https://localhost:44310/api/RegisterTicket",
            contentType: "application/json",
            success: function (data) {

                $.each(data, function (ex, item) {
                    item.nameTimeMeet = "";
                    $.ajax({
                        type: "GET",
                        url: backendUrl + "/api/TimeSlot/" + item.idTimeMeet,
                       // url: "https://localhost:44310/api/TimeSlot/" + item.idTimeMeet,
                        contentType: "application/json",
                        success: function (data1) {
                            item.nameTimeMeet = data1.decription;
                            console.log("Thoi gian", item.nameTimeMeet);
                            self.arrays.push(item);
                        },
                        error: function (err) {
                            console.log(err);
                        }
                    });                   
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
        self.save = function () {
            var check = true;
            if ($('#no').attr('checked', true)) {
                check = false;
            }
            var crdata = {
                IdTimeMeet: item.idTimeMeet,
                IdDepartment: parseInt($('#idDepartment').val()),
                IdRegisterTicket: item.id,
                DateMeet: item.dateMeet,
                Status: parseInt($('#status').val()),
                Decription: $('#decription').val(),
                Deleted: check
            }
            var DAdata = {
                Id: item.id,
                IdPatient: item.idPatient,
                Symptom: item.symptom,
                DateMeet: item.dateMeet,
                IdTimeMeet: item.idTimeMeet,
                DateCreate: item.dateCreate,
                Status: 1,
                Deleted: item.deleted
            }
            if (item.status == 0) {
                $.ajax({
                    type: "post",
                    url: backendUrl + "/api/ApointmentTickets",
                    //url: "https://localhost:44310/api/ApointmentTickets",
                    data: JSON.stringify(crdata),
                    contentType: "application/json",
                    success: function (data) {
                        console.log(data);      
                        $.ajax({
                            type: "PUT",
                            url: backendUrl + "/api/RegisterTicket",
                           // url: "https://localhost:44310/api/RegisterTicket",
                            data: JSON.stringify(DAdata),
                            contentType: "application/json",
                            success: function (data) { console.log(data) },
                            error: function (err) {
                                console.log(DAdata);
                                console.log(err);
                            },
                        });
                        self.click();
                    }, error: function (err) {
                        alert("loi " + err.status + "<!----!>" + err.statusText);
                        console.log(JSON.stringify(crdata));
                        self.click();
                    }
                });

            }
            else {
                crdata.Id = parseInt($('#idApointment').val());
                $.ajax({
                    type: "put",
                    url: backendUrl + "/api/ApointmentTickets",
                    //url: "https://localhost:44310/api/ApointmentTickets",
                    data: JSON.stringify(crdata),
                    contentType: "application/json",

                    success: function (data) {
                        
                        self.click();
                    }, error: function (err) {
                        alert("loi " + err.status + "<!----!>" + err.statusText);
                        console.log(JSON.stringify(crdata));
                        self.click();
                    }
                });
            }

            
        }


    }


    
}

$(function () {
    var abc = new viewmodel();
    abc.click();
    ko.applyBindings(abc);
});