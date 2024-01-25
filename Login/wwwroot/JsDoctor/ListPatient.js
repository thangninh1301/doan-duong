var viewmodel = function () {
    var self = this;
    self.arrays = ko.observableArray();
    self.arrays2 = ko.observableArray();
    /// list patient
    self.arrayResult = ko.observableArray();
    self.click = function () {
        var doctor = $('#doctor').val();
        $.ajax({
            type: "GET",
            url: backendUrl + "/api/Doctor/doctor/" + doctor,
          
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

    self.detail = function (item)
    {
        console.log("detail", item)
        /* document.location = "detailRegister";*/
        $('#list').modal('show');
        $('#name').text(item.lastName);
        self.arrays2([]);
        $.each(item.regis, function (ex, item1) {
            self.arrays2.push(item1);
           
        });     
    }

    self.arrayListTest=ko.observableArray();
    self.showResult = function (obResul) {
        var ob = obResul.apointmentTicket
        console.log("a", obResul)
        $('#symptom').text(obResul.symptom);
        $('#doctor').text(obResul.apointmentTicket.doctor.lastName);
        $('#dateMeet').text(obResul.dateMeet);
        $('#timeMeet').text(obResul.timeslot.decription);
        console.log("a", obResul.apointmentTicket.doctor.lastName)
        $('#showresult').modal('show');
        $.ajax({
            type: "GET",
            url: backendUrl + "/api/Result/" + ob.result.id,
            contentType: "application/json",
            success: function (data) {
                
                self.arrayListTest([]);

                $("#diagnostic").text(data.diagnostic)
                $("#therapyRegiment").text(data.therapyRegiment)
                $.each(data.resultDetails2, function (ex, test) {
                    self.arrayListTest.push(test);


                });
            }, error: function (err) {
                alert("loi " + err.status + "<!----!>" + err.statusText);
                self.click();
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