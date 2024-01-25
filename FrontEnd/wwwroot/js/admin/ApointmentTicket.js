////const { data } = require("jquery");
////const { toJSON } = require("knockout");

var viewmodel = function () {
    var self = this;
    self.arrays = ko.observableArray();
    self.arrayDepartment = ko.observableArray();
    self.arrayTimeSlot = ko.observableArray();
    self.loadTimeSlot = function (item1) {
        self.arrayTimeSlot([]);
        $.ajax({
            type: "get",
            url: "https://backend-btl-duong.mooo.com/api/TimeSlot",
            contentType: "application/json",
            success: function (data) {
                $.each(data, function (ex, item) {
                    self.arrayTimeSlot.push(item);
                });
              
                if (idTimeMeet != null) {
                    $('#idTimeMeet').val(item1.idTimeMeet)
                }

            },
            error: function () { },
        });
    }
    self.loadDepartment = function (item1) {
        self.arrayDepartment([]);
        $.ajax({
            type: "get",
            url: "https://backend-btl-duong.mooo.com/api/Department",
            contentType: "application/json",
            success: function (data) {
                $.each(data, function (ex, item) {
                    self.arrayDepartment.push(item);
                });
                if (idDepartment != null) {
                    $('#idDepartment').val(item1.idDepartment)
                }
                
            },
            error: function () {},
        });
    }
    self.click = function () {
      
        self.arrays([]);
        $.ajax({
            type: "get",
            url: "https://backend-btl-duong.mooo.com/api/ApointmentTickets",
            contentType: "application/json",
            success: function (data) {
                
                $.each(data, function (ex, item) {
                    item.nameDepartment = "";
                    $.ajax({
                        type: "get",
                        url: "https://backend-btl-duong.mooo.com/api/Department/" + item.idDepartment,
                        contentType: "application/json",
                        success: function (data1) {
                            item.nameDepartment = data1.name;
                            $.ajax({
                                type: "get",
                                url: "https://backend-btl-duong.mooo.com/api/TimeSlot/" + item.idTimeMeet,
                                contentType: "application/json",
                                success: function (data2) {
                                    item.decriptionTimeMet = data2.decription;
                                    
                                    self.arrays.push(item);
                                },
                                error: function (err) {
                                    console.log("errr");
                                }
                            });

                        },
                        error: function (err) {
                            console.log("errr" );
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
        $('#modal').modal('show')
        self.loadDepartment(null);
        self.loadTimeSlot(null);
        $('#idDepartment').val(1)
        $('#idRegisterTicket').val("")
        $('#dateMeet').val("")
        $('#status').val("")
        $('#dateCreate').val("")
        $('#decription').val("")



      
        self.save = function () {
            var check = true;
            if ($('#no').attr('checked', true)) {
                check = false;
            } 
            var crdata = {
                
                IdTimeMeet: parseInt($('#idTimeMeet').val()),
                IdDepartment: parseInt($('#idDepartment').val()),
                IdRegisterTicket: parseInt( $('#idRegisterTicket').val()),
                DateMeet: $('#dateMeet').val(),

                DateCreate: $('#dateMeet').val(),
                Status: $('#status').val(),

                Decription: $('#decription').val(),
                Deleted: check
            }
            $.ajax({
                type: "post",
                url: "/api/ApointmentTickets",
                data: JSON.stringify(crdata),
                contentType: "application/json",
      
                success: function (data) {
                    console.log(data);
                    self.click();
                }, error: function (err) {
                    alert("loi " + err.status + "<!----!>" + err.statusText);
                    console.log(JSON.stringify(crdata));
                    self.click();
                }
            });
        }


    }
    self.Update = function (item) {
        self.loadDepartment(item);
        self.loadTimeSlot(item);
        $('#modal').modal('show')
        $('#id').val(item.id)
        $('#idTimeMeet').val(item.idTimeMeet)
        $('#idRegisterTicket').val(item.idRegisterTicket)
        $('#dateMeet').val(item.dateMeet)
        $('#status').val(item.status)
        $('#decription').val(item.decription)
        if (!item.deleted) {
            $('#no').attr('checked', true);
        }else $('#yes').attr('checked', true);
        self.save = function () {
            var check = true;
            if ($('#no').prop('checked')) {
                check = false;
            }
            var id1 = item.id;
            var id2 = item.idTimeMeet;
            var id3 = item.idDepartment;
            var id4 = item.idRegisterTicket;
            var crdata = {
                Id: id1,
                IdTimeMeet: parseInt($('#idTimeMeet').val()),
                IdDepartment: parseInt($('#idDepartment').val()),
                IdRegisterTicket: id4,
                DateMeet: $('#dateMeet').val(),
                Status: $('#status').val(),
                DateCreate: $('#dateCreate').val(),
                Decription: $('#decription').val(),
                Deleted: check
            }
            console.log("item ", crdata);
            $.ajax({
                type: "PUT",
                url: "/api/ApointmentTickets",
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
    self.delete = function (item) {
        if (confirm("ban muon xoa" + item.id + "khong")) {
            $.ajax({
                type: "PUT",
                url: "/api/ApointmentTickets/" + item.id,
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