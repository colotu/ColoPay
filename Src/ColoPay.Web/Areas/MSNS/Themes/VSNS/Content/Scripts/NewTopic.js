var ImageUrl = "";
var Pid = "";
var GroupId = $("#groupid").val();
var isJoin = false;
var CheckUserIsJoinGroup = function() {

    $.ajax({
        url: $YSWL.BasePath+"home/AJaxCheckUserIsJoinGroup",
        type: 'post',
        dataType: 'text',
        timeout: 10000,
        data: { GroupId: GroupId },
        async: false,
        success: function(resultData) {
            if (resultData == "joined") {
                isJoin = true;
                ShowSuccessTip('您已经加入');

            } else {
                isJoin = false;
                $.jBox.confirm("确定加入此小组确定吗？", "提示", submit);
            }
        },
        error: function(XMLHttpRequest, textStatus, errorThrown) {
          
        }
    });
    return isJoin;
};

var submitJoin = function (v, h, f) {
    if (v == 'ok') {
        $.ajax({
            url: $YSWL.BasePath + "Home/AjaxJoinGroup",
            type: 'post', dataType: 'text', timeout: 10000,
            async: false,
            data: { GroupId: GroupId },
            success: function (resultData) {
                if (resultData == "joined") {
                    isJoin = true;
                    ShowSuccessTip('您已经加入');
                }
                else if (resultData == "Yes") {
                    isJoin = true;
                    ShowSuccessTip('您已经成功加入');
                }
                else { isJoin = false; ShowFailTip('出现异常', 'error'); }

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowFailTip("操作失败：" + errorThrown);
            }
        });
    }
    return isJoin;
};


$(function () {
    
    Upload("#UploadPhoto");
    $("#closeImg").click(function () {
        $(this).hide();
        $(".ImgSrc").fadeOut();
        ImageUrl = null;
        $(".attchImg").attr('src', '/Areas/MSNS/Themes/VSNS/Content/images/addpic.png');
    });

    $("#SubmitTopic").click(function () {
        var groupId = $("#groupid").val();
        $.post($YSWL.BasePath + "home/AJaxCheckUserIsJoinGroup", { GroupId: groupId }, function(resultData) {
            if (resultData != "joined") {
                ShowFailTip("请先加入小组！");
                return;
            }
        });

        var length = $("#contentLength").val();
        // var Title = $("#titleTopic").val();
        var Des = $('#content').val();
        if (Des == "") {
            ShowFailTip('请填写完整！');
            return;
        }
        if (length) {
            if (Des.length < length) {
            ShowFailTip("内容不能小于"+length+"个字！");
            return;
        }  
        }

        if (ContainsDisWords(Des)) {
            ShowFailTip('您输入的内容含有禁用词，请重新输入！');
            return;
        }
        var GroupId = $.getUrlParam("GroupId");
        $.ajax({
            url: $YSWL.BasePath+"Home/AJaxCreateTopic",
            type: 'post',
            dataType: 'text',
            timeout: 10000,
            data: { ImageUrl: ImageUrl, Des: Des, Pid: Pid, GroupId: GroupId },
            success: function (resultData) {
                if (resultData == "No") {
                    ShowFailTip('发贴失败，请重试一下！');
                    window.location.reload();
                } else {
                    var mediaIds = "";
                    $("#content").attr('readonly', false);
                    if ($(".isSendAll").attr('checked') != undefined) {
                        mediaIds = "-1";
                    } else {
                        var i = 0;
                        $(".bind>span").each(function () {
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
                    window.location = $YSWL.BasePath+"Home/Index";
                }

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowFailTip('发贴失败，请重试一下！');
                window.location.reload();
            }
        });
    });
});
function Upload(control) {
    $(".btn-success").find("input").css("height", "28px");
    var uploadbutton = $(control).html();
var templatehtml = '<div class="qq-uploader span12">' +
            '<pre class="qq-upload-drop-area span12"><span>{dragZoneText}</span></pre>' +
            '<div class="qq-upload-button btn btn-success" style="width: auto;padding-top: 0px;background:#fff;">{uploadButtonText}</div>' +
            '<span class="qq-drop-processing"><span>{dropProcessingText}</span><span class="qq-drop-processing-spinner"></span></span>' +
            '<ul class="qq-upload-list" style=" text-align: center; "></ul>' +
            '</div>';
var uploader = new qq.FineUploader({
    element: $(control)[0],
    request: {
        endpoint: '/Upload/SNSUploadTmpImg.aspx'
    },
    text: {
        uploadButton: uploadbutton,
        waitingForResponse: "\r处理中", dragZone: "上传", dropProcessing: "正在上传，请稍候..."
    },
    multiple: false,
    template: templatehtml,
    validation: {
        allowedExtensions: ['jpeg', 'jpg', 'gif', 'png'],
        sizeLimit: 10485760 // 10M = 10 * 1024*1024 bytes
    },
    callbacks: {
        message: {
            typeError: '玩儿我呢……'
        },
        onComplete: function (id, fileName, responseJSON) {
            $(".btn-success").find("input").css("height", "28px").css("width", "50px").css("font-size", "12px");
            if (responseJSON.success) {
                $(".qq-upload-list").hide();
                ImageUrl = responseJSON.data;
                $(".attchImg").attr("src", ImageUrl.format('T60X60_')); // before('  <li><div class="photoCut"><img src="' + ImageUrl.format('T60X60_') + '" class="attchImg" alt="photo"></div><a href="javascript:;" class="cBtn spr db " title="" >关闭</a></li>');
                $("#closeImg").show();
            } else {
                ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试：");
            }
        }
    }, message: {
        typeError: function () {
            ShowFailTip("文件格式错误！");
        }
    }

});
}
function addProduct() {
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
                $("#LoadProductWindow").fadeOut(300);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.jBox.tip('出现异常', 'error');
        }
    });
}

function insertsmilie(smilieface) {
    $("[id$='content']").val($("[id$='content']").val() + smilieface);
    $("#tbiaoqing").hide();
}
