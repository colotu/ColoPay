var loginNameStatus = true;
var passwordStatus = true;
$(document).ready(function () {
    //登录按钮的单击事件
    $('#loginsubmit').click(function () {
        if (CheckLogin()) {//验证通过
            $("#formlogin").submit(); //触发submit按钮
        }
    });

    //微信用户绑定
    $("#bindsubmit").click(function () {
        if (CheckLogin()) {//验证通过
            $(this).attr("disabled", "disabled");
            var userName = $("#txtLogin").val();
            var pwd = $("#password").val();
            var user = $("#txtUser").val();
            var open = $("#txtOpenId").val();
            $.ajax({
                url: $YSWL.BasePath + "Account/AjaxBind",
                type: 'post',
                dataType: 'text',
                timeout: 10000,
                async: false,
                data: {
                    Action: "post", UserName: userName, UserPwd: pwd, User: user, OpenId: open
                },
                success: function (resultData) {
                    if (resultData == "1") {
                        ShowSuccessTip("绑定用户成功！");
                    }
                    if (resultData == "2") {
                        ShowFailTip("该账户已被冻结，请联系管理员！");
                    }
                    if (resultData == "3") {
                        ShowFailTip("该账户已经绑定了其它帐号！");
                    }

                    if (resultData == "0") {
                        ShowFailTip("您输入的用户名和密码有误，请重试！");
                        $("#bindsubmit").removeAttr("disabled");
                    }
                    if (resultData == "-1") {
                        ShowFailTip("服务器繁忙，请稍候再试！");
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    ShowServerBusyTip("服务器繁忙，请稍候再试！");
                }

            });
        }
    });
    var regStr = $('#hfRegisterToggle').val(); //注册方式
    //邮箱文本框的光标
    $("#txtLogin").focus(function () {
        var str = '';
        if (regStr == "Phone") {
            str = '手机号码';
        } else {
            str = 'Email地址';
        }
            $("#divLoginTip").html("*");
    }).blur(function () {
        if (regStr == "Phone") {
            CheckLoginPhoneName($(this));
        } else {
            CheckLoginEmailName($(this));
        }
    });
    //密码文本框的光标
    $("#password").focus(function () {
        $("#divPasswordTip").html("*");
    }).keypress(function (event) {
        if (event.which == 13) {
            $("#loginsubmit").trigger("click");
        }
    }).blur(function () {
        CheckPassword($(this));
    });

});


//验证登录
function CheckLogin() {
    var regStr = $('#hfRegisterToggle').val(); //注册方式
    if (regStr == "Phone") {
        CheckLoginPhoneName($("#txtLogin"));
    } else {
        CheckLoginEmailName($("#txtLogin"));
    }
    
    CheckPassword($("#password"));
    var checkOK = false;
    if (!loginNameStatus || !passwordStatus) {
        checkOK = false;
    }
    else {
        checkOK = true;
    }
    return checkOK;
}

////验证邮箱
function CheckLoginEmailName(obj) {
    var regsEmail = /^[\w-]+(\.[\w-]+)*\@[A-Za-z0-9]+((\.|-|_)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
    var val = obj.val();
    if (val == ""  ) {
        loginNameStatus = false;
        $("#userValidate").html("*");
        ShowFailTip('请输入用户名');
        return false;
    } else if (!regsEmail.test(val)) {
        $("#userValidate").html("*");
        ShowFailTip('请输入正确的邮箱');
        loginNameStatus = false;
        return false;
    } else {
        loginNameStatus = true;
        $("#userValidate").html('');
        return;
    }
}

////验证手机
function CheckLoginPhoneName(obj) {
    var regs = /^1([38][0-9]|4[57]|5[^4])\d{8}$/;
    var val = obj.val();
    if (val == "" ) {
        loginNameStatus = false;
        $("#userValidate").html("*");
        ShowFailTip('请输入用户名');
        return false;
    } else if (!regs.test(val)) {
        $("#userValidate").html("*");
        ShowFailTip('请输入正确的手机号');
        loginNameStatus = false;
        return false;
    } else {
        loginNameStatus = true;
        $("#userValidate").html("");
    }
}


//验证密码
function CheckPassword(obj) {
    if (obj.val() != "") {
        passwordStatus = true;
        $("#passValidate").html('');
    }
    else {
        passwordStatus = false;
        $("#passValidate").html("*");
        ShowFailTip('请输入密码');
        return false;
    }
}

 