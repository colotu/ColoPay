$(function () {
    $(".replyUser").die("click").live("click", function () {
        if (CheckUserLogin()) {
            $("#fwin_mask_replyForm").show();
            $("#fwin_mask_replyForm").addClass("g-mask");
            $("#fwin_mask_replyForm").height($("#body").height() + 100);
            $("#fwin_dialog_replyForm").show();
            $(".sendReplyBtn").attr("itemid", $(this).attr("tid"));
            $(".").attr("userid", $(this).attr("uid"));
        };
    });
});