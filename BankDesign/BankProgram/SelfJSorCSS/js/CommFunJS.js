

;(function ($) {

    $.extend({
        fn_open: function (url, title) {
            $.ligerDialog.open({
                height: 400,
                width: 700,
                //width: 'auto', height: 'auto',
                title: title,
                url: url,
                showMax: false,
                showToggle: true,
                showMin: false,
                isResize: true,
                slide: false,
                data: {
                    //name: $("#txtValue").val()
                },
                //自定义参数
                myDataName: ""// $("#txtValue").val()
            });
        },
        fn_Delete: function (url, _list) {
            $.ajax({
                url: url,
                async: true,
                data: { "userids": _list },
                //data: _list,
                dataType: "json",
                traditional: true,
                type: "POST",
                //traditional: true,
                success: function (responseJSON) {
                    if (responseJSON.result == "1") {
                        $.ligerDialog.alert(responseJSON.errMsg, '删除', "success", function () {
                            window.location.reload();
                        });
                        //window.location.reload();
                    }
                    else {
                        $.ligerDialog.success(responseJSON.errMsg);
                    }
                }
            });
        },

    });


})(jQuery)
