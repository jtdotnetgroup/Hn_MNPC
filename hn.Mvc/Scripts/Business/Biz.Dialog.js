//打开数据字典选择窗口
function bindDictionaryDialog(control,category, callBack) {
    control.searchbox({
        searcher: function (value, name) {
            var hDialog = top.jQuery.hDialog({
                title: '选择数据',
                width: document.body.clientWidth / 1.5,
                height: document.body.clientHeight / 1.5,
                href: '/Search/Dialog',
                iconCls: 'icon-add',
                showBtns: false,
                onLoad: function () {
                    top.$('#grid').treegrid({
                        url: '/EasyUiTreeGrid/SYS_SUBDICSByCategory',
                        iconCls: 'icon icon-list',
                        width: document.body.clientWidth / 1.5 - 20,
                        height: document.body.clientHeight / 1.5 - 85,
                        nowrap: false, //折行
                        rownumbers: true, //行号
                        striped: true, //隔行变色
                        idField: 'FID', //主键
                        treeField: 'FNAME',
                        singleSelect: true, //单选
                        frozenColumns: [[]],
                        columns: [[
                            //{ field: 'FID', checkbox: true },
                            { title: '名称', field: 'FNAME', width: 200, sortable: true },
                            { title: '编码', field: 'FNUMBER', width: 140, sortable: true },
                            {
                                title: '状态', field: 'FSTATUS', width: 100, sortable: true, formatter: function (v, d, i) {
                                    return '<img src="/images/' + (v == true ? 'checkmark.gif' : 'checknomark.gif') + '" />';
                                }, align: 'center'
                            },
                            { title: '备注', field: 'FREMARK', width: 300, sortable: true }
                        ]],
                        onDblClickRow: function (rowIndex, rowData) {
                            var row = top.$('#grid').treegrid('getSelected');
                            if (row) {
                                callBack(row);
                                hDialog.dialog('close');
                            }
                        },
                        queryParams: {
                            category: category
                        }                        
                    });

                    top.$("#a_search").click(function () {
                        top.$('#grid').treegrid('load', { category: category, keywords: top.$('#txtKeyword').val() });
                    });

                    top.$("#a_reset").click(function () {
                        top.$('#txtKeyword').textbox('setValue', '');
                        top.$('#grid').treegrid('load', { category: category });
                    });
                },
                toolbar: [{
                    text: '确定',
                    iconCls: 'icon-database_save',
                    handler: function () {
                        var row = top.$('#grid').treegrid('getSelected');
                        if (row) {
                            callBack(row);
                            hDialog.dialog('close');
                        }
                    }
                }, {
                    text: '关闭',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        hDialog.dialog("close");
                    }
                }]
            });
        },
        prompt: ''
    });
}

function bindOrderDialog(control, callBack) {
    control.searchbox({
        searcher: function (value, name) {
            var hDialog = top.jQuery.hDialog({
                title: '选择订单',
                width: document.body.clientWidth / 1.5,
                height: document.body.clientHeight / 1.5,
                href: '/Search/Dialog',
                iconCls: 'icon-add',
                showBtns: false,
                onLoad: function () {
                    top.$('#grid').datagrid({
                        url: "/Order/List",
                        iconCls: 'icon icon-list',
                        width: document.body.clientWidth / 1.5 - 20,
                        height: document.body.clientHeight / 1.5 - 85,
                        nowrap: false, //折行
                        rownumbers: true, //行号
                        striped: true, //隔行变色
                        idField: 'ID', //主键
                        singleSelect: true,
                        checkOnSelect: true,
                        selectOnCheck: true,
                        frozenColumns: [[]],
                        columns: [[
                            { title: 'ERP订单号', field: 'ORDER_SN', width: 100 },
                            { title: '经营单位', field: 'STORE_NAME', width: 250 },
                            { title: '发货仓库', field: 'WAREHOUSE_NAME', width: 150 },
                            { title: '外部订单号', field: 'OUT_ORDER_SN', width: 130 },
                            { title: '外部订单状态', field: 'OUT_STATUS', width: 110 },
                            { title: '调度日期', field: 'DD_DATE', width: 150 },
                            { title: '审核日期', field: 'SH_DATE', width: 120 },
                            { title: '客户姓名', field: 'SHIP_NAME', width: 120 },
                            { title: '客户电话', field: 'SHIP_MOBILE', width: 120 },
                            { title: '客户地址', field: 'SHIP_ADDRESS', width: 350 },
                            { title: '订单金额', field: 'TOTAL_PRODUCT_PRICE', width: 120 },
                            { title: '物流公司', field: 'DISPATCH_MEMO', width: 120 },
                            { title: '物流单号', field: 'DISPATCH_SN', width: 120 },
                            { title: '客户姓名', field: 'SHIP_NAME', width: 120 },
                             { title: '发票信息', field: 'INVOICE_INFO', width: 120 },
                            { title: '备注2', field: 'MEMO2', width: 500 }
                        ]],
                        pagination: true,
                        pageSize: 20,
                        pageList: [20, 40, 50, 100, 500, 5000],
                        onDblClickRow: function (rowIndex, rowData) {
                            callBack(rowData);
                            hDialog.dialog('close');
                        }
                    });

                },
                toolbar: [{
                    text: '确定',
                    iconCls: 'icon-database_save',
                    handler: function () {
                        var row = top.$('#grid').datagrid('getSelected');
                        if (row) {
                            callBack(row);
                            hDialog.dialog('close');
                        }
                    }
                },  {
                    text: '关闭',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        hDialog.dialog("close");
                    }
                }]
            });
        },
        prompt: ''
    });
}

//打开组织架构选择择窗口
function bindOrganizeDialog(control, callBack) {
    control.searchbox({
        searcher: function (value, name) {
            var hDialog = top.jQuery.hDialog({
                title: '组织架构选择',
                width: document.body.clientWidth / 1.5,
                height: document.body.clientHeight / 1.5,
                href: '/Search/Dialog',
                iconCls: 'icon-add',
                showBtns: false,
                onLoad: function () {
                    top.$('#grid').treegrid({
                        url: "/EasyUiTreeGrid/TB_ORGANIZATION",
                        iconCls: 'icon icon-list',
                        width: document.body.clientWidth / 1.5 - 20,
                        height: document.body.clientHeight / 1.5 - 85,
                        nowrap: false, //折行
                        rownumbers: true, //行号
                        striped: true, //隔行变色
                        idField: 'FID', //主键
                        singleSelect: true, //单选
                        frozenColumns: [[]],
                        idField: 'FID',
                        treeField: 'FORGNAME',
                        columns: [[
                            { title: '组织名称', field: 'FORGNAME', width: 300 },
                            { title: '组织编码', field: 'FORGCODE', width: 120 },
                        ]],
                        pagination: true,
                        pageSize: 20,
                        pageList: [20, 40, 50],
                        onDblClickRow: function (rowIndex, rowData) {
                            callBack(rowData);
                            hDialog.dialog('close');
                        }
                    });
                },
                toolbar: [{
                    text: '确定',
                    iconCls: 'icon-database_save',
                    handler: function () {
                        var row = top.$('#grid').datagrid('getSelected');
                        if (row) {
                            callBack(row);
                            hDialog.dialog('close');
                        }
                    }
                }, {
                    text: '关闭',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        hDialog.dialog("close");
                    }
                }]
            });
        },
        prompt: ''
    });
}

//测试 展开子级
function bindOrganizeDialog2(control, callBack) {
    control.searchbox({
        searcher: function (value, name) {
            var hdia = top.jQuery.hDialog({
                title: '组织架构选择',
                width: document.body.clientWidth / 1.5,
                height: document.body.clientHeight / 1.5,
                href: '/Search/Dialog',
                iconCls: 'icon-add',
                showBtns: false,
                onLoad: function () {
                    top.$('#grid').treegrid({
                        url: "/EasyUiTreeGrid/TB_ORGANIZATIONNoSaleArea",
                        iconCls: 'icon icon-list',
                        width: document.body.clientWidth / 1.5 - 20,
                        height: document.body.clientHeight / 1.5 - 85,
                        nowrap: false, //折行
                        rownumbers: true, //行号
                        striped: true, //隔行变色
                        idField: 'FID', //主键
                        singleSelect: true, //单选
                        frozenColumns: [[]],
                        idField: 'FID',
                        treeField: 'FORGNAME',
                        columns: [[
                            { title: 'FID', field: 'FID', width: 100, hidden: true },
                            { title: 'FTYPE', field: 'FTYPE', width: 120, hidden: true },
                            { title: '组织名称', field: 'FORGNAME', width: 300 },
                            { title: '组织编码', field: 'FORGCODE', width: 120 },
                            { title: '组织类型', field: 'FTYPENAME', width: 120 }
                        ]],
                        //pagination: true,
                        pageSize: 20,
                        pageList: [20, 40, 50],
                        onBeforeExpand: function (row) {
                            if (row.FTYPE==1) {
                                top.$('#grid').treegrid('options').url = '/EasyUiTreeGrid/TB_ORGANIZATIONNoSaleArea?parentId=' + row.FID;
                                return true;
                            }
                            return false;
                        },
                        onDblClickRow: function (row) {
                            callBack(row);
                            hdia.dialog('close');
                        }
                    });

                    top.$("#a_search").click(function () {
                        top.$('#grid').treegrid('load', { keywords: top.$('#txtKeyword').val() });
                    });

                    top.$("#a_reset").click(function () {
                        top.$('#txtKeyword').textbox('setValue', '');
                        top.$('#grid').treegrid('options').url = '/EasyUiTreeGrid/TB_ORGANIZATIONNoSaleArea';
                        top.$('#grid').treegrid('load', { });
                    });

                },
                toolbar: [{
                    text: '确定',
                    iconCls: 'icon-database_save',
                    handler: function () {
                        var row = top.$('#grid').datagrid('getSelected');
                        if (row) {
                            callBack(row);
                            hdia.dialog('close');
                        }
                    }
                }, {
                    text: '关闭',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        hdia.dialog("close");
                    }
                }]
            });
        },
        prompt: ''
    });
}

//打开组织架构选择择窗口
function bindSubDicsDialog(control, callBack) {
    control.searchbox({
        searcher: function (value, name) {
            var hDialog = top.jQuery.hDialog({
                title: '请购计划选择',
                width: document.body.clientWidth / 1.5,
                height: document.body.clientHeight / 1.5,
                href: '/Search/Dialog',
                iconCls: 'icon-add',
                showBtns: false,
                onLoad: function () {
                    top.$('#grid').treegrid({
                        url: "/EasyUIComboTree/SYS_SUBDICS",
                        iconCls: 'icon icon-list',
                        width: document.body.clientWidth / 1.5 - 20,
                        height: document.body.clientHeight / 1.5 - 85,
                        nowrap: false, //折行
                        rownumbers: true, //行号
                        striped: true, //隔行变色
                        idField: 'FID', //主键
                        singleSelect: true, //单选
                        frozenColumns: [[]],
                        idField: 'FID',
                        treeField: 'FORGNAME',
                        columns: [[
                            { title: '销区名称', field: 'FORGNAME', width: 300 },
                            { title: '销区编码', field: 'FORGCODE', width: 120 },
                        ]],
                        pagination: true,
                        pageSize: 20,
                        pageList: [20, 40, 50],
                        onDblClickRow: function (rowIndex, rowData) {
                            callBack(rowData);
                            hDialog.dialog('close');
                        }
                    });
                },
                toolbar: [{
                    text: '确定',
                    iconCls: 'icon-database_save',
                    handler: function () {
                        var row = top.$('#grid').datagrid('getSelected');
                        if (row) {
                            callBack(row);
                            hDialog.dialog('close');
                        }
                    }
                }, {
                    text: '关闭',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        hDialog.dialog("close");
                    }
                }]
            });
        },
        prompt: ''
    });
}

//打开计量单位数组选择窗口
function bindMeasurementGroupDialog(control, callBack) {
    control.searchbox({
        searcher: function (value, name) {
            var hDialog = top.jQuery.hDialog({
                title: '计量单位数组选择',
                width: document.body.clientWidth / 1.5,
                height: document.body.clientHeight / 1.5,
                href: '/Search/Dialog',
                iconCls: 'icon-add',
                showBtns: false,
                onLoad: function () {
                    top.$('#grid').datagrid({
                        url: '/MeasuringGroup/List',
                        iconCls: 'icon icon-list',
                        width: document.body.clientWidth / 1.5 - 20,
                        height: document.body.clientHeight / 1.5 - 85,
                        nowrap: false, //折行
                        rownumbers: true, //行号
                        striped: true, //隔行变色
                        idField: 'FID', //主键
                        singleSelect: true, //单选
                        frozenColumns: [[]],
                        columns: [[
                            { field: 'FID', checkbox: true },
                            { title: '编号', field: 'FNUMBER', width: 120, sortable: true },
                            { title: '名称', field: 'FNAME', width: 200, sortable: true },
                            { title: '默认ID', field: 'FDEFALTUNITID', width: 250, sortable: true },
                            { title: '备注', field: 'FREMARK', width: 250, sortable: true }
                        ]],
                        pagination: true,
                        pageSize: 20,
                        pageList: [20, 40, 50],
                        onDblClickRow: function (rowIndex, rowData) {
                            callBack(rowData);
                            hDialog.dialog('close');
                        }
                    });
                },
                toolbar: [{
                    text: '确定',
                    iconCls: 'icon-database_save',
                    handler: function () {
                        var row = top.$('#grid').datagrid('getSelected');
                        if (row) {
                            callBack(row);
                            hDialog.dialog('close');
                        }
                    }
                }, {
                    text: '关闭',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        hDialog.dialog("close");
                    }
                }]
            });
        },
        prompt: ''
    });
}

//打开品牌选择择窗口
function bindBrandDialog(control, callBack) {
    control.searchbox({
        searcher: function (value, name) {
            var hDialog = top.jQuery.hDialog({
                title: '品牌选择',
                width: document.body.clientWidth / 1.2,
                height: document.body.clientHeight / 1.2,
                href: '/Search/Dialog',
                iconCls: 'icon-add',
                showBtns: false,
                onLoad: function () {
                    top.$('#grid').datagrid({
                        url: '/Brand/List',
                        iconCls: 'icon icon-list',
                        width: document.body.clientWidth / 1.2 - 20,
                        height: document.body.clientHeight / 1.2 - 85,
                        nowrap: false, //折行
                        rownumbers: true, //行号
                        striped: true, //隔行变色
                        idField: 'FID', //主键
                        singleSelect: true, //单选
                        frozenColumns: [[]],
                        columns: [[
                            { title: '品牌代码', field: 'FNUMBER', width: 120, sortable: true },
                            { title: '品牌名称', field: 'FNAME', width: 200, sortable: true },
                            { title: '厂家名称', field: 'FFACTORY', width: 250, sortable: true }
                        ]],
                        pagination: true,
                        pageSize: 20,
                        pageList: [20, 40, 50],
                        onDblClickRow: function (rowIndex, rowData) {
                            callBack(rowData);
                            hDialog.dialog('close');
                        }
                    });

                    top.$("#a_search").click(function () {
                        top.$('#grid').datagrid('load', { keywords: top.$('#txtKeyword').val() });
                    });

                    top.$("#a_reset").click(function () {
                        top.$('#txtKeyword').textbox('setValue', '');
                        top.$('#grid').datagrid('load', {});
                    });
                },
                toolbar: [{
                    text: '确定',
                    iconCls: 'icon-database_save',
                    handler: function () {
                        var row = top.$('#grid').datagrid('getSelected');
                        if (row) {
                            callBack(row);
                            hDialog.dialog('close');
                        }
                    }
                }, {
                    text: '关闭',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        hDialog.dialog("close");
                    }
                }]
            });
        },
        prompt: ''
    });
}

//打开经营场所选择择窗口
function bindMarketLocationDialog(control, callBack) {
    control.searchbox({
        searcher: function (value, name) {
            var hDialog = top.jQuery.hDialog({
                title: '经营场所选择',
                width: document.body.clientWidth / 1.1,
                height: document.body.clientHeight / 1.1,
                href: '/MarketLocation/Dialog',
                iconCls: 'icon-add',
                showBtns: false,
                onLoad: function () {
                    top.$('#grid').datagrid({
                        url: '/MarketLocation/List',
                        iconCls: 'icon icon-list',
                        width: document.body.clientWidth / 1.1 - 20,
                        height: document.body.clientHeight / 1.1 - 85,
                        nowrap: false, //折行
                        rownumbers: true, //行号
                        striped: true, //隔行变色
                        idField: 'FID', //主键
                        singleSelect: true, //单选
                        frozenColumns: [[]],
                        columns: [[
                            { title: '场所编号', field: 'FCODE', width: 80, sortable: true },
                            { title: '场所名称', field: 'FNAME', width: 300, sortable: true },

                            { title: '所属组织ID', field: 'FORGID', width: 100, hidden: true },
                            { title: '一级销区ID', field: 'FCLASSAREA1', width: 120, hidden: true },
                            { title: '二级销区ID', field: 'FCLASSAREA2', width: 120, hidden: true },
                            { title: '品牌部iID', field: 'FBRAND', width: 120, hidden: true },

                            { title: '所属组织', field: 'FORGNAME', width: 150, sortable: true },
                            { title: '一级销区', field: 'FCLASSAREA1NAME', width: 100, sortable: true },
                            { title: '二级销区', field: 'FCLASSAREA2NAME', width: 100, sortable: true },
                            { title: '品牌部', field: 'FBRANDNAME', width: 120, sortable: true },
                            {
                                title: '状态', field: 'FSTATUS', width: 50, sortable: true, align: 'center',
                                formatter: function (value, row, index) {
                                    if (value == "0") {
                                        return "启用";
                                    }
                                    else {
                                        return "禁用";
                                    }
                                }
                            },
                            { title: '备注', field: 'FREMARK', width: 120, sortable: false }
                        ]],
                        pagination: true,
                        pageSize: 20,
                        pageList: [20, 40, 50],
                        onDblClickRow: function (rowIndex, rowData) {
                            callBack(rowData);
                            hDialog.dialog('close');
                        }
                    });

                    top.$("#a_search").click(function () {
                        top.$('#grid').datagrid('load', { keywords: top.$('#txtKeyword').val() });
                    });

                    top.$("#a_reset").click(function () {
                        top.$('#txtKeyword').textbox('setValue', '');
                        top.$('#grid').datagrid('load', {});
                    });
                },
                toolbar: [{
                    text: '确定',
                    iconCls: 'icon-database_save',
                    handler: function () {
                        var row = top.$('#grid').datagrid('getSelected');
                        if (row) {
                            callBack(row);
                            hDialog.dialog('close');
                        }
                    }
                }, {
                    text: '关闭',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        hDialog.dialog("close");
                    }
                }]
            });
        },
        prompt: ''
    });
}

//打开承运商选择
function bindExpressCompanyDialog(control, callBack) {
    control.searchbox({
        searcher: function (value, name) {
            var hDialog = top.jQuery.hDialog({
                title: '承运商选择',
                width: document.body.clientWidth / 1.2,
                height: document.body.clientHeight / 1.2,
                href: '/Search/Dialog',
                iconCls: 'icon-add',
                showBtns: false,
                onLoad: function () {
                    top.$('#grid').datagrid({
                        url: '/LogisticsCarrier/List',
                        iconCls: 'icon icon-list',
                        width: document.body.clientWidth / 1.2 - 20,
                        height: document.body.clientHeight / 1.2 - 85,
                        nowrap: false, //折行
                        rownumbers: true, //行号
                        striped: true, //隔行变色
                        idField: 'FID', //主键
                        singleSelect: true, //单选
                        frozenColumns: [[]],
                        columns: [[
                              { title: '承运商编码', field: 'FCODE', width: 90, sortable: true },
                            { title: '承运商名称', field: 'FNAME', width: 200, sortable: true },
                            { title: '联系人', field: 'FCONTACT', width: 100, sortable: true },
                            { title: '联系电话', field: 'FPHONE', width: 110, sortable: true },
                            { title: '电子邮箱', field: 'FEMAIL', width: 160, sortable: true },
                            { title: '备注', field: 'FREMARK', width: 300, sortable: true }
                        ]],
                        pagination: true,
                        pageSize: 20,
                        pageList: [20, 40, 50],
                        onDblClickRow: function (rowIndex, rowData) {
                            callBack(rowData);
                            hDialog.dialog('close');
                        }
                    });

                    top.$("#a_search").click(function () {
                        top.$('#grid').datagrid('load', { keywords: top.$('#txtKeyword').val() });
                    });

                    top.$("#a_reset").click(function () {
                        top.$('#txtKeyword').textbox('setValue', '');
                        top.$('#grid').datagrid('load', {});
                    });
                },
                toolbar: [{
                    text: '确定',
                    iconCls: 'icon-database_save',
                    handler: function () {
                        var row = top.$('#grid').datagrid('getSelected');
                        if (row) {
                            callBack(row);
                            hDialog.dialog('close');
                        }
                    }
                }, {
                    text: '关闭',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        hDialog.dialog("close");
                    }
                }]
            });
        },
        prompt: ''
    });
}

function openExpressCompanyDialog( callBack) {
    var hDialog = top.jQuery.hDialog({
        title: '承运商选择',
        width: document.body.clientWidth / 1.2,
        height: document.body.clientHeight / 1.2,
        href: '/Search/Dialog',
        iconCls: 'icon-add',
        showBtns: false,
        onLoad: function () {
            top.$('#grid').datagrid({
                url: '/LogisticsCarrier/List',
                iconCls: 'icon icon-list',
                width: document.body.clientWidth / 1.2 - 20,
                height: document.body.clientHeight / 1.2 - 85,
                nowrap: false, //折行
                rownumbers: true, //行号
                striped: true, //隔行变色
                idField: 'FID', //主键
                singleSelect: true, //单选
                frozenColumns: [[]],
                columns: [[
                      { title: '承运商编码', field: 'FCODE', width: 90, sortable: true },
                    { title: '承运商名称', field: 'FNAME', width: 200, sortable: true },
                    { title: '联系人', field: 'FCONTACT', width: 100, sortable: true },
                    { title: '联系电话', field: 'FPHONE', width: 110, sortable: true },
                    { title: '电子邮箱', field: 'FEMAIL', width: 160, sortable: true },
                    { title: '备注', field: 'FREMARK', width: 300, sortable: true }
                ]],
                pagination: true,
                pageSize: 20,
                pageList: [20, 40, 50],
                onDblClickRow: function (rowIndex, rowData) {
                    callBack(rowData);
                    hDialog.dialog('close');
                }
            });

            top.$("#a_search").click(function () {
                top.$('#grid').datagrid('load', { keywords: top.$('#txtKeyword').val() });
            });

            top.$("#a_reset").click(function () {
                top.$('#txtKeyword').textbox('setValue', '');
                top.$('#grid').datagrid('load', {});
            });
        },
        toolbar: [{
            text: '确定',
            iconCls: 'icon-database_save',
            handler: function () {
                var row = top.$('#grid').datagrid('getSelected');
                if (row) {
                    callBack(row);
                    hDialog.dialog('close');
                }
            }
        }, {
            text: '关闭',
            iconCls: 'icon-cancel',
            handler: function () {
                hDialog.dialog("close");
            }
        }]
    });
}

//打开厂家账户选择窗口
function bindFactoryACCDialog(control, brand, callBack) {
    control.searchbox({
        searcher: function (value, name) {
            //var brandbrand = top.$('#FBRANDID').val();
            var hDialog = top.jQuery.hDialog({
                title: '厂家账户选择',
                width: document.body.clientWidth / 1.2,
                height: document.body.clientHeight / 1.2,
                href: '/Search/Dialog',
                iconCls: 'icon-add',
                showBtns: false,
                onLoad: function () {
                    top.$('#grid').datagrid({
                        url: '/FactoryAccountManagement/GetListByBrandID',
                        iconCls: 'icon icon-list',
                        width: document.body.clientWidth / 1.2 - 20,
                        height: document.body.clientHeight / 1.2 - 85,
                        nowrap: false, //折行
                        rownumbers: true, //行号
                        striped: true, //隔行变色
                        idField: 'FID', //主键
                        singleSelect: true, //单选
                        frozenColumns: [[]],
                        queryParams: {
                            brandid: brand
                        },
                        columns: [[
                            { title: '厂家账号', field: 'FACCOUNT', width: 90, sortable: true },
                             { title: '账号名称', field: 'FNAME', width: 500, sortable: true }
                        ]],
                        pagination: true,
                        pageSize: 20,
                        pageList: [20, 40, 50],
                        onDblClickRow: function (rowIndex, rowData) {
                            callBack(rowData);
                            hDialog.dialog('close');
                        }
                    });

                    top.$("#a_search").click(function () {
                        top.$('#grid').datagrid('load', { brandid: brand, keywords: top.$('#txtKeyword').val() });
                    });

                    top.$("#a_reset").click(function () {
                        top.$('#txtKeyword').textbox('setValue', '');
                        top.$('#grid').datagrid('load', { brandid: brand });
                    });
                },
                toolbar: [{
                    text: '确定',
                    iconCls: 'icon-database_save',
                    handler: function () {
                        var row = top.$('#grid').datagrid('getSelected');
                        if (row) {
                            callBack(row);
                            hDialog.dialog('close');
                        }
                    }
                },  {
                    text: '关闭',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        hDialog.dialog("close");
                    }
                }]
            });
        },
        prompt: ''
    });
}

//打开请购计划选择择窗口
function bindICPRBILLDialog(control, callBack) {
    control.searchbox({
        searcher: function (value, name) {
            var hDialog = top.jQuery.hDialog({
                title: '请购计划选择',
                width: document.body.clientWidth / 1.5,
                height: document.body.clientHeight / 1.5,
                href: '/Search/Dialog',
                iconCls: 'icon-add',
                showBtns: false,
                onLoad: function () {
                    top.$('#grid').datagrid({
                        url: '/EasyUiSearchDataGrid/ICPRBILL?approved=true',
                        iconCls: 'icon icon-list',
                        width: document.body.clientWidth / 1.5 - 20,
                        height: document.body.clientHeight / 1.5 - 85,
                        nowrap: false, //折行
                        rownumbers: true, //行号
                        striped: true, //隔行变色
                        idField: 'FID', //主键
                        singleSelect: true, //单选
                        frozenColumns: [[]],
                        columns: [[
                            { title: '销区', field: 'FORGNAME', width: 120, sortable: true },
                            { title: '日期', field: 'FDATE', width: 180, sortable: true },
                            { title: '品牌名称', field: 'FBRANDNAME', width: 200, sortable: true },
                            { title: '备注', field: 'FREMARK', width: 250, sortable: true }
                        ]],
                        pagination: true,
                        pageSize: 20,
                        pageList: [20, 40, 50],
                        onDblClickRow: function (rowIndex, rowData) {
                            callBack(rowData);
                            hDialog.dialog('close');
                        }
                    });
                },
                toolbar: [{
                    text: '确定',
                    iconCls: 'icon-database_save',
                    handler: function () {
                        var row = top.$('#grid').datagrid('getSelected');
                        if (row) {
                            callBack(row);
                            hDialog.dialog('close');
                        }
                    }
                }, {
                    text: '关闭',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        hDialog.dialog("close");
                    }
                }]
            });
        },
        prompt: ''
    });
}

//打开单位选择择窗口
function bindUnitDialog(control, callBack) {
    control.searchbox({
        editable:false,
        searcher: function (value, name) {
            var hDialog = top.jQuery.hDialog({
                title: '单位选择',
                width: document.body.clientWidth / 1.5,
                height: document.body.clientHeight / 1.5,
                href: '/Search/Dialog',
                iconCls: 'icon-add',
                showBtns: false,
                onLoad: function () {
                    top.$('#grid').datagrid({
                        url: '/EasyUiSearchDataGrid/TB_UNIT',
                        iconCls: 'icon icon-list',
                        width: document.body.clientWidth / 1.5 - 20,
                        height: document.body.clientHeight / 1.5 - 85,
                        nowrap: false, //折行
                        rownumbers: true, //行号
                        striped: true, //隔行变色
                        idField: 'FID', //主键
                        singleSelect: true, //单选
                        frozenColumns: [[]],
                        columns: [[
                            { title: '单位代码', field: 'FNUMBER', width: 120, sortable: true },
                            { title: '单位名称', field: 'FNAME', width: 200, sortable: true },
                            { title: '备注', field: 'FREMARK', width: 250, sortable: true }
                        ]],
                        pagination: true,
                        pageSize: 20,
                        pageList: [20, 40, 50],
                        queryParams: {
                            parentCode: 104
                        },
                        onDblClickRow: function (rowIndex, rowData) {
                            callBack(rowData);
                            hDialog.dialog('close');
                        }
                    });
                },
                toolbar: [{
                    text: '确定',
                    iconCls: 'icon-database_save',
                    handler: function () {
                        var row = top.$('#grid').datagrid('getSelected');
                        if (row) {
                            callBack(row);
                            hDialog.dialog('close');
                        }
                    }
                }, {
                    text: '关闭',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        hDialog.dialog("close");
                    }
                }]
            });
        },
        prompt: ''
    });
}

//打开商品分类选择窗口
function bindCategoryDialog(control, callBack) {
    control.searchbox({
        searcher: function (value, name) {
            var hDialog = top.jQuery.hDialog({
                title: '商品分类选择',
                width: document.body.clientWidth / 1.5,
                height: document.body.clientHeight / 1.5,
                href: '/Search/Dialog',
                iconCls: 'icon-add',
                showBtns: false,
                onLoad: function () {

                    top.$('#grid').treegrid({
                        iconCls: 'icon icon-list',
                        width: document.body.clientWidth / 1.5 - 20,
                        height: document.body.clientHeight / 1.5 - 85,
                        nowrap: false,
                        rownumbers: true,
                        animate: true,
                        collapsible: false,
                        url: '/Category/List',
                        idField: 'FID',
                        treeField: 'CATEGORY_NAME',
                        columns: [[
                            { title: '分类编码', field: 'CATEGORY_NUMBER', width: 120 },
                            { title: '分类名称', field: 'CATEGORY_NAME', width: 300 },
                            { title: '分类类型', field: 'TYPE', width: 120 }
                        ]],
                        onDblClickRow: function (rowIndex, rowData) {
                            var row = top.$('#grid').treegrid('getSelected');
                            callBack(row);
                            hDialog.dialog('close');
                        }
                    });
                },
                toolbar: [{
                    text: '确定',
                    iconCls: 'icon-database_save',
                    handler: function () {
                        var row = top.$('#grid').datagrid('getSelected');
                        if (row) {
                            callBack(row);
                            hDialog.dialog('close');
                        }
                    }
                }, {
                    text: '关闭',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        hDialog.dialog("close");
                    }
                }]
            });
        },
        prompt: ' '
    });
}

//打开商品选择窗口
function bindGoodsDialog(control, callBack) {
    control.searchbox({
        searcher: function (value, name) {
            openGoodsDialog(callBack, true);
        },
        prompt: ' '
    });
}

function openGoodsDialog(callBack, singleSelect,type) {
    var hDialog = top.jQuery.hDialog({
        title: '商品选择',
        width: document.body.clientWidth / 1.5,
        height: document.body.clientHeight / 1.5,
        href: '/Search/Dialog2',
        iconCls: 'icon-add',
        showBtns: false,
        onLoad: function () {

            top.$('#userGrid').datagrid({
                url: '/Goods/List?Type=' + type,
                fit: true,
                idField: 'FID',
                singleSelect: singleSelect,
                striped: true,
                columns: [[
                    { field: 'FID', checkbox: true },
                    { title: '商品名称', field: 'GOODS_NAME', width: 120, sortable: true },
                    { title: '商品编码', field: 'GOODS_NUMBER', width: 110, sortable: true },
                    {
                        title: '缩略图', field: 'IMAGE_URL', width: 60, align: 'center',
                        formatter: function (v, d, i) {
                            return '<a href="' + v + '" target="_blank">查看</a>';
                        }
                    },
                    { title: '规格参数', field: 'SPECIFICATION', width: 200, sortable: true },
                    {
                        title: '单价', field: 'PRICE', width: 100, align: 'right', sortable: true,
                        formatter: function (v, d, i) {
                            return fmoney(v, 2);
                        }
                    },
                    {
                        title: '应收费用', field: 'RECEIVABLE_FEE', width: 100, align: 'right', sortable: true,
                        formatter: function (v, d, i) {
                            return fmoney(v, 2);
                        }
                    },
                      {
                          title: '应付费用', field: 'PAYABLE_FEE', width: 100, align: 'right', sortable: true,
                          formatter: function (v, d, i) {
                              return fmoney(v, 2);
                          }
                      },
                    { title: '计量单位', field: 'UNIT', width: 200, sortable: true }
                ]],
                pagination: true,
                pageSize: 20
            });
           

            var filterObj = { "groupOp": "AND", "rules": [{ "field": "CATEGORY_TYPE", "op": "eq", "data": type }] };
            top.$('#userGrid').datagrid('load', { filter: JSON.stringify(filterObj) });

            top.$('#deptree').tree({
                lines: true,
                url: '/Goods/Category',
                animate: true,
                onLoadSuccess: function (node, data) {

                }, onClick: function (node) {
                    var depId = node.id;
                    var children = top.$('#deptree').tree('getChildren', node.target);
                    var arr = [];
                    arr.push(depId);
                    for (var i = 0; i < children.length; i++) {
                        arr.push(children[i].id);
                    }

                    var strIds = arr.join(',');
                    strIds = "'" + strIds.replace(",", "','") + "'";
                    var filterObj = { "groupOp": "AND", "rules": [{ "field": "CATEGORY_TYPE", "op": "eq", "data": type }, { "field": "CATEGORY_ID", "op": "in", "data": strIds }] };
                    top.$('#userGrid').datagrid('load', { filter: JSON.stringify(filterObj) });
                }
            });


        },
        toolbar: [{
            text: '确定',
            iconCls: 'icon-database_save',
            handler: function () {
                if (singleSelect) {
                    var row = top.$('#userGrid').datagrid('getSelected');
                    if (row) {
                        callBack(row);
                    }
                }
                else {
                    callBack(top.$('#userGrid').datagrid('getSelections'));
                }

                hDialog.dialog('close');
            }
        },  {
            text: '关闭',
            iconCls: 'icon-cancel',
            handler: function () {
                hDialog.dialog("close");
            }
        }]
    });
}

//打开商品选择窗口
function bindMasterDialog(control, callBack) {
    control.searchbox({
        searcher: function (value, name) {
            openMasterDialog(callBack, true);
        },
        prompt: ' '
    });
}

//绑定系统用户选择窗
function bindSYS_UserDialog(control, callBack)
{
    control.searchbox({
        searcher: function (value, name) {
            var hDialog = top.jQuery.hDialog({
                title: '用户选择',
                width: document.body.clientWidth / 1.5,
                height: document.body.clientHeight / 1.5,
                href: '/Search/Dialog',
                iconCls: 'icon-add',
                showBtns: false,
                onLoad: function () {
                    top.$('#grid').datagrid({
                        url: '/EasyUiSearchDataGrid/SYS_User',
                        iconCls: 'icon icon-list',
                        width: document.body.clientWidth / 1.5 - 20,
                        height: document.body.clientHeight / 1.5 - 85,
                        nowrap: false, //折行
                        rownumbers: true, //行号
                        striped: true, //隔行变色
                        idField: 'FID', //主键
                        singleSelect: true, //单选
                        frozenColumns: [[]],
                        columns: [[
                            { title: '选择', field: 'FID', checkbox: true },
                           // { title: '员工工号', field: 'EmployeeID', width: 150, sortable: true },
                            { title: '用户名', field: 'UserName', width: 100, sortable: true },
                            { title: '真实姓名', field: 'TrueName', width: 100, sortable: true },
                            { title: '联系电话', field: 'Mobile', width: 100, align: 'center' },
                        ]],
                        pagination: true,
                        pageSize: 20,
                        pageList: [20, 40, 50],
                        queryParams: {
                            parentCode: 104
                        },
                        onDblClickRow: function (rowIndex, rowData) {
                            callBack(rowData);
                            hDialog.dialog('close');
                        }
                    });

                    top.$("#a_search").click(function () {
                        top.$('#grid').datagrid('load', { keywords: top.$('#txtKeyword').val() });
                    });

                    top.$("#a_reset").click(function () {
                        top.$('#txtKeyword').textbox('setValue', '');
                        top.$('#grid').datagrid('load', {});
                    });
                },
                toolbar: [{
                    text: '确定',
                    iconCls: 'icon-database_save',
                    handler: function () {
                        var row = top.$('#grid').datagrid('getSelected');
                        if (row) {
                            callBack(row);
                            hDialog.dialog('close');
                        }
                    }
                }, {
                    text: '关闭',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        hDialog.dialog("close");
                    }
                }]
            });
        },
        prompt: ''
    });
}

//打开厂家代码选择窗口
function openSrcDialog(callBack, itemid) {
    var hDialog = top.jQuery.hDialog({
        title: '厂家代码选择',
        width: document.body.clientWidth / 1.5,
        height: document.body.clientHeight / 1.5,
        href: '/Search/Dialog',
        iconCls: 'icon-add',
        showBtns: false,
        onLoad: function () {
            top.$('#grid').datagrid({
                url: "/Product/Src",
                iconCls: 'icon icon-list',
                width: document.body.clientWidth / 1.5 - 20,
                height: document.body.clientHeight / 1.5 - 85,
                nowrap: false, //折行
                rownumbers: true, //行号
                striped: true, //隔行变色
                idField: 'FID', //主键
                singleSelect: true, //单选
                frozenColumns: [[]],
                columns: [[
                    { title: '厂家代码', field: 'FSRCCODE', width: 150, sortable: true },
                    { title: '厂家名称', field: 'FSRCNAME', width: 120, sortable: true },
                    { title: '厂家规格型号', field: 'FSRCMODEL', width: 120, sortable: true },
                    { title: '单位', field: 'FUNIT', width: 60, sortable: true, align: 'center' },
                    { title: '采购单位', field: 'FORDERUNIT', width: 70, sortable: true, align: 'center' },
                    {
                        title: '换算率', field: 'FRATE', width: 120, sortable: true, align: 'right', formatter: function (v, d, i) {
                            return fmoney(v, 2);
                        }
                    }
                ]],
                pagination: true,
                pageSize: 20,
                pageList: [20, 40, 50],
                onDblClickRow: function (rowIndex, rowData) {
                    callBack(rowData);
                    hDialog.dialog('close');
                },
                queryParams: {
                    FPRODUCTID:itemid
                }
            });
        },
        toolbar: [{
            text: '确定',
            iconCls: 'icon-database_save',
            handler: function () {
                var row = top.$('#grid').datagrid('getSelected');
                if (row) {
                    callBack(row);
                    hDialog.dialog('close');
                }
            }
        },  {
            text: '关闭',
            iconCls: 'icon-cancel',
            handler: function () {
                hDialog.dialog("close");
            }
        }]
    });

}