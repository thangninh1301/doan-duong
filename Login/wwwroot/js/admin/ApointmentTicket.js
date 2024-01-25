

var viewmodel = function () {
    var self = this;
    self.arrays = ko.observableArray();
    self.arrayDepartment = ko.observableArray();
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
    self.loadTimeSlot = function (item1) {
        self.arrayTimeSlot([]);
        $.ajax({
            type: "get",
            url: backendUrl + "/api/TimeSlot",
           // url: "https://localhost:44310/api/TimeSlot",
            contentType: "application/json",
            success: function (data) {
                $.each(data, function (ex, item) {
                    self.arrayTimeSlot.push(item);
                });
              
                if (parseInt(item1) != 0) {

                    $('#idTimeMeet').val(parseInt(item1))
                }

            },
            error: function () { },
        });
    }
    self.loadDepartment = function (idDepartment) {
        self.arrayDepartment([]);
        $.ajax({
            type: "get",
            url: backendUrl + "/api/Department",
           // url: "https://localhost:44310/api/Department",
            contentType: "application/json",
            success: function (data) {
                $.each(data, function (ex, item) {
                    self.arrayDepartment.push(item);
                });
                if (idDepartment != null) {

                    $('#idDepartment').val(idDepartment)
                }
              
                
            },
            error: function () {},
        });
    }
    self.arrayDoctor = ko.observableArray();
    self.loadDoctor = function (idDepart, idDoctor) {
        self.arrayDoctor([]);
        $.ajax({
            type: "get",
            url: backendUrl + "/api/Admin/Doctor/" + idDepart,
            // url: "https://localhost:44310/api/Admin/Doctor/" + idDepart ,
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
    self.click = function () {     
        self.arrays([]);
        $.ajax({
            type: "get",
            url: backendUrl + "/api/ApointmentTickets/Admin",
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
           
        }


    }
    self.Update = function (item) {
        $('#modal').modal('show')
        self.loadDepartment(item.idDepartment);
        self.loadTimeSlot(item.idTimeMeet);
      
        self.loadDoctor(item.idDepartment,item.idDoctor);
        $('#idDepartment').change(function () {
            self.loadDoctor($('#idDepartment').val(), 0);
        })
        $('#id').val(item.id)
      /*  $('#idDepartment').val(item.nameDepartment)*/
       /* $('#idRegisterTicket').val(item.idRegisterTicket)*/
        $('#dateMeet').val(moment(item.dateMeet).format('YYYY-MM-DD'));
      /*  $('#status').val(item.status)*/
        $('#decription').val(item.decription)

        if (!item.deleted) {
            $('#no').attr('checked', true);
        } else $('#yes').attr('checked', true);

        self.save = function () {
            var check = true;
            if ($('#no').prop('checked')) {
                check = false;
            }
            var id1 = item.id;
          /*  var id2 = item.idTimeMeet;
            var id3 = item.idDepartment;*/
            var id4 = item.idRegisterTicket;
            var crdata = {
                Id: id1,
                IdTimeMeet: parseInt($('#idTimeMeet').val()),
                IdDepartment: parseInt($('#idDepartment').val()),
                IdRegisterTicket: id4,
                IdDoctor: $('#idDoctor').val(),
                DateMeet: $('#dateMeet').val(),
                Status: 1,
                DateCreate: $('#dateCreate').val(),
                Decription: $('#decription').val(),
                Deleted: check
            }
            console.log("item ", crdata);
            $.ajax({
                type: "PUT",
                url: backendUrl + "/api/ApointmentTickets",
                //url: "https://localhost:44310/api/ApointmentTickets",
                contentType: "application/json",
                data: JSON.stringify(crdata),
                success: function (data) {

                    self.click();
                    $('#modal').modal('hide')
                    self.showtoastState("Sửa thành công");
                }, error: function (err) {
                    self.showtoastError("Lỗi");
                    self.click();
                }
            });
        }
       
        
    }
    self.delete = function (item) {
        if (confirm("ban muon xoa" + item.id + "khong")) {
            $.ajax({
                type: "PUT",
                url: backendUrl + "/api/ApointmentTickets" + item.id,
               // url: "https://localhost:44310/api/ApointmentTickets/" + item.id,
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
}

$(function () {
    var abc = new viewmodel();
    abc.click();
    
    ko.applyBindings(abc);
});
$(document).ready(function () {
    $("#myInput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#datatablesSimple tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});