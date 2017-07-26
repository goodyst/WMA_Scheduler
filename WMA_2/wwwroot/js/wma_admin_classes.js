function addClass() {
    htmlRow = `<div class="row">
                    <div class="col-md-5" >
                            <input data-state="add" class="form-control" />
                    </div > <div class="col-md-2">location n/a yet</div>
               </div >`;
    $("#class_list").append(htmlRow);
    $.ajax({
        type: "POST",
        url: "/ClassView/ClassTimesList",
        /*data: params,*/
        success: function (data) {
            justAddedRow = $("#class_list").children().last();
            $(justAddedRow.children()[0]).html(data);
            let ddl = $(justAddedRow).find("select")
            if (ddl != undefined) {
                $(ddl).addClass("form-control")
                $(ddl).data("state", "add");

            }
        },
        error: function (data) {
            alert(data);
        }
        
    })
    
}
function classview_edit_edit(a_edit_fld) {
    cv_edit_edit_replacement_fld = $($(a_edit_fld).parents().filter(".row")[0]).children().filter(".currentTime")
    cv_edit_edit_params = {}
    cv_edit_edit_params["mode"] = "edit";
    
    cv_edit_edit_params["id"] = cv_edit_edit_replacement_fld.data("orig_tm");
    $.ajax({
        type: "POST",
        url: "/ClassView/ClassTimesList",
        data: cv_edit_edit_params,
        success: function (data) {
            cv_edit_edit_replacement_fld.html(data);
            ddl = cv_edit_edit_replacement_fld.children().filter("#lstClassTimes")
            $(ddl).prop("id", "lstClassTimes" + cv_edit_edit_replacement_fld.data("id"))
            $(ddl).addClass("form-control")
            $(ddl).data("state", "edit");
        },
        error: function (data) {
            alert(data);
        }

    })

}
function classview_edit_save() {
    pm_classview_edit = {};
    // the name
    pm_classview_edit["Description"] = $("#Class_Description").val();
    classTimes = []
    // the classes
    
    $("#class_list").children().each(function () {
        
        let ddlTimes = $($(this).children()[0]).find("select")     
            
        let opt = ddlTimes.prop("selectedOptions")
        if (opt != undefined) {
            let ct = {}
            ct["classtime_id"] = parseInt($(opt[0]).val());
            ct["id"] = ddlTimes.parent().data("id");
            ct["location_id"] = -1;
            ct["text"] = $(opt[0]).text();
            ct["mode"] = ddlTimes.data("state");
            classTimes.push(ct);
        }
        ddlTimes = $($(this).children()[1]).find("select")

        opt = ddlTimes.prop("selectedOptions")
        if (opt != undefined) {
            let ct = {}
            ct["classtime_id"] = -1;
            ct["location_id"] = parseInt($(opt[0]).val());
            ct["id"] = ddlTimes.parent().data("id");
            ct["text"] = $(opt[0]).text();
            ct["mode"] = ddlTimes.data("state");
            classTimes.push(ct);
        }
        
    });
    /*
    classTimes = [];
    classTimes.push({"id":1,"text":"a"});
    classTimes.push({ "id": 2, "text": "b" });
    classTimes.push({ "id": 3, "text": "c" });
    */
    //params["class_times"] = classTimes;
    // get list of removed
    // class location
    params = {}
    pm_classview_edit["Id"] = $("#txtClassId").val();
    pm_classview_edit["class_times"] = classTimes;
    $.ajax({
        type: "POST",
        url: "/ClassView/EditTimes",
        data: pm_classview_edit,
        success: function (data) {
            if (!JSON) {
                alert("Sorry your browser does not support this operation");
                return;
            }
            try {
                let objData = JSON.parse(data);
                if (objData.Error != "") {
                    alert(objData.Error);
                    return;
                } else {
                    window.location.href = objData.Url
                }

            } catch (e) {
                alert("OOOPs something went wrong. Please try again.")
                window.location.href = "/Index";
            }
        },
        error: function (data) {
            alert(data);
        }

    })
}
function removeClass(aDel) {
    pm_classview_del = {}
    let dataDiv = $(aDel).parent().children().filter(".currentTime")
    if (!(dataDiv != undefined && dataDiv.length > 0)) {
        dataDiv = $(aDel).parent().children().find(".currentTime")
    }
    if (dataDiv != undefined && dataDiv.length > 0) {
        pm_classview_del["Id"] = dataDiv.data("id");
        $.ajax({
            type: "POST",
            url: "/ClassView/DeleteTimes/" + pm_classview_del["Id"],
            data: pm_classview_del,
            success: function (data) {
                if (!JSON) {
                    alert("Sorry your browser does not support this operation");
                    return;
                }
                try {
                    let objData = JSON.parse(data);
                    if (objData.Error != "") {
                        alert(objData.Error);
                        return;
                    } else {
                        div_remove = objData.Id;
                        $("#class_list").children().each(function () {
                            let divCurTime = $(this).children().find(".currentTime")
                            if (divCurTime != undefined && divCurTime.length > 0) {
                                if (divCurTime.data("id") == div_remove) {
                                    $(this).remove();
                                }
                            }
                        })
                        // window.location.href = objData.Url
                    }

                } catch (e) {
                    alert("OOOPs something went wrong. Please try again.")
                    window.location.href = "/Index";
                }
            },
            error: function (data) {
                alert(data);
            }

        })
    }
}

function classview_editlocation_edit(a_edit_fld) {

    cv_edit_editlocation_replacement_fld = $($(a_edit_fld).parents().filter(".row")[0]).children().filter(".currentLoc")
    cv_edit_editLocation_params = {}
    cv_edit_editLocation_params["mode"] = "edit";

    cv_edit_editLocation_params["id"] = cv_edit_editlocation_replacement_fld.data("orig_loc");
    $.ajax({
        type: "POST",
        url: "/ClassView/LocationList",
        data: cv_edit_editLocation_params,
        success: function (data) {
            cv_edit_editlocation_replacement_fld.html(data);
            ddl = cv_edit_editlocation_replacement_fld.children().filter("#lstLocations")
            $(ddl).prop("id", "lstLocations" + cv_edit_editlocation_replacement_fld.data("id"))
            $(ddl).addClass("form-control")
            $(ddl).data("state", "edit");
        },
        error: function (data) {
            alert(data);
        }

    })



}