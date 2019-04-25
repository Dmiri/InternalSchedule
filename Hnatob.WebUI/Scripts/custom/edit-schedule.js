"use strict";
window.onload = function () {
    init();
    togglesEvent();
    inputTimeEvent();
    inputMenuEvent();
    dropdownMenuEvent();
    //console.log("================================================================");

    addRowResponsible();
    dropdownCheckMenuEvent();
    //dropdownPositionInit();

    addRowService();
}

function init() {
    // Access
    let access = document.getElementById("Access");
    if (access.value == "") access.value = "Private";
    // Time
    let time = document.querySelector("input.btn.btn-info.control-label");
    let result = document.querySelector("#Duration").value;
    let hour = Math.floor(result / 60);
    let mitutes = result % 60;
    time.value = ((hour < 10) ? "0" + hour : hour) + ":" + ((mitutes < 10) ? "0" + mitutes : mitutes);
    // Lists responsibles
    initDropdownPositionMenu();
}

function togglesEvent(start = document) {
    if (start === null) return;

    let toggles = start.querySelectorAll(".btn-group.btn-group-toggle .btn");
    toggles.forEach(function (item) {
        item.addEventListener("click", function (e) {
            document.querySelector("#Access").value = e.srcElement.textContent.replace(/\s+/g, "");
        });
    });
};

function inputTimeEvent() {
    let time = document.querySelector("input.btn.btn-info.control-label");
    time.addEventListener("change", function (e) {
        var parts = e.srcElement.value.split(":");
        let result = Number(parts[0]) * 60 + Number(parts[1]);
        document.querySelector("#Duration").value = result;
    });
}

function inputMenuEvent(start = document) {
    if (start === null) return;
    let vals = start.querySelectorAll(".dropdown .form-control");
    vals.forEach(function (val) {
        val.addEventListener("change", function (e) {
            console.log(e.srcElement.value);
            e.srcElement.dataset.itemid = "";
            let hide = e.srcElement.parentElement.querySelector("[name ^= hidenParam]");
            if (hide !== undefined) {
                hide.value = "0";
            }
        });
    });
}

function dropdownMenuEvent(start = document) {
    if (start === null) return;

    let jstart = $(start);
    let dropdowns = jstart.find(".dropdown");
    dropdowns.each(function () {
        let jhidenParam = $(this).find("[name ^= hidenParam]:input");
        let jval = $(this).find(".form-control");
        let jitems = $(this).find(".dropdown-menu .dropdown-item");
        jitems.on("click", function (e) {
            jval.attr("value", this.textContent.replace(/\s+/g, " ").replace(/\s$/, ""));
            let varId = e.toElement.dataset.itemid;
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


function dropdownCheckMenuEvent(start = document) {
    if (start === null) return;

    $(start).find(".dropdown.btn-group").each(function() {//#post 
        $(this).find(".dropdown-menu .dropdown-item").on("click", function (e) {
            let jRow = e.toElement.parentElement.parentElement.parentElement.parentElement;
            let jBlockName = $(jRow).find("#name");
            let position = e.toElement.dataset.itemid;

            if (position == 0) {
                jRow.remove();
                return;
            }
                //axios
            var newPiopleList = new Promise((resolve, reject) => {
                //console.log("init list's responsible");
                if (!isNaN(position)) {
                    let resp = $.post("GetResponsiblePerson", { "position": position }, function (menu) {
                        jBlockName.find("div.dropdown-menu .dropdown-item").remove();
                        $(menu).appendTo(jBlockName.find(".dropdown-menu"));
                    });
                    resp.then(() => {
                        resolve(true);
                        reject(false);
                    });
                }
            });
            newPiopleList.then(
                (address) => {
                    dropdownMenuEvent(jBlockName);
                    let val = jBlockName.find(".form-control");
                    val.attr("value", "");
                    val.attr("dataset-itemid", "0");
                    jBlockName.find("[name ^= hidenParam]:input").attr("value", "0");
                });
        });
    });
}


//disappear or appear elements responsibles"s list
function dropdownPositionInit() {//(address = document) {
    let dropdowns = document.querySelectorAll("#post.dropdown");//let dropdowns = address.querySelectorAll(".dropdown.btn-group");

    //===============================================================
    let hidList = [];
    let i = 0;
    dropdowns.forEach(function (hidItem) {
        let fild = hidItem.querySelector(".form-control").textContent;
        if (fild !== "Empty") {
            hidList[i] = fild;
            i++;
        }
    });

    //---------------------------------------------------------------
    dropdowns.forEach(function (hidItem) {
        let list = hidItem.querySelector(".dropdown-menu").querySelectorAll(".dropdown-item");
        list.forEach(function (useItem) {
            for (let i = 0; hidList.length > i; ++i) {
                if (useItem.textContent === hidList[i]) {
                    useItem.style.display = "none";
                    break;
                }
                else {
                    useItem.style.display = "block";
                }
            };
        });
    });
}

function addRowResponsible() {
    let dropdowns = $("#addrowresp");
    let jitems = dropdowns.find(".dropdown-menu .dropdown-item");
    jitems.on("click", function (e) {
        let postNewRow = $("#responsibles #list");
        if (postNewRow === undefined || postNewRow === null) return;
        let position = e.toElement.dataset.itemid;

        var newRowResponsibles = new Promise((resolve, reject) => {
            //axios
            let resp = $.post("ResponsibleForEdit", {}, function (data) {
                let newDropdown = $(data).appendTo(postNewRow);
                $.post("GetResponsiblePerson", { "position": position }, function (menu) {
                    $(menu).appendTo(newDropdown.find("#name .dropdown-menu"));
                }).then(() => {
                    resolve(newDropdown);
                    reject("Responce position fail");
                });
            });
        });


        newRowResponsibles.then(
            (address) => {
                dropdownMenuEvent(address.toElement);
                dropdownCheckMenuEvent(address.toElement);
                dropdownPositionInit(address.toElement);
                inputMenuEvent(address.toElement);

                let jAddress = $(address);
                let jPost = jAddress.find("#post .form-control");
                let hidenParam = jAddress.find("input[name^=hidenParam]");
                if (hidenParam !== undefined && hidenParam !== null) {
                    jPost.attr("value", e.toElement.textContent);
                    hidenParam.attr("value", position);
                }
                else {
                    jPost.value = position;
                    if (position !== null && position !== undefined)
                        jPost.attr("data-itemid", position);
                }
            });
    });
}



function addRowService() {
    let dropdowns = $("#addrowservice");
    let jitems = dropdowns.find(".dropdown-menu .dropdown-item");
    jitems.on("click", function (e) {
        let serviceNewRow = $("#services #list");
        if (serviceNewRow === undefined || serviceNewRow === null) return;
        let serviceName = e.toElement.dataset.itemid;

        console.log(serviceName);



        var newRowService = new Promise((resolve, reject) => {
            let resp = $.post("CommentsToServicesForEdit", { "serviceName": serviceName }, function (data) {
                let newDropdown = $(data).appendTo(serviceNewRow);
            }).then((address) => {
                resolve(address);
                reject("Responce service fail");

                dropdownMenuEvent(address.toElement);
                inputMenuEvent(address.toElement);
                dropdownCheckMenuEvent(address.toElement);
                //dropdownPositionInit(address.toElement);
            });
        });
    });
}
