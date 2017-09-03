var MUserColumns = [
                { display: '客户编码', name: 'CusCode' },
                { display: '客户姓名', name: 'CusName' },,
                {
                    display: '客户性别', name: 'Gender',
                    render: function (item) {
                        if (parseInt(item.UserState) == 1)
                            return '男';
                        return '女';
                    }
                },
                { display: '出生日期', name: 'BornDate', type: 'date', format: 'yyyy-MM-dd' },
                { display: '联系电话', name: 'ContactNO' },
                { display: '客户地址', name: 'Address' },
                { display: '身份证号', name: 'IDCard' },
                { display: '紧急联系人信息', name: 'EmergencyLink' },
                { display: '备注', name: 'Remark' },
]