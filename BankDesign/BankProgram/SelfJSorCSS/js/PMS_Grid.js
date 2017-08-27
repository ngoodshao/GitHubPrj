

;(function($){
    var ligerGridEvent = {
        ControllerName: "",
        dataId: "",
    };

    $.extend({
        ligerGridTableCreate: function(pageloadid, gridid, cols, url) {
            var ligerManager = $("#" + gridid).ligerGrid({
                columns: cols,
                url: url,
                //data: CustomersData,
                pageSize: 20,
                width: '98%', height: '98%', checkbox: true, rownumbers: true,
                dataAction: "server",
                fixedCellHeight: false,
                //onSelectRow: function (rowdata, rowid, rowobj) {

                //},
                //onBeforeCheckRow: function (checked, data, rowid, rowdata) {
                //    var a = ligerManager.getSelected();
                //    if (a != null) {
                //        ligerManager.unselect(a.__id);
                //    }
                //},
                //onUnSelectRow: function (checked, data, rowid, rowdata) {

                //},
                toolbar: {
                    items: [
                    { text: '增加', click: itemclick, icon: 'add', ActionName: 'Add' },
                    { line: true },
                    { text: '修改', click: itemclick, icon: 'modify', ActionName: 'Edit' },
                    { line: true },
                    { text: '删除', click: itemclick, icon: 'delete', ActionName: 'Delete' },
                    { line: true },
                    { text: '导入', click: itemclick, icon: 'import', ActionName: 'Import' },
                    { line: true },
                    { text: '导出', click: itemclick, icon: 'export', ActionName: 'Export' },
                    { line: true },
                    ]
                },
                autoFilter: true
            });
            $("#" + pageloadid).hide();
            return ligerManager;
        },
        //按钮组
        itemclick: null,
    });
})(jQuery)

