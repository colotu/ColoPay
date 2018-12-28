function Upload(control, type) {
    $(".btn-success").find("input").css("height", "28px");
    Type = type;
    var multiple = true;
    var uploadbutton = "请选择图片";
    var templatehtml;
    var extensions = ['jpeg', 'jpg', 'gif', 'png'];
    templatehtml = '<div class="qq-uploader span12">' +
        '<pre class="qq-upload-drop-area span12"><span>{dragZoneText}</span></pre>' +
        '<div class="qq-upload-button btn-success" style="background: #04a3ff;float: none;">{uploadButtonText}</div>' +
        '<span class="qq-drop-processing"><span>{dropProcessingText}</span><span class="qq-drop-processing-spinner"></span></span>' +
        '<ul class="qq-upload-list" style="margin-top: 10px; text-align: center;"></ul>' +
        '</div>';
    if (type == "Image") {
        multiple = false;
        uploadbutton = $("#UploadImage").html();
        templatehtml = '<div class="qq-uploader span12">' +
            '<pre class="qq-upload-drop-area span12"><span>{dragZoneText}</span></pre>' +
            '<div class="qq-upload-button btn btn-success" style="width: auto;padding-top: 0px;background:#f7f7f7;">{uploadButtonText}</div>' +
            '<span class="qq-drop-processing"><span>{dropProcessingText}</span><span class="qq-drop-processing-spinner"></span></span>' +
            '<ul class="qq-upload-list" style="margin-top: 0px; text-align: center; "></ul>' +
            '</div>';
    }

    var endpoint = "/Upload/SNSUploadTmpImg.aspx";
    if (type == "SitePro") {
        endpoint = "/ProductUploadImg.aspx";
    }
    if (type == "SiteProEx") {
        uploadbutton = $(control).html();
        templatehtml = '<div class="qq-uploader SiteProEx span12">' +
            '<pre class="qq-upload-drop-area span12"><span>{dragZoneText}</span></pre>' +
            '<div class="qq-upload-button btn btn-success" style="width: auto;padding-top: 0px;background:#f7f7f7;">{uploadButtonText}</div>' +
            '<span class="qq-drop-processing"><span>{dropProcessingText}</span><span class="qq-drop-processing-spinner"></span></span>' +
            '<ul class="qq-upload-list" style="margin-top: 0px; text-align: center; "></ul>' +
            '</div>';
        endpoint = "/Upload/ShopUploadFile.aspx";
        extensions = ['DAE', 'OBJ', 'STL', 'X3D', 'X3DB', 'X3DV', 'WRL'];
        //DAE,OBJ,STL,X3D,X3DB X3DV,WRL
    }
    var uploader = new qq.FineUploader({
        element: $(control)[0],
        request: {
            endpoint: endpoint
        },
        text: {
            uploadButton: uploadbutton,
            waitingForResponse: "\r处理中", dragZone: "上传", dropProcessing: "正在上传，请稍候..."
        },
        template: templatehtml,
        multiple: multiple,
        validation: {
            allowedExtensions: extensions
        },
        callbacks: {
            onProgress: function (id, fileName, loaded, total) {
                ShowLoadingTip("正在上传，请稍候……");
            },
            onComplete: function (id, fileName, responseJSON) {
                VideoUrl = "";
                AudioUrl = "";
                PostExUrl = "";
                $(".qq-upload-list").hide();
                $(".btn-success").css("overflow", "");
                $(".btn-success").find("input").css("height", "28px").css("width", "50px").css("font-size", "12px");
                if (type == "SitePro") {
                    $(".btn-success").find("input").css("height", "28px").css("width", "120px").css("font-size", "12px");
                    $("#txtBtnPost").show();
                    $("#UploadSiteProWindow").hide();
                    $("#SiteProBase").show();
                    if (responseJSON.success) {
                        $("#SiteProResultTemplate").load("/profile/AjaxGetSiteProResult?image=" + responseJSON.data.format("T128X130_") + "&data=" + responseJSON.data, { limit: 25 },
                     function () {
                         $("#SiteProResultWindow").append($("#SiteProResultTemplate").html());
                         if ($("#SiteProResultWindow").find('.SiteProlist').length > 0) {
                             $(".lyz_tab_left").css({ "background-image": "none" });
                             $(".lyz_tab_right").css({ "background-image": "none",
                                 "background-color": "#f7f7f7",
                                 "border-style": "solid", "border-width": "1px", "width": "603px",
                                 "border-color": "#ccc"
                             });
                         }
                     });
                    }
                    else {
                        $("#txtBtnPost").hide();
                        ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试：");
                    }
                }
                else if (type == "Image") {
                    if (responseJSON.success) {
                        ImageUrl = responseJSON.data;
                        $("#yulantuimage").attr("src", responseJSON.data.format("T116x170_"));
                        $("#yulantu").fadeIn(300);
                        $("#contentWeibo").val($("#contentWeibo").val() + "分享图片");
                        resizeImg('#yulantu', 211, 1280);
                    }
                }
                else if (type == "SiteProEx") {
                    if (responseJSON.success) {
                        ShowSuccessTip("上传附件成功！");
                        $("#hfSiteProExPath").val(responseJSON.path);
                        var name = $("#hfSiteProExNames").val();
                        if (name == "") {
                            $("#hfSiteProExNames").val(responseJSON.name);
                        } else {
                            $("#hfSiteProExNames").val(name + "|" + responseJSON.name);
                        }
                        $(".ProExNames").append("<li>" + responseJSON.name + "</li>");
                    }
                } else {
                    ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试：");
                }
            }
        }
    });
}

$(function() {
    Upload("#UploadImage", "Image");
});