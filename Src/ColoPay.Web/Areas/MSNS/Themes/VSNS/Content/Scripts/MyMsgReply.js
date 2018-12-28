$(function() {
    $(".msgReply").die("click").live("click", function() {
        var userId = $(this).attr("uid"); //此条评论的创建者Id
        var replyId = $(this).attr("replyId"); //此条评论的评论Id
        var nickname = $(this).attr("author"); //此条评论的昵称
        var tid = $(this).attr("tid"); //原话题的Id
        $("#content").attr("placeholder", "回复：" + nickname);
        $("#fwin_mask_replyForm").show();
        $("#fwin_mask_replyForm").addClass("g-mask");
        $("#fwin_mask_replyForm").height($("#body").height() + 100);
        $("#fwin_dialog_replyForm").show();
        $(".sendReplyBtn").attr({ "userid": userId, "itemid": tid, "to": nickname });
    });

    $(".cancelBtn").click(function() { //回帖取消按钮
        $("#content").val('');
        $("#content").attr("placeholder", "回两句吧...");
        $("#pText").text(140);
        $(".expreSelect").removeClass("expreSelected");
        $(".tipLayer").hide();
        $("#content").attr('readonly', false);
        $("#fwin_mask_replyForm").removeClass();
        $("#fwin_dialog_replyForm").hide();
        $("#fwin_mask_replyForm").hide();
    });
    $(".sendReplyBtn").die("click").live("click", function() {
        var to = $(this).attr("to");
        var ReplyId = $(this).attr("itemid");
        var Des = $("#content").val();
        var userId = $(this).attr("userid");
        if (Des == "") {
            $.jBox.tip('请填写内容', 'error');
            return;
        }
        if (ContainsDisWords(Des)) {
            $.jBox.tip('您输入的内容含有禁用词，请重新输入！', 'error');
            return;
        }

        $.ajax({
            url: $YSWL.BasePath + "profile/AJaxCreateTopicReply",
            type: 'post',
            dataType: 'json',
            timeout: 10000,
            data: { Des: Des, TopicId: ReplyId, userId: userId },
            success: function(resultData) {
                if (resultData.status == "ok") { //评论成功
                    ShowSuccessTip("回复成功");
                    $(".sendReplyBtn").attr("to", "");
                    $(".cancelBtn").click();
                    $("#content").val('');
                    $("#pText").text(140);
                } else if (resultData.status == "no") { //评论失败
                    ShowFailTip("操作失败，请稍候重试！");
                } else {
                    ShowServerBusyTip("服务器忙，请重试！");
                }

            },
            error: function(XMLHttpRequest, textStatus, errorThrown) {
                ShowFailTip("操作失败：" + errorThrown);
            }
        });

    });
    $(".expreSelect").click(function () {
        $(this).toggleClass("expreSelected");
        $(".tipLayer").toggle();
    });
});