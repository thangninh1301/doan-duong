var viewmodel = function () {
    var self = this;
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
    self.convertToKoObject = function (data) {
        var newObj = ko.mapping.fromJS(data);
        newObj.Selected = ko.observable(false);
        return newObj;
    }
    self.convertToJson = function (item) {
        if (item == null || item == "") {
            return [];
        } else {
            return JSON.parse(item);
        }
    };
    self.doctor = ko.observable(
        self.convertToKoObject({
            UserName: '',
            Id: 0
        })
    );
    self.arrays = ko.observableArray();
    self.arrayDepartment = ko.observableArray();
    self.arrayTimeSlot = ko.observableArray();
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
               
                if (item1 != null) {
                    $('#idTimeMeet').val(item1.idTimeMeet);
                }
            },
            error: function (err) {
                alert(err);
            },
        });
    }
    self.loadDoctor = function (idDepart, regis) {
        $.ajax({
            type: "get",
            url: backendUrl + "/api/Admin/DoctorHaveApointmin/" + regis.dateMeet + "/" + regis.idTimeMeet + "/" + idDepart,
            contentType: "application/json",
            success: function (data) {
               
                self.doctor(self.convertToKoObject({
                    UserName: data==null?"": "Bs: " + data.lastName,
                    Id: data == null ? 0 : data.id
                }))

            },
            error: function () {
            },
        });
    }
   
    self.loadDepartment = function (input, regis) {
        self.arrayDepartment([]);
        $.ajax({
            type: "get",
            url: backendUrl + "/api/Department",
            contentType: "application/json",
            success: function (data) {
               
                self.arrayDepartment([]);
                $.each(data, function (ex, item) {                    
                    self.arrayDepartment.push(item);
                });
                if (input != null) {
                    $('#idDepartment').val(input.idDepartment);
                      self.loadDoctor($('#idDepartment').val(), regis);                 
                } 

            },
            error: function () { },
        });
    }
   
    self.loadApoinMent_ticket = function (regis) {
      
        if (regis.idApointment != 0) {
            $.ajax({
                type: "GET",
                url: backendUrl + "/api/ApointmentTickets/" + regis.idApointment,

                contentType: "application/json",

                success: function (data) {
                    
                    //lấy tt bác sỹ theo id để lấy dc id phòng ban
                    $.ajax({
                        type: "get",
                        url: backendUrl + "/api/User/" + data.idDoctor,
                        contentType: "application/json",
                        success: function (datauser) {
                            self.doctor(self.convertToKoObject({
                                UserName: datauser.userName,
                                IdUser: datauser.id
                            })) 
                            self.loadDepartment(datauser,regis);
                        },
                        error: function () { }

                    })   
                    self.checkTimeSlot(data);
                    
                    $('#idApointment').val(data.id);
                    $('#decription').val(data.decription)
                    if (data.status == 1)
                        $('#status').attr('checked', true);
                    else $('#status').attr('checked', false);



                },
                error: function () {
                    
                    $('#dateMeet').val(regis.dateMeet);
                    $('#decription').val("")
                   /* self.checkTimeSlot(regis);*/
                    self.loadDepartment(null, regis);
                    $('#status').attr('checked', false);
                },
            });
        }
        else {
            self.doctor(self.convertToKoObject({
                UserName: '',
                IdUser: 0
            })) 
            $('#decription').val("")
            self.checkTimeSlot(regis);
            self.loadDepartment(null, regis);
            $('#status').attr('checked', false);
        }
    }
    
    self.click = function () {
        self.arrays([]);
        $.ajax({
            type: "GET",
            url: backendUrl + "/api/Admin",
         
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
            url: backendUrl + "/api/Admin/Date/" + $('#inputDate').val(),
           // url: "https://localhost:44310/api/Admin/Date/" + $('#inputDate').val(),
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
        $('#modal').modal('show');
        self.click();
        
        $('#dateMeet').val(moment(item.dateMeet).format('YYYY-MM-DD'));
    
        $('#idTimeMeet').val(item.idTimeMeet);
        if (item.status!=0) {
            $('#status').attr('checked', true);
        }
                    else $('#status').attr('checked', false);
        
      
        self.loadApoinMent_ticket(item);
        self.permissionChanged = function (e) {
            if ($('#idDepartment').val()) {
                self.loadDoctor($('#idDepartment').val(), {
                    dateMeet: $('#dateMeet').val(),
                    idTimeMeet: item.idTimeMeet,
                });
            }
        }
        /*$('#idDepartment').change(function () {
            if ($('#idDepartment').val()) {
                self.loadDoctor($('#idDepartment').val(), {
                    dateMeet: $('#dateMeet').val(),
                    idTimeMeet: item.idTimeMeet,
                });
            }
        })*/
        self.save = function () {

           

            if ($('#idDoctor').val() == "") {             
                // cập nhật status phiếu đăng kí thành 2
                var DAdata = {
                    Id: item.idRegis,
                    IdPatient: $('#Id').val(),
                    Symptom: $('#symptom').val(),
                    DateMeet: $('#dateMeet').val(),
                    IdTimeMeet: parseInt($('#idTimeMeet').val()),
                    Status: 2,
                }
                $.ajax({
                    type: "PUT",
                    url: backendUrl + "/api/RegisterTicket",
                    data: JSON.stringify(DAdata),
                    contentType: "application/json",
                    success: function (data) {
                        self.click();
                        $('#modal').modal('hide');
                        self.showtoastState("Sửa thành công");

                    }, error: function (err) {
                        self.showtoastError("Lỗi");
                        console.log(DAdata);
                        self.click();
                    }
                });
            }
            else
            {
                var check = 0;
                if ($('#status').prop('checked')) {
                    check = 1;
                }
                var ob = {
                    Id: item.idApointment,
                    IdTimeMeet: parseInt($('#idTimeMeet').val()),
                    IdRegisterTicket: item.idRegis,
                    DateMeet: ($('#dateMeet').val()),
                    IdDoctor: self.doctor().Id(),
                    Status: check,
                    Decription: $('#decription').val(),
                    Deleted: false,
                }
                $.ajax({
                    type: "POST",
                    url: backendUrl + "/api/Admin/Add",

                    data: JSON.stringify(ob),
                    contentType: "application/json",
                    success: function (data) {
                        self.click();
                        $('#modal').modal('hide');
                        self.showtoastState("Thêm thành công");
                    }, error: function (err) {
                        self.showtoastError("Lỗi");
                        self.click();
                    }
                }) 
            }             
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