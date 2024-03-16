$(document).ready(function () {
    $("#Imagefile").hide();
    $("#RoomImageFile").hide();
    var state = true;
    $("#btn-image").click(function () {
        if (state) {
            $("#Imagefile").show();
            $("#Image").hide();
            $('form').attr('action', '/Hotel/EditImage');
        } else {
            $("#Image").show();
            $("#Imagefile").hide();
            $('form').attr('action', '/Hotel/Edit');
        }
        state = !state;
    });

    $("#btn-RoomImage").click(function () {
        if (state) {
            $("#RoomImageFile").show();
            $("#RoomImage").hide();
            $('form').attr('action', '/Hotel/EditRoomImage');
        } else {
            $("#RoomImage").show();
            $("#RoomImageFile").hide();
            $('form').attr('action', '/Hotel/EditRoom');
        }
        state = !state;
    });
    $("#IsExtraAdult").change(function () {
        if ($(this).prop("checked") == false) {
            $("#AdultRate").prop("disabled", true);
            $("#AdultRate").val("");
        }
        else {
            $("#AdultRate").prop("disabled", false);
        }
    });
    $("#IsChildAllow").change(function () {
        if ($(this).prop("checked") == false) {
            $("#childRate").prop("disabled", true);
            $("#childRate").val("");
        }
        else {
            $("#childRate").prop("disabled", false);
        }
    });
    $("#IsSingleEqualDouble").change(function () {
        if ($(this).prop("checked") == true) {
            $("#SingleRate").val($("#DoubleRate").val());
        }
        else {
            $("#childRate").prop("disabled", false);
            var value = ($("#SingleRate").val()) / 2;;
            $("#SingleRate").val(value);
        }
    });
 
    $(".showBlackOut").click(function () {
        $(".popBlackDate").show();
        $(".popBlackDate").prevAll().not(".popBlackDate").css("filter", "blur(3px)");
    });
    $(".closepopBlackOut").click(function () {
        $(".popBlackDate").prevAll().not(".popBlackDate").css("filter", "none");
        $(".popBlackDate").hide();
    })


});