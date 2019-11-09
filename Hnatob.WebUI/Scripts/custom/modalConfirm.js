"use strict";

function dataSetModal(actionAttr, modalBlockName, methodName) {
    $(actionAttr).on("click", function (e) {
        e.preventDefault();
        let id = $(this).parent().attr("data-target");
        $(modalBlockName).children().remove();
        $.post(methodName, { "id": id }, function (menu) {
        }).then((menu) => {
            console.log(id)
            $(menu).appendTo($(modalBlockName));
        });
    });
}