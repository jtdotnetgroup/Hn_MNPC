//打开启用禁用下拉框
function bindIsEnableComboBox(control,value, callBack) {
    var data = control.combobox({
        url: '/EasyUIComboBox/IsEnable',
        valueField:'id',
        textField: 'text',
        value:value,
        onSelect: function (item) {
            if (callBack != undefined) {
                callBack(item);
            }
           
        }
  });
}

//打开组织类型下拉框
function bindTB_ORGANIZATION_FTYPEComboBox(control,value, callBack) {
    control.combobox({
        url: '/EasyUIComboBox/TB_ORGANIZATION_FTYPE',
        valueField: 'id',
        textField: 'text',
        value:value,
        onSelect: function (item) {
            //callBack(item);
        }
    });
}

//打开启用禁用下拉框
function bindYesOrNoComboBox(control, value, callBack) {
    var data = control.combobox({
        url: '/EasyUIComboBox/YesOrNo',
        valueField: 'id',
        textField: 'text',
        value: value,
        onSelect: function (item) {
            //callBack(item);
        }
    });
}

