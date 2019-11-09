"use strict";
window.onload = function () {
    $().keydown(function (event) {
        if (event.keyCode = 13) event.preventDefault();
    });

    filterInit();
    checkDropDownItem();
    submitFilter();
    addFilterToPaging();
}

function filterInit() {
    let rout = window.location.pathname;

    let partRout = rout.split("/");
    if (partRout.length > 3) {
        let filterParams = partRout[3].split(",");

        $("#Period").val(filterParams[0].split("=")[1]);
        $("#Access").val(filterParams[1].split("=")[1]);
        $("#Type").val(filterParams[2].split("=")[1]);
        $("#Name").val(partRout[4]);
    }
    let jFilter = $(".form-inline .form-group");
    jFilter.each(function (index) {
        let input = $(this).find("input");
        let jField = $(this).find(".dropdown-toggle");
        if (input != input)
            jField.text(jField.val() + " ");
    });
}


function checkDropDownItem() {
    let jFilter = $(".form-inline .form-group .btn-group");
    jFilter.each(function () {
        let input = $(this).find("input");
        let jField = $(this).find(".dropdown-toggle");
        let items = $(this).find(".dropdown-menu .dropdown-item");
        items.on("click", function () {
            let jItem = $(this);
            let val = jItem.data("attribute");
            if (input == input)
                input.val(jItem.text());
            else {
                jField.text(jItem.text() + " ");
            }
            jField.val(val);
        })
    });
}

function submitFilter() {
    $("#SubmitFilter").on("click", function () {
        let page = $(".float-right.form-inline")
            .find(".float-right.btn-group")
            .find(".selected")
            .data("attribute");
        let pageSize = $("#Period").val().toLowerCase();
        let access = $("#Access").val().toLowerCase();
        let type = $("#Type").val().toLowerCase();
        let name = $("#Name").val();
        window.location.replace("/Scheduler/"
            + "Page=" + page + "/"
            + "Period=" + pageSize + ","
            + "Access=" + access + ","
            + "Type=" + type + "/"
            + name);
    });
}


function addFilterToPaging() {
    let pageSize = $("#Period").val();
    let access = $("#Access").val();
    let type = $("#Type").val();
    let name = $("#Name").val();
    if (pageSize != "week" && pageSize != 7
        || access != "all"
        || type != "all"
        || name != ""
    ) {
        $("div.form-inline .btn-group a.btn").each(function () {
            let href = $(this).attr("href");
            $(this).attr("href", href + "/"
                + "Period=" + pageSize + ","
                + "Access=" + access + ","
                + "Type=" + type + "/"
                + name);
        });
    }

}
