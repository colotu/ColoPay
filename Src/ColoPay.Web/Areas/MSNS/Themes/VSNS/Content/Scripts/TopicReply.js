
function insertsmilieToCom(sender, smilieface) {
    $(sender).parents(".pinglunkuang").find("input").val($(sender).parents(".pinglunkuang").find("input").val() + smilieface);
    // $("[id$='contentWeibo']")
    $(".cbiaoqing").hide();
}
$(function () {

    $(".addFav").die("click").live("click", function () {//添加收藏
        var TopicId = $(this).attr("topicid");
        if (CheckUserLogin()) {
            $.ajax({
                url: $YSWL.BasePath + "profile/AjaxAddTopicToFav",
                type: 'post',
                data: { TopicId: TopicId },
                success: function (resultData) {
                    if (resultData == "Repeate") {
                        ShowServerBusyTip('您已经收藏过了！');
                    } else if (resultData == "Yes") {
                        ShowSuccessTip('收藏成功！');
                    } else {
                        ShowServerBusyTip('服务器异常，请稍后再试！');
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    ShowFailTip("操作失败：" + errorThrown);
                }
            });
        }
    });







    $(".threadReply").die("click").live("click", function () {
        if (CheckUserLogin()) {
            $("#fwin_mask_replyForm").show();
            $("#fwin_mask_replyForm").addClass("g-mask");
            $("#fwin_mask_replyForm").height($("#body").height() + 100);
            $("#fwin_dialog_replyForm").show();
            $(".sendReplyBtn").attr("itemid", $(this).attr("itemid"));
        };
    });
    $(".cancelBtn").click(function () {//回帖取消按钮
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
    $(".sendReplyBtn").die("click").live("click", function () {
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
            success: function (resultData) {
                if (resultData.status == "ok") {//评论成功
                    if (to != "") { //回复给别人
                        $(".replyUl[itemid='" + ReplyId + "']").append("<li class='replyLi' id='r_" + resultData.repId + "' author='" + resultData.name + "' replyId='" + resultData.repId + "' uid='" + resultData.replyUserId + "' tid='" + resultData.topicId + "'><a href='javascript:;' class='sW fl'><span>" + resultData.name + "  回复  " + to + "：</span><span class='replyContent' replyId='" + resultData.repId + "'>" + resultData.Des + "</span></a></li>");
                    } else {
                        $(".replyUl[itemid='" + ReplyId + "']").append("<li class='replyLi' id='r_" + resultData.repId + "' author='" + resultData.name + "' replyId='" + resultData.repId + "' uid='" + resultData.replyUserId + "' tid='" + resultData.topicId + "'><a href='javascript:;' class='sW fl'><span>" + resultData.name + "：</span><span class='replyContent' replyId='" + resultData.repId + "'>" + resultData.Des + "</span></a></li>");
                    }
                    $(".sendReplyBtn").attr("to", "");
                    $(".replyCount[itemid='" + ReplyId + "']").text(parseInt($(".replyCount[itemid='" + ReplyId + "']").text()) + 1);
                    $(".cancelBtn").click();
                    $("#content").val('');
                    $("#pText").text(140);
                    $("#content").attr('readonly', false);
                } else if (resultData.status == "no") {//评论失败
                    ShowFailTip("操作失败，请稍候重试！");
                } else {
                    ShowServerBusyTip("服务器忙，请重试！");
                }

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowFailTip("操作失败：" + errorThrown);
            }
        });

    });
    // Upload("#UploadPhoto");
    $("#addProduct").click(addProduct);
    $("#LoadUrlShow").click(function () { $("#LoadProductWindow").fadeIn(300) });
    $("#LoadUrlClose").click(function () { $("#LoadProductWindow").fadeOut(300) });
    $("#biaoqingclose").click(function () { $("#tbiaoqing").hide(); });
    $(".biaoqingshow").click(function (e) {
        e.preventDefault();
        $("#tbiaoqing").slideToggle(0);
    });
    $("#CancelImage").click(function (e) {
        e.preventDefault();
        $("#yulanImage").fadeOut(300);
    });
    $("#PostReply").click(function () {
        var Des = $("#contentTopic").val();
        var GroupId = $("#GroupId").val();
        var TopicId = $("#TopicId").val();
        var ObjectThis = $(this);
        if (Des == "") {
            $.jBox.tip('请填写内容', 'error');
            return;
        }
        if (CheckUserState()) {
            $.jBox.tip("发布中，请稍后...", 'loading');
            $.ajax({
                url: $YSWL.BasePath + "profile/AJaxCreateTopicReply",
                type: 'post',
                dataType: 'text',
                timeout: 10000,
                data: { ImageUrl: ImageUrl, Des: Des, Pid: Pid, GroupId: GroupId, TopicId: TopicId },
                success: function (resultData) {
                    if (resultData == "No") {
                        //                            ShowFailTip("操作失败，请您重试！");
                        $.jBox.tip("出现异常请重试...", 'error');
                    } else {
                        var mediaIds = "";
                        if (ObjectThis.parents(".whole_fom").find(".isSendAll").attr('checked') != undefined) {
                            mediaIds = "-1";
                        } else {
                            var i = 0;
                            ObjectThis.parents("whole_fom").find(".bind>span").each(function () {
                                if ($(this).attr("s_type") == "1" && $(this).attr("value") != "") {
                                    if (i == 0) {
                                        mediaIds = $(this).attr("value");
                                    } else {
                                        mediaIds = mediaIds + "," + $(this).attr("value");
                                    }
                                    i++;
                                }
                            });
                        }
                        var ReplyId = $(resultData).find(".replyTopic").attr("replyid");

                        //同步到微博
                        var Option = {
                            ShareDes: Des,
                            ImageUrl: ImageUrl,
                            TopicID: TopicId,
                            ReplyId: ReplyId,
                            mediaIds: mediaIds
                        };
                        InfoSync.InfoSending(Option);
                        $("#yulanImage").fadeOut(300);
                        ImageUrl = "";
                        Pid = "";
                        $("#contentTopic").val("");
                        $("#YSWLTopicReply").prepend(resultData);
                        $.jBox.tip("发布成功...", 'success');


                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    ShowFailTip("操作失败：" + errorThrown);
                }
            });
        }
    });
});
function Upload(control) {
    var uploadbutton = $(control).html();
    var templatehtml = '<div class="qq-uploader span12">' +
            '<pre class="qq-upload-drop-area span12"><span>{dragZoneText}</span></pre>' +
            '<div class="qq-upload-button btn btn-success" style="width: auto;padding-top: 0px;auto: hidden;background:#fff;height: 32px">{uploadButtonText}</div>' +
            '<span class="qq-drop-processing"><span>{dropProcessingText}</span><span class="qq-drop-processing-spinner"></span></span>' +
            '<ul class="qq-upload-list" style=" text-align: center; "></ul>' +
            '</div>';

    var uploader = new qq.FineUploader({
        element: $(control)[0],
        request: {
            endpoint: '/Upload/SNSUploadTmpImg.aspx'
        },
        text: {
            uploadButton: uploadbutton
        },
        template: templatehtml,
        multiple: false,
        validation: {
            allowedExtensions: ['jpeg', 'jpg', 'gif', 'png']
        },
        callbacks: {
            onComplete: function (id, fileName, responseJSON) {
                $(".btn-success").find("input").css("height", "28px").css("width", "50px").css("font-size", "12px");
                if (responseJSON.success) {
                    $(".qq-upload-list").hide();
                    $(".btn-success").find("input").css("height", "28px");
                    $.jBox.tip('上传成功', 'success');
                    ImageUrl = responseJSON.data;
                    $("#yulantu").attr("src", responseJSON.data.format("T116x170_"));
                    $("#yulanImage").fadeIn(300);
                } else {
                    ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试：");
                }
            }
        }
    });
}
function addProduct() {
    if (CheckUserState()) {
        var LinkUrl = $("#ProductLink").val() + "&";
        ImageUrl = "";
        var urlreg = /http(s)?:\/\/([\w-]+\.)+[\w-]+(\/[\w- .\/?%&=]*)?/;
        if (LinkUrl && LinkUrl.length > 0 &&
                        !urlreg.test(LinkUrl)) {
            $.jBox.tip('请输入正确的链接', 'success');
            return false;
        }
        $.jBox.tip("努力给您获取中...", 'loading');
        $.ajax({
            url: $YSWL.BasePath + "profile/AjaxGetProductInfo",
            type: 'post', dataType: 'text', timeout: 10000,
            data: { ProductLink: LinkUrl },
            success: function (resultData) {
                if (resultData == "No") {
                    $.jBox.tip('亲，获取失败，如果一直不成功，请联系管理员检查淘宝设置是否正确', 'error');
                }
                else {
                    var Datas = resultData.split("|");
                    Pid = Datas[0];
                    ImageUrl = Datas[1];
                    $("#yulantu").attr("src", ImageUrl);
                    $("#yulanImage").fadeIn(300);
                    $.jBox.tip('获取成功', 'success');
                    $("#LoadProductWindow").fadeOut(300)
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.jBox.tip('出现异常', 'error');
            }
        });
    }
}
function insertsmilie(smilieface) {
    $("[id$='contentTopic']").val($("[id$='contentTopic']").val() + smilieface);
    $("#tbiaoqing").hide();
}

//检查是否登录
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
                window.location = $YSWL.BasePath+"Account/Login";
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
