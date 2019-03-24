<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main_index_dlb.aspx.cs" Inherits="ColoPay.Web.Admin.main_index_dlb" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>银河支付框架</title>
    <script src="/admin/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <link href="/admin/css/admin.css" rel="stylesheet" type="text/css" />
    <link href="/admin/js/easyui/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="css/reset_index.css" rel="stylesheet" />
    <link href="css/main_index.css" rel="stylesheet" />
    <script src="/Scripts/Highcharts/highcharts.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function addTab(title, url) {
            if ($('#tabs').tabs('exists', title)) {
                $('#tabs').tabs('select', title); //选中并刷新
                var currTab = $('#tabs').tabs('getSelected');
                var url = $(currTab.panel('options').content).attr('src');
                if (url != undefined && currTab.panel('options').title != '我的桌面') {
                    $('#tabs').tabs('update', {
                        tab: currTab,
                        options: {
                            content: createFrame(url)
                        }
                    });
                }
            } else {
                var index = $('#tabs').find(".tabs").find("li").length;
                if (index == 10) {
                    alert("开启的菜单太多，请先关闭部分菜单！");
                    return;
                }
                var content = createFrame(url);
                $('#tabs').tabs('add', {
                    title: title,
                    content: content,
                    closable: true
                });
            }
            tabClose();
        }
        function createFrame(url) {
            var s = '<iframe scrolling="auto" frameborder="0"  border="0px" src="' + url + '" style="width:100%;height:99%;"></iframe>';
            return s;
        }

        function tabClose() {
            /*双击关闭TAB选项卡*/
            $(".tabs-inner").dblclick(function () {
                var subtitle = $(this).children(".tabs-closable").text();
                $('#tabs').tabs('close', subtitle);
            });
            /*为选项卡绑定右键*/
            $(".tabs-inner").bind('contextmenu', function (e) {
                $('#mm').menu('show', {
                    left: e.pageX,
                    top: e.pageY
                });

                var subtitle = $(this).children(".tabs-closable").text();

                $('#mm').data("currtab", subtitle);
                $('#tabs').tabs('select', subtitle);
                return false;
            });
        }
        //绑定右键菜单事件
        function tabCloseEven() {
            //刷新
            $('#mm-tabupdate').click(function () {
                var currTab = $('#tabs').tabs('getSelected');
                var url = $(currTab.panel('options').content).attr('src');
                if (url != undefined && currTab.panel('options').title != '我的桌面') {
                    $('#tabs').tabs('update', {
                        tab: currTab,
                        options: {
                            content: createFrame(url)
                        }
                    })
                }
            })
            //关闭当前
            $('#mm-tabclose').click(function () {
                var currtab_title = $('#mm').data("currtab");
                $('#tabs').tabs('close', currtab_title);
            })
            //全部关闭
            $('#mm-tabcloseall').click(function () {
                $('.tabs-inner span').each(function (i, n) {
                    var t = $(n).text();
                    if (t != '我的桌面') {
                        $('#tabs').tabs('close', t);
                    }
                });
            });
            //关闭除当前之外的TAB
            $('#mm-tabcloseother').click(function () {
                var prevall = $('.tabs-selected').prevAll();
                var nextall = $('.tabs-selected').nextAll();
                if (prevall.length > 0) {
                    prevall.each(function (i, n) {
                        var t = $('a:eq(0) span', $(n)).text();
                        if (t != '我的桌面') {
                            $('#tabs').tabs('close', t);
                        }
                    });
                }
                if (nextall.length > 0) {
                    nextall.each(function (i, n) {
                        var t = $('a:eq(0) span', $(n)).text();
                        if (t != '我的桌面') {
                            $('#tabs').tabs('close', t);
                        }
                    });
                }
                return false;
            });
            //关闭当前右侧的TAB
            $('#mm-tabcloseright').click(function () {
                var nextall = $('.tabs-selected').nextAll();
                if (nextall.length == 0) {
                    //msgShow('系统提示','后边没有啦~~','error');
                    alert('后边没有啦~~');
                    return false;
                }
                nextall.each(function (i, n) {
                    var t = $('a:eq(0) span', $(n)).text();
                    $('#tabs').tabs('close', t);
                });
                return false;
            });
            //关闭当前左侧的TAB
            $('#mm-tabcloseleft').click(function () {
                var prevall = $('.tabs-selected').prevAll();
                if (prevall.length == 0) {
                    alert('到头了，前边没有啦~~');
                    return false;
                }
                prevall.each(function (i, n) {
                    var t = $('a:eq(0) span', $(n)).text();
                    $('#tabs').tabs('close', t);
                });
                return false;
            });

            //退出
            $("#mm-exit").click(function () {
                $('#mm').menu('hide');
            });
        }

        function wcfClick() {
            //addTab(title, href);
            $("#btnWcf").click();
        }

        $(function () {
            tabCloseEven();
            $('#tabIndex').find('a').click(function () {
                var $this = $(this);
                var href = $this.attr('src');
                var title = $this.text();
                addTab(title, href);
            });
        });

    </script>
    
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
        <div region="center" id="mainPanle">
            <div id="tabs" class="easyui-tabs" fit="true" border="false">
                <div title="我的桌面">
                    <div id="tabIndex" style="overflow: scroll; overflow-x: hidden;  height: 100%; width: 100%;">
                        <div class="saas_oms_wrap">
                            <div class="saas_oms_content1">
                                <div class="saas_oms_item mr">
                                    <div class="saas_oms_text">
                                        <table class="item_table">
                                            <tr>
                                                <td class="tc">
                                                    <img src="images/oms_dingdan.png" alt="">
                                                    <asp:Label ID="lblTime" runat="server" class="time" Enabled="False"></asp:Label>
                                                </td>
                                                <td>
                                                    <h3 class="color_orange">今日订单</h3>
                                                    <span class="color_orange" style="font-Size: 22px;">&nbsp;<asp:Label ID="lblOrderToDay" runat="server" Text=""></asp:Label>
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <table class="table_text">
                                                        <tr>
                                                            <td>昨日订单</td>
                                                            <td><asp:Label ID="lblOrderYest" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>七日订单</td>
                                                            <td><asp:Label ID="lblOrderWeek" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>本月订单</td>
                                                            <td><asp:Label ID="lblOrderMon" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="saas_oms_item mr">
                                    <div class="saas_oms_text">
                                        <table class="item_table">
                                            <tr>
                                                <td class="tc">
                                                    <img src="images/oms_xiaoshoue.png" alt="">
                                                </td>
                                                <td>
                                                    <h3 class="color_orange">今日交易额</h3>
                                                    <span class="color_orange" style="font-Size: 22px;">&nbsp;<asp:Label ID="lblSaleToDay" runat="server" Text=""></asp:Label>
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <table class="table_text">
                                                        <tr>
                                                            <td>昨日交易额</td>
                                                            <td><asp:Label ID="lblSaleYest" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>七日交易额</td>
                                                            <td><asp:Label ID="lblSaleWeek" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>本月交易额</td>
                                                            <td><asp:Label ID="lblSaleMon" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="saas_oms_item">
                                    <div class="saas_oms_text">
                                        <table class="item_table">
                                            <tr>
                                                <td class="tc">
                                                    <img src="images/oms_xiaoshoue.png" alt="">
                                                </td>
                                                <td>
                                                    <h3 class="color_orange">今日佣金</h3>
                                                     <span class="color_orange" style="font-Size: 22px;">&nbsp;<asp:Label ID="lblFeeToDay" runat="server" Text=""></asp:Label>
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <table class="table_text">
                                                        <tr>
                                                            <td>昨日佣金</td>
                                                            <td><asp:Label ID="lblFeeYest" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>七日佣金</td>
                                                            <td><asp:Label ID="lblFeeWeek" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>本月佣金</td>
                                                            <td><asp:Label ID="lblFeeMon" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                      
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="saas_oms_content2">
                                <div class="saas_title">
                                    <h2 class="color_grey saas_title_h2">业务简报</h2>
                                </div>
                                <div class="saas_oms_chart" id="orderCount">

                                </div>
                            </div>

                            <div class="saas_oms_content2">
                               
                                <div class="saas_title">
                                    <h2 class="color_grey saas_title_h2">交易额排行榜</h2>
                                </div>
                                <div class="saas_oms_chart" id="orderTop">

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
          <asp:HiddenField ID="createdDate" runat="server" />
          <asp:HiddenField ID="amount" runat="server" />
          <asp:HiddenField ID="amountFee" runat="server" />

          <asp:HiddenField ID="enterOrder" runat="server" />
        <asp:HiddenField ID="enterAmount" runat="server" />
        <asp:HiddenField ID="enterFee" runat="server" />
    </form>
    <div id="mm" class="easyui-menu" style="width: 120px;">
        <div id="mm-tabupdate">
            刷新
        </div>
        <div class="menu-sep">
        </div>
        <div id="mm-tabclose">
            关闭
        </div>
        <div id="mm-tabcloseother">
            关闭其他
        </div>
        <div id="mm-tabcloseall">
            关闭全部
        </div>
    </div>
</body>
</html>
<script type="text/javascript">
        $(document).ready(function () {

            //走势图
            var hfCategoryVal = $("[id$='createdDate']").val();
            if (hfCategoryVal.length <= 0) {
                return;
            }
            //hfCategoryVal = hfCategoryVal;
            var categories = hfCategoryVal.split(','); 
            var dayAmount = [];
              var dayFee = [];

            var datavalue = $("[id$='amount']").val().split(',');
            for (var i = 0; i < datavalue.length; i++) {
                var item = parseFloat(datavalue[i]);
                dayAmount.push(item);
            }

            var dataFee = $("[id$='amountFee']").val().split(',');
            for (var i = 0; i < dataFee.length; i++) {
                var item = parseFloat(dataFee[i]);
                dayFee.push(item);
            }



            $('#orderCount').highcharts({
                chart: {
                    type: 'line'
                },
                title: {
                    text: '交易额/佣金统计'
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    categories: categories
                },
                yAxis: {
                    title: {
                        text: '金额'
                    },
                    min:0
                },
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.x + '：</b>' + this.y + '元';
                    }
                },
                plotOptions: {
                    line: {
                        dataLabels: {
                            enabled: true
                        },
                        enableMouseTracking: true
                    }
                },
                series: [{
                    name: '交易额',
                    data: dayAmount
                },
                    {
                    name: '佣金',
                    data: dayFee
                }
                ]
            });

            $("#priceCount text:last").hide();
            $("#priceCount span:last").hide();

            //if ($('#priceCount .highcharts-tracker rect').length > 0) {
            //    if (parseInt($('#priceCount .highcharts-tracker rect').eq(0).attr('width')) > 25) {
            //        $('#priceCount .highcharts-tracker rect').css('width', '25px');
            //    }
            //}

            //控制线条宽度   最高为25  
            var rec_s = $('#priceCount .highcharts-tracker rect');
            if (rec_s.length > 0) {
                var rect_width = parseInt(rec_s.eq(0).attr('width'));
                var x_ = (rect_width - 25) / 2;//计算新的X轴位置
                if (rect_width > 25) {
                    for (var i = 0; i < rec_s.length; i++) {
                        rec_s.eq(i).css('width', '25px').attr('x', parseFloat(rec_s.eq(i).attr('x')) + x_);
                    }
                }
            }

            //排行版
            var hfEnterOrderVal = $("[id$='enterOrder']").val();
            if (hfEnterOrderVal.length <= 0) {
                return;
            }
            //hfCategoryVal = hfCategoryVal;
            var enterOrder = hfEnterOrderVal.split(','); 
            var enterAmount = [];
            var enterFee = [];

            var amountvalue = $("[id$='enterAmount']").val().split(',');
            for (var i = 0; i < amountvalue.length; i++) {
                var item = parseFloat(amountvalue[i]);
                enterAmount.push(item);
            }

            var dataEnterFee = $("[id$='enterFee']").val().split(',');
            for (var i = 0; i < dataEnterFee.length; i++) {
                var item = parseFloat(dataEnterFee[i]);
                enterFee.push(item);
            }

             $('#orderTop').highcharts({
                chart: {
                    type: 'bar'
                },
                title: {
                    text: '交易额排行榜'
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    categories: enterOrder
                },
                yAxis: {
                    title: {
                        text: '商家名称'
                    },
                    min:0
                },
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.x + '：</b>' + this.y + '元';
                    }
                },
                plotOptions: {
                    line: {
                        dataLabels: {
                            enabled: true
                        },
                        enableMouseTracking: true
                    }
                },
                series: [{
                    name: '交易额',
                    data: enterAmount
                },
                    {
                    name: '佣金',
                    data: enterFee
                }
                ]
            });

        });
    </script>
