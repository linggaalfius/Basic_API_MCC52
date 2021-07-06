////$.ajax({
////    url: "https://pokeapi.co/api/v2/pokemon/"
////}).done((result) => {
////    //console.log(result);
////    //console.log(result.results);
////    text = " ";
////    no = 1;
////    $.each(result.results, function (key, val) {
////        text += `<tr>
////        <td>${no++}</td>
////        <td>${val.name}</td>
////        <td button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal" onClick = details("${val.url}")>Detail</td>
////        </tr>`;
////    });
////    $("#listPoke").html(text);
////}).fail((error) => {
////    console.log(error);
////});

////function details(url) {
////    fetch(url).then(function (response) {
////        response.json().then(function (val) {
////            document.getElementById('detail').innerHTML = '';
////            document.getElementById('detail').insertAdjacentHTML('beforeend',
////                    `<h3> ${val.name} </h3>
////                     <img src='${val.sprites.front_default}'>
////                     <p> Ability: ${val.abilities[0].ability.name}, ${val.abilities[1].ability.name}</p>
////                     <p> Weight: ${val.weight} </p>`
////                )
////        })
////    })
////}

$(document).ready(function () {
    $('#myTable').DataTable({
        dom: 'Bfrtip',
        buttons: [
            'excel', 'csv'
        ],
        "ajax": {
            url: "https://localhost:44385/API/Employees/ViewRegistered",
            dataType: "json",
            dataSrc: "result"
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
                    return "+62" + row["phoneNumber"];
                }
            },
            {
                "data": "birthDate",
                //"render": "data.render.moment(DD - MM - YYYY)",
            },
            {
                "data": "gender",
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
        url: "https://localhost:44385/API/Employees/Register",
        type: "POST",
        contentType: "application/json",
        data: myJSON
    }).done((result) => {
        alert("Sukses");
    }).fail((error) => {
        alert("Gagal");
    })
}