"use strict";
window.onload = function () {
    deleteModal();
    //blockModal();
    positionModal();
    accessModal();
}

function deleteModal() {
    // Acces
    $("button[name='delete']").on('click', function () {
        //debugger;
        //let check = confirm('Are you shure you want to delete this person?');
        //if (check) {
        //    let url = $(this).attr('href');
        //    //$('context').load(url);//что сделать - post method for delete
        //    return false;
        //}
        let id = $(this).parent().attr("data-target");
        $("#modalcontent").children().remove();
        $.post("Delete", { "id": id }, function (menu) {
        }).then((menu) => {
            $(menu).appendTo($("#modalcontent"));
        });
    });
}



function positionModal() {
    $("button[name='position']").on("click", function () {
        let id = $(this).parent().attr("data-target");
        $("#modalcontent").children().remove();
        $.post("SetPosition", { "id": id }, function (menu) {
        }).then((menu) => {
            $(menu).appendTo($("#modalcontent"));
            //savePositionModal();
        });
    });
}

function accessModal() {
    $("button[name='access']").on("click", function () {
        let id = $(this).parent().attr("data-target");
        $("#modalcontent").children().remove();
        $.post("SetAccesss", { "id": id }, function (menu) {
        }).then((menu) => {
            $(menu).appendTo($("#modalcontent"));
            //saveAccessModal();
        });
    });
}
//подписать на Save в модальном окне!!!
//function savePositionModal() {
//    $("#save-position").on("click", function () {
//        let id = $(this).parent().attr("data-target");
//        console.log(id);
//        $.post("SavePosition", { }, function (menu) {
//            //$("#accesscontent").children().remove();
//        }).then(() => {
//            //$(menu).appendTo($("#save-position"));
//        });
//    });
//}


//подписать на Save в модальном окне!!!
//function saveAccessModal() {
//    $("#save-position").on("click", function () {
//        let id = $(this).parent().attr("data-target");
//        console.log(id);
//        $.post("SaveAccess", {}, function (menu) {
//            //$("#accesscontent").children().remove();
//        }).then(() => {
//            //$(menu).appendTo($("#save-position"));
//        });
//    });
//}



//function blockModal() {
//    $("button[name='block']").on("click", function () {
//        let resp = $.post("BlockUserAsync", { "id": $(this).parent().attr("data-target") }, function (menu) {
//            $(menu).appendTo(jname.find("div.dropdown-menu"));
//        });
//        resp.then(() => {
//            resolve(true);
//            reject(false);
//        });
//    });
//}
