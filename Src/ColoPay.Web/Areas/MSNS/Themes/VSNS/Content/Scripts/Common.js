//判断是否含有禁用词
function ContainsDisWords(desc) {
    var isContain = false;
    $.ajax({
        url: $YSWL.BasePath + "Home/ContainsDisWords",
        type: 'post', dataType: 'text', timeout: 10000,
        async: false,
        data: { Desc: desc },
        success: function (resultData) {
            if (resultData == "True") {
                isContain = true;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowFailTip("操作失败：" + errorThrown);
        }
    });
    return isContain;
}

//检查是否登录
var CheckUserState = function () {
    var islogin;
    $.ajax({
        url: $YSWL.BasePath + "Home/CheckUserState",
        type: 'post',
        dataType: 'text',
        timeout: 10000,
        async: false,
        success: function (resultData) {
            if (resultData != "Yes") {
                var html = "";
                $.jBox(html, { title: "登录", submit: submitLogin, width: 550, top: 300, height: 250, buttons: { '登录': true} });
                $(".jbox-button").hide();
                islogin = false;
                return false;
            } else {
                islogin = true;
                return true;

            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        }
    });

    return islogin;
};
var CheckUserState4UserType = function () {
    var islogin;
    $.ajax({
        url: $YSWL.BasePath + "Home/CheckUserState4UserType",
        type: 'post',
        dataType: 'text',
        timeout: 10000,
        async: false,
        success: function (resultData) {
            if (resultData == "Yes") {
                islogin = true;
                return true;
            } else if (resultData == "Yes4AA") {
                $.jBox.tip('管理员不能创建专辑, 请您更换普通帐号再试!');
                $(".jbox-button").hide();
                islogin = false;
                return false;
            } else {
                var html = "";
                
                $.jBox(html, { title: "登录", submit: submitLogin, width: 550, top: 300, height: 250, buttons: { '登录': true} });
                $(".jbox-button").hide();
                islogin = false;
                return false;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        }
    });

    return islogin;
};
var CheckUserLogin = function () {
    var islogin;
    $.ajax({
        url: $YSWL.BasePath + "Home/CheckUserState",
        type: 'post',
        dataType: 'text',
        timeout: 10000,
        async: false,
        success: function (resultData) {
            if (resultData != "Yes") {
                islogin = false;
                return false;
            } else {
                islogin = true;
                return true;

            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        }
    });
    return islogin;
};


$("#ajaxlogin").die("click").live("click", function () { $(".jbox-button").click(); });
var submitLogin = function (v, h, f) {
    if (f.name == '') {
        $("#tipname").show();
        $("#tippwd").hide();
        return false;
    }
    if (f.pwd == '') {
        $("#tippwd").show();
        $("#tipname").hide();
        return false;
    }
    $.ajax({
        type: "POST",
        dataType: "text",
        url: $YSWL.BasePath + "Account/AjaxLogin",
        async: false,
        data: { UserName: $("#loginname").val(), UserPwd: $("#loginpwd").val() },
        success: function (data) {
            if (parseInt(data) == -1) {
                $.jBox.tip('该功能已被管理员关闭，如有疑问，请联系网站管理员', 'success');
                return false;
            }
            if (parseInt(data.split("|")[0]) > 0) {
                AjaxLoginGetUserInfo(data.split("|")[1], data.split("|")[2]);
                return true;
            }
            else {
                $.jBox.tip('用户名或者密码不正确，请重试', 'success');

            }
        }
    });
    return true;
};
var AjaxLoginGetUserInfo = function (pointer, rankscore) {
    $.ajax({
        type: "get",
        dataType: "text",
        url: $YSWL.BasePath + "Partial/Header",
        data: { pointer: pointer, rankscore: rankscore },
        success: function (data) {
            $(".headers").html(data);
        }
    });
};
//字符长度判断
function textareastrlen(str) {
    var len;
    var i;
    len = 0;
    for (i = 0; i < str.length; i++) {
        if (str.charCodeAt(i) > 255) {
            len += 2;
        }
        else {
            len++;
        }
    }
    if (len % 2 != 0) {
        len = len + 1;
    }
    return parseInt(len / 2);
}

$(function () {
    $(".like").die("click").live("click", function () {
        var topicId = $(this).children(".noPraise").attr("itemid");
        $.ajax({
            url: $YSWL.BasePath + "Home/AddTopicFav",
            type: "post",
            async:false,
            dataType: "json",
            data: { topicId: topicId },
            success: function (result) {
                switch (result.result) {
                    case "0":
                        ShowFailTip("赞失败了，请稍后再试！");
                        break;
                    case "1":
                        //ShowSuccessTip("赞成功了哦！");
                        $(".likecount[itemid='" + topicId + "']").text(result.totalCount);
                        $("i[itemid='" + topicId + "']").toggleClass("Praise");
                        break;
                    case "3":
                        ShowServerBusyTip("请您先登录再赞……");
                        setTimeout(function () {
                            window.location = $YSWL.BasePath + "Account/Login";
                        }, 3000);
                        break;
                    case "2":
                    default:
                        ShowServerBusyTip("亲，已经赞过了哦……");
                        break;
                }
            }
        });
    });
    $(".usergravator").each(function (i, e) {
        var src = $(e).attr("src");
        $(e).load(function () {
        }).error(function () {
            $(e).attr("src", "/Upload/User/Gravatar/default.jpg");
        });

    });
    setInterval(10000, function () {
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('hideOptionMenu');
        });
    });

//    $("#content").bind('paste', function (e) {//禁止粘贴导致字数为负
//        e.preventDefault();
//    });
});


$(function () {

    $(".active").click(function () {//显示删除按钮
        var tid = $(this).attr('tid');
        $(".perPop[tid='" + tid + "']").toggle();
    });
    $(".banThread").die('click').live('click', function () {//删除帖子
        //显示遮罩层
        $("#fwin_mask_replyForm").show();
        $("#fwin_mask_replyForm").addClass("g-mask");
        $("#fwin_mask_replyForm").height($("#body").height() + 100);
        //显示确定取消按钮
        var tid = $(this).attr("tid");
        $(".deleteTopic").attr('tid', tid); //传话题Id
        $("#fwin_dialog_confirmBox").show();
    });
    $(".cancelDelete").click(function () {//放弃删除话题
        $("#fwin_mask_replyForm").removeClass();
        $("#fwin_dialog_replyForm").hide();
        $("#fwin_mask_replyForm").hide();
        $(".deleteTopic").attr('tid', 0);
        $("#fwin_dialog_confirmBox").hide();
        $(".perPop").hide();
    });

    $(".cancelReply").click(function () {//放弃删除回复
        $("#fwin_mask_replyForm").removeClass();
        $("#fwin_dialog_replyForm").hide();
        $("#fwin_mask_replyForm").hide();
        $(".deleteTopic").attr('replyid', 0);
        $("#confirmBox").hide();
    });
    $(".deleteTopic").click(function () {//删除帖子
        var tid = $(this).attr("tid");
        $.post($YSWL.BasePath+"Home/DeleteTopic", { id: tid }, function (resultData) {
            if (resultData == "ok") {//删除成功
                ShowSuccessTip("删除成功");
                $("#t_" + tid).remove();
                $(".cancelDelete").click();
            } else if (resultData == "no") {//删除失败
                ShowFailTip("删除失败，请稍后再试！");
            } else {//未知状态
                ShowServerBusyTip("服务器忙！");
            }
        });
    });

    $(".replyLi").die("click").live("click", function () {
        var userId = $(this).attr("uid"); //此条评论的创建者Id
        var replyId = $(this).attr("replyId"); //此条评论的评论Id
        var nickname = $(this).attr("author"); //此条评论的昵称
        var tid = $(this).attr("tid"); //原话题的Id
        $.get($YSWL.BasePath+"Home/GetCurrentUser", function (resultData) {
            if (parseInt(resultData) > 0) { //已经登录
                if (resultData == userId) {//此回复是登录用户自己回复的  显示删除
                    //显示遮罩层
                    $("#fwin_mask_replyForm").show();
                    $("#fwin_mask_replyForm").addClass("g-mask");
                    $("#fwin_mask_replyForm").height($("#body").height() + 100);
                    //显示删除
                    $(".deleteReply").attr('replyid', replyId); //传评论Id
                    $("#confirmBox").show();
                } else {//回复给别人
                    $("#content").attr("placeholder", "回复：" + nickname);
                    $("#fwin_mask_replyForm").show();
                    $("#fwin_mask_replyForm").addClass("g-mask");
                    $("#fwin_mask_replyForm").height($("#body").height() + 100);
                    $("#fwin_dialog_replyForm").show();
                    $(".sendReplyBtn").attr({ "userid": userId, "itemid": tid, "to": nickname });
                    //                        $(".sendReplyBtn").attr("userid", userId);
                    //                        $(".sendReplyBtn").attr("itemid", tid);
                }
            }
        });
    });

    $(".deleteReply").die("click").live("click", function () {//删除评论
        var replyId = $(this).attr("replyid");
        $.post($YSWL.BasePath+"Home/DeleteReply", { id: replyId }, function (resultData) {
            if (resultData == "ok") {//删除成功
                ShowSuccessTip("删除成功");
                $("#r_" + replyId).remove();
                $(".cancelReply").click();
            } else if (resultData == "no") {//删除失败
                ShowFailTip("删除失败，请稍后再试！");
            } else {//未知状态
                ShowServerBusyTip("服务器忙！");
            }
        });
    });


})