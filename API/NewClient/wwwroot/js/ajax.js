//$.ajax({
//    url: "https://pokeapi.co/api/v2/pokemon/"
//}).done((result) => {
//    //console.log(result);
//    //console.log(result.results);
//    text = " ";
//    no = 1;
//    $.each(result.results, function (key, val) {
//        text += `<tr>
//        <td>${no++}</td>
//        <td>${val.name}</td>
//        <td button type="button" class="btn btn-primary" data-toggle="modal" data-target="#mymodal" onclick = details("${val.url}")>detail</td>
//        </tr>`;
//    });
//    $("#listpoke").html(text);
//}).fail((error) => {
//    console.log(error);
//});

//function details(url) {
//    fetch(url).then(function (response) {
//        response.json().then(function (val) {
//            document.getelementbyid('detail').innerhtml = '';
//            document.getelementbyid('detail').insertadjacenthtml('beforeend',
//                    `<h3> ${val.name} </h3>
//                     <img src='${val.sprites.front_default}'>
//                     <p> ability: ${val.abilities[0].ability.name}, ${val.abilities[1].ability.name}</p>
//                     <p> weight: ${val.weight} </p>`
//                )
//        })
//    })
//}

$(document).ready(function () {
    $('#myTable').DataTable({
        dom: 'Bfrtip',
        buttons: [
            'excel', 'csv'
        ],
        "ajax": {
            //url: "https://localhost:44385/API/Employees/ViewRegistered",
            url: "/Employee/ViewRegistered",
            dataType: "json",
            dataSrc: ""
        },
        "columns": [
            { "data": "nik" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return row["firstName"] + " " + row["lastName"];
                }
            },
            { "data": "email" },
            { "data": "salary" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return formatTelfon(row["phoneNumber"], "");
                }
            },
            {
                "data": "birthDate",
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    if (row["gender"] == 1) {
                        return "Male";
                    } else {
                        return "Female";
                    }
                }
            },
            { "data": "degree" },
            { "data": "gpa" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<button class=\"btn btn-primary\">Edit</button>";
                },
                orderable: false,
            }
        ]
    })
})

function formatTelfon(angka, prefix) {
    var parse = parseInt(angka);
    var stringTO = '' + parse;
    var number_string = stringTO.replace(/^[\+]?[(]?[1-9]{3}[)]?[-\s\.]?[1-9]{3}[-\s\.]?[1-9]{4,6}$/im, '').toString(),
        split = number_string.split(','),
        sisa = split[0].length % 3,
        rupiah = split[0].substr(0, sisa),
        ribuan = split[0].substr(sisa).match(/\d{3}/gi);

    // tambahkan titik jika yang di input sudah menjadi angka ribuan
    if (ribuan) {
        separator = sisa ? '' : '';
        rupiah += separator + ribuan.join('');
    }

    rupiah = split[1] != undefined ? rupiah + ',' + split[1] : rupiah;
    return prefix == undefined ? rupiah : (rupiah ? '+62' + rupiah : '');
}

function Insert() {
    var obj = new Object();
    obj.NIK = $("#nik").val();
    obj.FirstName = $("#firstName").val();
    obj.LastName = $("#lastName").val();
    obj.Email = $("#email").val();
    obj.Password = $("#password").val();
    obj.Salary = parseInt($("#salary").val());
    obj.PhoneNumber = $("#phoneNumber").val();
    obj.BirthDate = $("#birthDate").val();
    obj.Gender = parseInt($("#gender").val());
    obj.Degree = $("#degree").val();
    obj.GPA = $("#gpa").val();
    obj.EducationID = parseInt($("#educationId").val());
    obj.UniversityID = parseInt($("#universityId").val());
    const myJSON = JSON.stringify(obj);

    $.ajax({
        url: "Employees/Register",
        type: "POST",
        contentType: "application/json",
        data: myJSON
    }).done((result) => {
        alert("Sukses");
    }).fail((error) => {
        alert("Gagal");
    })
}

 //////////////////////////////////////////////////////////

let Male = countGender(1);
let Female = countGender(2);

var pie = {
    chart: {
        type: 'donut',
        height: '380px'
    },
    dataLabels: {
        enable: false
    },
    series: [Male, Female],
    labels: ['Male', 'Female'],
    noData: {
        text: 'Loading'
    }
}

var chart = new ApexCharts(document.querySelector("#piechart"), pie);

chart.render();

function countGender(gender) {
    let count = 1;
    jquery.ajax({
        //url: "https://localhost:44385/API/Employees/ViewRegistered",
        url: "/Employee/ViewRegistered",
        success: function (result) {
            $.each(result, function (key, val) {
                if (val.gender === gender) {
                    ++count;
                }
            });
        },
        async: false
    });
    return count;
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function countUniversity(degree) {
    let count = 0;
    jQuery.ajax({
        url: "/Employee/ViewRegistered",
        success: function (result) {
            $.each(result, function (key, val) {
                if (val.degree === degree) {
                    ++count;
                }
            });
        },
        async: false
    });
    return count;
}

var options = {
    series: [{
        data: [countUniversity("Diploma"), countUniversity("Bachelor"), countUniversity("Magister")]
    }],
    chart: {
        height: 360,
        type: 'bar',
    },
    plotOptions: {
        bar: {
            borderRadius: 10,
            dataLabels: {
                position: 'top', // top, center, bottom
            },
        }
    },
    dataLabels: {
        enabled: true,
        offsetY: -20,
        style: {
            fontSize: '12px',
            colors: ["#304758"]
        }
    },

    xaxis: {
        categories: ["Diploma", "Bachelor", "Magister"],
        position: 'bottom',
        axisBorder: {
            show: false
        },
        axisTicks: {
            show: false
        },
        crosshairs: {
            fill: {
                type: 'gradient',
                gradient: {
                    colorFrom: '#D8E3F0',
                    colorTo: '#BED1E6',
                    stops: [0, 100],
                    opacityFrom: 0.4,
                    opacityTo: 0.5,
                }
            }
        },

    },
    yaxis: {
        axisBorder: {
            show: false
        },
        axisTicks: {
            show: false,
        },
        labels: {
            show: false,

        }

    },

};

var chart = new ApexCharts(document.querySelector("#barchart"), options);
chart.render();
