var viewmodel = function () {
    var self = this;
    self.arrays = ko.observableArray();
    self.arrays2 = ko.observableArray();
    /// list patient
    self.click = function () {
        $.ajax({
            type: "GET",
            url: backendUrl + "/api/Admin/statistic/patient",
            //url: "https://localhost:44310/api/Admin/statistic/patient",
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

    //thống kê bệnh nhân theo giờ
    /*   self.click2 = function () {
           $('#table2').modal('show');
           
           $.ajax({
               type: "GET",
               url: "https://localhost:44310/api/Admin/statisticPatient/Time",
               contentType: "application/json",
               dataType: "json",
               success: function (data) {
                   self.arrays2([]);
                   $.each(data, function (ex, item) {
                       self.arrays2.push(item);
                      
                   });
                  
               },
               error: function (error) {
                   alert("error");
               }
           });
       }*/

    /* self.detail = function (item)
     {
         *//* document.location = "detailRegister";*//*
    $('#list').modal('show');
    self.arrays2([]);
    $.each(item.register, function (ex, item1) {
        self.arrays2.push(item1);
        console.log(item1);
    });
   
   
}*/
}


$(function () {
    var ap = new viewmodel();
    ap.click();
    ko.applyBindings(ap);
});
$(function () {
    var abc = new viewmodel();
    abc.click2();
    ko.applyBindings(abc);
});

