var viewmodel = function () {
    var self = this;
    self.arrays = ko.observableArray();
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
   
    self.click = function () {
        var doctorId = $('#doctorId').val();
        $.ajax({
            type: "GET",
            url: backendUrl + "/api/Doctor/" + doctorId,
          
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
    self.arrayListTest = ko.observableArray();
   
    self.createResult = function (itemResult) {
        
        self.arrayListTest([])
        self.Load_Test();
        $('#patient').text(itemResult.patient);
        $('#createResult').modal('show');

        $('#idApointmentTicket').val("");
        $('#dateCreate').val("");
        $('#diagnostic').val("");
        $('#therapyRegiment').val("");
      
        self.deleteTest = function (itemTest) {
          
            if (itemTest.result != null) {
                var idRs = itemResult.result.id
                $.ajax({
                    type: "DELETE",
                    url: backendUrl + "/api/DoctorTest/" + idRs + "/" + itemTest.idDoctorTest,
                    contentType: "application/json",
                    success: function (data) {
                        self.click();
                        self.showtoastState("Xóa thành công!")
                    }, error: function (err) {
                        alert("loi " + err.status + "<!----!>" + err.statusText);
                        self.showtoastError("Lỗi!")
                        self.click();
                    }
                });
            } else {
                self.arrayListTest.remove(itemTest);
                self.showtoastState("Xóa thành công!")
            }
            
           
        }
        self.addTest = function () {
           
            var obtest = {
                idResult: 0,
             
                doctorTest: {
                    id: ($('#idDoctorTest').val()),
                    lastName: "",
                    test: {
                        id: parseInt($('#idTest').val()),
                        name: ""
                    }
                }
             
            }
            $.each(self.arrayDoctorTest(), function (ex, item) {
                if (item.id == obtest.doctorTest.id) {
                    obtest.doctorTest.lastName = item.lastName
                }
            })
            $.each(self.arrayTest(), function (ex, item) {
                if (item.id == obtest.doctorTest.test.id) {
                    obtest.doctorTest.test.name = item.name
                }
            })
          
            if (self.arrayListTest().every(x => { return x.doctorTest.id != obtest.doctorTest.id })) {
                console.log($('#idDoctorTest').val())
                if ($('#idDoctorTest').val() != "") {
                    self.arrayListTest.push(obtest);
                    self.showtoastState("Thêm test thành công!")
                } else {
                    self.showtoastError("Hãy chọn Bác Sỹ")
                }
            } else {
                self.showtoastError("Đã tồn tại phiếu test!")
            }
          
           
        };
        
      
        self.save = function () {
            
            var check = 0;
            if ($('#status').prop('checked')) {
                check = 1;
            }
           
            var arrayListTest = [];
            $.each(self.arrayListTest(), function (i, item) {
                console.log("a", item)
                var obtest = {
                    IdResult:0,
                    IdDoctorTest: item.doctorTest.id,
                    DateCreate: item.dateCreate == null ?"" : item.dateCreate,
                    DateUpdate: item.dateUpdate == null ? "" : item.dateUpdate,
                    UrlFile: item.urlFile == null ? "" : item.diagnostic,
                    Diagnostic: item.diagnostic == null ? "" : item.diagnostic
                }
                arrayListTest.push(obtest)
            })
            
            var DAdata = {
                Id: itemResult.result == null ? 0 : itemResult.result.id,
                IdApointmentTicket: itemResult.idApoint,
                DateCreate: itemResult.dateCreateResult,
                Diagnostic: $('#diagnostic').val(),
                TherapyRegiment: $('#therapyRegiment').val(),
                Deleted: false,
                Status: check,
                ResultDetails2: arrayListTest
            }
            console.log("Data", DAdata);
            $.ajax({
                type: "POST",
                url: backendUrl + "/api/Doctor/add",              
                data: JSON.stringify(DAdata),
                contentType: "application/json",
                success: function (data) {

                    console.log(DAdata);
                    self.click();
                    $('#createResult').modal('hide');
                    self.showtoastState("Tạo kết quả thành công");

                }, error: function (err) {
                    self.showtoastError("Lỗi");
                    console.log(DAdata);
                    self.click();
                }
            });
        }
    }
    self.resultOb = ko.observable();
    self.listResult = function (itemResult) {
        self.arrayListTest([])
       
       /// api bảng result lấy id result
        $.ajax({
            type: "GET",
            url: backendUrl + "/api/Result/" + itemResult.result.id,

            contentType: "application/json",
            success: function (data) {              
                self.arrayListTest([]);
                self.resultOb(data);
                $("#diagnostic").val(data.diagnostic)
                $("#therapyRegiment").val(data.therapyRegiment)
                if (data.status == 1) $("#status").attr('checked', true)
                
                $.each(data.resultDetails2, function (i, arrTest) {                                   
                    self.arrayListTest.push(arrTest);
                });
                
            }, error: function (err) {
                alert("loi " + err.status + "<!----!>" + err.statusText);
                self.click();
            }
        });
        self.Load_Test();
        self.addTest = function () {
           
            var obtest = {
                doctorTest:{
                    id:  ($('#idDoctorTest').val()),
                    lastName:"",
                    test: {
                        id: parseInt($('#idTest').val()),
                        name:""
                    }
                }             
            }
            $.each(self.arrayDoctorTest(), function (ex, item) {
                if (item.id == obtest.doctorTest.id) {
                    obtest.doctorTest.lastName = item.lastName
                }
            })
            $.each(self.arrayTest(), function (ex, item) {
                if (item.id == obtest.doctorTest.test.id) {
                    obtest.doctorTest.test.name = item.name
                }
            })
            if (self.arrayListTest().every(x => { return x.doctorTest.id != obtest.doctorTest.id })) {
                console.log($('#idDoctorTest').val())
                if ($('#idDoctorTest').val() != "") {
                    self.arrayListTest.push(obtest);
                    self.showtoastState("Thêm test thành công!")
                } else {
                    self.showtoastError("Hãy chọn Bác Sỹ")
                }
            } else {
                self.showtoastError("Đã tồn tại phiếu test!")
            }
            self.Load_Test();
        };
        self.deleteTest = function (itemTest) {

            var idRs = self.resultOb().id
            console.log("aa",itemResult);
            if (confirm("Bạn có muốn xóa phiếu test " + itemTest.doctorTest.test.name + " không?")) {
                if (itemTest.result != null) {
                    $.ajax({
                        type: "DELETE",
                        url: backendUrl + "/api/DoctorTest/" + idRs + "/" + itemTest.doctorTest.id,
                        contentType: "application/json",
                        success: function (data) {
                            $.ajax({
                                type: "GET",
                                url: backendUrl + "/api/Result/" + itemResult.result.id,

                                contentType: "application/json",
                                success: function (data) {
                                    self.arrayListTest([]);
                                    self.resultOb(data);

                                    $.each(data.resultDetails2, function (ex, item) {

                                        self.arrayListTest.push(item);
                                    });
                                    self.showtoastState("Xóa thành công!")
                                }, error: function (err) {
                                    alert("loi " + err.status + "<!----!>" + err.statusText);

                                    self.click();
                                    self.showtoastError("Lỗi!")
                                }
                            });

                        }, error: function (err) {
                            alert("loi " + err.status + "<!----!>" + err.statusText);

                            self.click();
                            self.showtoastError("Lỗi!")
                        }
                    });
                } else {
                    self.arrayListTest.remove(itemTest);
                    self.showtoastState("Xóa thành công!")
                }
            }
                
           


        }
        $('#createResult').modal('show');
       
        self.save = function () {
            var check = 0;
            if ($('#status').prop('checked')) {
                check = 1;
            }

            var arrayListTest = [];
            $.each(self.arrayListTest(), function (i, item) {
              
                var obtest = {
                    IdResult: 0,
                    IdDoctorTest: item.doctorTest.id,
                    DateCreate: item.dateCreate == null ? "" : item.dateCreate,
                    DateUpdate:"",
                    UrlFile: "",
                    Diagnostic: null
                }
                arrayListTest.push(obtest)
            })
           
            var DAdata = {
                Id: itemResult.result.id,
                IdApointmentTicket: itemResult.idApoint,
                DateCreate: itemResult.dateCreateResult,
                Diagnostic: $('#diagnostic').val(),
                TherapyRegiment: $('#therapyRegiment').val(),
                Deleted: false,
                Status: check,
                ResultDetails2: arrayListTest
            }
            $.ajax({
                type: "POST",
                url: backendUrl + "/api/Doctor/add",
                data: JSON.stringify(DAdata),
                contentType: "application/json",
                success: function (data) {
                    console.log("ad")
                    self.click()
                    $('#createResult').modal('hide');
                    self.showtoastState("Update kết quả thành công");

                }
            })
        }
       
       
    }

    self.arrayTest = ko.observableArray();
    self.Load_Test = function () {
       
        self.arrayTest([]);
        self.arrayDoctorTest([])
        $.ajax({
            type: "GET",
            url: backendUrl + "/api/Test/Admin",
            contentType: "application/json",
            success: function (data) {
                $.each(data, function (ex, item) {
                    self.arrayTest.push(item)
                });
               
                $('#idTest').change(function () {
                    if ($('#idTest').val()!="") {
                        self.Load_Doctor($('#idTest').val());
                    }
                })
            }
        });
    }

    self.arrayDoctorTest = ko.observableArray();
    self.Load_Doctor = function (idTest) {
       
        self.arrayDoctorTest([]);
        $.ajax({
            type: "GET",
            url: backendUrl + "/api/DoctorTest/DoctorByTest/" + idTest,
            contentType: "application/json",
            success: function (data) {
                $.each(data, function (ex, item) {
                    self.arrayDoctorTest([])
                    self.arrayDoctorTest.push(item)
                });
                /*$('#idDoctorTest').val(doctorId)*/
            }
        });
    }

}


$(function () {
    var ap = new viewmodel();
    ap.click();
    ko.applyBindings(ap);
    ap.Load_Test();
});

$(document).ready(function () {
    $("#myInput").on("keyup", function () {
        var value = $("#myInput").val().toLowerCase();
        $("#search_table tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});