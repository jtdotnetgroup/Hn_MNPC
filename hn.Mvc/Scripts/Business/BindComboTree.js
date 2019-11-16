//打开组织架构选择择窗口
function bindOrganizeComboTree(control, selectID, showIsEnable,notDisplayID, callBack) {
    control.combotree({
        url: '/EasyUIComboTree/TB_ORGANIZATION?showIsEnable=' + showIsEnable + '&NotDisplayID=' + notDisplayID,
        panelWidth: 200,
        value:selectID,
        onSelect: function (item) {
            callBack(item);
        }
    });
}