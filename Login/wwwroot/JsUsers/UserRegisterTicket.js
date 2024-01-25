var viewmodel = function () {
    var self = this;
    self.arrays = ko.observableArray();

    self.arrayTimeSlot = ko.observableArray();
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
    self.checkTimeSlot = function (item1) {
        self.arrayTimeSlot([]);
        $.ajax({
            type: "GET",
            url: backendUrl + "/api/TimeSlot",            
            contentType: "application/json",
            success: function (data) {
                $.each(data, function (ex, item) {
                    
                    self.arrayTimeSlot.push(item)
                });
             
                if (parseInt(item1) != 0) {
                
                    $('#idTimeMeet').val(parseInt(item1))
                }
            },
            error: function (err) {
                alert(err);
            },
        });
    }
    self.arrayListTest = ko.observableArray();
    self.resultOb = ko.observable();
    self.click = function () {
        var id = $('#Id').val();
        $.ajax({
            type: "GET",
            url: backendUrl + "/api/UserRegisterTicket/" + id,         
            contentType: "application/json",
            success: function (data) {
                self.arrays([]);
               
                $.each(data, function (ex, item) {
                    self.resultOb(item);
                    
                    self.arrays.push(item);
                });       
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
    // benh nhan xac nhan phieu hen
    self.updateApoint = function (item) {
        $('#idApoint').text(item.apointmentTicket1.id);
        $('#timeApoint').text(item.timeMeetApoint);
        $('#departApoint').text(item.departmentApoint);
        $('#dateMeetApoint').text(item.apointmentTicket1.dateMeet);
        $('#descripApoint').text(item.apointmentTicket1.decription);
        $('#doctor1').text(item.doctor1);   
        $('#t_Apoint').modal('show');
        // cap nhat phieu hen thanh 3
        var obj = {
            Id: item.apointmentTicket1.id,
            IdRegisterTicket: item.idRegis,
            IdTimeMeet: item.idTimeMeet,       
            DateMeet: item.apointmentTicket1.dateMeet,
            IdDoctor: item.apointmentTicket1.idDoctor,
            Status: 3,         
        }
        self.saveChange = function () {
            $.ajax({
                type: "PUT",
                url: backendUrl + "/api/ApointmentTickets",
                data: JSON.stringify(obj),
                contentType: "application/json",
                success: function (data) {
                    self.click();
                    $('#modal').modal('hide');
                    self.showtoastState("Bệnh nhân xác nhận thành công");
                }, error: function (err) {
                    self.showtoastError("Lỗi");
                    self.click();
                }
            })        
        }

    }
   
    self.showDetail = function (item) {
        $('#modal').modal('show');
        var x = self.resultOb();
        console.log("ob",x)
       
        $("#symptomList").val(x.symptomRegis)
        $("#department").val(x.departmentApoint)
        $("#doctor").val(x.doctor1)
        $("#timeMeet").val(x.timeMeetRegis)
        $("#dateMeetRg").val(x.dateMeetRegis)
       /* $("#doctorEx").val(x.lastName)*/
       //lấy resultdetail2      
        $.ajax({
            type: "GET",
            url: backendUrl + "/api/Result/" + item.apointmentTicket1.result.id,
            contentType: "application/json",
            success: function (data) {
               
                self.arrayListTest([]);
               
                $("#diagnostic").val(data.diagnostic)
                $("#therapyRegiment").val(data.therapyRegiment)
                $.each(data.resultDetails2, function (ex, test) {
                    self.arrayListTest.push(test);
                   

                });
            }, error: function (err) {
                alert("loi " + err.status + "<!----!>" + err.statusText);
                self.click();
            }
        });
       
    }

   
    self.createRegister = function (item) {
        $('#myModel').modal('show');

        self.checkTimeSlot(0);
        $('#symptom').val("");
        $('#dateMeet').val("");
        $('#idTimeMeet').val("");
        $('#status').val("");
        $('#deleted').val("");      
        self.save = function () {  
            if ($('#symptom').val() == "") {
                self.showtoastError("Không được để trống")
            }
            else {
                var DAdata = {
                    IdPatient: $('#Id').val(),
                    Symptom: $('#symptom').val(),
                    DateMeet: $('#dateMeet').val(),
                    IdTimeMeet: parseInt($('#idTimeMeet').val()),
                    Status: "0",
                }
                $.ajax({
                    type: "POST",
                    url: backendUrl + "/api/RegisterTicket",
                    data: JSON.stringify(DAdata),
                    contentType: "application/json",
                    success: function (data) {
                        /*  console.log(DAdata);*/
                        self.click();
                        $('#myModel').modal('toggle');
                        self.showtoastState("Thêm thành công");
                    }, error: function (err) {
                        self.showtoastError("Lỗi");
                        /* console.log(DAdata);*/
                        self.click();
                    }
                });
            }
            
        }
    }

    self.delete = function (item) {
        if (confirm("ban muon xoa " + item.idRegis + " khong")) {
            $.ajax({
                type: "PUT",
                url: backendUrl + "/api/RegisterTicket/" + item.idRegis,            
                contentType: "application/json",
                success: function (data) {
                    self.click();
                    self.showtoastState("Xóa thành công");
                }, error: function (err) {
                    self.showtoastError("Lỗi");
                   
                    self.click();
                }
            });
        }

    }
    self.update = function (item) {
        $('#myModel').modal('show');
        self.checkTimeSlot(item.idTimeMeet);
        $('#symptom').val(item.symptomRegis);
     
        $('#dateMeet').val(moment(item.dateMeetRegis).format('YYYY-MM-DD'));

     /*   console.log(item)*/
             
        self.save = function () {
            var DAdata = {
                Id: item.idRegis,
                IdPatient: item.idPatient,
                Symptom: $('#symptom').val(),
                DateMeet: $('#dateMeet').val(),
                IdTimeMeet: parseInt($('#idTimeMeet').val()),              
                Status: 0,
            }
            $.ajax({
                type: "PUT",
                url: backendUrl + "/api/RegisterTicket",
                data: JSON.stringify(DAdata),
                contentType: "application/json",
                success: function (data) {
                /*    console.log(DAdata);*/
                    self.click();
                    $('#myModel').modal('toggle');
                    self.showtoastState("Sửa thành công");
                }, error: function (err) {
                    self.showtoastError("Lỗi");
                  /*  console.log(DAdata);*/
                    self.click();
                }
            });
        }
    }


    self.search = function () {
        var id = $('#Id').val();
        $.ajax({
            type: "get",
            url: backendUrl + "/api/UserRegisterTicket/date/" + $('#inputDate').val() + "/" + id,
            contentType: "application/json",
            success: function (data) {
                self.arrays([]);
                $.each(data, function (ex, item1) {
                  /*  console.log(item1)*/
                    self.arrays.push(item1);
                })
                self.arrays([]);
               
            },
            error: function (err) {
                console.log(err);
            }
        });
        
    }
   
    
}
$(function () {
    var ap = new viewmodel();
    ap.click();
    ko.applyBindings(ap);
});
$(document).ready(function () {
    $("#myInput").on("keyup", function () {
        var value = $("#myInput").val().toLowerCase();
        $("#search_table tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});