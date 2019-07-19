"use strict";
window.onload = function () {
    init();
    togglesEvent();
    inputStartEvent();
    inputDurationEvent();
    resetFormsAndHidenEvent();
    dropdownMenuEvent();
    //console.log("================================================================");

    addRowResponsible();
    removeFildIfCheckEmptyEvent();

    unicPositionList();

    addRowService();
    $('.datepicker').datepicker({
        format: 'dd.mm.yyyy',
        startDate: '0d'
    });

    $().keydown(function (event) {
        console.log(event.keyCode);
        if (event.keyCode = 13) event.preventDefault();
    });

    //sortableList();
}

function init() {
    //console.log("init")
    // Access
    let access = document.getElementById("Access");
    if (access.value == "") access.value = "Private";

    // Date-time
    let valStart = $("[name=Start]").attr("value");
    let mStart = moment(valStart, "DD.MM.YYYY hh:mm")
    $("#day").attr("value", mStart.format("DD.MM.YYYY"));
    $("#time").attr("value", mStart.format("hh:mm"));

    // Time - duration
    let result = $("input#Duration.control-label").attr("value");
    let jTime = $("#duration[type=time]");
    let mDuration = moment(result/60 + ":" + result%60, "h:m").format("HH:mm");
    jTime.attr("value", mDuration);

    // Lists responsibles
    initDropdownPositionMenu();
}

//=========================================================
// Universal
//=========================================================
function togglesEvent(start = document) {
    if (start === null) return;

    let toggles = start.querySelectorAll(".btn-group.btn-group-toggle .btn");
    toggles.forEach(function (item) {
        item.addEventListener("click", function (e) {
            document.querySelector("#Access").value = e.srcElement.textContent.replace(/\s+/g, "");
        });
    });
};


function resetFormsAndHidenEvent(start = document) {
    if (start === null) return;
    let jRow = $(start);
    let jForms = jRow.find(".dropdown .form-control");
    jForms.on("change", function (e) {
        $(e.target).attr("data-itemid", "");
        $(e.target).parent().find("[name^=hidenParam]").attr("value", 0);
    });
}


function dropdownMenuEvent(start = document) {
    if (start === null) return;
    //console.log("dropdownMenuEvent - init");
    let jstart = $(start);
    let dropdowns = jstart.find(".dropdown");
    dropdowns.each(function () {
        let jhidenParam = $(this).find("[name ^= hidenParam]:input");
        let jval = $(this).find(".form-control");
        let jitems = $(this).find(".dropdown-menu .dropdown-item");
        jitems.on("click", function (e) {
            //console.log("dropdownMenuEvent - click");
            jval.attr("value", this.textContent.replace(/\s+/g, " ").replace(/\s$/, ""));
            let varId = $(e.toElement).attr("data-itemid");//.dataset.itemid;
            if (varId !== null && varId !== undefined) {
                if (jhidenParam !== undefined && jhidenParam !== null)
                    jhidenParam.attr("value", varId);
                else {
                    jval.attr( "data-itemid", varId);
                }
            }
        });
    });
};

function sortableList(start = document) {
    if (start === null) return;
    let jElement = $(start).find("#sortable");
    console.log(jElement);
    jElement.sortable();
    jElement.disableSelection();
};


//=========================================================
// Aim
//=========================================================
function inputStartEvent() {
    // Date-time
    let jStart = $("[name=Start]");
    $("#day").on("change", function (e) {

        let newDey = e.target.value;
        let newTime = "00:00";
        let day = $("#day").attr("value");//, mStart.format("DD.MM.YYYY")
        let time = document.getElementById("time").value;//, mStart.format("hh:mm")
        if (moment(time, "hh.mm").isValid()) {
            newTime = time;
        }
        jStart.attr("value", newDey + " " + newTime);
    });

    $("#time").on("change", function (e) {
        let newDey = "00.00.0000";
        let newTime = e.target.value;
        let day = document.getElementById("day").value;//, mStart.format("DD.MM.YYYY")
        let time = $("#time").attr("value");//, mStart.format("hh:mm")
        if (moment(day, "DD.MM.YYYY").isValid()) {
            newDey = day;
        }
        jStart.attr("value", newDey + " " + newTime);
    });
}


//=========================================================
function inputDurationEvent() {
    let time = document.querySelector("#duration.btn.btn-info.control-label");
    time.addEventListener("change", function (e) {
        var parts = e.srcElement.value.split(":");
        let result = Number(parts[0]) * 60 + Number(parts[1]);
        document.querySelector("#Duration").value = result;
    });
}


//=========================================================
function initDropdownPositionMenu() {
    let jRow = $("#responsibles #list .row");
    jRow.each(function () {
        let jPostValue = $(this).find("#post [name ^= hidenParam]:input");
        let jname = $(this).find("#name");
        let position = jPostValue.attr("value");

        var newPiopleList = new Promise((resolve, reject) => {
            if (!isNaN(position)) {
                let resp = $.post("GetResponsiblePerson", { "position": position }, function (menu) {
                    $(menu).appendTo(jname.find("div.dropdown-menu"));
                });
                resp.then(() => {
                    resolve(true);
                    reject(false);
                });
            }
        });
        newPiopleList.then(
            (address) => {
                dropdownMenuEvent(jname);
            });
    });
}


//=========================================================
function removeFildIfCheckEmptyEvent(start = document) {
    if (start === null) return;

    //console.log("removeFildIfCheckEmptyEvent - init");
    $(start).find("#service .dropdown.btn-group, #post .dropdown.btn-group").each(function() {//#post 
        $(this).find(".dropdown-menu .dropdown-item").on("click", function (e) {
            let row = this.closest("div.card");
            let jBlockName = $(row).find("#name");
            let position = $(e.toElement).attr("data-itemid");
            if (position == 0) {
                row.remove();
                return;
            }
            unicPositionList();
                //axios
            var newPiopleList = new Promise((resolve, reject) => {
                //console.log("init list's responsible");
                if (!isNaN(position)) {
                    let val = jBlockName.find(".form-control");
                    val.attr("value", "");
                    //val.attr("dataset-itemid", "0");
                    jBlockName.find("[name ^= hidenParam]:input").attr("value", "0");
                    let resp = $.post("GetResponsiblePerson", { "position": position }, function (menu) {
                        jBlockName.find("div.dropdown-menu .dropdown-item").remove();
                        $(menu).appendTo(jBlockName.find(".dropdown-menu"));
                    });
                    resp.then(() => {
                        resolve(true);
                        reject(false);

                        dropdownMenuEvent(jBlockName);
                    });
                }
            });
        });
    });
}


//===============================================================
//disappear or appear elements responsibles"s list
function unicPositionList() {//(address = document) {
    let hidList = [];
    let jDropdowuns = $("#post .dropdown");
    jDropdowuns.find(".form-control").each(function () {
        let value = $(this).attr("value");
        if (value != "Empty")
            hidList.push($(this).attr("value"));
    });
    jDropdowuns.find(".dropdown-menu .dropdown-item")
        .each(function () {
            let value = this.innerText;
            for (let i = 0; hidList.length > i; ++i) {
                if (value == hidList[i]) {
                    $(this).css("display", "none");
                    break;
                }
                else {
                    $(this).css("display", "block");
                }
            }
        });
}


//===============================================================
function addRowResponsible() {
    //console.log("addRowResponsible")

    let dropdowns = $("#addrowresp");
    let jitems = dropdowns.find(".dropdown-menu .dropdown-item");
    jitems.on("click", function (e) {
        let postList = $("#responsibles #list #sortable");
        console.log(postList);
        if (postList === undefined || postList === null) return;
        let position = e.toElement.dataset.itemid;
        var newRowResponsibles = new Promise((resolve, reject) => {
            //axios
            let resp = $.post("ResponsibleForEdit", { "position": position }, function (data) {
                let newDropdown = $(data).appendTo(postList);
                $.post("GetResponsiblePerson", { "position": position }, function (menu) {
                    $(menu).appendTo(newDropdown.find("#name .dropdown-menu"));
                }).then(() => {
                    let jPost = newDropdown.find("#post .form-control");
                    let hidenParam = newDropdown.find("#post input[name^=hidenParam]");
                    jPost.attr("value", e.toElement.textContent);
                    if (hidenParam !== undefined && hidenParam !== null) {
                        hidenParam.attr("value", position);
                    }
                    else {
                        //jPost.value = position;
                        if (position !== null && position !== undefined)
                            jPost.attr("data-itemid", position);
                    }


                    //console.log("newRowResponsibles", newDropdown);
                    dropdownMenuEvent(newDropdown);
                    resetFormsAndHidenEvent(newDropdown);
                    removeFildIfCheckEmptyEvent(newDropdown);
                    unicPositionList(newDropdown);
                    resolve(newDropdown);

                    //sortableList();
                    reject("Responce position fail");

                });
            });
        });

    });
}


//===============================================================
function addRowService() {
    let dropdowns = $("#addrowservice");
    let jitems = dropdowns.find(".dropdown-menu .dropdown-item");
    jitems.on("click", function (e) {
        let serviceNewRow = $("#services #list");
        if (serviceNewRow === undefined || serviceNewRow === null) return;
        let serviceName = e.toElement.dataset.itemid;

        var newRowService = new Promise((resolve, reject) => {
            let resp = $.post("CommentsToServicesForEdit", { "serviceName": serviceName }, function (data) {
                let newDropdown = $(data).appendTo(serviceNewRow);
            }).then((address) => {
                resolve(address);
                reject("Responce service fail");

                dropdownMenuEvent(address.toElement);
                resetFormsAndHidenEvent(address.toElement);
                removeFildIfCheckEmptyEvent(address.toElement);
            });
        });
    });
}


