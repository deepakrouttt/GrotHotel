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
        $.ajax({
            url: 'https://localhost:44309/api/HotelApi/GetBlackOutDate/',
            type: 'GET',
            success: function (dates) {
                $('#datepicker').datepicker({
                    beforeShowDay: function (date) {
                        var stringDate = $.datepicker.formatDate('yy-mm-dd', date);
                        var isBlackout = (dates.indexOf(stringDate) !== -1);
                        return [true, isBlackout ? 'blackout-date' : ''];
                    },
                    dateFormat: 'yy-mm-dd',
                    onSelect: function (dateText) {
                        $(this).find('.ui-state-active').css('background-color', 'yellow');
                    }
                });
                $("#datepicker").focus();

                $(".AddBlackOutDate").click(function () {
                    var RoomRateId = $(this).parent().siblings().closest('.container').find(".showBlackOut").data("id");
                    var date = $(this).siblings().closest("#datepicker").val();
                    var blackoutDate = {
                        "id": 0,
                        "dates": [
                            {
                                "id": 0,
                                "date": date                              
                            }
                        ],
                        "roomRateId": RoomRateId
                    };
                    $.ajax({
                        url: 'https://localhost:44309/api/HotelApi/addBlackOutDate',
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify(blackoutDate),
                        success: function (data) {
                            $(".popBlackDate").prevAll().not(".popBlackDate").css("filter", "none");
                            $(".popBlackDate").hide();
                        },
                        error: function (error) {  
                            console.log(error);
                        }
                    });
                });
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
    $(".closepopBlackOut").click(function () {
        $(".popBlackDate").prevAll().not(".popBlackDate").css("filter", "none");
        $(".popBlackDate").hide();
    })

});