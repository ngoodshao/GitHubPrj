var MUserColumns = [
                { display: '用户编号', name: 'UserID' },
                { display: '用户名称', name: 'UserName' },
                {
                    display: '用户权限', name: 'UserPermission',
                    render: function (item) {

                        var str = "";
                        var perm = item.UserPermission.split(",");
                        for (var i = 0; i < perm.length; i++) {
                            if (parseInt(perm[i]) == 0)
                                str += '增加' + ",";
                            if (parseInt(perm[i]) == 1)
                                str += '删除' + ",";
                            if (parseInt(perm[i]) == 2)
                                str += '修改' + ",";
                            if (parseInt(perm[i]) == 3)
                                str += '查询' + ",";
                        }
                        return str.substr(0, str.length - 1);
                    }
                },
                {
                    display: '用户状态', name: 'UserState',
                    render: function (item) {
                        if (parseInt(item.fDelFlag) == 1)
                            return '激活';
                        return '禁用';
                    }
                }
]