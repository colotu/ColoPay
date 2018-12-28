var ImageUrl = "";
var Pid = "";

$(function () {
    Upload("#UploadPhoto");
    $("#closeImg").click(function () {
        $(this).hide();
        $(".ImgSrc").fadeOut();
        ImageUrl = null;
        $(".attchImg").attr('src', '/Areas/MSNS/Themes/VSNS/Content/images/addpic.png');
    });

    $("#SubmitTopic").click(function () {
        // var Title = $("#titleTopic").val();
        var Des = $('#content').val();
        if (Des == "") {
            $.jBox.tip('请填写完整', 'error');
            return;
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
                    window.location = "/";
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
        //waitingForResponse: "\r处理中", dragZone: "上传", dropProcessing: "正在上传，请稍候..."
    },
    multiple: false,
    template: templatehtml,
    validation: {
        allowedExtensions: ['jpeg', 'jpg', 'gif', 'png'],
        sizeLimit: 10485760 // 10M = 10 * 1024*1024 bytes
    },
    callbacks: {
        message: {
            typeError: 'shit!玩儿我呢……'
        },
        onProgress:  function(id, fileName, loaded, total) {
            $(".qq-upload-list").hide(); $("#uploadify-queue").hide();
        },
        onComplete: function (id, fileName, responseJSON) {
            $(".btn-success").find("input").css("height", "28px").css("width", "50px").css("font-size", "12px");
            if (responseJSON.success) {
                $(".qq-upload-list").hide();
                var photoUrl = responseJSON.data;
                $(".attchImg").attr("src", photoUrl.format('T60X60_'));
                var t_x = 0;
                var t_y = 0;
                var t_w = 60;
                var t_h = 60;
                var t_name = photoUrl.format('T60X60_');
                $.ajax({
                    url: $YSWL.BasePath + "UserCenter/CutGravatar",
                    type: 'post',
                    dataType: 'text',
                    data: { Action: 'Cut', filename: t_name, x: t_x, y: t_y, w: t_w, h: t_h },
                    success: function (resultData) {
                        if (resultData != "success") {
                            ShowFailTip("更换头像失败，请稍后再试！");
                        } else {
                            window.location.reload();
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        ShowServerBusyTip("服务器忙，请稍后再试！");
                    }
                });


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



