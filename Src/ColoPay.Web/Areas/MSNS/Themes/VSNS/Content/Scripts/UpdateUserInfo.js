$(function() {
    /*验证用户昵称开始*/
    $("#txtNickName").blur(function () {
        checknickname();
    });
    /*验证用户结束*/

    /*验证手机号码开始*/
    $("#txtPhone").blur(function () {
        checkphone();
    });
    /*验证手机号码结束*/
    
});
// 验证用户昵称
function checknickname() {
    var nicknameVal = $.trim($('#txtNickName').val());
    if (nicknameVal.indexOf(";") > -1) {
        ShowFailTip("昵称不能包含“；”");
        return false;
    }
    if (nicknameVal != "") {
        //验证昵称是否存在
        var errnum = 0;
        $.ajax({
            url: $YSWL.BasePath + "UserCenter/CheckNickName",
            type: 'post',
            dataType: 'json',
            timeout: 10000,
            async: false,
            data: {
                Action: "post",
                NickName: nicknameVal
            },
            success: function (JsonData) {
                switch (JsonData.STATUS) {
                    case "OK":
                        $("#nciknameTip").removeClass("red").addClass("tipClass").html("&nbsp;");
                        break;
                    case "EXISTS":
                        errnum++;
                        ShowServerBusyTip("该昵称已被其他用户抢先使用，换一个试试");
                        break;
                    case "NOTNULL":
                        errnum++;
                        ShowFailTip("昵称不能为空！");
                        break;
                    default:
                        errnum++;
                        ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                        break;
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                errnum++;
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
            }
        });
        return errnum == 0 ? true : false;
    } else {
        ShowFailTip("昵称不能为空！");
        return false;
    }
}

// 验证用户手机号码
function checkphone() {
    var phoneVal = $.trim($('#txtPhone').val());
    if (phoneVal != '') {
        if (!/^(1[3,4,5,8,7]{1}\d{9})$/.test(phoneVal)) {
            ShowFailTip("请输入正确的手机号！");
            return false;
        } else {
            $("#phoneTip").removeClass("red").addClass("tipClass").html("&nbsp;");
            return true;
        }
    }
    return true;
}
function submit() {
    var errnum = 0;
    if (!checknickname()) {
        errnum++;
    }
    if (!checkphone()) {
        errnum++;
    }
    if (!(errnum == 0 ? true : false)) {
        return false;
    } else {
        var nickname = $.trim($("#txtNickName").val());
        var phone = $.trim($("#txtPhone").val());
        var sex = -1;
        if ($("#radman").attr("checked") == "checked") {
            sex = 1;
        }
        if ($("#radwoman").attr("checked") == "checked") {
            sex = 0;
        }
        $.ajax({
            url: $YSWL.BasePath + "UserCenter/UpdateUserInfo",
            type: 'post',
            dataType: 'json',
            timeout: 10000,
            async: false,
            data: {
                Action: "post",
                NickName: nickname,
                Phone: phone,
                Sex: sex
            },
            success: function (JsonData) {
                switch (JsonData.STATUS) {
                    case "SUCC":
                        ShowSuccessTip("修改成功！");
                        setTimeout(function () {
                            window.location = $YSWL.BasePath + "Profile/Index";
                        }, 3000);
                        break;
                    case "FAIL":
                        ShowFailTip("修改失败！");
                        break;
                    default:
                        ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                        break;
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
            }

        });
    }

};